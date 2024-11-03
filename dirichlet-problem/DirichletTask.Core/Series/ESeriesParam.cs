﻿using DirichletTask.Core.Abstraction.Series;
using DirichletTask.Core.Abstraction.Parameters;

namespace DirichletTask.Core.Series
{
    public class ESeriesParam : ISingleValuedSeriesCalculator<int, int, double>
    {
        private IReadOnlyParameterProvider<double> _parameters;

        public ESeriesParam(IReadOnlyParameterProvider<double> parameters)
        {
            _parameters = parameters;
        }


        // (-1)^k*omega^k*n!
        // -----------------
        //   (k!)^2*(n-k)!
        public double Calculate(int n, int k)
        {
            double alpha = _parameters.GetValue("alpha");
            double beta = _parameters.GetValue("beta");
            double omega = alpha + beta;

            double product = 1;
            for (int i = 1; i <= k; ++i)
            {
                product *= (omega * (n - k + i)) / (i * i);
            }

            int coef = ((-2) * (k % 2) + 1);
            return coef * product;
        }
    }
}
