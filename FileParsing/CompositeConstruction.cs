using System.Collections.Generic;
using System.Text;

namespace FileParsing
{
    abstract class CompositeConstruction : TextUnit
    {
        protected List<TextUnit> Units;

        protected CompositeConstruction()
        {
            Units = new List<TextUnit>();
        }
        protected CompositeConstruction(string data) : base(data)
        {
            Units = new List<TextUnit>();
        }
        public void AddUnit(TextUnit tu)
        {
            Units.Add(tu);
        }

        public override void Accept(Visitor v)
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
