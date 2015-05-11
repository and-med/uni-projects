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
        public string OutPutPage { get; set; }
        [DataMember(Name = "recepient")]
        public User Recepient { get; set; }
        [DataMember(Name = "sender")]
        public User Sender { get; set; }
        [DataMember(Name = "params")]
        public Dictionary<string, object> Params { get; set; }
    }
}
