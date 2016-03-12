using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView.Base
{
    internal abstract class CompositeToken
    {
        public abstract double Evaluate(double[] variables);
        public abstract void Draw(StringBuilder line);
        public abstract CompositeToken Derive(string variable);
        public abstract CompositeToken Clone();
        public abstract CompositeToken Accept(Visitor visitor);
    }
}
