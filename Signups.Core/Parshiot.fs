namespace Signups.Core
open FSharp.Data
open System


module Parshiot =
    type HcReadings = JsonProvider<"./hebcal-data.json">
    type Leyning = {
        Torah: string;
        Haftarah: string;
        Maftir: string option;
        Besorah: string;
    }

    type Reading = {
        Title:string;
        Date:DateTime;
        Leyning: Leyning;
        Link: Uri
    }
 
    let maftirIfSpecial (maftir : string) = 
        if maftir.Contains("|") then Some maftir
        else None

    let getBesorah title besorahYear = 
        // let parasha = getParashaFromTitle title
        // match parasha
        // | Korach -> ""
        "John 2:13–25"

    let ConvertLeyning (leyning : HcReadings.Leyning option, title: String, aliyah: int option, besorahYear: Besorot.Year) =
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
                    Maftir = maftirIfSpecial x.Maftir;
                    Besorah = getBesorah title besorahYear
                }
            | None -> {Torah = ""; Haftarah = ""; Maftir = None; Besorah = ""}
        

    let ParseWithAliyah data (aliyah: int option) (besorahYear: Besorot.Year) = 
        HcReadings.Parse(data).Items
            |> Seq.filter(fun i -> i.Leyning.IsSome)
            |> Seq.map(
                fun i -> 
                {
                    Title=i.Title.Replace("Parashat", ""); 
                    Date = i.Date; 
                    Link = new Uri(i.Link); 
                    Leyning = ConvertLeyning(i.Leyning, i.Title, aliyah, besorahYear)
                })

    let Parse data = 
        ParseWithAliyah data (Some 7) (Besorot.A)

