using System;
using System.Runtime.Serialization;

namespace FileParsing
{
    [DataContract]
    public class Product
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "price")]
        public int Price { get; set; }
        [DataMember(Name = "dateOfPurchase")]
        public DateTime DateOfPurchase { get; set; }

        public Product(string name, int pr, DateTime dt)
        {
            Name = name;
            Price = pr;
            DateOfPurchase = dt;
        }
    }
}
