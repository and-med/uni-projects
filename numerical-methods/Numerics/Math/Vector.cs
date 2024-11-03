using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Structures
{
    public class Vector<T> : Matrix<T>
    {
        public Vector(int n) : base(n, 1) { }

        public T this[int i]
        {
            get
            {
                return this[i, 0];
            }
            set
            {
                this[i, 0] = value;
            }
        }
    }
}
