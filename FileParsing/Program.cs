using System;
using FileParsing.Base;

namespace FileParsing
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid number of arguments...");
                Console.ReadKey();
            }
            else
            {
                MacroEngine.Initialize(args[0]);
                MacroEngine.ParsePages();
                //Console.WriteLine(MacroEngine.Merge("..\\..\\MyMacro.mv", new Context()));
                Console.WriteLine("File Parsing Completed...Press any key to continue..");
                Console.ReadKey();
            }
        }
    }
}