using System;

namespace Numerics.Approximation
{
    public class CombinedSecantAndNewtonMethod
    {
        private Func<double, double> _fun;
        private Func<double, double> _funDerivative1st;

        public int NumOfIteration { get; set; }

        public CombinedSecantAndNewtonMethod(Func<double, double> fun, Func<double, double> funDerivative1St)
        {
            _fun = fun;
            _funDerivative1st = funDerivative1St;
        }

        public double Evaluate(double a, double b, double eps)
        {
            double x1 = a;
            double x2 = b;
            double startedPointUp = b;
            double startedPointDown = a;
            double lastUp = startedPointUp;
            double lastDown = startedPointDown;
            do
            {
                startedPointDown = lastDown;
                startedPointUp = lastUp;
                double fSPD = _fun(startedPointDown);
                double fSPU = _fun(startedPointUp);
                lastDown = startedPointDown - ((startedPointDown - startedPointUp) * fSPD / (fSPD - fSPU));
                lastUp = startedPointUp - fSPU / _funDerivative1st(startedPointUp);
                ++NumOfIteration;
            } while (Math.Abs(lastUp - lastDown) > eps);
            return lastUp;
        }
    }
}
