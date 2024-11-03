using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.TreeView;
using RPN.TreeView.Base;

namespace RPN.VisitorStructure
{
    internal class SimplifyVisitor : Visitor
    {
        public override CompositeToken Visit(CosineComposite node)
        {
            node.Son = node.Son.Accept(this);
            return node;
        }

        public override CompositeToken Visit(CotangentComposite node)
        {
            node.Son = node.Son.Accept(this);
            return node;
        }

        public override CompositeToken Visit(DivideComposite node)
        {
            node.LeftSon = node.LeftSon.Accept(this);
            node.RightSon = node.RightSon.Accept(this);
            NumberComposite leftSonRes = node.LeftSon as NumberComposite;
            NumberComposite rightSonRes = node.RightSon as NumberComposite;
            if (leftSonRes != null && rightSonRes != null)
            {
                var number = new NumberComposite();
                number.Value = leftSonRes.Value / rightSonRes.Value;
                return number;
            }
            if (leftSonRes != null && leftSonRes.Value == 0)
            {
                return new NumberComposite();
            }
            if (rightSonRes != null && rightSonRes.Value == 0)
            {
                throw new DivideByZeroException();
            }
            if ((leftSonRes != null && rightSonRes != null) && (leftSonRes.Value == 1 && rightSonRes.Value == 1))
            {
                var number = new NumberComposite();
                number.Value = 1;
                return number;
            }
            if (rightSonRes != null && rightSonRes.Value == 1)
            {
                return node.LeftSon;
            }
            return node;
        }

        public override CompositeToken Visit(ExponentComposite node)
        {
            node.Son = node.Son.Accept(this);
            return node;
        }

        public override CompositeToken Visit(LgComposite node)
        {
            node.Son = node.Son.Accept(this);
            return node;
        }

        public override CompositeToken Visit(LnComposite node)
        {
            node.Son = node.Son.Accept(this);
            return node;
        }

        public override CompositeToken Visit(MinusComposite node)
        {
            node.LeftSon = node.LeftSon.Accept(this);
            node.RightSon = node.RightSon.Accept(this);
            NumberComposite leftSonRes = node.LeftSon as NumberComposite;
            NumberComposite rightSonRes = node.RightSon as NumberComposite;
            if (leftSonRes != null && rightSonRes != null)
            {
                var number = new NumberComposite();
                number.Value = leftSonRes.Value - rightSonRes.Value;
                return number;
            }
            if (leftSonRes != null && leftSonRes.Value == 0)
            {
                var minus = new UnaryMinusComposite();
                minus.Son = node.RightSon;
                return minus.Accept(this);
            }
            if (rightSonRes != null && rightSonRes.Value == 0)
            {
                return node.LeftSon;
            }
            return node;
        }

        public override CompositeToken Visit(MultiplyComposite node)
        {
            node.LeftSon = node.LeftSon.Accept(this);
            node.RightSon = node.RightSon.Accept(this);
            NumberComposite leftSonRes = node.LeftSon as NumberComposite;
            NumberComposite rightSonRes = node.RightSon as NumberComposite;
            if (leftSonRes != null && rightSonRes != null)
            {
                var number = new NumberComposite();
                number.Value = leftSonRes.Value * rightSonRes.Value;
                return number;
            }
            if (leftSonRes != null && leftSonRes.Value == 0)
            {
                return new NumberComposite();
            }
            if (rightSonRes != null && rightSonRes.Value == 0)
            {
                return new NumberComposite();
            }
            if (leftSonRes != null && leftSonRes.Value == 1)
            {
                return node.RightSon;
            }
            if (rightSonRes != null && rightSonRes.Value == 1)
            {
                return node.LeftSon;
            }
            return node;
        }

        public override CompositeToken Visit(NumberComposite node)
        {
            return node;
        }

        public override CompositeToken Visit(PlusComposite node)
        {
            node.LeftSon = node.LeftSon.Accept(this);
            node.RightSon = node.RightSon.Accept(this);
            NumberComposite leftSonRes = node.LeftSon as NumberComposite;
            NumberComposite rightSonRes = node.RightSon as NumberComposite;
            if (leftSonRes != null && rightSonRes != null)
            {
                var number = new NumberComposite();
                number.Value = leftSonRes.Value + rightSonRes.Value;
                return number;
            }
            if (leftSonRes != null && leftSonRes.Value == 0)
            {
                return node.RightSon;
            }
            if (rightSonRes != null && rightSonRes.Value == 0)
            {
                return node.LeftSon;
            }
            return node;
        }

        public override CompositeToken Visit(PowerComposite node)
        {
            node.LeftSon = node.LeftSon.Accept(this);
            node.RightSon = node.RightSon.Accept(this);
            NumberComposite leftSonRes = node.LeftSon as NumberComposite;
            NumberComposite rightSonRes = node.RightSon as NumberComposite;
            if (leftSonRes != null && leftSonRes.Value == 0)
            {
                return new NumberComposite();
            }
            if (rightSonRes != null && rightSonRes.Value == 0)
            {
                var number = new NumberComposite();
                number.Value = 1;
                return number;
            }
            if (leftSonRes != null && leftSonRes.Value == 1)
            {
                var number = new NumberComposite();
                number.Value = 1;
                return number;
            }
            if (rightSonRes != null && rightSonRes.Value == 1)
            {
                return node.LeftSon;
            }
            return node;
        }

        public override CompositeToken Visit(SineComposite node)
        {
            node.Son = node.Son.Accept(this);
            return node;
        }

        public override CompositeToken Visit(SqrtComposite node)
        {
            node.Son = node.Son.Accept(this);
            return node;
        }

        public override CompositeToken Visit(TangentComposite node)
        {
            node.Son = node.Son.Accept(this);
            return node;
        }

        public override CompositeToken Visit(UnaryMinusComposite node)
        {
            node.Son = node.Son.Accept(this);
            NumberComposite sonRes = node.Son as NumberComposite;
            if (sonRes != null && sonRes.Value == 0)
            {
                return new NumberComposite();
            }
            return node;
        }

        public override CompositeToken Visit(VariableComposite node)
        {
            return node;
        }
    }
}
