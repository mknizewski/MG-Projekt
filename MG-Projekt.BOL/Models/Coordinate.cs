namespace MG_Projekt.BOL.Models
{
    public class Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
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
}