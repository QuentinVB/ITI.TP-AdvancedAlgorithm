using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TP_UserBasedRecommendation
{
    public interface IUser
    {
        int Id { get; set; }

        List<IData> Scores { get; set; }
    }
}
