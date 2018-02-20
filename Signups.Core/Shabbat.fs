namespace Signups.Core
open System

module Shabbat =
    type OpeningCategory = Reading | Gabbai
        
    type Opening = {
        Title: string;
        Category: OpeningCategory
    }

    type Shabbat = DateTime * Opening list

    let parashaToEvent (parasha : Parshiot.Reading) = 
        let everyWeekReadings = [
            {Title=(sprintf "Torah (%s): %s" parasha.Title parasha.Leyning.Torah); Category=Reading};
            {Title=(sprintf "Haftarah: %s" parasha.Leyning.Haftarah); Category=Reading}]

        let openings = match parasha.Leyning.Maftir with
            | Some m -> {Title = (sprintf "Maftir: %s" m); Category=Reading} :: everyWeekReadings
            | None -> everyWeekReadings

        (parasha.Date, openings)


