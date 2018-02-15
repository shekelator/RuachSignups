namespace Signups.Core
open FSharp.Data
open System

module Parshiot =
    let hello name =
        sprintf "Hello %s" name

    type HcReadings = JsonProvider<"./hebcal-data.json">
    type Leyning = {
        Torah: string;
        Haftarah: string;
        Maftir: string
    }

    type Reading = {
        Title:string;
        Date:DateTime;
        Leyning: Leyning;
        Link: Uri
    }

    let maftirIfSpecial (maftir : string) = 
        if maftir.Contains("|") then maftir
        else ""

    let ConvertLeyning (leyning : HcReadings.Leyning option, aliyah: int option) =
        match leyning with
            | Some x -> 
                {
                    Torah = 
                        match aliyah with
                        | Some 1 -> x.``1``
                        | Some 2 -> x.``2``
                        | Some 3 -> x.``3``
                        | Some 4 -> x.``4``
                        | Some 5 -> x.``5``
                        | Some 6 -> x.``6``
                        | Some 7 -> x.``7``
                        | _ -> x.Torah;
                    Haftarah = x.Haftarah; 
                    Maftir = maftirIfSpecial x.Maftir
                }
            | None -> {Torah = ""; Haftarah = ""; Maftir = ""}
        

    let ParseWithAliyah data (aliyah: int option) = 
        HcReadings.Parse(data).Items
            |> Seq.filter(fun i -> i.Leyning.IsSome)
            |> Seq.map(
                fun i -> 
                {
                    Title=i.Title; 
                    Date = i.Date; 
                    Link = new Uri(i.Link); 
                    Leyning = ConvertLeyning(i.Leyning, aliyah)
                })

    let Parse data = 
        ParseWithAliyah data None


