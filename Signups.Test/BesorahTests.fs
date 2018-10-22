module BesorahTests

open System
open System.IO
open Xunit
open Signups.Core

[<Fact>]
let ``Gets items`` () =
    let data = File.ReadAllText("./besorah-data.json")
    let result = Besorot.GetReading data "A" "Parashat Bereshit"
    Assert.Equal("John 1:1–18", result.Reading)

