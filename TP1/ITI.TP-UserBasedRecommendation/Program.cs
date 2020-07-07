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
            Console.WriteLine("Loading data");

            Dictionary<int, User> users = CsvLoader<User>.LoadCSV("u.user").ToDictionary(p=>p.Id);
            Dictionary<int, Movie> movies = CsvLoader<Movie>.LoadCSV("u.item").ToDictionary(p => p.Id);
            List<Score> scores = CsvLoader<Score>.LoadCSV("u.data").ToList();

            foreach (Score score in scores)
            {
                if(movies.TryGetValue(score.ItemId, out Movie movie))
                {
                    score.Movie = movie;
                }
                if (users.TryGetValue(score.UserId, out User user))
                {
                    score.User = user;
                    user.Scores.Add(score);
                }
            }

            users = ComputeSimilarityMatrix(users, movies.Count);

            Console.WriteLine("Loaded !");

            //user input his user id

            //compute recommendation



            //output movie recommendation




            /*

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

            */
            Console.ReadKey();
            
        }

        private static Dictionary<int, User> ComputeSimilarityMatrix(Dictionary<int, User> users, int movieCount)
        {
            foreach (var user in users.Values)
            {
                foreach (var userTarget in users.Values)
                {
                    if(userTarget.Id ==3)
                    {
                        Console.WriteLine("ping");
                    }
                    var similarity = GetCosineSimilarity(user, userTarget, movieCount);
                    if (float.IsNaN(similarity)) throw new Exception("NAN !");

                    user.Similarity.Add( (userTarget, similarity) );


                }
            }
            return users;
        }
        public static float GetCosineSimilarity(User userA, User userB, int movieCount)
        {
            if (userA.Id == userB.Id) return 1.0f;

            //create matrix for each users
            if(userA.ScoresMatrix == null)
            {
                int[] userAScoreMatrix = new int[movieCount];
                for (int i = 0; i < userA.Scores.Count(); i++)
                {
                    IEnumerable<IData> scores = userA.Scores.Where(s => s.Movie.Id == i);
                    if (scores.Count()!=0)
                    {
                        userAScoreMatrix[i] = scores.First().Rate;
                    }
                }
                userA.ScoresMatrix = userAScoreMatrix;
            }

            if(userB.ScoresMatrix==null)
            {
                int[] userBScoreMatrix = new int[movieCount];
                for (int i = 0; i < userB.Scores.Count(); i++)
                {
                    IEnumerable<IData> scores = userB.Scores.Where(s => s.Movie.Id == i);
                    if (scores.Count() != 0)
                    {
                        userBScoreMatrix[i] = scores.First().Rate;
                    }
                }
                userB.ScoresMatrix = userBScoreMatrix;
            }
            
            //TODO : store userB cosine for A


            float dot = 0.0f;
            float mag1 = 0.0f;
            float mag2 = 0.0f;
            for (int n = 0; n < movieCount; n++)
            {
                dot += userA.ScoresMatrix[n] * userB.ScoresMatrix[n];
                mag1 += userA.ScoresMatrix[n] * userA.ScoresMatrix[n];
                mag2 += userB.ScoresMatrix[n] * userB.ScoresMatrix[n];
            }

            if (dot == 0.0f) return 0.0f;

            return dot / (float)(Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }


        
    }
}
