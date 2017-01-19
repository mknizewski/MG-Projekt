namespace MG_Projekt.BOL.Models
{
    public class Function
    {
        // Koszt
        public double[] C { get; set; }

        // Szukane
        public double[] X { get; set; }

        public Function(double[] c)
        {
            this.C = c.Clone() as double[];
        }

        public double CalculateFunction()
        {
            return double.Epsilon;
        }

        public override string ToString()
        {
            string displayFunctionOfTarget = "K(xij) = ";

            for (int i = 0; i < C.Length; i++)
            {
                if (i == C.Length - 1)
                    displayFunctionOfTarget += $"{C[i]}x1{i + 1}";
                else
                    displayFunctionOfTarget += $"{C[i]}x1{i + 1} +";
            }

            return displayFunctionOfTarget;
        }
    }
}