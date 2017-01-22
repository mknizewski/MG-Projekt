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
                Solution b = GetRandomSolution();
                Solutions.Add(b);

                if (b.TargetFunction() < a.TargetFunction())
                    a = b;
                else if (random.NextDouble() < Annealing.PropabilityFunction(a, b))
                    a = b;

                Annealing.LowerTemperature();
            }

            BestSolution = a;
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
                while (servedDeliversIndex.Count != deliversCount - 1);
            }

            return solution;
        }

        public void Dispose()
        {
            ;
        }
    }
}