namespace MG_Projekt.BOL.Models
{
    public class Solution
    {
        public double[] X { get; set; }
        public double[] C { get; set; }

        public double TargetFunction()
        {
            return double.Epsilon;
        }
    }
}
