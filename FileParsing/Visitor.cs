using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParsing
{
    abstract class Visitor
    {
        public abstract void Visit(StaticText st);
        public abstract void Visit(CompositeConstruction comCon);
    }
}
