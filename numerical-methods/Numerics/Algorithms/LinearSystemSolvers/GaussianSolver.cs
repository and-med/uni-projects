using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics.Structures;

namespace Numerics.Algorithms.LinearSystemSolvers
{
    public class GaussianSolver : ILinearSystemSolver
    {
        public Matrix<double> A { get; set; }

        public Vector<double> B { get; set; }

        public Vector<double> X { get; set; }

        private Matrix<double> T;

        private int n;

        public void Solve()
        {
            PerformCheck();
            Initialize();

            for (int k = 0; k < n; ++k)
            {
                double value = T[k, k];
                int maxRow = k;
                for (int i = k + 1; i < n; ++i)
                {
                    if (T[i, k] > value)
                    {
                        value = T[i, k];
                        maxRow = i;
                    }
                }
                SwapRows(k, maxRow);
                X[k] = X[k] / value;
                for (int i = k; i < n; ++i)
                {
                    T[k, i] = T[k, i] / value;
                }
                for (int i = k + 1; i < n; ++i)
                {
                    value = T[i, k];
                    X[i] = X[i] - value * X[k];
                    for (int j = k; j < n; ++j)
                    {
                        T[i, j] = T[i, j] - value * T[k, j];
                    }
                }
            }

            for (int k = n - 1; k >= 0; --k)
            {
                for (int i = 0; i < k; ++i)
                {
                    X[i] = X[i] - T[i, k] * X[k];
                    T[i, k] = 0;
                }
            }
        }

        private void PerformCheck()
        {
            if (A.M != A.N)
            {
                throw new ArgumentException("Matrix must be square!", "A");
            }
            if (A.N != B.N)
            {
                throw new ArgumentException("Matrix size and vector size must be equal", "B");
            }
        }
        private void Initialize()
        {
            n = A.N;
            X = new Vector<double>(n);
            T = (Matrix<double>)A.Clone();
            CopyBValue();
        }

        private void CopyBValue()
        {
            for(int i = 0; i < n; ++i)
            {
                X[i] = B[i];
            }
        }

        public void SwapRows(int k, int j)
        {
            double temp = X[k];
            X[k] = X[j];
            X[j] = temp;
            for (int i = k; i < n; ++i)
            {
                temp = T[k, i];
                T[k, i] = T[j, i];
                T[j, i] = temp;
            }
        }
    }
}
