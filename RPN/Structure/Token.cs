using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Structure
{
    internal class Token
    {
        private TokenType token;
        private string value;

        public Token(string val, TokenType tokenType)
        {
            value = val;
            token = tokenType;
        }

        public TokenType TokenType
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }
        public string Value
        {
            get
            {
                return value;
            }
        }

        public bool IsNumber()
        {
            return TokenType == TokenType.Number;
        }

        public bool IsNumberOrVariable()
        {
            return TokenType == TokenType.Number || TokenType == TokenType.Variable;
        }

        public bool IsOperator()
        {
            return TokenType == TokenType.Plus
                || TokenType == TokenType.Minus
                || TokenType == TokenType.UnaryMinus
                || TokenType == TokenType.Multiply
                || TokenType == TokenType.Divide
                || TokenType == TokenType.Power;
        }

        public bool IsFunction()
        {
            return TokenType == TokenType.Sine
                || TokenType == TokenType.Cosine
                || TokenType == TokenType.Tangent
                || TokenType == TokenType.Cotangent
                || TokenType == TokenType.Sqrt
                || TokenType == TokenType.Abs
                || TokenType == TokenType.Ln
                || TokenType == TokenType.Lg
                || TokenType == TokenType.Exponent;
        }

        public bool IsLeftParenthesis()
        {
            return TokenType == TokenType.LeftParenthesis;
        }

        public bool IsRightParenthesis()
        {
            return TokenType == TokenType.RightPrenthesis;
        }

        public bool IsUnaryOperaor()
        {
            return TokenType == TokenType.Sine
                || TokenType == TokenType.Cosine
                || TokenType == TokenType.Tangent
                || TokenType == TokenType.Cotangent
                || TokenType == TokenType.Sqrt
                || TokenType == TokenType.Abs
                || TokenType == TokenType.Ln
                || TokenType == TokenType.Lg
                || TokenType == TokenType.Exponent
                || TokenType == TokenType.UnaryMinus;
        }

        public bool IsBinaryOperator()
        {
            return TokenType == TokenType.Plus
                || TokenType == TokenType.Minus
                || TokenType == TokenType.Multiply
                || TokenType == TokenType.Divide
                || TokenType == TokenType.Power;
        }
    }
}
