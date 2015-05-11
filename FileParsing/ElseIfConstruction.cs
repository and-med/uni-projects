using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParsing
{
    class ElseIfConstruction:ElseConstruction
    {
        private Condition Cond { get; set; }
        

        public ElseIfConstruction(string FileData, string cond): base(FileData)
        {
            Cond = new Condition(cond);
        }
        public override bool GetResultOfCondition(Context cont)
        {
            return Cond.Evaluate(cont);
        }
    }
}
