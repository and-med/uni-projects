using System.Runtime.Serialization;

namespace FileParsing.JsonData
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
