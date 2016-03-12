using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Integration
{
    public class RectanglesMethod: IIntegrator
    {
        private int n;

        public RectanglesMethod(int initialN = 4)
        {
            n = initialN;
        }

        public double Integrate(Func<double, double> func, double a, double b, double epselon)
        {
            double previous = IntegrateWithPartition(func, a, b, n);
            n = 2 * n;
            double next = IntegrateWithPartition(func, a, b, n);
            while(Math.Abs(next - previous) > epselon)
            {
                n = 2 * n;
                previous = next;
                next = IntegrateWithPartition(func, a, b, n);
            }
            n /= 2;
            return next;
        }

        public int GetSegmentsCount()
        {
            return n;
        }

        private double IntegrateWithPartition(Func<double, double> func, double a, double b, int n)
        {
            double h = (b - a) / n;
            double res = 0;
            for(int i = 1; i <= n; ++i)
            {
                res += func(a + ((2 * i - 1) * h) / 2);
            }
            res *= h;
            return res;
        }
    }
}
