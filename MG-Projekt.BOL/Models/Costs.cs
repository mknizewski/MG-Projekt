using System;

namespace MG_Projekt.BOL.Models
{
    public class Costs
    {
        public Coordinate DeliveryCoordinate { get; set; }
        public double Cost { get; set; }
        public double DrivenKilometers { get; set; }

        public Costs(
            Coordinate deliveryCoordainate, 
            Coordinate sednerCoordinate, 
            double petrolUsage, 
            double petrolPrice)
        {
            this.DeliveryCoordinate = deliveryCoordainate;
            CalculateDrivenKilometers(sednerCoordinate);
            CalculateCost(petrolUsage, petrolPrice);
        }

        private void CalculateDrivenKilometers(Coordinate senderCoordinate)
        {
            int senderX = senderCoordinate.X;
            int senderY = senderCoordinate.Y;
            int deliveryX = DeliveryCoordinate.X;
            int deliveryY = DeliveryCoordinate.Y;

            DrivenKilometers = Math.Sqrt(Math.Pow(deliveryX - senderX, 2.0) + Math.Pow(deliveryY - senderX, 2.0)) * 10;
        }

        private void CalculateCost(double petrolUsage, double petrolPrice)
        {
            Cost = (DrivenKilometers / petrolUsage) * petrolPrice;
            Cost = Math.Round(Cost, 2);
        }
    }
}
