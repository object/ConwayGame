module ConwayGameUsingPSeqTests

open NUnit.Framework
open FsUnit

open ConwayGameUsingPSeq

[<TestFixture>]
type ConwayGameUsingPSeqTests() =

    [<Test>]
    [<Explicit>]
    [<TestCase(10)>]
    [<TestCase(25)>]
    [<TestCase(50)>]
    [<TestCase(100)>]
    member this.``Large pattern``(size : int) =
        let pattern = [for i in 1..size do for j in 1..size do if (i+j) % 2 = 0 then yield (i,j)]
        pattern
        |> nextGeneration
        |> should not (equal pattern)
