using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileParsing
{
    class StaticText: TextUnit
    {
        public StaticText(string data) : base(data) { }

        public override string Evaluate(Context context)
        {
            StringBuilder builder = new StringBuilder(TextData);
            List<string> valuesToReplace = new List<string>();
            int startingPosition = 0;
            int positionOfVariable;
            while ((positionOfVariable = TextData.IndexOf(StaticData.VariableSeparator, startingPosition)) != -1)
            {
                startingPosition = GetEndOfVariableAtPosition(TextData, positionOfVariable + 1);
                valuesToReplace.Add(TextData.Substring(positionOfVariable + 1, startingPosition - positionOfVariable));
            }
            foreach (var val in valuesToReplace.Where(context.Contains))
            {
                builder.Replace(StaticData.VariableSeparator + val, context.GetValue(val).ToString());
            }
            return builder.ToString();
        }

        public static int GetEndOfVariableAtPosition(string textData, int position)
        {
            int startPosition = position;
            if (StaticData.IsAllowedFirstCharacterForVariable(textData[position]))
            {
                position += 1;
                while (StaticData.IsAllowedCharacterForVariable(textData[position]) || textData[position] == '.' 
                    || (textData[position] == '(' && textData[position + 1] == ')'))
                {
                    if (textData[position] == '(')
                    {
                        position += 2;
                    }
                    else
                    {
                        position += 1;
                    }
                }
            }
            return position - 1;
        }
    }
}
