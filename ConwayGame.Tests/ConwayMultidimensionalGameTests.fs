module ConwayMultidimensionalGameTests

open NUnit.Framework
open FsUnit

open ConwayMultidimensionalGame

[<TestFixture>]
type ConwayMultidimensionalGameTests() =

    [<Test>]
    member this.``Square in 2d should not change``() =
        let pattern = [Surface(1,1); Surface(1,2); Surface(2,1); Surface(2,2)]
        pattern
        |> nextGeneration
        |> should equal pattern

    [<Test>]
    member this.``Square in 3d should not change``() =
        let pattern = [Space(1,1,0); Space(1,2,0); Space(2,1,0); Space(2,2,0)]
        pattern
        |> nextGeneration
        |> should equal pattern

    [<Test>]
    member this.``Cube in 3d should die``() =
        let pattern = [Space(1,1,1); Space(1,2,1); Space(2,1,1); Space(2,2,1); Space(1,1,2); Space(1,2,2); Space(2,1,2); Space(2,2,2)]
        pattern
        |> nextGeneration
        |> should equal List.empty
