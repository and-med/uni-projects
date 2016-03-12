using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Approximation
{
    public class IterationsMethod
    {
        private Func<double, double> _fun;

        public int NumOfIteration { get; set; }

        public IterationsMethod(Func<double, double> fun)
        {
            _fun = fun;
        }

        public double Evaluate(double l, double startedPoint, double eps)
        {
            double last = startedPoint;
            int iterationCount = 0;
            do
            {
                startedPoint = last;
                last = startedPoint + l * (_fun(startedPoint));
                ++iterationCount;
            } while (Math.Abs(last - startedPoint) > eps);
            NumOfIteration = iterationCount;
            return last;
        }
    }
}
