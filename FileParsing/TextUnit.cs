namespace FileParsing
{
    abstract class TextUnit
    {
        public string TextData { get; set; }

        public abstract void Accept(Visitor v);
        protected TextUnit()
        {
        }
        protected TextUnit(string data)
        {
            TextData = data;
        }

        public abstract string Evaluate(Context context);
    }
}
