using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class UnaryMinusComposite : UnaryOperation
    {
        public override double Evaluate(double[] variables)
        {
            return -Son.Evaluate(variables);
        }

        public override void Draw(StringBuilder line)
        {
            line.Append("(-");
            Son.Draw(line);
            line.Append(")");
        }

        public override CompositeToken Clone()
        {
            var clone = new UnaryMinusComposite();
            clone.Son = Son.Clone();
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var unaryMinus = new UnaryMinusComposite();
            unaryMinus.Son = Son.Derive(variable);
            return unaryMinus;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
