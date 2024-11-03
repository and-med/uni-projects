using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class TangentComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return Math.Tan(Son.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("tan(");
            Son.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new TangentComposite();
            clone.Son = Son.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var multiply = new MultiplyComposite();
            var divide = new DivideComposite();
            var number1 = new NumberComposite();
            number1.Value = 1;
            var power = new PowerComposite();
            var cos = new CosineComposite();
            var number2 = new NumberComposite();
            number2.Value = 2;
            multiply.LeftSon = divide;
            multiply.RightSon = Son.Derive(variable);
            divide.LeftSon = number1;
            divide.RightSon = power;
            power.LeftSon = cos;
            power.RightSon = number2;
            cos.Son = Son.Clone();
            return multiply;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
