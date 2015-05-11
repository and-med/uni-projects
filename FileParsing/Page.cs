using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileParsing
{
    [DataContract]
    public class Page
    {
        [DataMember(Name = "page")]
        public string PageName { get; set; }
        [DataMember(Name = "outputPath")]
        public string OutputPath { get; set; }
        [DataMember(Name = "recipient")]
        public User Recepient { get; set; }
        [DataMember(Name = "sender")]
        public User Sender { get; set; }
        [DataMember(Name = "params")]
        public List<Params> Params { get; set; }
    }
}
