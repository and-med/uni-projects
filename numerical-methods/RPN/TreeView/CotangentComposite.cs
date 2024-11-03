using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class CotangentComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return 1/Math.Tan(Son.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("cotan(");
            Son.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new CotangentComposite();
            clone.Son = Son.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var multiply = new MultiplyComposite();
            var unariMinus = new UnaryMinusComposite();
            var division = new DivideComposite();
            var number1 = new NumberComposite();
            number1.Value = 1;
            var power = new PowerComposite();
            var sin = new SineComposite();
            var number2 = new NumberComposite();
            number2.Value = 2;
            unariMinus.Son = division;
            division.LeftSon = number1;
            division.RightSon = power;
            power.LeftSon = sin;
            power.RightSon = number2;
            sin.Son = Son.Clone();
            multiply.LeftSon = unariMinus;
            multiply.RightSon = Son.Derive(variable);
            return multiply;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
