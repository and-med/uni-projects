using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FileParsing
{
    class Program
    {
        static Context MyContext { get; set; }

        static Program()
        {
            MyContext = new Context();
        }
        static List<Product> GetProducts(string name)
        {
            object myUser = MyContext.GetValue(name);
            return (List<Product>)myUser.GetType().InvokeMember("GetProducts", BindingFlags.InvokeMethod, null, myUser, new object[0]);
        }

        static void Main(string[] args)
        {
            //Serialize(GetPageInfo());
            Deserialize();
            MacroEngine.Initialize(@"D:\Projects\C#\MacroEngine\_macros\json_conf\check1.json");
                MacroEngine.ParsePages();
            //Console.WriteLine(MacroEngine.Merge(@"D:\Projects\C#\MacroEngine\FileParsing\MyMacro1.mv", myCont));
            Console.ReadLine();
        }

        static void Deserialize()
        {
            PagesInfo newPages;
            using (StreamReader stream = new StreamReader("..\\..\\temp.json"))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PagesInfo), new DataContractJsonSerializerSettings()
                {
                    DateTimeFormat = new DateTimeFormat("dd.MM.yyyy")
                });
                byte[] bytes = Encoding.UTF8.GetBytes(stream.ReadToEnd());
                MemoryStream mStream = new MemoryStream(bytes);
                newPages = (PagesInfo)ser.ReadObject(mStream);
            }
            Console.WriteLine(newPages.ConfigFile);
        }
        static void Serialize(PagesInfo p)
        {
            using (var stream = File.Create("..\\..\\temp.json"))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PagesInfo), new DataContractJsonSerializerSettings()
                {
                    DateTimeFormat = new DateTimeFormat("dd.MM.yyyy")
                });
                ser.WriteObject(stream, p);
            }
        }
        static PagesInfo GetPageInfo()
        {
            User recepient = new User()
            {
                FirstName = "Pavlo",
                LastName = "Halepa",
                Email = "ph...@gmail.com",
                Address = "Lviv, Franka st. 3"
            };
            recepient.AddProduct(new Product("iphone5", 600, new DateTime(2015, 3, 23)));
            recepient.AddProduct(new Product("macbook air", 1600, new DateTime(2015, 4, 28)));
            recepient.AddProduct(new Product("Some Stupid Thing", 5000, new DateTime(2015, 10, 10)));
            User sender = new User()
            {
                FirstName = "Lee",
                LastName = "Engee",
                Email = "sa...@ebay.com",
                Address = "USA"
            };
            Page temp = new Page()
            {
                PageName = "page1.mv",
                OutputPath = "D:\\temp_path.txt",
                Recepient = recepient,
                Sender = sender,
                Params = new List<Params>()
                {
                    new Params(){ Key = "logoUrl", Value = "http..." }, 
                    new Params(){ Key = "counter", Value = 25}
                }
            };
            PagesInfo newPagesInfo = new PagesInfo
            {
                ConfigFile = "temp.txt",
                Pages = new List<Page>() { temp }
            };
            newPagesInfo.ConfigFile = "D:\\temp_path.txt";
            return newPagesInfo;
        }
    }
}
