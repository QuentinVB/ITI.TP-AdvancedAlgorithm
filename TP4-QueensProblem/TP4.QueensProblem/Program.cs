using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TP4.QueensProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            //build Chess Board
            bool[,] board = new bool[8,8];
            int width = 8;
            int height = 8;
            Random rnd = new Random();

            //TODO : group paramters
            int startPopulation = 20;
            int bestToKeep = 7;
            int dumbToKeep = 5;
            int aleaToAdd = startPopulation - (bestToKeep + dumbToKeep);

            int maxIterations = 20;

            double mutationRate = 0.10;
            int populationBreed = 50;



            //initialize population
            List<bool[,]> population = new List<bool[,]>();
            int queenCount = 0;
            for (int i = 0; i < startPopulation; i++)
            {
                bool[,] tBoard = new bool[width, height];
                do
                {
                    int x = rnd.Next(0, width);
                    int y = rnd.Next(0, height);

                    if (tBoard[x,y])
                    {
                        continue;
                    }

                    tBoard[x, y] = true;
                    queenCount++;
                }
                while (queenCount != 8);
                population.Add(tBoard);
                //Console.WriteLine(population);

            }

            //    //evolution loop
            //    int interations = 0;
            //    do
            //    {
            //        Console.WriteLine($"Begin iteration {interations}");



            //breed population
            List<bool[,]> tempPopulation = new List<bool[,]>();
            do
            {
                int parent1idx = (int)Math.Floor(rnd.NextDouble() * population.Count());
                int parent2idx = (int)Math.Floor(rnd.NextDouble() * population.Count());

                //TODO find better solution ?
                if (parent1idx == parent2idx)
                {
                    if (parent2idx + 1 >= population.Count())
                    {
                        parent2idx--;
                    }
                    else
                    {
                        parent2idx++;
                    }
                }

                var parent1 = population[parent1idx];
                var parent2 = population[parent2idx];

                Console.WriteLine("test");


                bool[,] newChild = new bool[width, height];
                for (int i = 0; i < parent1.GetLength(0); i++)
                {
                    for (int j = 0; j < parent1.GetLength(1); j++)
                    {
                        newChild[j, i] = (i>height/2)? parent1[j, i] : parent2[j,i];
                    }
                }

                tempPopulation.Add(newChild);

            } while (tempPopulation.Count() < populationBreed);
            tempPopulation.AddRange(population);

            //Console.WriteLine("test");


            //        //select surviving population
            //        //TODO : add safety
            //        tempPopulation.Sort(delegate ((int, int) individual1, (int, int) individual2)
            //        {
            //            //< 0 : individual1 is better
            //            //= 0 : individuals are equals
            //            //> 0 : individual2 is better
            //            return (int)(SumAround(brut, width, height, individual1.Item1, individual1.Item2) - SumAround(brut, width, height, individual2.Item1, individual2.Item2));
            //        });
            //        population = tempPopulation.GetRange(0, bestToKeep);
            //        population.AddRange(tempPopulation.GetRange(tempPopulation.Count - dumbToKeep, dumbToKeep));

            //        //Add alea
            //        for (int i = 0; i < aleaToAdd; i++)
            //        {
            //            population.Add(((int)Math.Floor(rnd.NextDouble() * width), (int)Math.Floor(rnd.NextDouble() * height)));
            //        }

            //        //mutate population
            //        int mutationCount = 0;

            //        for (int i = 0; i < population.Count; i++)
            //        {
            //            if (rnd.NextDouble() < mutationRate)
            //            {
            //                //mutate item1 
            //                population[i] = (
            //                    (rnd.NextDouble() < 0.5) ? (int)Math.Floor(rnd.NextDouble() * width) : population[i].Item1,
            //                    (rnd.NextDouble() < 0.5) ? (int)Math.Floor(rnd.NextDouble() * width) : population[i].Item2
            //                    );
            //                mutationCount++;
            //            }
            //        }
            //        Console.WriteLine($" Muted {mutationCount} individuals with a {mutationRate} rate");

            //        float bestScore = SumAround(brut, width, height, population[0].Item1, population[0].Item2);
            //        Console.WriteLine($" Best population score is : {bestScore}");

            //        interations++;
            //    } while (interations < maxIterations);
            //    Console.WriteLine("finished !");

            //    //get best result
            //    var best = population[0];
            //    Console.WriteLine($"Best position for the firestation should be {best.Item1}:{best.Item2}");

            //    //float rslt = SumAround(brut, width, height, 1, 1);
            //    Console.Read();
            //}


            //public static float SumAround(int[] map, int width, int height, int x, int y)
            //{
            //    //int width = map.GetLength(0);
            //    //int height = map.GetLength(1);

            //    int caseIdx = y * width + x;

            //    int top = caseIdx - width;
            //    int topLeft = caseIdx - 1 - width;
            //    int topRight = caseIdx + 1 - width;

            //    int left = caseIdx - 1;
            //    int right = caseIdx + 1;
            //    int bottom = caseIdx + width;
            //    int bottomLeft = caseIdx - 1 + width;
            //    int bottomRight = caseIdx + 1 + width;

            //    int sum = map[caseIdx];

            //    //TODO : find better limits... (pacman effect)
            //    sum += top >= 0 ? map[top] : 0;
            //    sum += topLeft >= 0 ? map[topLeft] : 0;
            //    sum += topRight >= 0 ? map[topRight] : 0;
            //    sum += left >= 0 ? map[left] : 0;
            //    sum += right < map.Length ? map[right] : 0;
            //    sum += bottom < map.Length ? map[bottom] : 0;
            //    sum += bottomLeft < map.Length ? map[bottomLeft] : 0;
            //    sum += bottomRight < map.Length ? map[bottomRight] : 0;


            //    return sum;
        }
    }
}
