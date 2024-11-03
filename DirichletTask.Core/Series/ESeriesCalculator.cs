using DirichletTask.Core.Abstraction.Cache;
using DirichletTask.Core.Abstraction.Parameters;
using DirichletTask.Core.Abstraction.Series;
using DirichletTask.Core.Cache;
using System;

namespace DirichletTask.Core.Series
{
    public class ESeriesCalculator : ArrCachedSingleSeriesCalculator
    {
        private ICachedSingleSeriesCalculator<int, double, double> _gSeries;
        private ISingleValuedSeriesCalculator<int, int, double> _param;
        private IReadOnlyParameterProvider<double> _parameters;

        public ESeriesCalculator(ICachedSingleSeriesCalculator<int, double, double> gSeries,
            ISingleValuedSeriesCalculator<int, int, double> param,
            IReadOnlyParameterProvider<double> parameters)
        {
            _gSeries = gSeries;
            _param = param;
            _parameters = parameters;
        }

        public override double Calculate(int n, double x)
        {
            double sum = 0;
            double result = 0;
            double alpha = _parameters.GetValue("alpha");
            double beta = _parameters.GetValue("beta");
            double hama = alpha + (beta / 2.0);
            double omega = alpha + beta;

            for (int k = 0; k <= n - 1; ++k)
            {
                sum += _param.Calculate(n - 1, k) * _gSeries.Calculate(k, x); 
            }

            if (n == 0)
            {
                result = Math.Sqrt(omega) * (Math.Exp(-Math.Sqrt(hama) * Math.Abs(x))) / (4.0 * Math.PI * Math.Abs(x));
            }
            else
            {
                if (n == 1)
                {
                    result = -(1 / (double)n) * sum;
                }
                else
                {
                    result = (1.0 - 1.0 / (double)n) * Calculate(n - 1, x) - (1 / (double)n) * sum;
                }
            }

            return result;
        }

        public override void EmptyCache()
        {
            _gSeries.EmptyCache();
            base.EmptyCache();
        }

        public override void SetCacheSize(int size)
        {
            _gSeries.SetCacheSize(size);
            base.SetCacheSize(size);
        }
    }
}
