using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class SqrtComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return Math.Sqrt(Son.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("sqrt(");
            Son.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new SqrtComposite();
            clone.Son = Son.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var multiply1 = new MultiplyComposite();
            var divide = new DivideComposite();
            var number1 = new NumberComposite();
            number1.Value = 1;
            var multiply2 = new MultiplyComposite();
            var number2 = new NumberComposite();
            number2.Value = 2;
            var sqrt = new SqrtComposite();
            multiply1.LeftSon = divide;
            multiply1.RightSon = Son.Derive(variable);
            divide.LeftSon = number1;
            divide.RightSon = multiply2;
            multiply2.LeftSon = number2;
            multiply2.RightSon = sqrt;
            sqrt.Son = Son.Clone();
            return multiply1;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
