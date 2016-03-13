using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Operators.Factory
{
    public interface ILinearDifferentialSecondOrderOperatorFactory<ResultType, ArgumentType>
        : IOperatorFactory<IOperator<ResultType, ArgumentType>>
    {
        Func<ResultType, ArgumentType> U { get; set; }
        Func<ResultType, ArgumentType> UPrime { get; set; }
        Func<ResultType, ArgumentType> UPrimePrime { get; set; }
    }
}
