using System;
using Signups.Core;

namespace RuachSignups.Models
{
    public class ShabbatModel
    {
        public ShabbatModel(Parshiot.Reading reading)
        {
            Date = reading.Date;
            Title = reading.Title;
        }

        public DateTime Date { get; }
        public string Title { get; }
    }
}
