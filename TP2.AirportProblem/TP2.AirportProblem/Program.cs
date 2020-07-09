using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Salesman
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Airport> airport = CsvLoader<Airport>.LoadCSV("airports.txt").ToList();
            List<Travel> travel = CsvLoader<Travel>.LoadCSV("small-schedule.txt",",").ToList();

            //Test
            

            //Recueillir airport departure et arrival
            

            //Durée des voyages et escales

            //Somme des prix des escales



            Console.ReadLine();
        }
    }
}
