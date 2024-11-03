using System;
using DirichletTask.Core.Abstraction.Functions;
using DirichletTask.Core.Abstraction.Series;
using DirichletTask.Core.Abstraction.Parameters;

namespace DirichletTask.Core.Functions
{
    public class EFundSolutionNumerical : IFunction<double, double, double>
    {
        ISingleValuedSeriesCalculator<int, double, double> _eFund;
        IFunction<int, double, double> _laguerreFunc;
        IReadOnlyParameterProvider<double> _parameterProvider;

        public int N { get; set; }

        public EFundSolutionNumerical(ISingleValuedSeriesCalculator<int, double, double> eFund,
            IFunction<int, double, double> laguerreFunc, IReadOnlyParameterProvider<double> parameterProvider)
        {
            _eFund = eFund;
            _laguerreFunc = laguerreFunc;
            _parameterProvider = parameterProvider;
        }

        public double Calculate(double x, double t)
        {
            double omega = _parameterProvider.GetValue("omega");
            double beta = _parameterProvider.GetValue("beta");
            double res = 0;

            for (int i = 0; i <= N; ++i)
            {
                res += _eFund.Calculate(i, x) * Math.Sqrt(omega) 
                    * _laguerreFunc.Calculate(i, omega * t) * Math.Exp(- (beta / 2.0) * t);
            }

            return res;
        }
    }
}
