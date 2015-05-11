using System.Collections.Generic;
using System.Text;

namespace FileParsing
{
    abstract class CompositeConstruction : TextUnit
    {
        public int StartPositionInFile { get; set; }
        public int EndPositionInFile { get; set; }
        protected List<TextUnit> Units;
        protected CompositeConstruction(TextUnit father, string data) : base(father, data)
        {
            Units = new List<TextUnit>();
        }
        public void AddUnit(TextUnit tu)
        {
            Units.Add(tu);
        }
        public virtual void Accept(Visitor v)
        {
            v.Visit(this);
        }
        public override string Evaluate(Context context)
        {
            StringBuilder result = new StringBuilder();
            foreach (var unit in Units)
            {
                result.Append(unit.Evaluate(context));
            }
            return result.ToString();
        }
    }
}
