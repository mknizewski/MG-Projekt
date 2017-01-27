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

                if (!Solutions.Contains(b))
                    Solutions.Add(b);
                else
                    continue;

                if (b.TargetFunction() < a.TargetFunction())
                    a = SwapSolutions(a, b);
                else if (random.NextDouble() < Annealing.PropabilityFunction(a, b))
                    a = SwapSolutions(a, b);

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

        private Solution SwapSolutions(Solution a, Solution b)
        {
            int deliversCount = ParametersManager.DeliveryCoordinates.Count;
            int senderCount = ParametersManager.SenderCoordiantes.Count;

            a = new Solution(senderCount, deliversCount, ParametersManager.CostsList);
            a.C = b.C.Clone() as double[,];
            a.X = b.X.Clone() as double[,];
            a.Vector = b.Vector.Clone() as int[];

            return a;
        }

        public Solution GetRandomSolution()
        {
            ParametersManager.RestoreLimitsAndRequests();

            int deliversCount = ParametersManager.DeliveryCoordinates.Count;
            int senderCount = ParametersManager.SenderCoordiantes.Count;
            Solution solution = new Solution(senderCount, deliversCount, ParametersManager.CostsList);
            int i = 0;

            do
            {
                Random random = new Random();
                int q = random.Next(0, senderCount * deliversCount);

                if (!solution.IsSeen[q])
                {
                    solution.IsSeen[q] = true;
                    solution.Vector[i] = q;
                    i++;

                    double calculatingRow = (q) / (senderCount);
                    int row = (int)Math.Floor(calculatingRow);
                    int column = (q) % senderCount;

                    SenderCooridante senderCoords = ParametersManager.SenderCoordiantes[row];
                    DeliveryCoordinate deliveryCoords = ParametersManager.DeliveryCoordinates[column];

                    int value = Math.Min(senderCoords.CurrentLimit, deliveryCoords.CurrentRequest);

                    solution.X[row, column] = value;
                    senderCoords.CurrentLimit = senderCoords.CurrentLimit - value;
                    deliveryCoords.CurrentRequest = deliveryCoords.CurrentRequest - value;
                }
            }
            while (!solution.AllPositionIsSeen());

            return solution;
        }

        private Solution ChangeTwoRandomElements(Solution solution)
        {
            int deliversCount = ParametersManager.DeliveryCoordinates.Count;
            int senderCount = ParametersManager.SenderCoordiantes.Count;
            Solution newSolution = new Solution(senderCount, deliversCount, ParametersManager.CostsList);

            // Inwersja
            int[] vector = solution.Inversion();

            // Mutacja
            int vectorCount = vector.Length;
            Random random = new Random();
            int first = random.Next(0, vectorCount);
            int second = random.Next(0, vectorCount);

            int a = vector[first];
            int b = vector[second];

            vector[first] = b;
            vector[second] = a;

            newSolution.Vector = vector.Clone() as int[];
            newSolution.X = RebulidSolutionByVector(newSolution);

            return newSolution;
        }

        private double[,] RebulidSolutionByVector(Solution solution)
        {
            int deliversCount = ParametersManager.DeliveryCoordinates.Count;
            int senderCount = ParametersManager.SenderCoordiantes.Count;
            double[,] x = new double[senderCount, deliversCount];

            ParametersManager.RestoreLimitsAndRequests();

            for (int i = 0; i < solution.Vector.Length; i++)
            {
                int q = solution.Vector[i];
                int row = (int)Math.Floor((double)(q / senderCount));
                int column = q % senderCount;

                SenderCooridante senderCoords = ParametersManager.SenderCoordiantes[row];
                DeliveryCoordinate deliveryCoors = ParametersManager.DeliveryCoordinates[column];

                int value = Math.Min(senderCoords.CurrentLimit, deliveryCoors.CurrentRequest);
                senderCoords.CurrentLimit = senderCoords.CurrentLimit - value;
                deliveryCoors.CurrentRequest = deliveryCoors.CurrentRequest - value;

                x[row, column] = value;
            }

            return x;
        }

        public void Dispose()
        {
            ;
        }
    }
}