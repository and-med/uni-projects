using System;
using System.Text;

namespace FileParsing.Exceptions
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
