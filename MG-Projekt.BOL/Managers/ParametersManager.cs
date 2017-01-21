using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Models;
using System.Collections.Generic;
using System.Linq;

namespace MG_Projekt.BOL.Managers
{
    public class ParametersManager : IManager
    {
        // Parameters
        public double PetrolCost { get; set; }

        public double PetrolUsage { get; set; }

        // Algorithm
        public double Temparature { get; set; }

        public double Delta { get; set; }
        public int IterationCount { get; set; }

        // Coords
        public List<DeliveryCoordinate> DeliveryCoordinates { get; set; }

        public List<SenderCooridante> SenderCoordiantes { get; set; }

        // Costs
        public Costs[,] CostsList { get; set; }

        public ParametersManager()
        {
            this.DeliveryCoordinates = new List<DeliveryCoordinate>();
            this.SenderCoordiantes = new List<SenderCooridante>();
        }

        public void CalculateCosts()
        {
            int senderCount = SenderCoordiantes.Count;
            int deliveryCount = DeliveryCoordinates.Count;

            CostsList = new Costs[senderCount, deliveryCount];

            for (int i = 0; i < senderCount; i++)
            {
                for (int j = 0; j < deliveryCount; j++)
                {
                    SenderCooridante sendCoords = SenderCoordiantes[i];
                    DeliveryCoordinate deliveryCoors = DeliveryCoordinates[j];

                    CostsList[i, j] = new Costs(deliveryCoors, sendCoords, PetrolUsage, PetrolCost);
                }
            }
        }

        public string DisplayFunction()
        {
            string fun = "K(xij) = ";
            int senderCount = SenderCoordiantes.Count;
            int deliveryCount = DeliveryCoordinates.Count;

            for (int i = 0; i < senderCount; i++)
            {
                for (int j = 0; j < deliveryCount; j++)
                {
                    Costs cost = CostsList[i, j];
                    fun += $"{cost.Cost}x{i}{j} + ";
                }

                if (i == senderCount - 1)
                    fun = fun.Remove(fun.Length - 2, 2);
            }

            return fun;
        }

        public string[] DisplayDeliversLimit()
        {
            string[] limit = new string[DeliveryCoordinates.Count];

            for (int i = 0; i < limit.Length; i++)
            {
                string deliver = string.Empty;
                int senderCount = SenderCoordiantes.Count;

                for (int j = 0; j < senderCount; j++)
                    deliver += $"x{j}{i} + ";

                deliver = deliver.Remove(deliver.Length - 2, 2);
                deliver += $" = {DeliveryCoordinates[i].Request}";

                limit[i] = deliver;
            }

            return limit;
        }

        public string[] DisplaySendersLimit()
        {
            string[] limit = new string[SenderCoordiantes.Count];

            for (int i = 0; i < limit.Length; i++)
            {
                string sender = string.Empty;
                int delivierCount = DeliveryCoordinates.Count;

                for (int j = 0; j < delivierCount; j++)
                    sender += $"x{i}{j} + ";

                sender = sender.Remove(sender.Length - 2, 2);
                sender += $" = {SenderCoordiantes[i].Limit}";

                limit[i] = sender;
            }

            return limit;
        }

        public bool CheckDemondAndSupply()
        {
            return DeliveryCoordinates.Sum(x => x.Request) == SenderCoordiantes.Sum(x => x.Limit);
        }

        public void Dispose()
        {
            this.DeliveryCoordinates.Clear();
        }
    }
}