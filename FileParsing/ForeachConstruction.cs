using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace FileParsing
{
    class ForeachConstruction: CompositeConstruction
    {
        private string MacroData;

        public ForeachConstruction(string fileData, string foreachData):base(fileData)
        {
            MacroData = foreachData;
        }
        private void EmitRedundantCharacters()
        {
            MacroData = new string(MacroData.Reverse().ToArray());
            MacroData = new string((MacroData.SkipWhile(c =>
            { return StaticData.CharactersToIgnore.Any(character => c == character); })).ToArray());
            MacroData = new string(MacroData.Reverse().ToArray());
            MacroData = new string((MacroData.SkipWhile(c =>
            { return StaticData.CharactersToIgnore.Any(character => c == character); })).ToArray());
        }
        private void SplitToVarAndContainer(out string variable, out string container)
        {
            EmitRedundantCharacters();
            if (MacroData.Contains("in"))
            {
                int varStartPosition = MacroData.IndexOf(StaticData.VariableSeparator);
                int containerStartPosition = MacroData.LastIndexOf(StaticData.VariableSeparator);
                variable = MacroData.Substring(varStartPosition + 1, 
                    MacroData.IndexOfAny(StaticData.CharactersToIgnore, varStartPosition) - varStartPosition - 1);
                container = MacroData.Substring(containerStartPosition + 1, MacroData.Length - containerStartPosition - 1);
            }
            else
            {
                throw new ArgumentException("Incorrect expression in foreach loop!");
            }
        }
        public override string Evaluate(Context context)
        {
            string variableName;
            string containerName;
            SplitToVarAndContainer(out variableName, out containerName);
            IEnumerable container = (IEnumerable)context.GetValue(containerName);
            StringBuilder result = new StringBuilder();
            foreach (var variable in container)
            {
                context.AddNewValue(variableName, variable);
                foreach (var unit in Units)
                {
                    result.Append(unit.Evaluate(context));
                }
            }
            context.DeleteValue(variableName);
            return result.ToString();
        }
    }
}
