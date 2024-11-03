using DirichletTask.Core.Abstraction.Series;
using DirichletTask.Core.Parameters;
using DirichletTask.Core.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DirichletTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double x1 = 0.1;
        double x2 = 1;
        double y1 = -0.03;
        double y2 = 1;
        double width = 1000;
        double height = 500;
        double dx = 0.01;
        double sigma = 1;

        int n = 0;

        private Point GetPointAt(double x, double y)
        {
            return new Point(((x - x1) / (x2 - x1)) * 1000, (y2 - y) / (y2 - y1) * 500);
        }

        private ISingleValuedSeriesCalculator<int, double, double> DoDI()
        {
            var parameters = new DictionaryParametersProvider(new Dictionary<string, double> { { "sigma", sigma } });
            var eSeriesParam = new ESeriesParam(parameters);
            var gSeries = new GSeriesCalculator(parameters);
            var eSeries = new ESeriesCalculator(gSeries, eSeriesParam, parameters);

            eSeries.SetCacheSize(n);

            return eSeries;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var calculator = DoDI();
            
            for (double x = x1; x < x2; x += dx)
            {
                var p = GetPointAt(x, calculator.Calculate(n, x));
                Ellipse dot = new Ellipse();
                dot.Stroke = new SolidColorBrush(Colors.Black);
                dot.StrokeThickness = 3;
                dot.Height = 4;
                dot.Width = 4;
                dot.Fill = new SolidColorBrush(Colors.Black);
                dot.Margin = new Thickness(p.X, p.Y, 0, 0);
                canvas.Children.Add(dot);
            }
        }
    }
}
