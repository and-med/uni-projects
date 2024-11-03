using System;
using DirichletTask.Core.Abstraction.Functions;
using DirichletTask.Core.Abstraction.Parameters;

namespace DirichletTask.Core.Functions
{
    public class EFundSolution : IFunction<double, double, double>
    {
        IReadOnlyParameterProvider<double> _parameters;

        public EFundSolution(IReadOnlyParameterProvider<double> parameters)
        {
            _parameters = parameters;
        }

        public double Calculate(double x, double t)
        {
            var omega = _parameters.GetValue("alpha") + _parameters.GetValue("beta");
            return Math.Sqrt(omega) * (Math.Exp(-((Math.Abs(x) * Math.Abs(x)) / (4 * t)))) / (2 * Math.Pow(Math.Sqrt(Math.PI * t), 3.0));
        }
    }
}
