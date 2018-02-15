module Tests

open System.IO
open Xunit
open Signups.Core

[<Fact>]
let ``My test`` () =
    Assert.Equal((Parshiot.hello "friend"), "Hello friend")

[<Fact>]
let ``Gets items`` () =
    let data = File.ReadAllText("./hebcal-data.json")
    let result = Parshiot.Parse(data)
    Assert.Contains(result, fun x -> x.Title = "Parashat Shemot")

[<Fact>]
let ``Gets Torah portion without aliyah`` () =
    let data = File.ReadAllText("./hebcal-data.json")
    let result = Parshiot.Parse(data)
    let parasha = Seq.find (fun (x: Parshiot.Reading) -> x.Title = "Parashat Chukat") result
    Assert.Equal("Numbers 19:1 - 22:1", parasha.Leyning.Torah)
    Assert.Equal("Judges 11:1 - 11:33", parasha.Leyning.Haftarah)

[<Fact>]
let ``Gets Torah portion with aliyah`` () =
    let data = File.ReadAllText("./hebcal-data.json")
    let result = Parshiot.ParseWithAliyah data (Some 5)
    let parasha = Seq.find (fun (x: Parshiot.Reading) -> x.Title = "Parashat Chukat") result
    Assert.Equal("Numbers 20:22 - 21:9", parasha.Leyning.Torah)
    Assert.Equal("Judges 11:1 - 11:33", parasha.Leyning.Haftarah)

[<Fact>]
let ``Maftir is left out unless it's a special shabbat`` () =
    let data = File.ReadAllText("./hebcal-data.json")
    let result = Parshiot.ParseWithAliyah data (Some 5)
    let parasha = Seq.find (fun (x: Parshiot.Reading) -> x.Title = "Parashat Ki Tisa") result
    Assert.Equal("", parasha.Leyning.Maftir)
