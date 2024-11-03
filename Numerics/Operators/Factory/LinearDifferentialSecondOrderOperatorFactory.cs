using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Operators.Factory
{
    public class LinearDifferentialSecondOrderOperatorFactory : ILinearDifferentialSecondOrderOperatorFactory<double, double>
    {
        public Func<double, double> U { get; set; }

        public Func<double, double> UPrime { get; set; }

        public Func<double, double> UPrimePrime { get; set; }

        private Func<double, double> p;

        private Func<double, double> q;

        public LinearDifferentialSecondOrderOperatorFactory(Func<double, double> p, Func<double, double> q)
        {
            this.p = p;
            this.q = q;
        }

        public IOperator<double, double> GetOperator()
        {
            var result = new LinearDifferentialSecondOrderOperator();
            result.P = p;
            result.Q = q;
            result.U = U;
            result.UPrime = UPrime;
            result.UPrimePrime = UPrimePrime;
            return result;
        }
    }
}
