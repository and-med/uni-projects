using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Structure
{
    internal enum TokenType
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Sine,
        Cosine,
        Tangent,
        Cotangent,
        Sqrt,
        Abs,
        Ln,
        Lg,
        Power,
        Exponent,
        LeftParenthesis,
        RightPrenthesis,
        Number,
        Variable,
        UnaryMinus
    }
}
