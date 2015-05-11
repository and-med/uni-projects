using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FileParsing
{
    class MacroEngine
    {
        private static string filesDirectory = "";

        public static void RegisterUserDefinedMacross(string filename, out string fileData, TableOfMacros table)
        {
            string filedat;
            using (var stream = new StreamReader(Path.Combine(filesDirectory, filename)))
            {
                filedat = stream.ReadToEnd();
            }
            UserDefinedMacross.Register(table, ref filedat);
            fileData = filedat;
        }
        public static string Merge(string filename, Context context)
        {
            TableOfMacros table = new TableOfMacros();
            string fileData;
            RegisterUserDefinedMacross(filename, out fileData, table);
            CompositeView compositeView = new CompositeView(fileData);
            Visitor v = new BuildCompositeVisitor(fileData, table);
            compositeView.Accept(v);
            return compositeView.Evaluate(context);
        }
        public static List<Page> DownloadJsonFile(string path)
        {
            PagesInfo newPages;
            using (StreamReader stream = new StreamReader(path))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PagesInfo), new DataContractJsonSerializerSettings()
                {
                    DateTimeFormat = new DateTimeFormat("dd.MM.yyyy")
                });
                byte[] bytes = Encoding.UTF8.GetBytes(stream.ReadToEnd());
                MemoryStream mStream = new MemoryStream(bytes);
                newPages = (PagesInfo)ser.ReadObject(mStream);
            }
            filesDirectory = newPages.ConfigFile;
            return newPages.Pages;
        }
    }
}
