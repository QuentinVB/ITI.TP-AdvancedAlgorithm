using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TP_UserBasedRecommendation
{
    public class User : IUser
    {
        List<IData>  _scores = new List<IData>();
        List<(User, double)> _similarity = new List<(User, double)>();

        public int Id { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string ZipCode { get; set; }

        [Ignore]
        public List<IData> Scores { get => _scores; set => _scores = value; }
        [Ignore]
        public int[] ScoresMatrix { get; set ; }
        [Ignore]
        public List<(User,double)> Similarity { get => _similarity; set => _similarity = value; }
        
    }
}
