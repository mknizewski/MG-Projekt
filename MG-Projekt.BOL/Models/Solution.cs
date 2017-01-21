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
            double function = 0.0;
            int senders = C.GetLength(0);
            int delivers = C.GetLength(1);

            for (int i = 0; i < senders; i++)
            {
                for (int j = 0; j < delivers; j++)
                {
                    function += C[i, j] * X[i, j];
                }
            }

            return function; 
        }
    }
}