using System;

namespace MG_Projekt.BOL.Models
{
    public class SimulateAnnealing
    {
        public double Temperature { get; set; }
        public double Delta { get; set; }

        public SimulateAnnealing(double startTemperature, double startDelta)
        {
            this.Temperature = startTemperature;
            this.Delta = startDelta;
        }

        public double PropabilityFunction(Solution oldSolution, Solution newSolution)
        {
            double result = Math.Exp((-newSolution.TargetFunction() - oldSolution.TargetFunction()) / Temperature);
            return result;
        }

        public double LowerTemperature()
        {
            return Delta * Temperature;
        }
    }
}