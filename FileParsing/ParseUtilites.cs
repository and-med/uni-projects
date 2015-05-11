using System;
using System.Collections.Generic;
using System.Linq;

namespace FileParsing
{
    static class ParseUtilites
    {
        public static void ParseConfigFile(string fileData, out string rootToFiles, out string[] preloaded)
        {
            int firstEqualityOperatorPos = fileData.IndexOf('=') + 1;
            int secondEqualityOperatorPos = fileData.IndexOf('=', firstEqualityOperatorPos);
            while (StaticData.CharactersToIgnore.Any(c => c == fileData[firstEqualityOperatorPos]))
            {
                firstEqualityOperatorPos++;
            }
            rootToFiles = fileData.Substring(firstEqualityOperatorPos, 
                fileData.IndexOfAny(StaticData.CharactersToIgnore, firstEqualityOperatorPos) - firstEqualityOperatorPos);
            preloaded = SplitCommaSeparated(fileData.Substring(secondEqualityOperatorPos + 1));
        }
        public static string GetMacrosNameInPosition(string where, int macroSeparatorPos)
        {
            int index = @where.IndexOfAny(new[] {' ', '\t', '\n', '\r', '('}, macroSeparatorPos);
            if (index == -1)
            {
                index = @where.Length;
            }
            return @where.Substring(macroSeparatorPos, index - macroSeparatorPos);
        }
        public static int GetMacrosCloseBracketPosAfterMacroSep(string where, int macroSeparatorPosition)
        {
            int afterOpenBracketPos = @where.IndexOf('(', macroSeparatorPosition) + 1;
            return GetMacrosCloseBracketPosAfterOpenBracket(@where, afterOpenBracketPos);
        }
        public static int GetMacrosCloseBracketPosAfterOpenBracket(string where, int afterOpenBracketPos)
        {
            int currentPos = afterOpenBracketPos;
            int countNotClosedBrackets = 1;
            while (countNotClosedBrackets != 0)
            {
                if (@where[currentPos] == ')')
                {
                    countNotClosedBrackets -= 1;
                }
                else if (@where[currentPos] == '(')
                {
                    countNotClosedBrackets += 1;
                }
                currentPos += 1;
            }
            return currentPos - 1;
        }
        public static string GetMacrossData(string where, int macroSeparatorPos, int macrosCloseBracketPos)
        {
            int macrosDataStartPos = @where.IndexOf('(', macroSeparatorPos) + 1;
            return @where.Substring(macrosDataStartPos,
                macrosCloseBracketPos - macrosDataStartPos);
        }
        public static string GetMacrossData(string where, int macroSeparatorPosition)
        {
            int macrosDataStartPos = @where.IndexOf('(', macroSeparatorPosition) + 1;
            int macrosDataEndPos = ParseUtilites.GetMacrosCloseBracketPosAfterOpenBracket(@where, macrosDataStartPos);
            return @where.Substring(macrosDataStartPos,
                macrosDataEndPos - macrosDataStartPos);
        }
        public static string[] SplitCommaSeparated(string data)
        {
            char[] charSeparators = StaticData.CharactersToIgnore.Concat(new[] {','}).ToArray();
            return data.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string[] SplitAvoidingRedundantCharacters(string data)
        {
            return data.Split(StaticData.CharactersToIgnore, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string[] SplitIncludingStrings(string data)
        {
            List<string> res = new List<string>();
            int variableBegin = 0;
            bool searchingDoubleQuotes = false;
            for (int i = 0; i < data.Length; ++i)
            {
                if (!searchingDoubleQuotes)
                {
                    if (StaticData.CharactersToIgnore.Any(c => c == data[i]))
                    {
                        variableBegin = i + 1;
                    }
                    else if (data[i] == '\"')
                    {
                        variableBegin = i;
                        searchingDoubleQuotes = true;
                    }
                    else
                    {
                        i = data.IndexOfAny(StaticData.CharactersToIgnore, variableBegin);
                        if (i == -1)
                        {
                            i = data.Length;
                        }
                        res.Add(data.Substring(variableBegin, i - variableBegin));
                    }
                }
                else
                {
                    if (data[i] == '\"')
                    {
                        res.Add(data.Substring(variableBegin, i - variableBegin + 1));
                        searchingDoubleQuotes = false;
                    }
                }
            }
            return res.ToArray();
        }
        public static object ParseArgument(string arg, Context context)
        {
            object res;
            if (arg.Contains(StaticData.VariableSeparator) && context.Contains(arg.Remove(0, 1)))
            {
                res = context.GetValue(arg.Remove(0, 1));
            }
            else if (arg.Contains('\"'))
            {
                if (arg.IndexOf('\"') == 0 && arg.LastIndexOf('\"') == arg.Length - 1)
                {
                    res = arg.Remove(0,1).Remove(arg.Length - 1 - 1, 1);
                }
                else
                {
                    throw new ArgumentException("Invalid format of string!");
                }
            }
            else if (arg.Contains('.'))
            {
                res = Double.Parse(arg);
            }
            else
            {
                int tempRes;
                if (Int32.TryParse(arg, out tempRes))
                {
                    return tempRes;
                }
                return null;
            }
            return res;
        }
        public static object[] ParseArgumentsOfMacro(string arguments, Context context)
        {
            string[] split = SplitIncludingStrings(arguments);
            object[] res = new object[split.Length];
            for(int i = 0; i < split.Length; ++i)
            {
                res[i] = ParseArgument(split[i], context);
            }
            return res;
        }
        public static int GetEndOfVariableAtPosition(string textData, int position)
        {
            if (StaticData.IsAllowedFirstCharacterForVariable(textData[position]))
            {
                position += 1;
                while (position < textData.Length && (StaticData.IsAllowedCharacterForVariable(textData[position]) || textData[position] == '.'
                       || (textData[position] == '(' && textData[position + 1] == ')')))
                {
                    if (textData[position] == '(')
                    {
                        position += 2;
                    }
                    else
                    {
                        position += 1;
                    }
                }
            }
            return position - 1;
        }
    }
}
