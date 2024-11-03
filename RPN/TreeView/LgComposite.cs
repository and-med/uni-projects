using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class LgComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return Math.Log10(Son.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("lg(");
            Son.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new LgComposite();
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
            var ln = new LnComposite();
            var number2 = new NumberComposite();
            number2.Value = 10;
            divide.LeftSon = number1;
            divide.RightSon = multiply2;
            multiply2.LeftSon = Son.Clone();
            multiply2.RightSon = ln;
            ln.Son = number2;
            multiply1.LeftSon = divide;
            multiply1.RightSon = Son.Derive(variable);
            return multiply1;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
