namespace MG_Projekt.BOL.Models
{
    public class Solution
    {
        public double[,] X { get; set; }
        public double[,] C { get; set; }

        public Solution(int sender, int deliver)
        {
            this.X = new double[sender, deliver];
            this.C = new double[sender, deliver];
        }

        public double TargetFunction()
        {
            return double.Epsilon;
        }
    }
}