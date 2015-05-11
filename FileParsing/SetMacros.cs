using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParsing
{
    class SetMacros:TextUnit
    {
        private readonly string NewVariable;
        public SetMacros(TextUnit father, string fileData, string macroData) : base(father, fileData)
        {
            NewVariable = macroData;
        }
        public override string Evaluate(Context context)
        {
            string[] arguments = ParseUtilites.SplitForSet(NewVariable);
            context.AddNewValue(arguments[0],ParseUtilites.ParseArgument(arguments[1],context));
            return "";
        }
    }
}
