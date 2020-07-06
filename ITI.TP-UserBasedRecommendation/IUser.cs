using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TP_UserBasedRecommendation
{
    interface IUser
    {
        List<IItem> Scores { get; set; }
    }
}
