module ConwayMultidimensionalGameDiscrUnion

open ConwayMultidimensionalGame

let nextGeneration1 pattern =
    nextGenerationWithConversion pattern (fun (a) -> Line(a)) (fun (Line(a)) -> (a))

let nextGeneration2 pattern =
    nextGenerationWithConversion pattern (fun (a,b) -> Surface(a,b)) (fun (Surface(a,b)) -> (a,b))

let nextGeneration3 pattern =
    nextGenerationWithConversion pattern (fun (a,b,c) -> Space(a,b,c)) (fun (Space(a,b,c)) -> (a,b))

let nextGeneration4 pattern =
    nextGenerationWithConversion pattern (fun (a,b,c,d) -> Spacetime(a,b,c,d)) (fun (Spacetime(a,b,c,d)) -> (a,b))
