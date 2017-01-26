using System;

namespace MG_Projekt.BOL.Models
{
    public class Solution : IEquatable<Solution>
    {
        public double[,] X { get; set; }
        public double[,] C { get; set; }
        public int[] Vector { get; set; }
        public bool[] IsSeen { get; set; }

        private int _sender;
        private int _deliver;

        public Solution(int sender, int deliver)
        {
            this._sender = sender;
            this._deliver = deliver;
            this.IsSeen = new bool[sender * deliver];
            this.Vector = new int[sender * deliver];
            this.X = new double[sender, deliver];
            this.C = new double[sender, deliver];
        }

        public bool AllPositionIsSeen()
        {
            for (int i = 0; i < IsSeen.Length; i++)
            {
                if (!IsSeen[i])
                    return false;
            }

            return true;
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

        public Solution SwitchRandomElements()
        {
            return null;
        }

        private void Initialization()
        {

        }

        private void Mutation()
        {
            
        }

        private void Inversion()
        {

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