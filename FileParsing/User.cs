using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileParsing
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "address")]
        public string Address { get; set; }

        public User()
        {
            products = new List<Product>();
        }
        public User(string fn, string ln, string em, string address)
        {
            FirstName = fn;
            LastName = ln;
            Email = em;
            Address = address;
            products = new List<Product>();
        }
        [DataMember]
        private List<Product> products;

        public void AddProduct(Product p)
        {
            products.Add(p);
        }
        public List<Product> GetProducts()
        {
            return products;
        }
    }
}
