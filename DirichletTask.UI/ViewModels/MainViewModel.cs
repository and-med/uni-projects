using DirichletTask.Core.Abstraction.Cache;
using DirichletTask.Core.Parameters;
using DirichletTask.Core.Series;
using System;
using System.Collections.Generic;
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
        
        public void TabulateCommand()
        {
            var parameters = new DictionaryParametersProvider(new Dictionary<string, double>
            {
                { "alpha", double.Parse(Alpha) },
                { "beta", double.Parse(Beta) }
            });
            int[] numbsToUse = { 0, 1, 2, 3, 4, 5, 6 };

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
            //var e0SeriesParam = new ESeriesParam(parameters);
            //var g0Series = new GSeriesCalculator(parameters);
            //var e0series = new ESeriesCalculator(g0Series, e0SeriesParam, parameters);
            //e0series.SetCacheSize(1);
            //var e1SeriesParam = new ESeriesParam(parameters);
            //var g1Series = new GSeriesCalculator(parameters);
            //var e1series = new ESeriesCalculator(g1Series, e1SeriesParam, parameters);
            //e1series.SetCacheSize(2);
            //var e5SeriesParam = new ESeriesParam(parameters);
            //var g5Series = new GSeriesCalculator(parameters);
            //var e5series = new ESeriesCalculator(g5Series, e5SeriesParam, parameters);
            //e5series.SetCacheSize(6);
            //var e6SeriesParam = new ESeriesParam(parameters);
            //var g6Series = new GSeriesCalculator(parameters);
            //var e6series = new ESeriesCalculator(g6Series, e6SeriesParam, parameters);
            //e6series.SetCacheSize(7);
            //var e7SeriesParam = new ESeriesParam(parameters);
            //var g7Series = new GSeriesCalculator(parameters);
            //var e7series = new ESeriesCalculator(g7Series, e7SeriesParam, parameters);
            //e7series.SetCacheSize(8);
            //var e10SeriesParam = new ESeriesParam(parameters);
            //var g10Series = new GSeriesCalculator(parameters);
            //var e10series = new ESeriesCalculator(g10Series, e10SeriesParam, parameters);
            //e10series.SetCacheSize(11);

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
