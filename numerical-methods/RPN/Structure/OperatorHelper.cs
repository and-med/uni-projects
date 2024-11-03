using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RPN.Structure
{
    internal static class OperatorHelper
    {
        public static readonly string[] Operators = { "+", "-", "*", "/", "sin", "cos", "tan", "cotan", "sqrt", "abs",
                                          "ln", "lg", "^", "exp"};
        public static readonly string[] UnaryOperators = { "sin", "cos", "tan", "cotan", "ln", "lg", "exp" };
        public static readonly string[] BinaryOperators = { "+", "-", "*", "/", "^" };
        public static readonly string LeftParenthesis = "(";
        public static readonly string RightParethesis = ")";
        public static readonly string DoubleRegex = @"(?<number>\d+(\.\d+)?)";

        public static Token ParseOperator(string op)
        {
            int index = Array.IndexOf(Operators, op);

            if(index == -1)
            {
                if(op == LeftParenthesis)
                {
                    return new Token(op, TokenType.LeftParenthesis);
                }
                else if(op == RightParethesis)
                {
                    return new Token(op, TokenType.RightPrenthesis);
                }
                throw new ArgumentException("Argumet is not an operator", "op");
            }

            return new Token(op, (TokenType)index);
        }

        public static string[] SplitByOperators(string expression, string[] argNames)
        {
            StringBuilder pattern = new StringBuilder("(?<token>(");
            if (argNames != null)
            {
                foreach (var name in argNames)
                {
                    pattern.Append(name + '|');
                }
            }
            foreach (var op in OperatorHelper.Operators)
            {
                if (op == "*" || op == "^" || op == "+")
                {
                    pattern.Append("\\" + op + '|');
                }
                else
                {
                    pattern.Append(op + '|');
                }
            }
            pattern.Append('\\' + OperatorHelper.LeftParenthesis + '|');
            pattern.Append('\\' + OperatorHelper.RightParethesis);
            pattern.Append("))");

            expression = Regex.Replace(expression, pattern.ToString(), " ${token} ");
            expression = Regex.Replace(expression, DoubleRegex, " ${number} ");

            return expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int GetPiority(TokenType tokenType)
        {
            switch(tokenType)
            {
                case TokenType.LeftParenthesis:
                case TokenType.RightPrenthesis: return 1;
                case TokenType.Plus:
                case TokenType.Minus: return 2;
                case TokenType.Multiply:
                case TokenType.Divide: return 3;
                case TokenType.UnaryMinus: return 4;
                case TokenType.Power: return 5;
            }
            throw new ArgumentException("Not expected token");
        }

        public static int ComparePriorities(Token token1, Token token2)
        {
            return GetPiority(token1.TokenType).CompareTo(GetPiority(token2.TokenType));
        }

        public static bool IsRightAssociative(Token token)
        {
            return token.TokenType == TokenType.Power;
        }

        public static bool IsLeftAssociative(Token token)
        {
            return token.TokenType != TokenType.Power;
        }
    }
}
