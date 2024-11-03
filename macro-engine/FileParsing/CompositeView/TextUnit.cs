using FileParsing.Base;

namespace FileParsing.CompositeView
{
    abstract class TextUnit
    {
        public TextUnit Father { get; set; }
        public string TextData { get; set; }

        protected TextUnit(TextUnit father, string data)
        {
            Father = father;
            TextData = data;
        }

        public abstract string Evaluate(Context context);
    }
}
