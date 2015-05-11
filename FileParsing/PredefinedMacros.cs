using System;
using System.Collections.Generic;

namespace FileParsing
{
    class PredefinedMacros : Macross
    {
        private static readonly Dictionary<string, Macross> builtInMacroses;

        public PredefinedMacros(string name, uint countArgs, bool isComposite) : base(name, countArgs, isComposite, true) { }

        static PredefinedMacros()
        {
            builtInMacroses = new Dictionary<string, Macross>()
            {
                {"#if", new PredefinedMacros("if", 1, true)},
                {"#foreach", new PredefinedMacros("foreach", 1, true)},
                {"#end", new PredefinedMacros("end", 0, false)}
            };
        }

        public static TextUnit GetCompositePart(string macroName, string fileData, string macroData)
        {
            switch (macroName)
            {
                case "#if":
                    return new IfConstruction(fileData, macroData);
                case "#foreach":
                    return new ForeachConstruction(fileData, macroData);
            }
            throw new ArgumentException("There's no such macros in predefined macroses");
        }
        public static Dictionary<string, Macross> Get()
        {
            return builtInMacroses;
        }
    }
}
