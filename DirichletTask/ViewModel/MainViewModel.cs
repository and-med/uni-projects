using DirichletTask.Core.Abstraction.Cache;
using DirichletTask.Core.Parameters;
using DirichletTask.Core.Series;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DirichletTask.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _number = "1";
        private string _kappa = "1";

        private string x1 = "0.1";
        private string x2 = "1";
        private string y1 = "-0.03";
        private string y2 = "1";
        private string dx = "0.01";
        private double width;
        private double height;
        private ObservableCollection<Ellipse> _dots;

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
                Set(() => Number, ref _number, value);
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
                Set(() => Kappa, ref _kappa, value);
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
                Set(() => X1, ref x1, value);
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
                Set(() => X2, ref x2, value);
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
                Set(() => Y1, ref y1, value);
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
                Set(() => Y2, ref y2, value);
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
                Set(() => Dx, ref dx, value);
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
                Set(() => Width, ref width, value);
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
                Set(() => Height, ref height, value);
            }
        }

        public ObservableCollection<Ellipse> Dots
        {
            get
            {
                return _dots;
            }
            set
            {
                Set(() => Dots, ref _dots, value);
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

            return new Point(((x - x1) / (x2 - x1)) * 1000, (y2 - y) / (y2 - y1) * 500);
        }

        private Ellipse GetEllipseByPoint(Point p)
        {
            Ellipse dot = new Ellipse();
            dot.Stroke = new SolidColorBrush(Colors.Black);
            dot.StrokeThickness = 3;
            dot.Height = 4;
            dot.Width = 4;
            dot.Fill = new SolidColorBrush(Colors.Black);
            dot.Margin = new Thickness(p.X, p.Y, 0, 0);
            return dot;
        }

        public void DrawCommand()
        {
            CreateFromConfigs();

            var x1 = double.Parse(X1);
            var x2 = double.Parse(X2);
            var dx = double.Parse(Dx);

            for(double x = x1; x <= x2; ++dx)
            {
                var point = GetPointAt(x, _eSeries.Calculate(int.Parse(Number), x));
                var ellipse = GetEllipseByPoint(point);
                Dots.Add(ellipse);
            }
        }
    }
}