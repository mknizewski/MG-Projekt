namespace MG_Projekt.BOL.Models
{
    public class Function
    {
        public double[] C { get; set; }
        public double[] X { get; set; }
        
        public double CalculateFunction()
        {
            return double.Epsilon;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}