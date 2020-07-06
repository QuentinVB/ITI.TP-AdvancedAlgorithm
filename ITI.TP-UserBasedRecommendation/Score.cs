using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TP_UserBasedRecommendation
{
    public class Score
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Rating { get; set; }
        public int TimeStamp { get; set; }
    }
}
