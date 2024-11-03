using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Structure
{
    internal class RPNParser
    {
        private Tokenizer tokenizer;

        public RPNParser(Tokenizer _tokenizer)
        {
            tokenizer = _tokenizer;
        }

        public List<Token> Parse(string expression)
        {
            List<Token> tokens = tokenizer.Tokenize(expression);
            List<Token> postfixExpression = new List<Token>();
            Stack<Token> tokensStack = new Stack<Token>();
            foreach(var token in tokens)
            {
                if(token.IsNumberOrVariable())
                {
                    postfixExpression.Add(token);
                }
                else if (token.IsFunction())
                {
                    tokensStack.Push(token);
                }
                else if (token.IsLeftParenthesis())
                {
                    tokensStack.Push(token);
                }
                else if (token.IsRightParenthesis())
                {
                    Token top;
                    while(!(top = tokensStack.Pop()).IsLeftParenthesis())
                    {
                        postfixExpression.Add(top);
                    }
                    if(tokensStack.Count != 0 && tokensStack.Peek().IsFunction())
                    {
                        postfixExpression.Add(tokensStack.Pop());
                    }
                }
                else if (token.IsOperator())
                {
                    if (tokensStack.Count != 0)
                    {
                        Token top = tokensStack.Peek();
                        while ((OperatorHelper.IsLeftAssociative(token) && OperatorHelper.ComparePriorities(token, top) <= 0)
                            || (OperatorHelper.IsRightAssociative(token) && OperatorHelper.ComparePriorities(token, top) < 0))
                        {
                            postfixExpression.Add(tokensStack.Pop());
                            if(tokensStack.Count == 0)
                            {
                                break;
                            }
                            top = tokensStack.Peek();
                        }
                    }
                    tokensStack.Push(token);
                }
            }
            while(tokensStack.Count != 0)
            {
                postfixExpression.Add(tokensStack.Pop());
            }
            return postfixExpression;
        }
    }
}
