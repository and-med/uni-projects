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
        private void HandleConstructionAtPosition(CompositeConstruction comCon, string name, ref int position)
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

        private void HandleEndConstructionAtPosition(CompositeConstruction comCon, int position)
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
                    if (table.Contains(macrosName))// and != elseif else
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
                int macrosDataStartPos = fileData.IndexOf('(', position) + 1;
                int macrosDataEndPos = Macros.GetClosedBrackedPos(fileData, macrosDataStartPos);
                macrosData = fileData.Substring(macrosDataStartPos,
                    macrosDataEndPos - macrosDataStartPos);
                currentPosition = macrosDataEndPos + 1;
            }
            else
            {
                currentPosition = currentPosition + "#else".Length;
            }
            ElseConstruction compositePart = (ElseConstruction)PredefinedMacros.GetCompositePart(name, macrosData);
            compositePart.SetFatherIfConstruction(ifCon);
            ifCon.AddElseConstruction(compositePart);
            compositePart.Accept(this);
            position = currentPosition;

        }
        public override void Visit(IfConstruction ifCon)
        {
            for (int position = currentPosition; position < fileData.Length && ifCon.Stop; ++position)
            {
                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = GetMacrosNameInPosition(position);
                    if (table.Contains(macrosName))
                    {

                        if (macrosName =="#else" || macrosName =="#elseif")
                        {
                            HandleElseConstructionAtPosition(ifCon,macrosName, ref position);  
                        }
                        else if (macrosName == "#end")
                        {
                            HandleEndConstructionAtPosition(ifCon, position);
                            
                            return;
                        }
                        else
                        {
                            HandleConstructionAtPosition(ifCon,macrosName,ref position);
                        }
                    }
                }
            }
        }

        public override void Visit(ElseConstruction elseCon,IfConstruction ifCon)
        {
            for (int position = currentPosition; position < fileData.Length && ifCon.Stop; ++position)
            {
               
                if (fileData[position] == StaticData.MacroSeparator)
                {
                    string macrosName = GetMacrosNameInPosition(position);
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
