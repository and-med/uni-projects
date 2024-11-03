using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN.TreeView;
using RPN.TreeView.Base;

namespace RPN.VisitorStructure
{
    internal abstract class Visitor
    {
        public abstract CompositeToken Visit(CosineComposite node);
        public abstract CompositeToken Visit(CotangentComposite node);
        public abstract CompositeToken Visit(DivideComposite node);
        public abstract CompositeToken Visit(ExponentComposite node);
        public abstract CompositeToken Visit(LgComposite node);
        public abstract CompositeToken Visit(LnComposite node);
        public abstract CompositeToken Visit(MinusComposite node);
        public abstract CompositeToken Visit(MultiplyComposite node);
        public abstract CompositeToken Visit(NumberComposite node);
        public abstract CompositeToken Visit(PlusComposite node);
        public abstract CompositeToken Visit(PowerComposite node);
        public abstract CompositeToken Visit(SineComposite node);
        public abstract CompositeToken Visit(SqrtComposite node);
        public abstract CompositeToken Visit(TangentComposite node);
        public abstract CompositeToken Visit(UnaryMinusComposite node);
        public abstract CompositeToken Visit(VariableComposite node);
    }
}
