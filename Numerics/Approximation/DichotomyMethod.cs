using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Approximation
{
    public class DichotomyMethod
    {        
        private Func<double, double> _fun;

        public int NumOfIteration { get; set; }

        public DichotomyMethod(Func<double, double> fun)
        {
            _fun = fun;
        }

        public double Evaluate(double a, double b, double eps)
        {
            while (Math.Abs(b - a) > eps)
            {
                double c = (a + b)/2.0;
                if(_fun(a)*_fun(c) <= 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
                NumOfIteration++;
            }
            return b;
        }

    }
}
