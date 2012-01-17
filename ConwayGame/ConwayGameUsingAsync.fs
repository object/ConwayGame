module ConwayGameUsingAsync

let neighbours (x, y) =
    [ for i in x-1..x+1 do 
      for j in y-1..y+1 do 
      if not (i = x && j = y) then yield (i,j) ]

let isAlive cell pattern =
    pattern |> List.exists ((=) cell)

let aliveNeighbours cell pattern =
    neighbours cell
    |> List.filter (fun x -> isAlive x pattern)

let survives cell pattern =
    aliveNeighbours cell pattern |> List.length |> fun x -> x >= 2 && x <= 3

let reproducible cell pattern =
    aliveNeighbours cell pattern |> List.length = 3

let allDeadNeighbours pattern =
    pattern
    |> List.collect neighbours
    |> Set.ofList |> Set.toList
    |> List.filter (fun x -> not (isAlive x pattern))

let collectByCriteria pattern criteria =
    pattern 
    |> List.map (fun x -> async { return (x, (criteria x pattern)) })
    |> Async.Parallel
    |> Async.RunSynchronously
    |> List.ofArray
    |> List.filter (fun (x,y) -> y)
    |> List.map (fun (x,y) -> x)

let nextGeneration pattern =
    [ 
        async { return (collectByCriteria pattern survives) };
        async { return (collectByCriteria (allDeadNeighbours pattern) reproducible) } 
    ]
    |> Async.Parallel
    |> Async.RunSynchronously
    |> List.ofArray
