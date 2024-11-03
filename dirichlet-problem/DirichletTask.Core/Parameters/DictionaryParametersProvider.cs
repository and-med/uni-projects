using DirichletTask.Core.Abstraction.Parameters;
using System.Collections.Generic;

namespace DirichletTask.Core.Parameters
{
    public class DictionaryParametersProvider : IReadOnlyParameterProvider<double>
    {
        private Dictionary<string, double> _values;

        public DictionaryParametersProvider(Dictionary<string, double> values)
        {
            _values = values;
        }

        public double GetValue(string parameterName)
        {
            return _values[parameterName];
        }
    }
}
