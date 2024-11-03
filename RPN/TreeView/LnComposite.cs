using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class LnComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return Math.Log(Son.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("ln(");
            Son.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new LnComposite();
            clone.Son = Son.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var multiply = new MultiplyComposite();
            var divide = new DivideComposite();
            var number = new NumberComposite();
            number.Value = 1;
            divide.LeftSon = number;
            divide.RightSon = Son.Clone();
            multiply.LeftSon = divide;
            multiply.RightSon = Son.Derive(variable);
            return multiply;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
