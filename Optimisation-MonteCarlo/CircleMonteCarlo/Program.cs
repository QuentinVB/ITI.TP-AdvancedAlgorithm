using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleMonteCarlo
{
    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {

            int piIteration = int.Parse(Console.ReadLine());

            float piSum = 0;

            for (int i = 0; i < piIteration; i++)
            {
                int N = (int)Math.Floor(100 * rnd.NextDouble()) ;

                piSum += iterationPi(N);
            }
            float pi = piSum / piIteration;

            Console.WriteLine($"using {piIteration} iterations, pi goes to : {pi}");
            Console.ReadKey();

        }

        public static float iterationPi(int N)
        {
            int c = 0;

            for (int i = 0; i <= N; i++)
            {
                float x = (float)rnd.NextDouble();
                float y = (float)rnd.NextDouble();

                //Console.WriteLine($"{x}:{y}");

                if (x * x + y * y <= 1)
                {
                    c++;
                }
            }


            float areaRatio = (float)c / N;

            return 4 * areaRatio;
        }
    }
}
