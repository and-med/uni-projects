using System;
using DirichletTask.Core.Abstraction.Series;
using DirichletTask.Core.Abstraction.Parameters;
using DirichletTask.Core.Abstraction.Cache;
using DirichletTask.Core.Cache;

namespace DirichletTask.Core.Series
{
    public class ETildaSeries : ISingleValuedSeriesCalculator<int, double, double>
    {
        IReadOnlyParameterProvider<double> _parameters;
        ISingleValuedSeriesCalculator<int, double, double> _gSeries;
        ISingleValuedSeriesCalculator<int, int, double> _param;

        public ETildaSeries(IReadOnlyParameterProvider<double> parameters,
            ISingleValuedSeriesCalculator<int, double, double> gSeries,
            ISingleValuedSeriesCalculator<int, int, double> param)
        {
            _parameters = parameters;
            _gSeries = gSeries;
            _param = param;
        }

        public double Calculate(int k, double x)
        {
            double sum = 0;

            for (int i = 0; i <= k; ++i)
            {
                sum += _param.Calculate(k, i) * _gSeries.Calculate(i, x);
            }

            return sum;
        }
    }
}
