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
            double sigma = _parameters.GetValue("sigma");

            if (n == 0)
            {
                result = (Math.Exp(-Math.Sqrt(sigma) * Math.Abs(x))) / (8.0 * Math.PI * Math.Sqrt(sigma));
            }
            else if (n == 1)
            {
                result = ((Math.Exp(-Math.Sqrt(sigma) * Math.Abs(x))) / (16.0 * Math.PI * sigma * Math.Sqrt(sigma))) * (Math.Sqrt(sigma) * Math.Abs(x) + 1.0);
            }
            else
            {
                result = (Math.Abs(x) * Math.Abs(x) / 4.0) * Calculate(n - 2, x) + (n - 0.5) * Calculate(n - 1, x);
            }
            return result;
        }
    }
}
