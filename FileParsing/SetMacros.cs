using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParsing
{
    class SetMacros:TextUnit
    {
        public SetMacros(string data) : base(data)
        {
            
        }

        public override string Evaluate(Context context)
        {
            return string.Format("pes");
        }
    }
}
