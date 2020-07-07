using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace ITI.TP_UserBasedRecommendation
{
    public class Movie:IItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public string ReleaseDate { get; set; }
        public string VideoReleaseDate { get; set; }
        public string ThatThing { get; set; }
        public string IMDBurl { get; set; }
        public int Unknown { get; set; }
        public int Action { get; set; }
        public int Adventure { get; set; }
        public int Animation { get; set; }
        public int Childrens { get; set; }
        public int Comedy { get; set; }
        public int Crime { get; set; }
        public int Documentary { get; set; }
        public int Drama { get; set; }
        public int Fantasy { get; set; }
        public int FilmNoir { get; set; }
        public int Horror { get; set; }
        public int Musical { get; set; }
        public int Mystery { get; set; }
        public int Romance { get; set; }
        public int SciFi { get; set; }
        public int Thriller { get; set; }
        public int War { get; set; }
        public int Western { get; set; }
    }
}
