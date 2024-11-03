namespace FileParsing.Base
{
    abstract class Macross
    {
        public string Name { get; set; }
        private readonly bool isBuiltIn;
        private readonly bool isComposite;
        private readonly uint CountArguments;
        protected Macross(string name, uint countArgs, bool composite, bool builtIn)
        {
            Name = name;
            isComposite = composite;
            isBuiltIn = builtIn;
        }
        public bool IsCompositeMacross()
        {
            return isComposite;
        }
        public bool IsBuiltIn()
        {
            return isBuiltIn;
        }
    }
}
