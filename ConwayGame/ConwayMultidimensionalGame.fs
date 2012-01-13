module ConwayMultidimensionalGame

open Utils

type Cell =
    | Line of int
    | Surface of int * int
    | Space of int * int * int
    | Spacetime of int * int * int * int

let isAlive cell cells =
    cells |> List.exists (fun x -> x = cell)

let neighbours cell =
    match cell with
    | Line(a) -> [ for i in a-1..a+1 do if not (i = a) then yield Line(i) ]
    | Surface(a,b) -> [ for i in a-1..a+1 do for j in b-1..b+1 do if not (i = a && j = b) then yield Surface(i,j) ]
    | Space(a,b,c) -> [ for i in a-1..a+1 do for j in b-1..b+1 do for k in c-1..c+1 do if not (i = a && j = b && k = c) then yield Space(i,j,k) ]
    | Spacetime(a,b,c,d) -> [ for i in a-1..a+1 do for j in b-1..b+1 do for k in c-1..c+1 do for l in d-1..d+1 do if not (i = a && j = b && k = c && l = d) then yield Spacetime(i,j,k,l) ]

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

let nextGeneration cells =
    cells
    |> List.filter (fun x -> survives x cells)
    |> List.append (allDeadNeighbours cells |> List.filter (fun x -> reproducible x cells))

let rec cellsFromTuples cellFromTuple cells  =
    match cells with
    | [] -> []
    | head :: tail -> (cellFromTuple head) :: (cellsFromTuples cellFromTuple tail)

let rec tuplesFromCells tupleFromCell cells =
    match cells with
    | [] -> []
    | head :: tail -> (tupleFromCell head) :: (tuplesFromCells tupleFromCell tail)

let nextGenerationWithConversion cells cellFromTuple tupleFromCell =
    let typedCells = cells |> cellsFromTuples cellFromTuple
    typedCells
    |> nextGeneration
    |> tuplesFromCells tupleFromCell
