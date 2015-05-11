using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParsing
{
    class IncludeConstruction:TextUnit
    {

        private readonly string PathToFile;
        public IncludeConstruction(string fileData, string macroData):base(fileData)
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
