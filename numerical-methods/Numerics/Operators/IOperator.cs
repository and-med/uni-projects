using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Operators
{
    public interface IOperator<ResultType, ArgumentType>
    {
        ResultType Evaluate(ArgumentType x);
    }
}
