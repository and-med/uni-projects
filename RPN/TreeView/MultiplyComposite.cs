using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class MultiplyComposite : BinaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return LeftSon.Evaluate(variables) * RightSon.Evaluate(variables);
        }

        public override void Draw(StringBuilder line)
        {
            LeftSon.Draw(line);
            line.Append("*");
            RightSon.Draw(line);
        }

        public override CompositeToken Clone()
        {
            var clone = new MultiplyComposite();
            clone.LeftSon = LeftSon.Clone();
            clone.RightSon = RightSon.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var plus = new PlusComposite();
            var multiply1 = new MultiplyComposite();
            var multiply2 = new MultiplyComposite();
            multiply1.LeftSon = LeftSon.Derive(variable);
            multiply1.RightSon = RightSon.Clone();
            multiply2.LeftSon = LeftSon.Clone();
            multiply2.RightSon = RightSon.Derive(variable);
            plus.LeftSon = multiply1;
            plus.RightSon = multiply2;
            return plus;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
