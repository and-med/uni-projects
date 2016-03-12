using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Integration
{
    public class GaussianIntegration : IIntegrator
    {
        private static double[] t = new double[5] { 0, -0.5384693101056831, 0.5384693101056831, -0.9061798459386640, 0.9061798459386640 };
        private static double[] c = new double[5] { 0.5688888888888889, 0.4786286704993665, 0.4786286704993665, 0.2369268850561891, 0.2369268850561891 };

        private int n;

        public GaussianIntegration(int initialN)
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
            return res;
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
                res += c[i]*func(x[i]);
            }
            return res * ((b - a) / 2);
        }

        public int GetSegmentsCount()
        {
            return n;
        }
    }
}
