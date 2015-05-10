using System;
using System.Collections.Generic;

namespace FileParsing
{
    class TableOfMacros
    {
        private Dictionary<string, Macros> table;

        public TableOfMacros()
        {
            table = PredefinedMacros.Get();
        }

        public bool Contains(string name)
        {
            return table.ContainsKey(name);
        }

        public Macros Get(string name)
        {
            if (Contains(name))
            {
                return table[name];
            }
            throw new ArgumentException("There are not such a key in a table");
        }
    }
}
