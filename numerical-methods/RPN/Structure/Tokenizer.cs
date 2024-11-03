using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RPN.Structure
{
    internal class Tokenizer
    {
        private string[] argNames;

        public Tokenizer(string[] names = null)
        {
            argNames = names;
        }

        public List<Token> Tokenize(string expression)
        {
            string[] tokens = OperatorHelper.SplitByOperators(expression, argNames);
            List<Token> result = new List<Token>();
            for (int i = 0; i < tokens.Length; ++i)
            {
                double res;
                if (Double.TryParse(tokens[i], out res))
                {
                    if (i != 0 && result[result.Count - 1].IsRightParenthesis())
                    {
                        result.Add(new Token("*", TokenType.Multiply));
                    }
                    result.Add(new Token(tokens[i], TokenType.Number));
                }
                else if (argNames != null && argNames.Contains(tokens[i]))
                {
                    if(i!=0 && (
                        result[result.Count - 1].IsNumberOrVariable() || result[result.Count - 1].IsRightParenthesis()))
                    {
                        result.Add(new Token("*", TokenType.Multiply));
                    }
                    result.Add(new Token(tokens[i], TokenType.Variable));
                }
                else
                {
                    Token op = OperatorHelper.ParseOperator(tokens[i]);
                    if(op.TokenType == TokenType.Minus && 
                        (i == 0 || result[result.Count - 1].IsLeftParenthesis() 
                        || result[result.Count - 1].IsFunction() || result[result.Count - 1].IsOperator()))
                    {
                        op.TokenType = TokenType.UnaryMinus;
                    }
                    if(op.IsLeftParenthesis() && i!=0 && result[result.Count - 1].IsNumberOrVariable())
                    {
                        result.Add(new Token("*", TokenType.Multiply));
                    }
                    result.Add(op);
                }
            }
            return result;
        }
    }
}
