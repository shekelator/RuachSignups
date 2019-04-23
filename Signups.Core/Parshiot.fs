namespace Signups.Core
open FSharp.Data
open System

module Parshiot =
    type HcReadings = JsonProvider<"./hebcal-data.json">
    type Leyning = {
        Torah: string;
        Haftarah: string;
        Maftir: string option
    }

    type Reading = {
        Title:string;
        Date:DateTime;
        Leyning: Leyning;
        Link: Uri
    }
 
    type ShabbatType = 
        ``Bereshit``
        |``Noach``
        |``Lech-Lecha``
        |``Vayera``
        |``Chayei Sara``
        |``Toldot``
        |``Vayetzei``
        |``Vayishlach``
        |``Vayeshev``
        |``Miketz``
        |``Vayigash``
        |``Vayechi``
        |``Shemot``
        |``Vaera``
        |``Bo``
        |``Beshalach``
        |``Yitro``
        |``Mishpatim``
        |``Shabbat Shekalim``
        |``Terumah``
        |``Tetzaveh``
        |``Shabbat Zachor``
        |``Erev Purim``
        |``Purim``
        |``Ki Tisa``
        |``Vayakhel-Pekudei``
        |``Shabbat Parah``
        |``Vayikra``
        |``Shabbat HaChodesh``
        |``Tzav``
        |``Shabbat HaGadol``
        |``Pesach I``
        |``Pesach VII``
        |``Pesach VIII``
        |``Shmini``
        |``Tazria-Metzora``
        |``Achrei Mot-Kedoshim``
        |``Emor``
        |``Behar-Bechukotai``
        |``Bamidbar``
        |``Shavuot I``
        |``Shavuot II``
        |``Nasso``
        |``Beha'alotcha``
        |``Sh'lach``
        |``Korach``
        |``Chukat``
        |``Balak``
        |``Pinchas``
        |``Rosh Chodesh Av``
        |``Matot-Masei``
        |``Devarim``
        |``Shabbat Chazon``
        |``Vaetchanan``
        |``Shabbat Nachamu``
        |``Eikev``
        |``Re'eh``
        |``Shoftim``
        |``Ki Teitzei``
        |``Ki Tavo``
        |``Nitzavim``
        |``Rosh Hashana I``
        |``Rosh Hashana II``
        |``Vayeilech``
        |``Shabbat Shuva``
        |``Erev Yom Kippur``
        |``Yom Kippur``
        |``Ha'Azinu``
        |``Sukkot I``
        |``Sukkot II``
        |``Sukkot VII (Hoshana Raba)``
        |``Shmini Atzeret``
        |``Simchat Torah``

    let maftirIfSpecial (maftir : string) = 
        if maftir.Contains("|") then Some maftir
        else None

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
            | None -> {Torah = ""; Haftarah = ""; Maftir = None}
        

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
        ParseWithAliyah data (Some 5)

