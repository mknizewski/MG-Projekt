namespace MG_Projekt.BOL.Models
{
    public class Coordinate
    {
        public int X;
        public int Y;
        public bool IsSeen;

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.IsSeen = false;
        }

        public override string ToString()
        {
            return $"X = {X} Y = {Y}";
        }

        public static Coordinate GetInstance(int x, int y)
        {
            return new Coordinate(x, y);
        }
    }

    public class SenderCooridante : Coordinate
    {
        public int Limit;
        public int CurrentLimit;

        public SenderCooridante(
            int x,
            int y,
            int limit) : base(x, y)
        {
            this.Limit = limit;
            this.CurrentLimit = limit;
        }

        public override string ToString()
        {
            string baseString = base.ToString() + $" B = {Limit}";
            return baseString;
        }
    }

    public class DeliveryCoordinate : Coordinate
    {
        public int Request;
        public int CurrentRequest;

        public DeliveryCoordinate(
            int x,
            int y,
            int request) : base(x, y)
        {
            this.Request = request;
            this.CurrentRequest = request;
        }

        public override string ToString()
        {
            string baseString = base.ToString() + $" A = {Request}";
            return baseString;
        }
    }
}