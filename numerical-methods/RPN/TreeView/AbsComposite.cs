using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class AbsComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return Math.Abs(Son.Evaluate(variables));
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("|");
            Son.Draw(line);
            line.Append("|");
        }

        public override CompositeToken Clone()
        {
            var clone = new AbsComposite();
            clone.Son = Son.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            throw new ArgumentException("Too hard to derive from abs function, Sorry:)");
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            throw new ArgumentException("This code is unreachable from outside the assembly!");
        }
    }
}
