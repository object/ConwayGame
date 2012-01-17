module ConwayGame3d

let neighbours (x, y, z) =
    [ for i in x-1..x+1 do 
      for j in y-1..y+1 do 
      for k in z-1..z+1 do 
      if not (i = x && j = y && k = z) then yield (i,j,k) ]

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

let nextGeneration pattern =
    List.append
        (pattern |> List.filter (fun x -> survives x pattern))
        (allDeadNeighbours pattern |> List.filter (fun x -> reproducible x pattern))
