module ConwayGameTests

open NUnit.Framework
open FsUnit

open ConwayGame

[<TestFixture>]
type ConwayGameTests() =

    let rec nthGeneration n cells =
        match n with
        | 0 -> cells
        | n -> nthGeneration (n-1) (nextGeneration cells)

    [<Test>]
    member this.``Block should not change``() =
        let pattern = [(1,1); (1,2); (2,1); (2,2)]
        pattern
        |> nextGeneration
        |> should equal pattern

    [<Test>]
    member this.``Beehive should not change``() =
        let pattern = [(1,2); (1,3); (2,1); (2,4); (3,2); (3,3)]
        pattern
        |> nextGeneration
        |> should equal pattern

    [<Test>]
    member this.``Loaf should not change``() =
        let pattern = [(1,2); (1,3); (2,1); (2,4); (3,2); (3,4); (4,3)]
        pattern
        |> nextGeneration
        |> should equal pattern

    [<Test>]
    member this.``Boat should not change``() =
        let pattern = [(1,1); (1,2); (2,1); (2,3); (3,2)]
        pattern
        |> nextGeneration
        |> should equal pattern

    [<Test>]
    member this.``Blinker should have a period of two``() =
        let pattern = [(2,1); (2,2); (2,3)]
        pattern
        |> nextGeneration
        |> nextGeneration
        |> List.sort
        |> should equal pattern

    [<Test>]
    member this.``Blinker's first generation should give a different pattern``() =
        let pattern = [(2,1); (2,2); (2,3)]
        pattern
        |> nextGeneration
        |> List.sort
        |> should equal ([(1,2); (2,2); (3,2)])

    [<Test>]
    member this.``Toad should have a period of two``() =
        let pattern = [(2,2); (2,3); (2,4); (3,1); (3,2); (3,3)]
        pattern
        |> nextGeneration
        |> nextGeneration
        |> List.sort
        |> should equal pattern

    [<Test>]
    member this.``Toad's first generation should give a different pattern``() =
        let pattern = [(2,2); (2,3); (2,4); (3,1); (3,2); (3,3)]
        pattern
        |> nextGeneration
        |> List.sort
        |> should equal ([(1,3); (2,1); (2,4); (3,1); (3,4); (4,2)])

    [<Test>]
    member this.``Beacon should have a period of two``() =
        let pattern = [(1,1); (1,2); (2,1); (2,2); (3,3); (3,4); (4,3); (4,4)]
        pattern
        |> nextGeneration
        |> nextGeneration
        |> List.sort
        |> should equal pattern

    [<Test>]
    member this.``Beacon's first generation should give a different pattern``() =
        let pattern = [(1,1); (1,2); (2,1); (2,2); (3,3); (3,4); (4,3); (4,4)]
        pattern
        |> nextGeneration
        |> List.sort
        |> should equal ([(1,1); (1,2); (2,1); (3,4); (4,3); (4,4)])

    [<Test>]
    member this.``Diehard should not die after 1 generations``() =
        let pattern = [(1,7); (2,1); (3,2); (3,6); (3,7); (3,8)]
        pattern
        |> nthGeneration 1
        |> List.sort
        |> should not (equal Seq.empty)

    [<Test>]
    member this.``Diehard should not die after 2 generations``() =
        let pattern = [(1,7); (2,1); (3,2); (3,6); (3,7); (3,8)]
        pattern
        |> nthGeneration 2
        |> List.sort
        |> should not (equal Seq.empty)

    [<Test>]
    member this.``Diehard should not die after 3 generations``() =
        let pattern = [(1,7); (2,1); (3,2); (3,6); (3,7); (3,8)]
        pattern
        |> nthGeneration 3
        |> List.sort
        |> should not (equal Seq.empty)

    [<Test>]
    member this.``Diehard should not die after 129 generations``() =
        let pattern = [(1,7); (2,1); (3,2); (3,6); (3,7); (3,8)]
        pattern
        |> nthGeneration 129
        |> List.sort
        |> should not (equal Seq.empty)

    [<Test>]
    member this.``Diehard should die after 130 generations``() =
        let pattern = [(1,7); (2,1); (2,2); (3,2); (3,6); (3,7); (3,8)]
        pattern
        |> nthGeneration 130
        |> List.sort
        |> should equal Seq.empty

    [<Test>]
    [<TestCase(10)>]
    [<TestCase(25)>]
    [<TestCase(50)>]
    [<TestCase(100)>]
    member this.``Large pattern``(size : int) =
        let pattern = [for i in 1..size do for j in 1..size do if (i+j) % 2 = 0 then yield (i,j)]
        pattern
        |> nextGeneration
        |> should not (equal pattern)
