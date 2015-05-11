using System;
using System.Collections.Generic;
using System.Reflection;

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
            List<Page> pages = MacroEngine.DownloadJsonFile("..//..//jsonFile.json");
            Context myCont = new Context();
            myCont.AddNewValue("page", GetPageInfo());
            Console.WriteLine(MacroEngine.Merge(@"D:\Projects\C#\MacroEngine\FileParsing\MyMacro1.mv", myCont));
            Console.ReadLine();
        }

        static Page GetPageInfo()
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
                Recepient = recepient,
                Sender = sender,
                Params = new Dictionary<string, object>()
                {
                    { "logoUrl", "http..." }, 
                    { "counter", 25}
                }
            };
            PagesInfo newPagesInfo = new PagesInfo
            {
                ConfigFile = "temp.txt",
                Pages = new List<Page>() { temp }
            };
            return temp;
        }
    }
}
