using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;

namespace RPN.TreeView
{
    internal class VariableComposite : Numeric
    {
        public string Name { get; set; }

        public string[] VarNames { get; set; }

        public override double Evaluate(double[] variables)
        {
            if(VarNames == null)
            {
                if(variables.Length != 0)
                {
                    throw new ArgumentException("Parameter should be null or empty");
                }
            }
            return variables[Array.IndexOf(VarNames, Name)];
        }

        public override void Draw(StringBuilder line)
        {
            line.Append(Name);
        }

        public override CompositeToken Clone()
        {
            var clone = new VariableComposite();
            clone.Name = Name;
            clone.VarNames = VarNames;
            return clone;
        }

        public override CompositeToken Derive(string variable)
        {
            var number = new NumberComposite();
            if(variable == Name)
            {
                number.Value = 1;
            }
            else
            {
                number.Value = 0;
            }
            return number;
        }

        public override CompositeToken Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
