module ConwayGameUsingAsync

let neighbours (x, y) =
    [ for i in x-1..x+1 do 
      for j in y-1..y+1 do 
      if not (i = x && j = y) then yield (i,j) ]

let isAlive pattern cell =
    pattern |> List.exists ((=) cell)

let aliveNeighbours pattern cell =
    neighbours cell
    |> List.filter (isAlive pattern)

let survives pattern cell =
    aliveNeighbours pattern cell |> List.length |> fun x -> x >= 2 && x <= 3

let reproducible pattern cell =
    aliveNeighbours pattern cell |> List.length = 3

let allDeadNeighbours pattern =
    pattern
    |> List.collect neighbours
    |> Set.ofList |> Set.toList
    |> List.filter (not << isAlive pattern)

let collectByCriteria pattern criteria =
    pattern 
    |> List.map (fun x -> async { return (x, (criteria pattern x)) })
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
