using System;
using System.Collections.Generic;

namespace FileParsing
{
    static class PredefinedMacros
    {
        private static Dictionary<string, Macros> builtInMacroses;

        static PredefinedMacros()
        {
            builtInMacroses = new Dictionary<string, Macros>()
            {
                {"#if", new Macros("if", 1, true, true)},
                {"#foreach", new Macros("foreach", 1, true, true)},
                {"#end", new Macros("end", 0, false, true)}
            };
        }

        public static TextUnit GetCompositePart(string macroName, string macroData)
        {
            switch (macroName)
            {
                case "#if":
                    return new IfConstruction(macroData);
                case "#foreach":
                    return new ForeachConstruction(macroData);
            }
            throw new ArgumentException("There's no such macros in predefined macroses");
        }
        public static Dictionary<string, Macros> Get()
        {
            return builtInMacroses;
        }
    }
}
