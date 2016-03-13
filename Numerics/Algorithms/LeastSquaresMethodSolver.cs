using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics.Integration;
using Numerics.Models;
using Numerics.Operators;
using Numerics.Operators.Factory;
using Numerics.Structures;

namespace Numerics.Algorithms
{
    public class LeastSquaresMethodSolver
    {
        public LeastSquaresDataModel Model { get; set; }

        public Matrix<double> C { get; set; }

        public Vector<double> B { get; set; }

        public LeastSquaresMethodSolver(LeastSquaresDataModel model)
        {
            Model = model;
        }

        public void BuildMatrix()
        {
            C = new Matrix<double>(Model.BasisWronskian.M, Model.BasisWronskian.M);
            B = new Vector<double>(Model.BasisWronskian.M);
            int n = Model.BasisWronskian.M;
            for(int i = 0; i < n; ++i)
            {
                IOperator<double, double> afi = GetAByBasisFunction(i);
                for(int k = 0; k < i + 1; ++k)
                {
                    if (i != k)
                    {
                        IOperator<double, double> afk = GetAByBasisFunction(k);
                        C[i, k] = C[k, i] = Model.Integrator.Integrate(
                            x => afi.Evaluate(x)*afk.Evaluate(x), Model.IntervalStart, Model.IntervalEnd, Model.Epselon);
                    }
                    else
                    {
                        C[i, i] = Model.Integrator.Integrate(
                            x => afi.Evaluate(x) * afi.Evaluate(x), Model.IntervalStart, Model.IntervalEnd, Model.Epselon);
                    }
                }
                B[i] = Model.Integrator.Integrate(
                    x => Model.F(x) * afi.Evaluate(x), Model.IntervalStart, Model.IntervalEnd, Model.Epselon);
            }
        }

        private IOperator<double, double> GetAByBasisFunction(int i)
        {
            Model.AFactory.U = Model.BasisWronskian[0, i];
            Model.AFactory.UPrime = Model.BasisWronskian[1, i];
            Model.AFactory.UPrimePrime = Model.BasisWronskian[2, i];

            return Model.AFactory.GetOperator();
        }
    }
}
