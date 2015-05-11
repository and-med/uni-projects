using System.Runtime.Serialization;

namespace FileParsing
{
    [DataContract]
    public class Params
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
        [DataMember(Name = "value")]
        public object Value { get; set; }
    }
}
