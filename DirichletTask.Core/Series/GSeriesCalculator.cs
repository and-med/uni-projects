using System;
using DirichletTask.Core.Abstraction.Parameters;
using DirichletTask.Core.Cache;

namespace DirichletTask.Core.Series
{
    public class GSeriesCalculator : ArrCachedSingleSeriesCalculator
    {
        private IReadOnlyParameterProvider<double> _parameters;

        public GSeriesCalculator(IReadOnlyParameterProvider<double> parameters)
        {
            _parameters = parameters;
        }

        public override double Calculate(int n, double x)
        {
            double result = 0;
            double alpha = _parameters.GetValue("alpha");
            double beta = _parameters.GetValue("beta");
            double hama = alpha + (beta / 2.0);
            double omega = alpha + beta;

            if (n == 0)
            {
                result = omega * Math.Sqrt(omega) * (Math.Exp(-Math.Sqrt(hama) * Math.Abs(x))) / (8.0 * Math.PI * Math.Sqrt(hama));
            }
            else if (n == 1)
            {
                result = omega * Math.Sqrt(omega) * ((Math.Exp(-Math.Sqrt(hama) * Math.Abs(x))) / (16.0 * Math.PI * hama * Math.Sqrt(hama))) * (Math.Sqrt(hama) * Math.Abs(x) + 1.0);
            }
            else
            {
                result = (-2.0 / (2.0 * alpha + beta)) * ((Math.Abs(x) * Math.Abs(x) / 4.0) * Calculate(n - 2, x) + (n - 0.5) * Calculate(n - 1, x));
            }
            return result;
        }
    }
}
