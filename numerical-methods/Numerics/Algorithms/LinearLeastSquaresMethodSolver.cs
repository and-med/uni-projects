using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics.Models;
using Numerics.Structures;

namespace Numerics.Algorithms
{
    public class LinearLeastSquaresMethodSolver
    {
        public LinearLeastSquaresDataModel Model { get; set; }

        public Matrix<double> A { get; set; }

        public Vector<double> B { get; set; }

        public LinearLeastSquaresMethodSolver(LinearLeastSquaresDataModel model)
        {
            Model = model;
        }

        public void BuildMatrix()
        {
            A = new Matrix<double>(Model.N, Model.N);
            B = new Vector<double>(Model.N);

            for (int k = 0; k < Model.N; k++)
            {
                for (int i = 0; i < Model.M; i++)
                {
                    for (int j = 0; j < Model.N; j++)
                    {
                        A[k,j] += Model.A[i, j] * Model.A[i, k];
                    }
                    B[k] += Model.B[i] * Model.A[i, k];
                }
            }
        }
    }
}
