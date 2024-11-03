using DirichletTask.Core.Abstraction.Cache;
using DirichletTask.Core.Abstraction.Functions;
using DirichletTask.Core.Abstraction.Series;
using DirichletTask.Core.Functions;
using DirichletTask.Core.Parameters;
using DirichletTask.Core.Series;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DirichletTask.UI.ViewModels
{
    public class MainViewModel
    {
        private string _number = "1";
        private string _kappa = "1";

        private string t = "1";
        private string x1 = "0";
        private string _alpha = "0.5";
        private string _beta = "1";
        private string x2 = "1";
        private string y1 = "-0.1";
        private string y2 = "0.1";
        private string dx = "0.005";
        private double width;
        private double height;

        ICachedSingleSeriesCalculator<int, double, double> _eSeries;
        ISingleValuedSeriesCalculator<int, double, double> _eTildaSeries;
        IFunction<double, double, double> _eSumSeries;
        IFunction<double, double, double> _eAnalytical;

        #region Binding Properties

        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }

        public string Kappa
        {
            get
            {
                return _kappa;
            }
            set
            {
                _kappa = value;
            }
        }

        public string Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = value;
            }
        }

        public string Beta
        {
            get
            {
                return _beta;
            }
            set
            {
                _beta = value;
            }
        }

        public string X1
        {
            get
            {
                return x1;
            }
            set
            {
                x1 = value;
            }
        }

        public string X2
        {
            get
            {
                return x2;
            }
            set
            {
                x2 = value;
            }
        }

        public string T
        {
            get
            {
                return t;
            }
            set
            {
                t = value;
            }
        }

        public string Y1
        {
            get
            {
                return y1;
            }
            set
            {
                y1 = value;
            }
        }

        public string Y2
        {
            get
            {
                return y2;
            }
            set
            {
                y2 = value;
            }
        }

        public string Dx
        {
            get
            {
                return dx;
            }
            set
            {
                dx = value;
            }
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        #endregion Binding Properties

        public MainViewModel()
        {
        }

        public void CreateFromConfigs()
        {
            var parameters = new DictionaryParametersProvider(new Dictionary<string, double>
            {
                { "alpha", double.Parse(Alpha) },
                { "beta", double.Parse(Beta) }
            });
            var eSeriesParam = new ESeriesParam(parameters);
            var gSeries = new GSeriesCalculator(parameters);
            _eSeries = new ESeriesCalculator(gSeries, eSeriesParam, parameters);

            _eSeries.SetCacheSize(int.Parse(Number));
        }

        private Point GetPointAt(double x, double y)
        {
            var x1 = double.Parse(X1);
            var x2 = double.Parse(X2);
            var y1 = double.Parse(Y1);
            var y2 = double.Parse(Y2);

            return new Point((int)(((x - x1) / (x2 - x1)) * Width), (int)((y2 - y) / (y2 - y1) * Height));
        }

        public void DrawCommand(Graphics g)
        {
            CreateFromConfigs();

            var x1 = double.Parse(X1);
            var x2 = double.Parse(X2);
            var dx = double.Parse(Dx);

            for (double x = x1; x <= x2; x+=dx)
            {
                try
                {
                    var point = GetPointAt(x, _eSeries.Calculate(int.Parse(Number), x));
                    g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(point.X - 2, point.Y - 2, 4, 4));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Drawing point failed: {ex.Message}");
                }
            }
        }

        public void CreateFromConfigs2()
        {
            var parameters = new DictionaryParametersProvider(new Dictionary<string, double>
            {
                { "alpha", double.Parse(Alpha) },
                { "beta", double.Parse(Beta) },
                { "omega", double.Parse(Alpha + Beta) }
            });
            var eSeriesParam = new ESeriesParam(parameters);
            var gSeries = new GTildaSeries(parameters);
            _eTildaSeries = new ETildaSeries(parameters, gSeries, eSeriesParam);

            _eSumSeries = new EFundSolutionNumerical(_eTildaSeries, new LaguerreFunction(), parameters)
            {
                N = int.Parse(Number)
            };
            _eAnalytical = new EFundSolution(parameters);
        }
        public void DrawCommand2(Graphics g)
        {
            CreateFromConfigs2();

            var x1 = double.Parse(X1);
            var x2 = double.Parse(X2);
            var dx = double.Parse(Dx);
            var t = double.Parse(T);

            for (double x = x1; x <= x2; x += dx)
            {
                try
                {
                    var point = GetPointAt(x, _eSumSeries.Calculate(x, t));
                    g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(point.X - 2, point.Y - 2, 4, 4));
                    point = GetPointAt(x, _eAnalytical.Calculate(x, t));
                    g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(point.X - 2, point.Y - 2, 4, 4));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"Drawing point failed: {ex.Message}");
                }
            }
        }

        public void TabulateCommand()
        {
            var parameters = new DictionaryParametersProvider(new Dictionary<string, double>
            {
                { "alpha", double.Parse(Alpha) },
                { "beta", double.Parse(Beta) }
            });
            int[] numbsToUse = { 0, 1, 2, 3, 4, 5, 6, 7 };

            var eSeriesParam = new ESeriesParam[numbsToUse.Length];
            var gSeries = new GSeriesCalculator[numbsToUse.Length];
            var eSeries = new ESeriesCalculator[numbsToUse.Length];
            for (int i = 0; i < numbsToUse.Length; ++i)
            {
                eSeriesParam[i] = new ESeriesParam(parameters);
                gSeries[i] = new GSeriesCalculator(parameters);
                eSeries[i] = new ESeriesCalculator(gSeries[i], eSeriesParam[i], parameters);

                eSeries[i].SetCacheSize(i + 1);
            }

            var x1 = double.Parse(X1);
            var x2 = double.Parse(X2);
            var dx = double.Parse(Dx);

            using (var stream = File.Open("../../output.txt", FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    string header = "X\t" + string.Join("\t", numbsToUse.Select(x => $"E{x}"));
                    writer.WriteLine(header);
                    for (double x = x1; x <= x2; x += dx)
                    {
                        try
                        {
                            string row = $"{x}\t" + string.Join("\t", numbsToUse.Select(i => eSeries[i].Calculate(i, x)));

                            writer.WriteLine(row);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Drawing point failed: {ex.Message}");
                        }
                    }
                }
            }
            MessageBox.Show("Finished output!");
        }
    }
}
