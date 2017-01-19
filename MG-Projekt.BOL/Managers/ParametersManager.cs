using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Models;
using System.Collections.Generic;

namespace MG_Projekt.BOL.Managers
{
    public class ParametersManager : IManager
    {
        // Parameters
        public int DeliveryCount { get; set; }
        public double PetrolCost { get; set; }
        public double PetrolUsage { get; set; }
        public int Trucks { get; set; }

        // Algorithm
        public double Temparature { get; set; }
        public double Delta { get; set; }
        public int IterationCount { get; set; }

        // Coords
        public List<Coordinate> DeliveryCoordinates { get; set; }
        public Coordinate SenderCoordiante { get; set; }

        // Costs
        public List<Costs> CostsList { get; set; }

        // Function
        public Function Func { get; set; }

        public ParametersManager()
        {
            this.DeliveryCoordinates = new List<Coordinate>();
        }

        public void CalculateCosts()
        {
            CostsList = new List<Costs>();

            DeliveryCoordinates.ForEach(coords =>
            {
                Costs costs = new Costs(coords, SenderCoordiante, PetrolUsage, PetrolCost);
                CostsList.Add(costs);
            });
        }

        public void CalculateFunction()
        {

        }

        public void Dispose()
        {
            this.DeliveryCoordinates.Clear();
        }
    }
}
