using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics.Structures;

namespace Numerics.Models
{
    public class LinearLeastSquaresDataModel
    {
        public Matrix<double> A { get; set; }

        public Vector<double> B { get; set; }

        public int N { get; set; }

        public int M { get; set; }
    }
}
