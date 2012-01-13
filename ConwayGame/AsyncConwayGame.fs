module AsyncConwayGame

open Utils

let isAlive cell cells =
    cells |> List.exists (fun x -> x = cell)

let neighbours (x, y) =
    [ for i in x-1..x+1 do for j in y-1..y+1 do if not (i = x && j = y) then yield (i,j) ]

let aliveNeighbours cell cells =
    neighbours cell
    |> List.filter (fun x -> isAlive x cells)

let allDeadNeighbours cells =
    cells
    |> List.collect (fun x -> neighbours x)
    |> Set.ofList |> Set.toList
    |> List.filter (fun x -> not (isAlive x cells))

let survives cell cells =
    aliveNeighbours cell cells |> List.length >=< (2,3)

let reproducible cell cells =
    aliveNeighbours cell cells |> List.length = 3

let collectSurvivals cells =
    cells 
    |> List.map (fun x -> async { return (x, (survives x cells)) })
    |> Async.Parallel
    |> Async.RunSynchronously
    |> List.ofArray
    |> List.filter (fun (x,y) -> y)
    |> List.map (fun (x,y) -> x)

let collectReproducibles cells =
    allDeadNeighbours cells
    |> List.map (fun x -> async { return (x, (reproducible x cells)) })
    |> Async.Parallel
    |> Async.RunSynchronously
    |> List.ofArray
    |> List.filter (fun (x,y) -> y)
    |> List.map (fun (x,y) -> x)

let nextGeneration cells =
    collectSurvivals cells
    |> List.append (collectReproducibles cells)
