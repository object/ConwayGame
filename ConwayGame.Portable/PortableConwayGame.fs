namespace PortableConwayGame

open System
open System.Collections.Generic

type ConwayGame() = 

    member this.NextGeneration (pattern : IEnumerable<int * int>) =
        pattern 
        |> List.ofSeq 
        |> ConwayGame.nextGeneration
