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
                {"#end", new PredefinedMacros("end", 0, false)},
                {"#else", new PredefinedMacros("else",0,false)},
                {"#elseif",new PredefinedMacros("elseif",1,false)},
                {"#set",new PredefinedMacros("set",1,false)},
                {"#include",new PredefinedMacros("include",1,false) },
                {"#parse", new PredefinedMacros("parse",1,false) },
                {"#break", new PredefinedMacros("break", 0, false)}
            };
        }

        public static TextUnit GetMacro(TextUnit father, string macroName, string fileData, string macroData)
        {
            switch (macroName)
            {
                case "#if":
                    return new IfConstruction(father, fileData, macroData);
                case "#foreach":
                    return new ForeachConstruction(father, fileData, macroData);
                case "#else":
                    return new ElseConstruction(fileData);
                case "#elseif":
                    return new ElseIfConstruction(fileData, macroData);
                case "#set":
                    return new SetMacros(father, fileData,macroData);
                case "#include":
                    return new IncludeConstruction(father, fileData,macroData);
                case "#parse":
                    return new ParseConstruction(father, fileData,macroData);
            }
            throw new ArgumentException("There's no such macros in predefined macroses");
        }
        public static Dictionary<string, Macross> Get()
        {
            return builtInMacroses;
        }
    }
}
