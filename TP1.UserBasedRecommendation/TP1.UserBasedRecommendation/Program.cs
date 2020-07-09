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
        static long averageUserTime = 0;
        static void Main(string[] args)
        {
            //CONFIG
            int MAXMOVIE = 10;
            int MAXSIMILARUSERS = 5;


            Console.WriteLine("Loading data and initialization, it may take a while...");
            //load files
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Dictionary<int, User> users = CsvLoader<User>.LoadCSV("u.user").ToDictionary(p=>p.Id);
            Dictionary<int, Movie> movies = CsvLoader<Movie>.LoadCSV("u.item").ToDictionary(p => p.Id);
            List<Score> scores = CsvLoader<Score>.LoadCSV("u.data").ToList();
            watch.Stop();
            Console.WriteLine($"1/3 : File loaded in {watch.ElapsedMilliseconds}ms !");

            //link relations
            watch = System.Diagnostics.Stopwatch.StartNew();
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
            watch.Stop();
            Console.WriteLine($"2/3 : Data linked in {watch.ElapsedMilliseconds}ms !");

            //compute similarity matrix
            watch = System.Diagnostics.Stopwatch.StartNew();
            users = ComputeSimilarityMatrix(users, movies.Count);
            watch.Stop();
            Console.WriteLine($"3/3 : Similarity matrixes computed in {watch.ElapsedMilliseconds}ms !");
            Console.WriteLine($"Averge time per user : {Program.averageUserTime}");
            Console.WriteLine("Loaded !\n");

            //user input his user id
            bool validUser = false;
            int userId = -1;
            User activeUser=new User();
            while (!validUser)
            {
                Console.WriteLine("Wich user id are you ?");
                var userInput = Console.ReadLine();
                if (!int.TryParse(userInput, out userId))
                {
                    Console.WriteLine("This is not an integer ! ");
                    continue;
                }
                if (userId < 0) Console.WriteLine("The input should be positive !");
                validUser = users.TryGetValue(userId, out activeUser);
                if(validUser == false) Console.WriteLine("This user does not exist");
            }
            Console.WriteLine($"Selected user {userId} ; we are searching the top {MAXMOVIE} best movies for you !");
            Console.WriteLine($"Computing...\n");
            watch = System.Diagnostics.Stopwatch.StartNew();

            //TODO : define similar user properly (ratio ? userinput ?)
            var bestsUsersForTheGivenUser = activeUser.Similarity.GetRange(1, MAXSIMILARUSERS + 1);

            //interesect film not in common
            HashSet<int> uniqueMovies = new HashSet<int>();
            //active user movies
            foreach (IData movie in activeUser.Scores)
            {
                uniqueMovies.Add(movie.Movie.Id);
            }

            //compute recommendation
            Dictionary<Movie, (double, double) > recommandedMovies = new Dictionary<Movie, (double, double)>();
            foreach ((User,double) user in bestsUsersForTheGivenUser)
            {
                foreach (IData movie in user.Item1.Scores)
                {
                    //ignore movie already known by user
                    if(!uniqueMovies.TryGetValue(movie.Movie.Id,out int actualMovie))
                    {
                        //try get the movie from the recommendation list
                        if(recommandedMovies.TryGetValue(movie.Movie, out (double,double) sumAndweight))
                        {
                            //weighted rate sum
                            sumAndweight.Item1 += (movie.Rate * user.Item2);

                            //sum weight
                            sumAndweight.Item2 += user.Item2;
                        }
                        else
                        {
                            recommandedMovies.Add(
                                movie.Movie,
                                (movie.Rate * user.Item2, user.Item2)
                            );
                        }
                    }
                }
            }

            List<(Movie, double)> movieRated = new List<(Movie, double)>();
            foreach (var recommandedMovie in recommandedMovies)
            {
                movieRated.Add((recommandedMovie.Key, (double)(recommandedMovie.Value.Item1 / recommandedMovie.Value.Item2)));
            }

            var sortedMovieRated= movieRated.OrderByDescending(x => x.Item2).ToArray();


            //output movie recommendation
            Console.WriteLine("The others users recommend to you :");
            Console.WriteLine($"Score |  Title ");
            for (int i = 0; i < MAXMOVIE; i++)
            {
                (Movie, double) item = sortedMovieRated[i];
                Console.WriteLine($"  {item.Item2}   | {item.Item1.Title}");
            }

            foreach ((Movie, double) item in sortedMovieRated)
            {
                
            }


            //Recommendation : item based model

            watch.Stop();
            Console.WriteLine($"Computed in {watch.ElapsedMilliseconds}ms !");
            Console.ReadKey();
            
        }
        //TODO : improve speed here
        private static Dictionary<int, User> ComputeSimilarityMatrix(Dictionary<int, User> users, int movieCount)
        {
            
            foreach (var user in users.Values)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                foreach (var userTarget in users.Values)
                {                   
                    var similarity = GetCosineSimilarity(user, userTarget, movieCount);
                    if (float.IsNaN(similarity)) throw new Exception("NAN !");

                    user.Similarity.Add( (userTarget, similarity) );
                }
                user.Similarity.Sort(
                delegate ((User, double) UserA, (User, double) UserB)
                {
                    //< 0 : UserA is better
                    //= 0 : users are equals
                    //> 0 : UserB is better
                    return (int)(UserA.Item2 - UserB.Item2);
                });
                watch.Stop();
                Program.averageUserTime += watch.ElapsedMilliseconds;
            }

            Program.averageUserTime /= users.Count();

            return users;
        }
        //TODO : improve speed here
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
