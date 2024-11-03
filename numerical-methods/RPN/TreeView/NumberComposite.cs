using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class NumberComposite : Numeric
    {
        public double Value { get; set; }

        public override double Evaluate(double[] variables)
        {
            return Value;
        }

        public override void Draw(StringBuilder line)
        {
            line.Append(Value);
        }

        public override CompositeToken Clone()
        {
            var clone = new NumberComposite();
            clone.Value = Value;
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var number = new NumberComposite();
            number.Value = 0;
            return number;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
