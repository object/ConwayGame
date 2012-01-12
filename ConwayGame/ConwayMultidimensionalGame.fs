module ConwayMultidimensionalGame

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

let underPopulated cell cells =
    aliveNeighbours cell cells |> List.length < 2

let overCrowded cell cells =
    aliveNeighbours cell cells |> List.length > 3

let reproducible cell cells =
    aliveNeighbours cell cells |> List.length = 3

let cellFromTuple cell =
    let nthItem n = 
        System.Convert.ToInt32 (Microsoft.FSharp.Reflection.FSharpValue.GetTupleField(cell, n))

    let typeName = cell.GetType().Name
    let arity = System.Convert.ToInt32 (typeName.Substring (typeName.Length-1))
    match arity with
    | 1 -> Line(nthItem 0)
    | 2 -> Surface(nthItem 0, nthItem 1)
    | 3 -> Space(nthItem 0, nthItem 1, nthItem 2)
    | 4 -> Spacetime(nthItem 0, nthItem 1, nthItem 2, nthItem 3)
    | n -> raise (new System.InvalidOperationException (sprintf "Unsupported tuple cardinality %d" n))

let tupleFromCell cell =
    let values = match cell with
    | Line(a) -> [|a|]
    | Surface(a,b) -> [|a;b|]
    | Space(a,b,c) -> [|a;b;c|]
    | Spacetime(a,b,c,d) -> [|a;b;c;d|]
    let types = values |> Array.map (fun x -> x.GetType())
    let tupleType = Microsoft.FSharp.Reflection.FSharpType.MakeTupleType types
    Microsoft.FSharp.Reflection.FSharpValue.MakeTuple (values |> Array.map (fun x -> x :> System.Object), tupleType)

let tupleFromCell1 cell =
    tupleFromCell cell :?> int

let tupleFromCell2 cell =
    tupleFromCell cell :?> int * int

let tupleFromCell3 cell =
    tupleFromCell cell :?> int * int * int

let tupleFromCell4 cell =
    tupleFromCell cell :?> int * int * int * int

let rec cellsFromTuples cells =
    match cells with
    | [] -> []
    | head :: tail -> (cellFromTuple head) :: (cellsFromTuples tail)

let rec tuplesFromCells cells tupleFromCell =
    match cells with
    | [] -> []
    | head :: tail -> (tupleFromCell head) :: (tuplesFromCells tail tupleFromCell)

let nextGeneration cells =
    let typedCells = cells |> cellsFromTuples
    typedCells
    |> List.filter (fun x -> not (underPopulated x typedCells) && not (overCrowded x typedCells))
    |> List.append (allDeadNeighbours typedCells |> List.filter (fun x -> reproducible x typedCells))
    |> tuplesFromCells

let nextGeneration1 cells =
    nextGeneration cells tupleFromCell1

let nextGeneration2 cells =
    nextGeneration cells tupleFromCell2

let nextGeneration3 cells =
    nextGeneration cells tupleFromCell3

let nextGeneration4 cells =
    nextGeneration cells tupleFromCell4
