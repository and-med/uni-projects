using DirichletTask.Core.Abstraction.Series;
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


        // (-1)^k*sigma^k*n!
        // -----------------
        //   (k!)^2*(n-k)!
        public double Calculate(int n, int x)
        {
            double sigma = _parameters.GetValue("sigma");

            double product = 1;
            for (int i = 1; i <= x; ++i)
            {
                product *= (sigma * (n - x + i)) / (i * i);
            }

            int coef = ((-2) * (x % 2) + 1);
            return coef * product;
        }
    }
}
