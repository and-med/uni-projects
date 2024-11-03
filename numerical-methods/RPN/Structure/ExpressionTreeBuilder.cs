using RPN.TreeView;
using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Structure
{
    internal class ExpressionTreeBuilder
    {
        private string[] varNames;

        public ExpressionTreeBuilder(string[] names)
        {
            varNames = names;
        }

        public CompositeToken Build(List<Token> postfixForm)
        {
            Stack<Token> values = new Stack<Token>();
            Stack<CompositeToken> valuesComposite = new Stack<CompositeToken>();

            foreach(var token in postfixForm)
            {
                CompositeToken node = GetTreeNode(token);
                if(token.IsNumberOrVariable())
                {
                    values.Push(token);
                    valuesComposite.Push(node);
                    if(token.IsNumber())
                    {
                        ((NumberComposite)node).Value = Double.Parse(token.Value);
                    }
                    else
                    {
                        ((VariableComposite)node).Name = token.Value;
                        ((VariableComposite)node).VarNames = varNames;
                    }
                }
                else
                {
                    if(token.IsUnaryOperaor())
                    {
                        values.Pop();
                        UnaryOperation uOp = (UnaryOperation)node;
                        uOp.Son = valuesComposite.Pop();
                        values.Push(new Token("", TokenType.Number));
                        valuesComposite.Push(uOp);
                    }
                    if(token.IsBinaryOperator())
                    {
                        values.Pop();
                        values.Pop();
                        BinaryOperation bOp = (BinaryOperation)node;
                        bOp.RightSon = valuesComposite.Pop();
                        bOp.LeftSon = valuesComposite.Pop();
                        values.Push(new Token("", TokenType.Number));
                        valuesComposite.Push(bOp);
                    }
                }
            }
            return valuesComposite.Pop();
        }

        public CompositeToken GetTreeNode(Token token)
        {
            switch(token.TokenType)
            {                
                case TokenType.Plus: return new PlusComposite();
                case TokenType.Minus: return new MinusComposite();
                case TokenType.Multiply: return new MultiplyComposite();
                case TokenType.Divide: return new DivideComposite();
                case TokenType.Sine: return new SineComposite();
                case TokenType.Cosine: return new CosineComposite();
                case TokenType.Tangent: return new TangentComposite();
                case TokenType.Cotangent: return new CotangentComposite();
                case TokenType.Sqrt: return new SqrtComposite();
                case TokenType.Abs: return new AbsComposite();
                case TokenType.Ln: return new LnComposite();
                case TokenType.Lg: return new LgComposite();
                case TokenType.Power: return new PowerComposite();
                case TokenType.Exponent: return new ExponentComposite();
                case TokenType.Number: return new NumberComposite();
                case TokenType.Variable: return new VariableComposite();
                case TokenType.UnaryMinus: return new UnaryMinusComposite();
            }
            throw new ArgumentException("Values was not expected");
        }
    }
}
