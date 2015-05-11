using System;
using System.Collections.Generic;

namespace FileParsing
{
    static class PredefinedMacros
    {
        private static Dictionary<string, Macros> builtInMacroses;

        static PredefinedMacros()
        {
            builtInMacroses = new Dictionary<string, Macros>
            {
                {"#if", new Macros("if", 1, true, true)},
                {"#foreach", new Macros("foreach", 1, true, true)},
                {"#end", new Macros("end", 0, false, true)},
                {"#else", new Macros("else",0,false,true)},
                {"#elseif",new Macros("elseif",1,false,true)},
                {"#set",new Macros("set",1,false,true)}
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
                case "#else":
                    return new ElseConstruction();
                case "#elseif":
                    return new ElseIfConstruction(macroData);
            }
            throw new ArgumentException("There's no such macros in predefined macroses");
        }
        public static Dictionary<string, Macros> Get()
        {
            return builtInMacroses;
        }
    }
}
