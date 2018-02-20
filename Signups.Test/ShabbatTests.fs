module ShabbatTests

open System
open System.IO
open Xunit
open Signups.Core

[<Fact>]
let ``Gets Torah portion without aliyah`` () =
    let data = File.ReadAllText("./hebcal-data.json")
    let result = Parshiot.Parse(data)
    let parasha = Seq.find (fun (x: Parshiot.Reading) -> x.Title = "Parashat Chukat") result
    Assert.Equal("Numbers 19:1 - 22:1", parasha.Leyning.Torah)
    Assert.Equal("Judges 11:1 - 11:33", parasha.Leyning.Haftarah)

[<Fact>]
let ``Gets items`` () =
    let data = File.ReadAllText("./hebcal-data.json")
    let result = Parshiot.Parse(data)
    let parasha = Seq.find (fun (x: Parshiot.Reading) -> x.Title = "Parashat Shemot") result
    let shabbat = Shabbat.parashaToEvent parasha
    Assert.Equal(new DateTime(2018, 1, 6), fst shabbat)
    Assert.Contains(snd shabbat, fun x -> x.Title = "Torah (Parashat Shemot):")

