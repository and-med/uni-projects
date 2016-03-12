using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class DivideComposite : BinaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return LeftSon.Evaluate(variables) / RightSon.Evaluate(variables);
        }

        public override void Draw(StringBuilder line)
        {
            LeftSon.Draw(line);
            line.Append("/");
            RightSon.Draw(line);
        }

        public override CompositeToken Clone()
        {
            var clone = new DivideComposite();
            clone.LeftSon = LeftSon.Clone();
            clone.RightSon = RightSon.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var divide = new DivideComposite();
            var minus = new MinusComposite();
            var multiply1 = new MultiplyComposite();
            var multiply2 = new MultiplyComposite();
            var power = new PowerComposite();
            var number = new NumberComposite();
            number.Value = 2;
            divide.LeftSon = minus;
            divide.RightSon = power;
            minus.LeftSon = multiply1;
            minus.RightSon = multiply2;
            multiply1.LeftSon = LeftSon.Derive(variable);
            multiply1.RightSon = RightSon.Clone();
            multiply2.LeftSon = LeftSon.Clone();
            multiply2.RightSon = RightSon.Derive(variable);
            power.LeftSon = RightSon.Clone();
            power.RightSon = number;
            return divide;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
