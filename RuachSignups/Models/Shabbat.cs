using System;
using Signups.Core;

namespace RuachSignups.Models
{
    public class Shabbat
    {
        public Shabbat(Parshiot.Reading reading)
        {
            Date = reading.Date;
            Title = reading.Title;
        }

        public DateTime Date { get; }
        public string Title { get; }
    }
}
