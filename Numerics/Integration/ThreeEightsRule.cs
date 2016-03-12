using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Integration
{
    public class ThreeEightsRule : IIntegrator
    {
        private int n;

        public ThreeEightsRule(int initialN = 4)
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
            int m = n / 3;
            double h = (b - a) / n;
            double res = 0;
            for (int i = 0; i < n; i += 3)
            {
                res += func(a + i * h) + 3 * func(a + (i + 1) * h) + 3 * func(a + (i + 2) * h) + func(a + (i + 3) * h);
            }
            res *= ((3.0 / 8.0) * h);
            return res;
        }
    }
}
