using System;
using DirichletTask.Core.Abstraction.Functions;

namespace DirichletTask.Core.Functions
{
    public class LaguerreFunction : IFunction<int, double, double>
    {
        public double Calculate(int n, double t)
        {
            double res = 1;

            if(n == 1)
            {
                res = 1 - t;
            }
            else if (n > 1)
            {
                //Ln = ((2n - 1 - t) / n) * L(n - 1) - ((n - 1) / n) * L(n - 2)
                double L0 = 1;
                double L1 = (1 - t);
                for (int i = 2; i <= n; i++)
                {
                    double L2 = (L1 * (2 * i - 1 - t) / i) - L0 * (i - 1) / i;
                    L0 = L1;
                    L1 = L2;
                }
                res = L1;
            }

            return res;
        }
    }
}
