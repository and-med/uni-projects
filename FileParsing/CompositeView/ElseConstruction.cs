using System.Text;
using FileParsing.Base;
using FileParsing.CompositeView.visitor;
using FileParsing.Exceptions;

namespace FileParsing.CompositeView
{
    class ElseConstruction : CompositeConstruction
    {
        public override string Evaluate(Context context)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                foreach (var unit in Units)
                {
                    result.Append(unit.Evaluate(context));
                }
            }
            catch (BreakException breakException)
            {
                breakException.AddToResult(result.ToString());
                throw;
            }
            return result.ToString();
        }

        public ElseConstruction(string FileData)
            : base(null, FileData)
        {
        }
        public virtual bool GetResultOfCondition(Context cont)
        {
            return true;
        }

        public void SetFatherIfConstruction(IfConstruction ifCon)
        {
            Father = ifCon;
        }

        public void SetFatherStop()
        {
            ((IfConstruction)Father).Stop = false;
        }
        public override void Accept(Visitor v)
        {
            v.Visit(this, (IfConstruction)Father);
        }
    }
}
