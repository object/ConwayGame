module AsyncConwayGame

let isAlive cell pattern =
    pattern |> List.exists (fun x -> x = cell)

let neighbours (x, y) =
    [ for i in x-1..x+1 do for j in y-1..y+1 do if not (i = x && j = y) then yield (i,j) ]

let aliveNeighbours cell pattern =
    neighbours cell
    |> List.filter (fun x -> isAlive x pattern)

let allDeadNeighbours pattern =
    pattern
    |> List.collect (fun x -> neighbours x)
    |> Set.ofList |> Set.toList
    |> List.filter (fun x -> not (isAlive x pattern))

let survives cell pattern =
    aliveNeighbours cell pattern |> List.length |> fun x -> x >= 2 && x <= 3

let reproducible cell pattern =
    aliveNeighbours cell pattern |> List.length = 3

let collectByCriteria pattern criteria =
    pattern 
    |> List.map (fun x -> async { return (x, (criteria x pattern)) })
    |> Async.Parallel
    |> Async.RunSynchronously
    |> List.ofArray
    |> List.filter (fun (x,y) -> y)
    |> List.map (fun (x,y) -> x)

let collectSurvivals pattern =
    collectByCriteria pattern survives

let collectReproducibles pattern =
    collectByCriteria (allDeadNeighbours pattern) reproducible

let nextGeneration pattern =
    collectSurvivals pattern
    |> List.append (collectReproducibles pattern)
