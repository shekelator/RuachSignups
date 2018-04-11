using System;
using Microsoft.FSharp.Core;
using Signups.Core;

namespace RuachSignups.Models
{
    public class ShabbatModel
    {
        public ShabbatModel(Parshiot.Reading reading)
        {
            Date = reading.Date;
            Title = reading.Title;
            Torah = reading.Leyning.Torah;
            Haftarah = reading.Leyning.Haftarah;
            Maftir = FSharpOption<string>.get_IsSome(reading.Leyning.Maftir)
                ? reading.Leyning.Maftir.Value
                : null;
        }

        public DateTime Date { get; }
        public string Title { get; }
        public string Torah { get; }
        public string Haftarah { get; }
        public string Maftir { get; }
    }
}
