using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Operators
{
    public class LinearDifferentialSecondOrderOperator : IOperator<double, double>
    {
        public Func<double, double> U { get; set; }
        public Func<double, double> UPrime { get; set; }
        public Func<double, double> UPrimePrime { get; set; }
        public Func<double, double> P { get; set; }
        public Func<double, double> Q { get; set; }

        public double Evaluate(double x)
        {
            return (UPrimePrime ?? (arg => 0))(x) 
                + (P ?? (arg => 0))(x) * (UPrime ?? (arg => 0))(x) 
                + (Q ?? (arg => 0))(x) * (U ?? (arg => 0))(x);
        }
    }
}
