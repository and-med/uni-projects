using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Integration
{
    public class ChebyshevQuadrature : IIntegrator
    {
        private static double[] t = new double[5] { -0.832497, -0.374541, 0, 0.374541, 0.832497 };

        private int n;

        public ChebyshevQuadrature(int initialN = 5)
        {
            n = initialN;
        }

        public double Integrate(Func<double, double> func, double a, double b, double epselon)
        {
            double previous = IntegrateWithPartition(func, a, b, n);
            n = 2 * n;
            double next = IntegrateWithPartition(func, a, b, n);
            while (Math.Abs(next - previous) > epselon)
            {
                n = 2 * n;
                previous = next;
                next = IntegrateWithPartition(func, a, b, n);
            }
            n /= 2;
            return next;
        }

        private double IntegrateWithPartition(Func<double, double> func, double a, double b, int n)
        {
            n = n / 5;
            double h = (b - a) / n;
            double res = 0;
            for(int i = 0; i < n; ++i)
            {
                res += IntegrateWithPartitionFive(func, a + i * h, a + (i + 1) * h);
            }
            return res*0.4;
        }

        private double IntegrateWithPartitionFive(Func<double, double> func, double a, double b)
        {
            double[] x = new double[5];
            for(int i = 0; i < 5; ++i)
            {
                x[i] = 0.5 * ((b - a) * t[i] + (b + a));
            }
            double res = 0;
            for(int i = 0; i < 5; ++i)
            {
                res += func(x[i]);
            }
            return res * ((b - a) / 2);
        }

        public int GetSegmentsCount()
        {
            return n;
        }
    }
}
