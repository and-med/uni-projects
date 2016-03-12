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
using System.Windows.Shapes;
using ComputationalMethods.Views.IntegrationMethods;
using ComputationalMethods.Views.IterationMethods;

namespace ComputationalMethods
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

        private void IterationMethods_Click(object sender, RoutedEventArgs e)
        {
            IterationMethodsMainWindow window = new IterationMethodsMainWindow();
            window.Show();
        }

        private void Integration_Click(object sender, RoutedEventArgs e)
        {
            IntegrationMainWindow window = new IntegrationMainWindow();
            window.Show();
        }
    }
}
