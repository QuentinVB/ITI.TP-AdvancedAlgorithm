using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TP_UserBasedRecommendation
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] userMatrix = new int[,]
            {
                {4,3,0,0,5,0},
                {5,0,4,0,4,0},
                {4,0,5,3,4,0},
                {0,3,0,0,0,5},
                {0,4,0,0,0,4},
                {0,0,2,4,0,5}
            };

            float[,] similarityMatrix = ComputeSimilarityMatrix(userMatrix);

            //Recommendation : user based model

            //TODO : Secure input
            int givenUser = int.Parse(Console.ReadLine());

            Debug.Assert(givenUser > 0 && givenUser < similarityMatrix.GetLength(1));
            givenUser--;

            float maxSimilarity = float.MinValue;
            float maxSimilarUserIdx = -1;

            for (int i = 0; i < similarityMatrix.GetLength(1); i++)
            {
                if (i != givenUser && similarityMatrix[givenUser, i] > maxSimilarity)
                {
                    maxSimilarity = similarityMatrix[givenUser, i];
                    maxSimilarUserIdx = i;
                }
            }
            Console.WriteLine($"similar user : {maxSimilarUserIdx + 1}");



            //Recommendation : item based model


            Console.ReadKey();

        }

        //TODO : switch to IEnumerable ?
        //TODO : manage jagged array ?
        public static float GetCosineSimilarity(int[,] userMatrix, int userAidx, int userBidx)
        {
            int N = userMatrix.GetLength(1); //UserMatrix.GetLength(userBidx) < UserMatrix.GetLength(userAidx) ? UserMatrix.GetLength(userBidx) : UserMatrix.GetLength(userAidx);

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

        //TODO : add security
        public static float[,] ComputeSimilarityMatrix(int[,] userMatrix)
        {
            float[,] similarityMatrix = new float[userMatrix.GetLength(0), userMatrix.GetLength(1)];

            for (int i = 0; i < userMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < userMatrix.GetLength(1); j++)
                {
                    similarityMatrix[i, j] = GetCosineSimilarity(userMatrix, i, j);
                }
            }
        

            return similarityMatrix;
        }
    }
}
