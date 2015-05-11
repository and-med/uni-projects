namespace FileParsing
{
    class BuildCompositeVisitor : Visitor
    {
        private readonly string fileData;
        private int currentPosition;
        private readonly TableOfMacros table;
        public BuildCompositeVisitor(string fd, TableOfMacros tb, int currPos = 0)
        {
            fileData = fd;
            currentPosition = currPos;
            table = tb;
        }
        private void HandleConstructionAtPosition(CompositeConstruction comCon, string name, ref int position)
        {
            Macross currentMacros = table.Get(name);
            int macrosDataEndPos = ParseUtilites.GetMacrosCloseBracketPosAfterMacroSep(fileData, position);
            string macrosData = ParseUtilites.GetMacrossData(fileData, position, macrosDataEndPos);
            comCon.AddUnit(
                new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
            currentPosition = macrosDataEndPos + 1;
            if (currentMacros.IsCompositeMacross() && currentMacros.Name!="#elseif" && currentMacros.Name!="#else")
            {
                TextUnit compositePart = PredefinedMacros.GetMacro(name, fileData, macrosData);
                comCon.AddUnit(compositePart);
                CompositeConstruction comp = (CompositeConstruction)compositePart;
                comp.StartPositionInFile = position;
                comp.Accept(this);
                position = currentPosition;
            }
            else
            {
                if (currentMacros is UserDefinedMacross)
                {
                    UserDefinedMacross userDefined = (UserDefinedMacross) currentMacros;
                    TextUnit comp = new UserDefinedMacrosConstruction(userDefined, fileData, macrosData);
                    comCon.AddUnit(comp);
                    position = currentPosition;
                }
                else
                {
                    TextUnit comp = PredefinedMacros.GetMacro(name, fileData, macrosData);
                    comCon.AddUnit(comp);                
                    position = currentPosition;
                }
            }
        }

        private void HandleEndConstructionAtPosition(CompositeConstruction comCon, int position)
        {
            comCon.AddUnit(
                new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
            currentPosition = position + "#end".Length;
            comCon.EndPositionInFile = position;
        }
        public override void Visit(CompositeConstruction comCon)
        {
            int position = currentPosition;
            for (; position < fileData.Length; ++position)
            {
                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = ParseUtilites.GetMacrosNameInPosition(fileData, position);
                    if (table.Contains(macrosName))
                    {
                        if (macrosName != "#end")
                        {
                            HandleConstructionAtPosition(comCon, macrosName, ref position);
                        }
                        else
                        {
                            HandleEndConstructionAtPosition(comCon, position);
                            return;
                        }
                    }
                }
            }
            comCon.AddUnit(
                new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
        }
        private void HandleEndOfElseConstruction(ElseConstruction elseCon, int position)
        {
            elseCon.AddUnit(
               new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
            currentPosition = position;
        }
        private void HandleElseConstructionAtPosition(IfConstruction ifCon, string name, ref int position)
        {
            ifCon.AddUnit(
                        new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
            string macrosData = null;
            if (name == "#elseif")
            {
                int macrosDataEndPos = ParseUtilites.GetMacrosCloseBracketPosAfterMacroSep(fileData, position);
                macrosData = ParseUtilites.GetMacrossData(fileData, position, macrosDataEndPos);
                currentPosition = macrosDataEndPos + 1;
            }
            else
            {
                currentPosition = currentPosition + "#else".Length;
            }
            ElseConstruction compositePart = (ElseConstruction)PredefinedMacros.GetMacro(name, fileData, macrosData);
            compositePart.SetFatherIfConstruction(ifCon);
            ifCon.AddElseConstruction(compositePart);
            compositePart.StartPositionInFile = position;
            compositePart.Accept(this);
            position = currentPosition;

        }
        public override void Visit(IfConstruction ifCon)
        {
            for (int position = currentPosition; position < fileData.Length && ifCon.Stop; ++position)
            {
                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = ParseUtilites.GetMacrosNameInPosition(fileData, position);
                    if (table.Contains(macrosName))
                    {

                        if (macrosName == "#else" || macrosName == "#elseif")
                        {
                            HandleElseConstructionAtPosition(ifCon, macrosName, ref position);
                        }
                        else if (macrosName == "#end")
                        {
                            HandleEndConstructionAtPosition(ifCon, position);
                            ifCon.EndPositionInFile = position;
                            return;
                        }
                        else
                        {
                            HandleConstructionAtPosition(ifCon, macrosName, ref position);
                        }
                    }
                }
            }
        }

        public override void Visit(ElseConstruction elseCon, IfConstruction ifCon)
        {
            for (int position = currentPosition; position < fileData.Length && ifCon.Stop; ++position)
            {

                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = ParseUtilites.GetMacrosNameInPosition(fileData, position);
                    if (table.Contains(macrosName))
                    {

                        if (macrosName == "#else" || macrosName == "#elseif")
                        {
                            HandleEndOfElseConstruction(elseCon, position);
                            HandleElseConstructionAtPosition(ifCon, macrosName, ref position);
                        }
                        else if (macrosName == "#end")
                        {
                            HandleEndConstructionAtPosition(elseCon, position);
                            elseCon.SetFatherStop();
                            return;
                        }
                        else
                        {
                            HandleConstructionAtPosition(elseCon, macrosName, ref position);
                        }
                    }
                }
            }
        }

       

    }
}
