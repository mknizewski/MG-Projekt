using System;

namespace MG_Projekt.BOL.Models
{
    public class Costs
    {
        public DeliveryCoordinate DeliveryCoordinate { get; set; }
        public SenderCooridante SenderCoordinate { get; set; }
        public double Cost { get; set; }
        public double DrivenKilometers { get; set; }

        private const int Kilometers = 10;
        private const int RoundedToPrice = 2;

        public Costs(
            DeliveryCoordinate deliveryCoordainate, 
            SenderCooridante sednerCoordinate, 
            double petrolUsage, 
            double petrolPrice)
        {
            this.DeliveryCoordinate = deliveryCoordainate;
            this.SenderCoordinate = sednerCoordinate;

            CalculateDrivenKilometers(sednerCoordinate);
            CalculateCost(petrolUsage, petrolPrice);
        }

        private void CalculateDrivenKilometers(Coordinate senderCoordinate)
        {
            int senderX = senderCoordinate.X;
            int senderY = senderCoordinate.Y;
            int deliveryX = DeliveryCoordinate.X;
            int deliveryY = DeliveryCoordinate.Y;

            DrivenKilometers = Math.Sqrt(Math.Pow(deliveryX - senderX, 2.0) + Math.Pow(deliveryY - senderY, 2.0)) * Kilometers;
        }

        private void CalculateCost(double petrolUsage, double petrolPrice)
        {
            Cost = (DrivenKilometers / petrolUsage) * petrolPrice;
            Cost = Math.Round(Cost, RoundedToPrice);
        }
    }
}
