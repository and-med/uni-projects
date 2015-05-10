namespace FileParsing
{
    class Macros
    {
        public string Name { get; set; }
        private readonly bool isBuiltIn;
        private readonly bool hasEndMacro;
        private uint CountArguments;
        public Macros(string name, uint countArgs = 0, bool hasEnd = false, bool builtIn = false)
        {
            Name = name;
            hasEndMacro = hasEnd;
            isBuiltIn = builtIn;
        }
        public static int GetClosedBrackedPos(string where, int afterOpenBracketPos)
        {
            int currentPos = afterOpenBracketPos;
            int countNotClosedBrackets = 1;
            while (countNotClosedBrackets != 0)
            {
                if (where[currentPos] == ')')
                {
                    countNotClosedBrackets -= 1;
                }
                else if (where[currentPos] == '(')
                {
                    countNotClosedBrackets += 1;
                }
                currentPos += 1;
            }
            return currentPos - 1;
        }

        public bool HasEnd()
        {
            return hasEndMacro;
        }
        public bool IsBuiltIn()
        {
            return isBuiltIn;
        }
    }
}
