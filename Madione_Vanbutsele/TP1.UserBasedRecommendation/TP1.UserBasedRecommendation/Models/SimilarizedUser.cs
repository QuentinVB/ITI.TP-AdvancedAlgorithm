using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TP_UserBasedRecommendation.Models
{
    public struct SimilarizedUser
    {
        public SimilarizedUser(User user, double similarityScore)
        {
            User = user;
            SimilarityScore = similarityScore;
        }
        public User User { get; set; }
        public double SimilarityScore { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SimilarizedUser user &&
                   EqualityComparer<User>.Default.Equals(User, user.User);
        }

        public override int GetHashCode()
        {
            int hashCode = -1091305050;
            hashCode = hashCode * -1521134295 + EqualityComparer<User>.Default.GetHashCode(User);
            return hashCode;
        }
    }
}
