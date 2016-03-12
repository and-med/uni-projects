using System;

namespace Numerics.Approximation
{
    public class NewtonMethod
    {
        private Func<double, double> _fun;
        private Func<double, double> _funDerivative1st;
        private Func<double, double> _funDerivative2nd;

        public int NumOfIteration { get; set; }

        public NewtonMethod(Func<double, double> fun, Func<double, double> funDerivative1St, Func<double, double> funDerivative2Nd)
        {
            _fun = fun;
            _funDerivative1st = funDerivative1St;
            _funDerivative2nd = funDerivative2Nd;
        }

        public double Evaluate(double a, double b, double eps)
        {
            double c;
            if (_fun(a) * _funDerivative2nd(a) > 0)
            {
                c = a - _fun(a) / _funDerivative1st(a);
            }
            else
            {
                c = b - _fun(b) / _funDerivative1st(b);
            }

            do
            {
                c = c - _fun(c) / _funDerivative1st(c);
                NumOfIteration++;
            }
            while (Math.Abs(_fun(c)) >= eps);
            return c;
           
        }
    }
}
