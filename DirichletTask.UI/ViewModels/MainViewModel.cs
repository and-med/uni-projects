using DirichletTask.Core.Abstraction.Cache;
using DirichletTask.Core.Parameters;
using DirichletTask.Core.Series;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DirichletTask.UI.ViewModels
{
    public class MainViewModel
    {
        private string _number = "1";
        private string _kappa = "1";

        private string x1 = "0";
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
            var parameters = new DictionaryParametersProvider(new Dictionary<string, double> { { "sigma", double.Parse(Kappa) } });
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
    }
}
