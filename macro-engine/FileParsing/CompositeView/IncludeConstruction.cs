using System.IO;
using FileParsing.Base;
using FileParsing.Utilites;

namespace FileParsing.CompositeView
{
    class IncludeConstruction: TextUnit
    {

        private readonly string PathToFile;
        public IncludeConstruction(TextUnit father, string fileData, string macroData):base(father, fileData)
        {
            PathToFile = macroData;
        }
        
        public override string Evaluate(Context context)
        {
            using (StreamReader reader = new StreamReader(Path.GetFullPath(ParseUtilites.GetPathForIncludeParse(PathToFile))))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
