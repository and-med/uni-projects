using System;
using System.Collections.Generic;
using System.Text;
using FileParsing.CompositeView;
using FileParsing.CompositeView.visitor;
using FileParsing.Utilites;

namespace FileParsing.Base
{
    class UserDefinedMacross : Macross
    {
        private TextUnit compositeView;
        private readonly List<string> argumentsNames;

        public TextUnit CompositeView
        {
            private set { compositeView = value; }
            get { return compositeView; }
        }

        public List<string> ArgumentsNames
        {
            get
            {
                return argumentsNames;
            }
        }

        UserDefinedMacross(string name, List<string> argNames)
            : base(name, (uint)argNames.Count, false, false)
        {

            argumentsNames = argNames;
        }

        private static void RegisterWitoutBuilding(TableOfMacros table, string fileData)
        {
            int positionOfMacros = 0;
            int searchAfter = 0;
            while ((positionOfMacros = fileData.IndexOf("#macro", searchAfter, StringComparison.Ordinal)) != -1)
            {
                string macrosData = ParseUtilites.GetMacrossData(fileData, positionOfMacros);
                List<string> split = new List<string>(ParseUtilites.SplitAvoidingRedundantCharacters(macrosData));
                string macrosName = StaticData.MacroSeparator + split[0];
                if (table.Contains(macrosName))
                {
                    throw new Exception("MacroDefinitionAlreadyExists!");
                }
                split.RemoveAt(0);
                Macross macrossToAdd = new UserDefinedMacross(macrosName, split);
                table.Add(macrosName, macrossToAdd);
                searchAfter = positionOfMacros + 1;
            }
        }
        public static void RegisterWithParsingFile(TableOfMacros table, ref string fileData)
        {
            RegisterWitoutBuilding(table, fileData);
            List<KeyValuePair<int, int>> startVsEndPositions = new List<KeyValuePair<int, int>>();
            for (int position = 0; position < fileData.Length; ++position)
            {
                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = ParseUtilites.GetMacrosNameInPosition(fileData, position);
                    if (macrosName == "#macro")
                    {
                        string macrosData = ParseUtilites.GetMacrossData(fileData, position);
                        string userDefinedMacrossName = StaticData.MacroSeparator + ParseUtilites.SplitAvoidingRedundantCharacters(macrosData)[0];
                        MainCompositeView cv = new MainCompositeView(fileData);
                        cv.StartPositionInFile = position;
                        int startOfMacroData = ParseUtilites.GetMacrosCloseBracketPosAfterMacroSep(fileData, position) + 1;
                        Visitor v = new BuildCompositeVisitor(fileData, table, startOfMacroData);
                        cv.Accept(v);
                        position = cv.EndPositionInFile + "#end".Length;
                        ((UserDefinedMacross)table.Get(userDefinedMacrossName)).CompositeView = cv;
                        startVsEndPositions.Add(new KeyValuePair<int, int>(cv.StartPositionInFile, cv.EndPositionInFile));
                    }
                }
            }
            UpdateString(ref fileData, startVsEndPositions);
        }
        public static void Register(TableOfMacros table, string fileData)
        {
            RegisterWitoutBuilding(table, fileData);
            for (int position = 0; position < fileData.Length; ++position)
            {
                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = ParseUtilites.GetMacrosNameInPosition(fileData, position);
                    if (macrosName == "#macro")
                    {
                        string macrosData = ParseUtilites.GetMacrossData(fileData, position);
                        string userDefinedMacrossName = StaticData.MacroSeparator + ParseUtilites.SplitAvoidingRedundantCharacters(macrosData)[0];
                        MainCompositeView cv = new MainCompositeView(fileData);
                        cv.StartPositionInFile = position;
                        int startOfMacroData = ParseUtilites.GetMacrosCloseBracketPosAfterMacroSep(fileData, position) + 1;
                        Visitor v = new BuildCompositeVisitor(fileData, table, startOfMacroData);
                        cv.Accept(v);
                        position = cv.EndPositionInFile + "#end".Length;
                        ((UserDefinedMacross)table.Get(userDefinedMacrossName)).CompositeView = cv;
                    }
                }
            }
        }
        private static void UpdateString(ref string fileData, List<KeyValuePair<int, int>> startVsEndPositions)
        {
            StringBuilder newFileData = new StringBuilder();
            int startingAddPosition = 0;
            foreach (var pair in startVsEndPositions)
            {
                newFileData.Append(fileData.Substring(startingAddPosition, pair.Key - startingAddPosition));
                startingAddPosition = pair.Value + "#end".Length;
            }
            newFileData.Append(fileData.Substring(startingAddPosition, fileData.Length - startingAddPosition));
            fileData = newFileData.ToString();
        }
    }
}
