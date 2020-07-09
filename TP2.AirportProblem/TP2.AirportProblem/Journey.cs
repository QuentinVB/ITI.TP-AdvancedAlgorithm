using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Salesman
{
    public class Journey
    {
        Airport departure;
        Airport arrival;
        LinkedList<Travel> path;

        public Journey(Airport departure, Airport arrival, Travel[] travels)
        {
            this.departure = departure;
            this.arrival = arrival;
            this.path = new LinkedList<Travel>(travels);
        }

        public int MoneyCost { get => ComputeMoneyCost(); }

        private int ComputeMoneyCost()
        {
            int sumCost = 0;
            foreach (Travel travel in path)
            {
                sumCost += travel.Price;
            }
            return sumCost;
        }

        public TimeSpan TimeCost { get => ComputeTimeCost(); }

        private TimeSpan ComputeTimeCost()
        {
            //todo améliorer la performance
            TimeSpan sumTimeCost = new TimeSpan();
            DateTime previousArrival = new DateTime();

            foreach (Travel travel in path)
            {
                sumTimeCost += travel.FlightDuration;
                if (!previousArrival.Equals(default))
                {
                    TimeSpan stopOver = (travel.DepartureDate - previousArrival).Duration();
                    sumTimeCost += stopOver;
                }
                previousArrival = travel.ArrivalDate;
            }
            return sumTimeCost;
        }

        public Journey Mutate()
        {
            throw new NotImplementedException();
        }

    }
}
