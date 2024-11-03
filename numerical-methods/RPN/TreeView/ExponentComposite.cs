using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class ExponentComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return Math.Exp(Son.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("exp(");
            Son.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new ExponentComposite();
            clone.Son = Son.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var multiply = new MultiplyComposite();
            multiply.LeftSon = this.Clone();
            multiply.RightSon = Son.Derive(variable);
            return multiply;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
