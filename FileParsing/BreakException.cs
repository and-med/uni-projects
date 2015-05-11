using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParsing
{
    class BreakException : Exception
    {
        public StringBuilder Result { get; set; }

        public BreakException(StringBuilder res)
        {
            Result = res;
        }

        public void AddToResult(string str)
        {
            Result.Insert(0, str);
        }
        public override string Message
        {
            get { return "I'm breakException"; }
        }
    }
}
