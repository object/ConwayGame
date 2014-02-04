type Color = Red | Green | Blue | White | Gray | Black | Orange | Yellow | Brown

let pattern = [Red;Green;Orange;Black]

let neighbours color =
    match color with
    | Red -> [Red;Orange;Brown]
    | Green -> [Green;Blue;Yellow]
    | Blue -> [Blue;Green]
    | White -> [White;Gray]
    | Black -> [Black;Gray]
    | Gray -> [Gray;Black;White]
    | Orange -> [Orange;Red;Yellow]
    | Yellow -> [Yellow;Orange;Green]
    | Brown -> [Brown;Red]

let isAlive pattern cell =
    pattern |> List.exists ((=) cell)

let aliveNeighbours pattern cell =
    neighbours cell |> List.filter (isAlive pattern)

let survives pattern cell =
    aliveNeighbours pattern cell 
    |> List.length |> fun x -> x >= 2 && x <= 3

let reproducible pattern cell =
    aliveNeighbours pattern cell 
    |> List.length = 3

let allDeadNeighbours pattern =
    pattern
    |> List.collect neighbours
    |> Set.ofList |> Set.toList
    |> List.filter (not << isAlive pattern)

let nextGeneration pattern =
    List.append
        (pattern |> List.filter (survives pattern))
        (allDeadNeighbours pattern |> List.filter (reproducible pattern))
nextGeneration pattern
