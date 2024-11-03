using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics.Structures;

namespace Numerics.Algorithms.LinearSystemSolvers
{
    public interface ILinearSystemSolver
    {
        Matrix<double> A { get; set; }
        
        Vector<double> B { get; set; }

        Vector<Double> X { get; set; }

        void Solve();
    }
}
