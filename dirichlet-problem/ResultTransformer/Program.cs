using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultTransformer
{
    class Program
    {
        public static double [][] arr;
        public static double x1 = 0;
        public static double x2 = 10.01;
        public static double dx = 0.1;
        public static int count = 0;
        public static int countOfColumns = 7;

        public static double t1 = 1;
        public static double t2 = 2;
        public static double dt = 0.2;


        static void Main(string[] args)
        {
            for(var x = x1; x <= x2; x+=dx)
            {
                count++;
            }
            arr = new double[count][];
            for (int i =0; i < count; ++i)
            {
                arr[i] = new double[countOfColumns];
            }
            int j = 0;
            for (double x = x1; x <= x2; x += dx, ++j)
            {
                arr[j][0] = x;
            }

            Read();
            Output();
        }

        static void Output()
        {
            using (var stream = File.OpenWrite("../../output.txt"))
            {
                using (var writer = new StreamWriter(stream))
                {
                    var t = t1;
                    var firstLine = "T\t" + string.Join("\t", Enumerable.Range(0, countOfColumns - 1).Select(x =>
                    {
                        var temp = t * 10;
                        int number = (int)Math.Round(temp);
                        if (number % 10 == 0) number = number / 10;
                        t += dt;
                        return $"X{number.ToString()}";
                    }));
                    writer.WriteLine(firstLine);
                    foreach(var subArr in arr)
                    {
                        writer.WriteLine(string.Join("\t", subArr));
                    }
                }
            }
        }

        static void Read()
        {
            using (var stream = File.OpenRead("../../input.txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    for (int j = 1; j < countOfColumns; ++j)
                    {
                        for (int i = 0; i < count; ++i)
                        {
                            var line = reader.ReadLine();
                            var number = double.Parse(line);
                            arr[i][j] = number;
                        }
                    }
                }
            }
        }
    }
}
