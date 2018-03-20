module ShabbatTests

open System
open System.IO
open Xunit
open Signups.Core

[<Fact>]
let ``Gets items`` () =
    let data = File.ReadAllText("./hebcal-data.json")
    let result = Parshiot.Parse(data)
    let parasha = Seq.find (fun (x: Parshiot.Reading) -> x.Title = "Parashat Shemot") result
    let shabbat = Shabbat.parashaToShabbat parasha
    Assert.Equal(new DateTime(2018, 1, 6), shabbat.Date)
    Assert.Equal("Parashat Shemot", shabbat.Title)
    Assert.Contains(shabbat.Openings, fun x -> x.Title.StartsWith("Torah (Parashat Shemot)"))
    Assert.True(shabbat.Id.IsNone)

