using System.Globalization;
using System.Text;

namespace FileParsing
{
    class BreakConstruction : TextUnit
    {
        public BreakConstruction(TextUnit father, string fileData) : base(father, fileData) { }

        private bool IsForeachDescendant()
        {
            TextUnit currentFather = Father;
            while (currentFather != null)
            {
                if (currentFather is ForeachConstruction)
                {
                    return true;
                }
                currentFather = currentFather.Father;
            }
            return false;
        }
        public override string Evaluate(Context context)
        {
            StringBuilder res = new StringBuilder();
            if (IsForeachDescendant())
            {
                throw new BreakException(res);
            }
            res.Append("#break");
            return res.ToString();
        }
    }
}
