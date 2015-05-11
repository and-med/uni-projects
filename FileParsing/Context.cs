using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace FileParsing
{
    [DataContract]
    public class Context
    {
        [DataMember]
        private Dictionary<string, object> map;
        public Context()
        {
            map = new Dictionary<string, object>();
        }
        public Context(Dictionary<string, object> range)
        {
            map = range;
        }
        public Dictionary<string, object> Map
        {
            get
            {
                return map;
            }
        }
        public bool Contains(string sequence)
        {
            string key = sequence.Split('.')[0];
            return map.ContainsKey(key);
        }
        public object GetValue(string sequenceToEvaluate)
        {
            String[] split = sequenceToEvaluate.Split('.');
            if (map.ContainsKey(split[0]))
            {
                object result = map[split[0]];
                for (int i = 1; i < split.Length; ++i)
                {
                    if (split[i].Contains("()"))
                    {
                        result = result.GetType()
                            .InvokeMember(split[i].Replace("()", ""), BindingFlags.InvokeMethod, null, result, new object[] {});
                    }
                    else
                    {
                        result = result.GetType().GetProperty(split[i]).GetValue(result);
                    }
                }
                return result;
            }
            throw new ArgumentException("There is no such a value!");
        }
        public void AddNewValue(string key, object value)
        {
            map[key] = value;
        }

        public void DeleteValue(string key)
        {
            map.Remove(key);
        }
    }
}
