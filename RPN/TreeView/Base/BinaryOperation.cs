using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.TreeView.Base
{
    internal abstract class BinaryOperation : CompositeToken
    {
        public CompositeToken LeftSon { get; set; }
        public CompositeToken RightSon { get; set; }


    }
}
