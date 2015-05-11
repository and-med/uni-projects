using System.Text;

namespace FileParsing
{
    class IfConstruction : CompositeConstruction
    {
        private Condition Cond { get; set; }

        public IfConstruction(string fileData, string condition):base(fileData)
        {
            Cond = new Condition(condition); 
        }
        public override string Evaluate(Context context)
        {
            StringBuilder result = new StringBuilder();
            if (Cond.Evaluate(context))
            {
                foreach (var unit in Units)
                {
                    result.Append(unit.Evaluate(context));
                }
            }
            return result.ToString();
        }
    }
}
