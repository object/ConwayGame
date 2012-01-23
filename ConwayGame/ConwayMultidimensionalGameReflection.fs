module ConwayMultidimensionalGameReflection

open ConwayMultidimensionalGame

let cellFromTuple cell =
    let nthItem n = 
        System.Convert.ToInt32 (Microsoft.FSharp.Reflection.FSharpValue.GetTupleField(cell, n))

    let typeName = cell.GetType().Name
    let rank = System.Convert.ToInt32 (typeName.Substring (typeName.Length-1))
    match rank with
    | 1 -> Line(nthItem 0)
    | 2 -> Surface(nthItem 0, nthItem 1)
    | 3 -> Space(nthItem 0, nthItem 1, nthItem 2)
    | 4 -> Spacetime(nthItem 0, nthItem 1, nthItem 2, nthItem 3)
    | n -> raise <| new System.InvalidOperationException (sprintf "Unsupported tuple cardinality %d" n)

let tupleFromCell cell =
    let values = match cell with
                 | Line(a) -> [|a|]
                 | Surface(a,b) -> [|a;b|]
                 | Space(a,b,c) -> [|a;b;c|]
                 | Spacetime(a,b,c,d) -> [|a;b;c;d|]
    let types = values |> Array.map (fun x -> x.GetType())
    let tupleType = Microsoft.FSharp.Reflection.FSharpType.MakeTupleType types
    Microsoft.FSharp.Reflection.FSharpValue.MakeTuple (values |> Array.map (fun x -> x :> System.Object), tupleType)

let nextGeneration1 pattern =
    nextGenerationWithConversion pattern cellFromTuple (fun x -> tupleFromCell x :?> int)

let nextGeneration2 pattern =
    nextGenerationWithConversion pattern cellFromTuple (fun x -> tupleFromCell x :?> int * int)

let nextGeneration3 pattern =
    nextGenerationWithConversion pattern cellFromTuple (fun x -> tupleFromCell x :?> int * int * int)

let nextGeneration4 pattern =
    nextGenerationWithConversion pattern cellFromTuple (fun x -> tupleFromCell x :?> int * int * int * int)
