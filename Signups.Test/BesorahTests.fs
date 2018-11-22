module BesorahTests

open System
open System.IO
open Xunit
open Signups.Core

[<Theory>]
[<InlineData(5777)>]
[<InlineData(5778)>]
[<InlineData(5782)>]
let ``Bereshit always has the beginning of John`` (year) =
    let result = Besorot.GetReading year "Parashat Bereshit"
    Assert.Equal("John 1:1–18", result.Reading)

[<Theory>]
[<InlineData(5777, "A")>]
[<InlineData(5778, "B")>]
[<InlineData(5779, "C")>]
[<InlineData(5780, "A")>]
[<InlineData(5782, "C")>]
 let ``Chooses correct cycle based on year`` (year: int, cycleString: string) =
    let cycle = 
        match cycleString with
        | "A" -> Besorot.Year.A
        | "B" -> Besorot.Year.B
        | _ -> Besorot.Year.C

    let readingYear = Besorot.GetYear year
    Assert.Equal(cycle, readingYear)


