namespace Signups.Core
open System

module Shabbat =
    type OpeningCategory = Reading | Gabbai
        
    [<CLIMutable>]
    type Opening = {
        Title: string;
        Category: OpeningCategory
    }

    type Occasion(date: DateTime, title: string, openings: Opening list, id: string option) =
        member this.Date = date
        member this.Title = title
        member this.Openings = openings
        member this.Id = id
        new(date, title, openings) = Occasion(date, title, openings, None)


    let parashaToShabbat (parasha : Parshiot.Reading) = 
        let everyWeekReadings = [
            {Title=(sprintf "Torah (%s): %s" parasha.Title parasha.Leyning.Torah); Category=Reading};
            {Title=(sprintf "Haftarah: %s" parasha.Leyning.Haftarah); Category=Reading}]

        let openings = 
            match parasha.Leyning.Maftir with
                | Some m -> {Title = (sprintf "Maftir: %s" m); Category=Reading} :: everyWeekReadings
                | None -> everyWeekReadings

        new Occasion(parasha.Date, parasha.Title, openings)


