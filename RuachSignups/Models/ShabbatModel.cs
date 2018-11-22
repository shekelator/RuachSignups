using System;
using Microsoft.FSharp.Core;
using Signups.Core;
using Microsoft.FSharp.Core;

namespace RuachSignups.Models
{
    public class ShabbatModel
    {
        public ShabbatModel(Parshiot.Reading reading)
        {
            Date = reading.Date;
            Title = reading.Title;
            Torah = reading.Leyning?.Torah;
            Maftir = FSharpOption<string>.get_IsSome(reading.Leyning?.Maftir)
                ? reading.Leyning?.Maftir?.Value
                : null;
            Haftarah = reading.Leyning?.Haftarah;
            Besorah = "N/A";
        }

        public DateTime Date { get; }
        public string Title { get; }
        public string Torah { get; }
        public string Maftir { get; }
        public string Haftarah { get; }
        public string Besorah { get; }

    }
}
