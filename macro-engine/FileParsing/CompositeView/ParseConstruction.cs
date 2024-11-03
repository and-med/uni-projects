using FileParsing.Base;
using FileParsing.Utilites;

namespace FileParsing.CompositeView
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
