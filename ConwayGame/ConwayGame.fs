module ConwayGame

let isAlive cell pattern =
    pattern |> List.exists (fun x -> x = cell)

let neighbours (x, y) =
    [ for i in x-1..x+1 do for j in y-1..y+1 do if not (i = x && j = y) then yield (i,j) ]

let aliveNeighbours cell pattern =
    neighbours cell
    |> List.filter (fun x -> isAlive x pattern)

let allDeadNeighbours pattern =
    let allNeighbours = 
        pattern
        |> List.collect (fun x -> neighbours x)
        |> Set.ofList 
    Set.difference allNeighbours (Set.ofList pattern) |> Set.toList     
 
let underPopulated cell pattern =
    aliveNeighbours cell pattern |> List.length < 2

let overCrowded cell pattern =
    aliveNeighbours cell pattern |> List.length > 3

let survives cell pattern =
    aliveNeighbours cell pattern |> List.length |> fun x -> x >= 2 && x <= 3

let reproducible cell pattern =
    aliveNeighbours cell pattern |> List.length = 3

let nextGeneration pattern =
    List.append
        (pattern |> List.filter (fun x -> survives x pattern))
        (allDeadNeighbours pattern |> List.filter (fun x -> reproducible x pattern))
