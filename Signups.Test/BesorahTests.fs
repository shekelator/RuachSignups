module BesorahTests

open System
open System.IO
open Xunit
open Signups.Core
open Signups.Core.Besorot

let GetCycle cycleString = 
    match cycleString with
    | "A" -> Besorot.Year.A
    | "B" -> Besorot.Year.B
    | _ -> Besorot.Year.C

[<Theory>]
[<InlineData(5777)>]
[<InlineData(5778)>]
[<InlineData(5782)>]
let ``Bereshit always has the beginning of John`` (year) =
    let result = Besorot.GetReadingBySedra year "Bereshit"
    Assert.Equal("John 1:1–18", Option.get(result).Reading)

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


[<Theory>]
[<InlineData("Yitro", 5782, "John 7:1–13")>]
let ``Can get Yitro right`` (sedra: string, year: int, expected: string) =
    let result = Besorot.GetReadingBySedra year sedra
    Assert.Equal(expected, Option.get(result).Reading)

[<Theory>]
[<InlineData("Mishpatim", 5782)>]
[<InlineData("Vayakhel", 5782)>]
let ``Can get shabbat shekalim right`` (sedra: string, year: int) =
    let expected = "Mark 12:41-44"
    let result = Besorot.GetReading year {Sedra = sedra; Shabbat = Some SpecialShabbat.Shekalim}
    Assert.Equal(expected, result.Value.Reading)

