using FileParsing.Base;
using FileParsing.Utilites;

namespace FileParsing.CompositeView
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
