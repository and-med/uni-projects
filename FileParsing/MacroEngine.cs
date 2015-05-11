using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FileParsing
{
    class MacroEngine
    {
        private static PagesInfo pagesInfo { get; set; }
        private static TableOfMacros table { get; set; }
        private static string RootToPreloaded { get; set; }
        private static string[] PreloadedFileNames { get; set; }
        static MacroEngine()
        {
            table = new TableOfMacros();
        }

        private static void RegisterUserDefinedMacross(string filepath)
        {
            string filedat;
            using (var stream = new StreamReader(filepath))
            {
                filedat = stream.ReadToEnd();
            }
            UserDefinedMacross.Register(table, filedat);
        }
        private static void RegisterUserDefinedMacrossIncludingParsing(string filepath, out string fileData)
        {
            string filedat;
            using (var stream = new StreamReader(filepath))
            {
                filedat = stream.ReadToEnd();
            }
            UserDefinedMacross.RegisterWithParsingFile(table, ref filedat);
            fileData = filedat;
        }
        private static void ParseConfigurationFile()
        {
            string fileData;
            using (var stream = new StreamReader(pagesInfo.ConfigFile))
            {
                fileData = stream.ReadToEnd();
            }
            string rootToPreloaded;
            string[] preloadedFileNames;
            ParseUtilites.ParseConfigFile(fileData, out rootToPreloaded, out preloadedFileNames);
            RootToPreloaded = rootToPreloaded;
            PreloadedFileNames = preloadedFileNames;
            ParsePreloadedFiles();
        }
        private static void ParsePreloadedFiles()
        {
            foreach (var filename in PreloadedFileNames)
            {
                RegisterUserDefinedMacross(Path.Combine(RootToPreloaded, filename));
            }
        }
        public static string Merge(string filepath, Context context)
        {
            string fileData;
            RegisterUserDefinedMacrossIncludingParsing(filepath, out fileData);
            CompositeView compositeView = new CompositeView(fileData);
            Visitor v = new BuildCompositeVisitor(fileData, table);
            compositeView.Accept(v);
            return compositeView.Evaluate(context);
        }
        public static void Initialize(string path)
        {
            PagesInfo newPages = new PagesInfo();
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
            pagesInfo = newPages;
            ParseConfigurationFile();
        }

        public static void ParsePages()
        {
            foreach (var page in pagesInfo.Pages)
            {
                Context context = new Context(page.Params);
                context.AddNewValue("recepient", page.Recepient);
                context.AddNewValue("sender", page.Sender);
                string pageData = Merge(Path.Combine(RootToPreloaded, page.PageName), context);
                using (var stream = File.Create(page.OutputPath))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(pageData);
                    }
                }
            }
        }
    }
}
