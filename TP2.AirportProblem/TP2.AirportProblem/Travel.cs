using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Salesman
{
    public class Travel
    {
        public string DepartureCode { get; set; }
        public string ArrivalCode { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int Price { get; set; }

        [Ignore]
        public DateTime DepartureDate { get => DateTime.ParseExact(DepartureTime, "H:mm", new CultureInfo("fr-FR")); }

        [Ignore]
        public DateTime ArrivalDate { get => DateTime.ParseExact(ArrivalTime, "H:mm", new CultureInfo("fr-FR")); }

        [Ignore]
        public TimeSpan FlightDuration { get => (DepartureDate - ArrivalDate).Duration(); }



    }
}
