using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Models;
using System;
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

        public AlgorithmManager()
        {
            Solutions = new List<Solution>();
        }

        public void CalculatePossibleSolution()
        {
            double startTemp = ParametersManager.Temparature;
            double startDelta = ParametersManager.Delta;
            int iteration = ParametersManager.IterationCount;
            Annealing = new SimulateAnnealing(startTemp, startDelta);
            Solutions.Clear();

            Solution a = GetRandomSolution();
            Solutions.Add(a);

            for (int i = 0; i < iteration; i++)
            {
                Random random = new Random();
                Solution b;

                do
                {
                    b = GetRandomSolution();
                }
                while (a.Equals(b));

                if (!Solutions.Contains(b))
                    Solutions.Add(b);

                if (b.TargetFunction() < a.TargetFunction())
                    SwapSolutions(a, b);
                else if (random.NextDouble() < Annealing.PropabilityFunction(a, b))
                    SwapSolutions(a, b);

                Annealing.SetLowerTemperature();
            }

            BestSolution = a;
        }

        public double GetTotalKilometers()
        {
            double kilometers = 0.0;
            int senders = ParametersManager.SenderCoordiantes.Count;
            int delivers = ParametersManager.DeliveryCoordinates.Count;

            for (int i = 0; i < senders; i++)
            {
                for (int j = 0; j < delivers; j++)
                {
                    if (BestSolution.X[i, j] != 0)
                        kilometers += ParametersManager.CostsList[i, j].DrivenKilometers;
                }
            }

            return Math.Round(kilometers, 2);
        }

        public double GetTotalKilometersBySolution(Solution solution)
        {
            double kilometers = 0.0;
            int senders = ParametersManager.SenderCoordiantes.Count;
            int delivers = ParametersManager.DeliveryCoordinates.Count;

            for (int i = 0; i < senders; i++)
            {
                for (int j = 0; j < delivers; j++)
                {
                    if (solution.X[i, j] != 0)
                        kilometers += ParametersManager.CostsList[i, j].DrivenKilometers;
                }
            }

            return Math.Round(kilometers, 2);
        }

        public double GetTotalCostBySolution(Solution solution)
        {
            return solution.TargetFunction();
        }

        public double GetTotalCost()
        {
            return BestSolution.TargetFunction();
        }

        private void SwapSolutions(Solution a, Solution b)
        {
            int deliversCount = ParametersManager.DeliveryCoordinates.Count;
            int senderCount = ParametersManager.SenderCoordiantes.Count;

            a = new Solution(senderCount, deliversCount);
            a.C = b.C.Clone() as double[,];
            a.X = b.X.Clone() as double[,];
        }

        public Solution GetRandomSolution()
        {
            ParametersManager.RestoreLimitsAndRequests();

            int deliversCount = ParametersManager.DeliveryCoordinates.Count;
            int senderCount = ParametersManager.SenderCoordiantes.Count;
            Solution solution = new Solution(senderCount, deliversCount);

            for (int i = 0; i < ParametersManager.CostsList.GetLength(0); i++)
            {
                for (int j = 0; j < ParametersManager.CostsList.GetLength(1); j++)
                {
                    solution.C[i, j] = ParametersManager.CostsList[i, j].Cost;
                }
            }

            for (int i = 0; i < senderCount; i++)
            {
                SenderCooridante sender = ParametersManager.SenderCoordiantes[i];
                Random random = new Random();
                List<int> servedDeliversIndex;

                do
                {
                    servedDeliversIndex = new List<int>();
                    int randomDelivery = random.Next(deliversCount);
                    DeliveryCoordinate delivery = ParametersManager.DeliveryCoordinates[randomDelivery];

                    if (sender.CurrentLimit == 0)
                        break;
                    else if (delivery.CurrentRequest == 0)
                        continue;

                    if (delivery.CurrentRequest > sender.CurrentLimit)
                    {
                        solution.X[i, randomDelivery] = sender.CurrentLimit;
                        delivery.CurrentRequest = delivery.CurrentRequest - sender.CurrentLimit;
                        sender.CurrentLimit = 0;
                    }
                    else
                    {
                        solution.X[i, randomDelivery] = delivery.CurrentRequest;
                        sender.CurrentLimit = sender.CurrentLimit - delivery.CurrentRequest;
                        delivery.CurrentRequest = 0;
                    }

                    servedDeliversIndex.Add(randomDelivery);
                }
                while (servedDeliversIndex.Count != deliversCount);
            }

            return solution;
        }

        public void Dispose()
        {
            ;
        }
    }
}