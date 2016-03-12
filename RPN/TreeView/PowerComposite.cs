using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class PowerComposite : BinaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return Math.Pow(LeftSon.Evaluate(variables), RightSon.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            LeftSon.Draw(line);
            line.Append("^");
            RightSon.Draw(line);
        }

        public override CompositeToken Clone()
        {
            var clone = new PowerComposite();
            clone.LeftSon = LeftSon.Clone();
            clone.RightSon = RightSon.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            if(LeftSon is NumberComposite)
            {
                var multiply1 = new MultiplyComposite();
                var multiply2 = new MultiplyComposite();
                var power = new PowerComposite();
                var ln = new LnComposite();
                multiply2.LeftSon = power;
                multiply2.RightSon = ln;
                power.LeftSon = LeftSon.Clone();
                power.RightSon = RightSon.Clone();
                ln.Son = LeftSon.Clone();
                multiply1.LeftSon = multiply2;
                multiply1.RightSon = RightSon.Derive(variable);
                return multiply1;
            }
            else if (RightSon is NumberComposite)
            {
                var multiply1 = new MultiplyComposite();
                var multiply2 = new MultiplyComposite();
                var power = new PowerComposite();
                var number = new NumberComposite();
                number.Value = (RightSon as NumberComposite).Value - 1;
                multiply2.LeftSon = RightSon.Clone();
                multiply2.RightSon = power;
                power.LeftSon = LeftSon.Clone();
                power.RightSon = number;
                multiply1.LeftSon = multiply2;
                multiply1.RightSon = LeftSon.Derive(variable);
                return multiply1;
            }
            else
            {
                var multiply1 = new MultiplyComposite();
                var exp = new ExponentComposite();
                var multiply2 = new MultiplyComposite();
                var ln1 = new LnComposite();
                var plus = new PlusComposite();
                var multiply3 = new MultiplyComposite();
                var ln2 = new LnComposite();
                var multiply4 = new MultiplyComposite();
                var divide = new DivideComposite();
                var number = new NumberComposite();
                number.Value = 1;
                var multiply5 = new MultiplyComposite();
                multiply1.LeftSon = exp;
                multiply1.RightSon = plus;
                exp.Son = multiply2;
                multiply2.LeftSon = RightSon.Clone();
                multiply2.RightSon = ln1;
                ln1.Son = LeftSon.Clone();
                plus.LeftSon = multiply3;
                plus.RightSon = multiply5;
                multiply3.LeftSon = RightSon.Derive(variable);
                multiply3.RightSon = ln2;
                ln2.Son = LeftSon.Clone();
                multiply5.LeftSon = multiply4;
                multiply5.RightSon = LeftSon.Derive(variable);
                multiply4.LeftSon = RightSon.Clone();
                multiply4.RightSon = divide;
                divide.LeftSon = number;
                divide.RightSon = LeftSon.Clone();
                return multiply1;
            }
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
