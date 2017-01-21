using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Models;
using System.Collections.Generic;

namespace MG_Projekt.BOL.Managers
{
    public class AlgorithmManager : IManager
    {
        // All Solutions
        public List<Solution> Solutions { get; set; }

        // Finded solution
        public Solution BestSolution { get; set; }

        // Algorithm
        public SimulateAnnealing Annealing { get; set; }

        // Parameters Manager
        public ParametersManager ParametersManager { get; set; }

        public void CalculatePossibleSolution()
        {

        }

        public Solution GetRandomSolution()
        {
            return null;
        }

        public void Dispose()
        {
            ;
        }
    }
}
