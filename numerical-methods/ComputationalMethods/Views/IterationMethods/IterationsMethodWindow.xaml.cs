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
    /// Interaction logic for IterationsMethodWindow.xaml
    /// </summary>
    public partial class IterationsMethodWindow : Window
    {
        public IterationsMethodWindow()
        {
            InitializeComponent();
        }

        private void EvaluateButton_Click(object sender, RoutedEventArgs e)
        {
            var epselon = Parser.ParseInput(EpselonTextBox.Text);
            var lambda = Parser.ParseInput(LambdaTextBox.Text);
            var x0 = Parser.ParseInput(X0TextBox.Text);
            if (epselon != null && lambda != null && x0 != null)
            {
                Function expression = new Function(new[] { "x" });
                expression.Parse(Function.Text);
                IterationsMethod expr = new IterationsMethod((x) =>
                {
                    return expression.Evaluate(new[] { x });
                });
                try
                {
                    MessageBox.Show("Result: " + expr.Evaluate((double)lambda, (double)x0, (double)epselon).ToString()
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

        private void X0TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("x0");
            tb.LostFocus += Handler.GetLostFocusEventHandler("x0");
            Handler.GetLostFocusEventHandler("x0")(sender, null);
        }

        private void LambdaTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("lambda");
            tb.LostFocus += Handler.GetLostFocusEventHandler("lambda");
            Handler.GetLostFocusEventHandler("lambda")(sender, null);
        }
    }
}
