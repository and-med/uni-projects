using System;

namespace Numerics.Approximation
{
    public class SecantMethod
    {
        private Func<double, double> _fun;

        public int NumOfIteration { get; set; }

        public SecantMethod(Func<double, double> fun)
        {
            _fun = fun;
        }

        public double Evaluate(double a, double b, double eps)
        {
            while (Math.Abs(b - a) > eps)
            {
                a = b - (b - a) * _fun(b) / (_fun(b) - _fun(a));
                b = a - (a - b) * _fun(a) / (_fun(a) - _fun(b));
                NumOfIteration++;
            }
            return b;
        }
    }
}
