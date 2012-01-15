module ConwayGameUsingSet

let neighbours (x, y) =
    [ for i in x-1..x+1 do for j in y-1..y+1 do if not (i = x && j = y) then yield (i,j) ]
    |> Set.ofList

let nextGeneration pattern =
    pattern
    |> Set.filter (fun x -> 
                  (neighbours x |> Set.intersect pattern |> Set.count |> fun z -> z >=2 && z <= 3))
    |> Set.union 
        (Set.difference (Set.unionMany (seq { for x in pattern do yield neighbours x })) pattern 
        |> Set.filter (fun x -> 
                      (neighbours x |> Set.intersect pattern |> Set.count = 3)))
    |> Set.toList