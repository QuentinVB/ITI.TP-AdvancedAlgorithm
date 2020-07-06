using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace ITI.TP_UserBasedRecommendation
{
    public class Score : IData
    {
        //filled by csv data
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Rate { get; set; }
        public int TimeStamp { get; set; }

        
        //internal link
        [Ignore]
        public User User { get; set; }
        [Ignore]
        public Movie Movie { get; set; }
    }
}
