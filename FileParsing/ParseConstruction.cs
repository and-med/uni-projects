using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileParsing
{
    internal class ParseConstruction : TextUnit
    {

        private readonly string FilePath;

        public ParseConstruction(TextUnit father, string fileData, string macroData) : base(father, fileData)
        {
            FilePath = macroData;
        }

        public override string Evaluate(Context context)
        {
            string path = ParseUtilites.GetPathForIncludeParse(FilePath);
            return MacroEngine.Merge(path, new Context());
        }

    }
}
