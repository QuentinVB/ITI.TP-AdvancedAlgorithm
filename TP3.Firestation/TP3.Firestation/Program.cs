using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3.Firestation
{
    class Program
    {
        /*
         // size = nombre de carré par ligne ou colonne
            x = squareId % size
            y = floor(squareId / size)

 

            top = y * (2 * size + 1) + x
            left = top + size
            right = left + 1
            bot = top + (2 * size + 1)
         
         */
        static void Main(string[] args)
        {
            //build citymap
            int[] brut = new int[] { 5, 2, 4, 8, 9, 0, 3, 3, 8, 7, 5, 5, 3, 4, 4, 6, 4, 1, 9, 1, 4, 1, 2, 1, 3, 8, 7, 8, 9, 1, 1, 7, 1, 6, 9, 3, 1, 9, 6, 9, 4, 7, 4, 9, 9, 8, 6, 5, 4, 2, 7, 5, 8, 2, 5, 2, 3, 9, 8, 2, 1, 4, 0, 6, 8, 4, 0, 1, 2, 1, 1, 5, 2, 1, 2, 8, 3, 3, 6, 2, 4, 5, 9, 6, 3, 9, 7, 6, 5, 10, 0, 6, 2, 8, 7,1, 2, 1, 5, 3 };
            int width = 10;
            int height = 10;
            Random rnd = new Random();

            int[,] citymap = new int[width, height];

            for (int i = 0; i < brut.Length; i++)
            {
                int y = i % citymap.GetLength(0);
                int x = (int)(i / citymap.GetLength(1));
                citymap[x, y] = brut[i];
            }

            //TODO : group paramters
            int startPopulation = 20;
            int bestToKeep = 7;
            int dumbToKeep = 5;
            int aleaToAdd = startPopulation - (bestToKeep + dumbToKeep);

            int maxIterations = 20;

            double mutationRate = 0.25;
            int populationBreed = 50;



            //initialize population
            List<(int, int)> population = new List<(int, int)>();
            for (int i = 0; i < startPopulation; i++)
            {
                population.Add(((int)Math.Floor(rnd.NextDouble() * width), (int)Math.Floor(rnd.NextDouble() * height)));
            }

            //evolution loop
            int interations = 0;
            do
            {
                Console.WriteLine($"Begin iteration {interations}");
                //mutate population
                int mutationCount = 0;

                for (int i = 0; i < population.Count; i++)
                {
                    if (rnd.NextDouble() < mutationRate)
                    {
                        //mutate item1 
                        population[i] = (
                            (rnd.NextDouble() < 0.5) ? (int)Math.Floor(rnd.NextDouble() * width) : population[i].Item1,
                            (rnd.NextDouble() < 0.5) ? (int)Math.Floor(rnd.NextDouble() * width) : population[i].Item2
                            );
                        mutationCount++;
                    }
                }
                Console.WriteLine($" Muted {mutationCount} individuals with a {mutationRate} rate");


                //breed population
                List<(int, int)> tempPopulation = new List<(int, int)>();
                do
                {
                    int parent1idx = (int)Math.Floor(rnd.NextDouble() * population.Count());
                    int parent2idx = (int)Math.Floor(rnd.NextDouble() * population.Count());

                    //TODO find better solution ?
                    if (parent1idx == parent2idx)
                    {
                        if(parent2idx + 1>= population.Count())
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


                    var newChild = (
                        (rnd.NextDouble() < 0.5) ? parent1.Item1 : parent2.Item1,
                        (rnd.NextDouble() < 0.5) ? parent1.Item1 : parent2.Item1
                        );
                    tempPopulation.Add(newChild);

                } while (tempPopulation.Count() < populationBreed);
                tempPopulation.AddRange(population);

                //select surviving population
                //TODO : add safety
                tempPopulation.Sort(delegate ((int, int) individual1, (int, int) individual2)
                {
                    //< 0 : individual1 is better
                    //= 0 : individuals are equals
                    //> 0 : individual2 is better
                    return (int)(SumAround(brut, width, height, individual1.Item1, individual1.Item2) - SumAround(brut, width, height, individual2.Item1, individual2.Item2));
                });
                population = tempPopulation.GetRange(0, bestToKeep);
                population.AddRange(tempPopulation.GetRange(tempPopulation.Count - dumbToKeep, dumbToKeep));

                //Add alea
                for (int i = 0; i < aleaToAdd; i++)
                {
                    population.Add(((int)Math.Floor(rnd.NextDouble() * width), (int)Math.Floor(rnd.NextDouble() * height)));
                }

                float bestScore = SumAround(brut, width, height, population[0].Item1, population[0].Item2);
                Console.WriteLine($" Best population score is : {bestScore}");

                interations++;
            } while (interations<maxIterations);
            Console.WriteLine("finished !");

            //get best result
            var best =population[0];
            Console.WriteLine($"Best position for the firestation should be {best.Item1}:{best.Item2}");

            //float rslt = SumAround(brut, width, height, 1, 1);
            Console.Read();
        }


        public static float SumAround(int[] map, int width, int height, int x, int y)
        {
            //int width = map.GetLength(0);
            //int height = map.GetLength(1);

            int caseIdx = y*width + x;

            int top= caseIdx - width;
            int topLeft = caseIdx-1 - width;
            int topRight = caseIdx+1 - width;

            int left = caseIdx-1;
            int right= caseIdx + 1;
            int bottom= caseIdx+width;
            int bottomLeft = caseIdx-1 + width;
            int bottomRight = caseIdx+ 1 + width;

            int sum = map[caseIdx];

            //TODO : find better limits... (pacman effect)
            sum += top >= 0 ? map[top] : 0;
            sum += topLeft >= 0 ? map[topLeft] : 0;
            sum += topRight >= 0 ? map[topRight] : 0;
            sum += left >= 0 ? map[left] : 0;
            sum += right < map.Length ? map[right] : 0;
            sum += bottom < map.Length ? map[bottom] : 0;
            sum += bottomLeft < map.Length ? map[bottomLeft] : 0;
            sum += bottomRight < map.Length ? map[bottomRight] : 0;


            return sum;
        }
    }
}
