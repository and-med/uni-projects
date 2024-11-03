using DirichletTask.Core.Abstraction.Parameters;
using DirichletTask.Core.Abstraction.Series;
using DirichletTask.Core.Cache;
using System;

namespace DirichletTask.Core.Series
{
    public class GTildaSeries : ISingleValuedSeriesCalculator<int, double, double>
    {
        private IReadOnlyParameterProvider<double> _parameters;

        public GTildaSeries(IReadOnlyParameterProvider<double> parameters)
        {
            _parameters = parameters;
        }

        public double Calculate(int n, double x)
        {
            double result = 0;
            double alpha = _parameters.GetValue("alpha");
            double beta = _parameters.GetValue("beta");
            double hama = alpha + (beta / 2.0);
            double omega = alpha + beta;

            if (n == 0)
            {
                result = Math.Sqrt(omega) * (Math.Exp(-Math.Sqrt(hama) * Math.Abs(x))) / (4.0 * Math.PI * Math.Sqrt(x));
            }
            else if (n == 1)
            {
                result = Math.Sqrt(omega) * ((Math.Exp(-Math.Sqrt(hama) * Math.Abs(x))) / (8.0 * Math.PI * Math.Sqrt(hama)));
            }
            else
            {
                result = (-2.0 / (2.0 * alpha + beta)) * ((Math.Abs(x) * Math.Abs(x) / 4.0) * Calculate(n - 2, x) + (n - 1.5) * Calculate(n - 1, x));
            }
            return result;
        }
    }
}
