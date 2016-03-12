using RPN.Structure;
using RPN.TreeView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.VisitorStructure;
using Numerics.Integration;

namespace RPN
{
    public class Function
    {
        private string[] argNames;

        private CompositeToken expressionTree;
        
        public Function(string[] names)
        {
            argNames = names;
        }

        public void Parse(string expression)
        {
            Tokenizer tokenizer = new Tokenizer(argNames);
            RPNParser parser = new RPNParser(tokenizer);
            List<Token> postfixExpression = parser.Parse(expression);
            ExpressionTreeBuilder builder = new ExpressionTreeBuilder(argNames);
            expressionTree = builder.Build(postfixExpression);
        }

        public string Draw()
        {
            StringBuilder line = new StringBuilder();
            expressionTree.Draw(line);
            return line.ToString();
        }

        public double Evaluate(double[] variables)
        {
            return expressionTree.Evaluate(variables);
        }

        public Function Derive(string variable)
        {
            var derivedFunction = new Function(argNames);
            derivedFunction.expressionTree = expressionTree.Derive(variable);
            SimplifyVisitor visitor = new SimplifyVisitor();
            derivedFunction.expressionTree = derivedFunction.expressionTree.Accept(visitor);
            return derivedFunction;
        }

        public double Integrate(IIntegrator integrator, double a, double b, double epselon)
        {
            return integrator.Integrate((x) => Evaluate(new double[] { x }), a, b, epselon);
        }
    }
}
