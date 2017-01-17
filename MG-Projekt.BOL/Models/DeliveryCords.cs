namespace MG_Projekt.BOL.Models
{
    public class DeliveryCords
    {
        public int X;
        public int Y;
        public string DispCords;

        public DeliveryCords(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.DispCords = $"X = {x} Y = {y}";
        }
    }
}