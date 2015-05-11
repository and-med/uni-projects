using System.Collections.Generic;
using System.Text;

namespace FileParsing
{
    class IfConstruction : CompositeConstruction
    {
        private List<ElseConstruction> ElseUnits;
        private Condition Cond { get; set; }
        public bool Stop { get; set; }
        public IfConstruction(TextUnit father, string fileData, string condition)
            : base(father, fileData)
        {
            Cond = new Condition(condition);
            ElseUnits = new List<ElseConstruction>();
            Stop = true;
        }
        public IfConstruction(TextUnit father, string fileData)
            : base(father, fileData)
        {
            Stop = true;
        }
        public override string Evaluate(Context context)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                if (Cond.Evaluate(context))
                {
                    foreach (var unit in Units)
                    {
                        result.Append(unit.Evaluate(context));
                    }
                }
                else
                {
                    foreach (var eUnit in ElseUnits)
                    {
                        if (eUnit.GetResultOfCondition(context))
                        {
                            result.Append(eUnit.Evaluate(context));

                            break;
                        }
                    }
                }
            }
            catch (BreakException ex)
            {
                ex.AddToResult(result.ToString());
                throw;
            }
            return result.ToString();
        }
        public void AddElseConstruction(ElseConstruction eCon)
        {
            ElseUnits.Add(eCon);
        }

        public override void Accept(Visitor v)
        {
            v.Visit(this);
        }
    }
}
