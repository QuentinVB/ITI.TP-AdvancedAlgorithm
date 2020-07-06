using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TP_UserBasedRecommendation
{
    class Functions
    {
        /*
        //TODO : switch to IEnumerable ?
        //TODO : manage jagged array ?
        public static float GetCosineSimilarity(IEnumerable<IUser> userMatrix, int userAidx, int userBidx)
        {
            int N = userMatrix.Count(); //UserMatrix.GetLength(userBidx) < UserMatrix.GetLength(userAidx) ? UserMatrix.GetLength(userBidx) : UserMatrix.GetLength(userAidx);

            float dot = 0.0f;
            float mag1 = 0.0f;
            float mag2 = 0.0f;
            for (int n = 0; n < N; n++)
            {
                dot += userMatrix[userAidx, n] * userMatrix[userBidx, n];
                mag1 += userMatrix[userAidx, n] * userMatrix[userAidx, n];
                mag2 += userMatrix[userBidx, n] * userMatrix[userBidx, n];
            }
            return dot / (float)(Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }
        */
    }
}
