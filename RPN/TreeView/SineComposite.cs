using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class SineComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return Math.Sin(Son.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("sin(");
            Son.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new SineComposite();
            clone.Son = Son.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var multiply = new MultiplyComposite();
            var cos = new CosineComposite();
            multiply.LeftSon = cos;
            multiply.RightSon = Son.Derive(variable);
            cos.Son = Son.Clone();
            return multiply;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
