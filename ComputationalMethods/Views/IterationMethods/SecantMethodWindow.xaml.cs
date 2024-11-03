using ComputationalMethods.Helpers;
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
using RPN;
using Numerics.Approximation;

namespace ComputationalMethods.Views.IterationMethods
{
    /// <summary>
    /// Interaction logic for SecantMethodWindow.xaml
    /// </summary>
    public partial class SecantMethodWindow : Window
    {
        public SecantMethodWindow()
        {
            InitializeComponent();
        }

        private void EvaluateButton_Click(object sender, RoutedEventArgs e)
        {
            var a = Parser.ParseInput(LeftIntervalSideTextBox.Text);
            var b = Parser.ParseInput(RightIntervalSideTextBox.Text);
            var epselon = Parser.ParseInput(EpselonTextBox.Text);
            if (a != null && b != null && epselon != null)
            {
                Function expression = new Function(new[] { "x" });
                expression.Parse(Function.Text);
                SecantMethod expr = new SecantMethod((x) =>
                    {
                        return expression.Evaluate(new [] { x });
                    });
                try
                {
                    MessageBox.Show("Result: " + expr.Evaluate((double)a, (double)b, (double)epselon).ToString()
                        + "\nIterations: " + expr.NumOfIteration, "Result");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                }
            }
        }
        private void LeftIntervalSideTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("a");
            tb.LostFocus += Handler.GetLostFocusEventHandler("a");
            Handler.GetLostFocusEventHandler("a")(sender, null);
        }

        private void RightIntervalSideTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("b");
            tb.LostFocus += Handler.GetLostFocusEventHandler("b");
            Handler.GetLostFocusEventHandler("b")(sender, null);
        }

        private void EpselonTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("epselon");
            tb.LostFocus += Handler.GetLostFocusEventHandler("epselon");
            Handler.GetLostFocusEventHandler("epselon")(sender, null);
        }

        private void Function_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("function");
            tb.LostFocus += Handler.GetLostFocusEventHandler("function");
            Handler.GetLostFocusEventHandler("function")(sender, null);
        }
    }
}
