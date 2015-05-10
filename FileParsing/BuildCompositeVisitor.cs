namespace FileParsing
{
    class BuildCompositeVisitor : Visitor
    {
        private readonly string fileData;
        private int currentPosition;
        private readonly TableOfMacros table;
        public BuildCompositeVisitor(string fd, TableOfMacros tb)
        {
            fileData = fd;
            currentPosition = 0;
            table = tb;
        }
        public override void Visit(StaticText st)
        {
            throw new System.NotImplementedException();
        }
        private string GetMacrosNameInPosition(int position)
        {
            return fileData.Substring(position, fileData.IndexOfAny(new[] { ' ', '\t', '\r', '(' }, position) - position);
        }
        private void HandleConstructionAtPosition(ref CompositeConstruction comCon, string name, ref int position)
        {
            Macros currentMacros = table.Get(name);
            int macrosDataStartPos = fileData.IndexOf('(', position) + 1;
            int macrosDataEndPos = Macros.GetClosedBrackedPos(fileData, macrosDataStartPos);
            string macrosData = fileData.Substring(macrosDataStartPos,
                macrosDataEndPos - macrosDataStartPos);
            comCon.AddUnit(
                new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
            currentPosition = macrosDataEndPos + 1;
            if (currentMacros.HasEnd())
            {
                TextUnit compositePart = PredefinedMacros.GetCompositePart(name, macrosData);
                comCon.AddUnit(compositePart);
                if (compositePart is CompositeConstruction)
                {
                    CompositeConstruction comp = (CompositeConstruction)compositePart;
                    comp.Accept(this);
                    position = currentPosition;
                }
            }
        }

        private void HandleEndConstructionAtPosition(ref CompositeConstruction comCon, int position)
        {
            comCon.AddUnit(
                new StaticText(fileData.Substring(currentPosition, position - currentPosition)));
            currentPosition = position + "#end".Length;
        }
        public override void Visit(CompositeConstruction comCon)
        {
            for (int position = currentPosition; position < fileData.Length; ++position)
            {
                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = GetMacrosNameInPosition(position);
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
