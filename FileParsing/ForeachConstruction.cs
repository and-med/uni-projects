using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace FileParsing
{
    class ForeachConstruction: CompositeConstruction
    {
        private string varInContainerString;

        public ForeachConstruction(string foreachData)
        {
            varInContainerString = foreachData;
        }
        private void EmitRedundantCharacters()
        {
            varInContainerString = new string(varInContainerString.Reverse().ToArray());
            varInContainerString = new string((varInContainerString.SkipWhile(c =>
            { return StaticData.CharactersToIgnore.Any(character => c == character); })).ToArray());
            varInContainerString = new string(varInContainerString.Reverse().ToArray());
            varInContainerString = new string((varInContainerString.SkipWhile(c =>
            { return StaticData.CharactersToIgnore.Any(character => c == character); })).ToArray());
        }
        private void SplitToVarAndContainer(out string variable, out string container)
        {
            EmitRedundantCharacters();
            if (varInContainerString.Contains("in"))
            {
                int varStartPosition = varInContainerString.IndexOf(StaticData.VariableSeparator);
                int containerStartPosition = varInContainerString.LastIndexOf(StaticData.VariableSeparator);
                variable = varInContainerString.Substring(varStartPosition + 1, 
                    varInContainerString.IndexOfAny(StaticData.CharactersToIgnore, varStartPosition) - varStartPosition - 1);
                container = varInContainerString.Substring(containerStartPosition + 1, varInContainerString.Length - containerStartPosition - 1);
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
