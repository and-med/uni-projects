using System.Text;

namespace FileParsing
{
    class UserDefinedMacrosConstruction : TextUnit
    {
        private UserDefinedMacross actualMacross;
        private string arguments;
        public UserDefinedMacrosConstruction(TextUnit father, UserDefinedMacross macross, string fileData, string macroData)
            : base(father, fileData)
        {
            arguments = macroData;
            actualMacross = macross;
        }
        public override string Evaluate(Context context)
        {
            StringBuilder res = new StringBuilder();
            object[] args = ParseUtilites.ParseArgumentsOfMacro(arguments, context);
            string[] names = actualMacross.ArgumentsNames.ToArray();
            for (int i = 0; i < names.Length; ++i)
            {
                names[i] = names[i].Remove(0, 1);
            }
            for (int i = 0; i < args.Length; ++i)
            {
                context.AddNewValue(names[i], args[i]);
            }
            res.Append(actualMacross.CompositeView.Evaluate(context));
            for (int i = 0; i < args.Length; ++i)
            {
                context.DeleteValue(names[i]);
            }
            return res.ToString();
        }
    }
}
