using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Symbol
    {
        public string Text { get; set; }
        public int Count { get; set; }
        public double Probability { get; set; }
        public string Code { get; set;}
        public string Length { get; set; }
    }
    class Program
    {
        private static double probabilityRate = 19.0;

        static void Main(string[] args)
        {
            //List<char> seq = "x001101100".ToList();
            //List<char> code = "00110110100111".ToList();
            //Dictionary<string, int> map = new Dictionary<string, int>()
            //{
            //    { "y1", 0 },
            //    { "y2", 1 },
            //    { "x1", 2 },
            //    { "y3", 3 },
            //    { "x2", 4 },
            //    { "x3", 5 },
            //    { "x4", 6 },
            //    { "y4", 7 },
            //    { "x5", 8 },
            //    { "x6", 9 },
            //    { "x7", 10 },
            //    { "x8", 11 },
            //    { "x9", 12 }
            //};
            //var toSum1 = new []{ "y4", "x5", "x6", "x7", "x8", "x9" };
            //var toSum2 = new[] { "y3", "x2", "x3", "x4", "x8", "x9" };
            //var toSum3 = new[] { "y2", "x1", "x3", "x4", "x6", "x7" };
            //var toSum4 = new[] { "y1", "x1", "x2", "x4", "x5", "x7", "x9" };
            //Console.WriteLine(string.Join(" ", toSum1.Select(x => code[map[x]] == '0' ? 0 : 1)));
            //Console.WriteLine("Sum 1: " + toSum1.Select(x => code[map[x]] == '0' ? 0 : 1).Sum());

            //Console.WriteLine(string.Join(" ", toSum2.Select(x => code[map[x]] == '0' ? 0 : 1)));
            //Console.WriteLine("Sum 2: " + toSum2.Select(x => code[map[x]] == '0' ? 0 : 1).Sum());

            //Console.WriteLine(string.Join(" ", toSum3.Select(x => code[map[x]] == '0' ? 0 : 1)));
            //Console.WriteLine("Sum 3: " + toSum3.Select(x => code[map[x]] == '0' ? 0 : 1).Sum());

            //Console.WriteLine(string.Join(" ", toSum4.Select(x => code[map[x]] == '0' ? 0 : 1)));
            //Console.WriteLine("Sum 4: " + toSum4.Select(x => code[map[x]] == '0' ? 0 : 1).Sum());
            //int sum = 0;
            //for (int i = 1; i <= seq.Count(); ++i)
            //{
            //    //if (i == 1 || i == 2 || i == 4 || i == 5 || i == 9 || i == 7)// || i == 11 || i == 12 || i == 14)
            //    //{
            //    //    sum += seq[i - 1] == '0' ? 0 : 1;
            //    //}
            //    //if (i == 1 || i == 3 || i == 4 || i == 6 || i == 7)// || i == 10 || i == 11 || i == 13 || i == 14)
            //    //{
            //    //    sum += seq[i - 1] == '0' ? 0 : 1;
            //    //}
            //    //if (i == 2 || i == 3 || i == 4 || i == 8 || i == 9)//|| i == 10 || i == 11 || i == 15)
            //    //{
            //    //    sum += seq[i - 1] == '0' ? 0 : 1;
            //    //}
            //    //if (i == 5 || i == 6 || i == 7 || i == 8 || i == 9)// || i == 10 || i == 11)
            //    //{
            //    //    sum += seq[i - 1] == '0' ? 0 : 1;
            //    //}
            //}

            //Console.WriteLine(sum);
            //Console.ReadLine();
            //return;
            var symbols = new[] {new Symbol { Text = "A", Count = 5, Probability = double.NaN, Code= "" },
                                 new Symbol { Text = "_", Count = 3, Probability = double.NaN, Code = "" },
                                 new Symbol { Text = "R", Count = 3, Probability = double.NaN, Code = "" },
                                 new Symbol { Text = "E", Count = 2, Probability = double.NaN, Code = "" },
                                 new Symbol { Text = "P", Count = 2, Probability = double.NaN, Code = "" },
                                 new Symbol { Text = "S", Count = 2, Probability = double.NaN, Code = "" },
                                 new Symbol { Text = "D", Count = 1, Probability = double.NaN, Code = "" },
                                 new Symbol { Text = "T", Count = 1, Probability = double.NaN, Code = "" }
        }.ToList();

            Func<IEnumerable<Symbol>, int> totalCount =
                symbols_ => symbols_.Aggregate(0, (a, s) => a + s.Count);

            var total = totalCount(symbols);
            foreach (var symbol in symbols)
            {
                symbol.Probability = (symbol.Count > 0) ? symbol.Count / (double)total : 0;
            }

            //symbols.Sort((a, b) => b.Count.CompareTo(a.Count));

            // Where is the Y-Combinator when you need it ?
            Action<IEnumerable<Symbol>, string, int> recurse = null;
            recurse = (symbols_, str, depth) => {
                if (symbols_.Count() == 1)
                {
                    symbols_.Single().Code = str;
                    return;
                }

                var bestDiff = int.MaxValue;
                int i;
                for (i = 1; i < symbols_.Count(); i++)
                {
                    var firstPartCount = totalCount(symbols_.Take(i));
                    var secondPartCount = totalCount(symbols_.Skip(i));
                    var diff = Math.Abs(firstPartCount - secondPartCount);

                    if (diff < bestDiff) bestDiff = diff;
                    else break;
                }
                i = i - 1;

                Console.WriteLine("{0}{1}|{2}", new String('\t', depth),
                    symbols_.Take(i).Aggregate("", (a, s) => a + s.Text + " "),
                    symbols_.Skip(i).Aggregate("", (a, s) => a + s.Text + " "));

                recurse(symbols_.Take(i), str + "1", depth + 1);
                recurse(symbols_.Skip(i), str + "0", depth + 1);
            };

            recurse(symbols, "", 0);

            Console.WriteLine(new string('-', 78));
            foreach (var symbol in symbols)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", symbol.Code, symbol.Text,
                    symbol.Count, symbol.Probability, symbol.Code.Length, symbol.Code.Length * symbol.Count / probabilityRate);
            }
            Console.WriteLine(symbols.Sum(x => x.Code.Length * x.Count / probabilityRate));
            Console.ReadLine();

            using (var stream = File.Open("temp.txt", FileMode.Create))
            {
                using (var stringWriter = new StreamWriter(stream))
                {
                    var toCode = "BBCBCCACCABBCCBBBACC";
                    StringBuilder sb = new StringBuilder();
                    //stringWriter.WriteLine(string.Join("", toCode.Select(s => symbols.Where(t => t.Text == s.ToString()).FirstOrDefault().Code.ToString())));
                    var length = 1;
                    for (int i = 0; i < toCode.Length; i+= length)
                    {
                        var subString = toCode.Substring(i, length);
                        var mappedCode = symbols.Where(t => t.Text == subString).FirstOrDefault().Code;
                        sb.Append(mappedCode);
                    }
                    stringWriter.WriteLine(sb.ToString());
                    stringWriter.WriteLine(sb.ToString().Length);
                }
            }
        }
    }
}
