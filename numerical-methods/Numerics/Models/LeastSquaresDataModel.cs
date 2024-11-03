using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics.Integration;
using Numerics.Operators.Factory;
using Numerics.Structures;

namespace Numerics.Models
{
    public class LeastSquaresDataModel
    {
        public Matrix<Func<double, double>> BasisWronskian { get; set; }

        public Func<double, double> F { get; set; }

        public IIntegrator Integrator { get; set; }

        public ILinearDifferentialSecondOrderOperatorFactory<double, double> AFactory { get; set; }

        public double IntervalStart { get; set; }

        public double IntervalEnd { get; set; }

        public double Epselon { get; set; }
    }
}
