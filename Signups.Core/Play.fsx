#r @"C:\Users\David\.nuget\packages\fsharp.data\2.4.4\lib\portable-net45+netcore45\FSharp.Data.dll";;
#r "System.Globalization"

open FSharp.Data

[<Literal>]
let hebcalUrl = "https://www.hebcal.com/hebcal/?v=1&cfg=json&year=now&month=x&maj=on&nx=on&ss=on&s=on&i=off"
let path = System.IO.Path.Combine(__SOURCE_DIRECTORY__, "..", "Signups.Test", "hebcal-data.json")
// type Readings = JsonProvider<"../Signups.Test/hebcal-data.json">
//type Readings = JsonProvider<"c:/users/david/source/repos/avodat-guf/Signups.Test/hebcal-data.json">
type Readings = JsonProvider<hebcalUrl>
let doc = Readings.GetSample()
doc.Items

