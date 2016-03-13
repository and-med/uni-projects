using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Structures
{
    public class Matrix<T> : ICloneable
    {
        private T[,] buffer;

        public int N { get; set; }
        public int M { get; set; }

        private Matrix(Matrix<T> matrix)
        {
            N = matrix.N;
            M = matrix.M;
            buffer = (T[,])matrix.buffer.Clone();
        }

        public Matrix(int n, int m)
        {
            N = n;
            M = m;
            buffer = new T[n, m];
        }

        public T this[int i, int j]
        {
            get
            {
                return buffer[i, j];
            }
            set
            {
                buffer[i, j] = value;
            }
        }

        public object Clone()
        {
            return new Matrix<T>(this);
        }
    }
}
