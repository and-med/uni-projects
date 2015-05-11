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
        //public override void Visit(UserDefinedMacrosConstruction usDef)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public override void Visit(StaticText st)
        //{
        //    throw new System.NotImplementedException();
        //}
        private void HandleConstructionAtPosition(ref CompositeConstruction comCon, string name, ref int position)
        {
            Macross currentMacros = table.Get(name);
            int macrosDataEndPos = ParseUtilites.GetMacrosCloseBracketPosAfterMacroSep(fileData, position);
            string macrosData = ParseUtilites.GetMacrossData(fileData, position, macrosDataEndPos);
            comCon.AddUnit(
                new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
            currentPosition = macrosDataEndPos + 1;
            if (currentMacros.IsCompositeMacross())
            {
                TextUnit compositePart = PredefinedMacros.GetCompositePart(name, fileData, macrosData);
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
            }
        }

        private void HandleEndConstructionAtPosition(ref CompositeConstruction comCon, int position)
        {
            comCon.AddUnit(
                new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
            currentPosition = position + "#end".Length;
            comCon.EndPositionInFile = position;
        }
        public override void Visit(CompositeConstruction comCon)
        {
            for (int position = currentPosition; position < fileData.Length; ++position)
            {
                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = ParseUtilites.GetMacrosNameInPosition(fileData, position);
                    if (table.Contains(macrosName))
                    {
                        if (macrosName != "#end")
                        {
                            HandleConstructionAtPosition(ref comCon, macrosName, ref position);
                        }
                        else
                        {
                            HandleEndConstructionAtPosition(ref comCon, position);
                            return;
                        }
                    }
                }
            }
        }
    }
}
