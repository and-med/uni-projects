using System;
using System.Collections.Generic;

namespace FileParsing
{
    class TableOfMacros
    {
        private Dictionary<string, Macross> table;

        public TableOfMacros()
        {
            table = PredefinedMacros.Get();
        }

        public void Add(string name, Macross macro)
        {
            table[name] = macro;
        }
        public bool Contains(string name)
        {
            return table.ContainsKey(name);
        }

        public Macross Get(string key)
        {
            if (Contains(key))
            {
                return table[key];
            }
            throw new ArgumentException("There are not such a key in a table");
        }
    }
}
