using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class PlusComposite : BinaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return LeftSon.Evaluate(variables) + RightSon.Evaluate(variables);
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("(");
            LeftSon.Draw(line);
            line.Append("+");
            RightSon.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new PlusComposite();
            clone.LeftSon = LeftSon.Clone();
            clone.RightSon = RightSon.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var plus = new PlusComposite();
            plus.LeftSon = LeftSon.Derive(variable);
            plus.RightSon = RightSon.Derive(variable);
            return plus;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
