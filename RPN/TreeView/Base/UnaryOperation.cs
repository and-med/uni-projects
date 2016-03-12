using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.TreeView.Base
{
    internal abstract class UnaryOperation : CompositeToken
    {
        public CompositeToken Son { get; set; }
    }
}
