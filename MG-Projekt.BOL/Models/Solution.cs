using System;

namespace MG_Projekt.BOL.Models
{
    public class Solution : IEquatable<Solution>
    {
        public double[,] X { get; set; }
        public double[,] C { get; set; }

        private int _sender;
        private int _deliver;

        public Solution(int sender, int deliver)
        {
            this._sender = sender;
            this._deliver = deliver;
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

        public bool Equals(Solution other)
        {
            for (int i = 0; i < _sender; i++)
            {
                for (int j = 0; j < _deliver; j++)
                {
                    if (this.X[i, j] != other.X[i, j])
                        return false;
                }
            }

            return true;
        }
    }
}