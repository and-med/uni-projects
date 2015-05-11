namespace FileParsing
{
    abstract class TextUnit
    {
        public string TextData { get; set; }

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
