using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RPN;
using ComputationalMethods.Helpers;

namespace ComputationalMethods.Views.IterationMethods
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class IterationMethodsMainWindow : Window
    {
        public IterationMethodsMainWindow()
        {
            InitializeComponent();
        }

        private void NewtonMethodButton_Click(object sender, RoutedEventArgs e)
        {
            NewtonMethodWindow window = new NewtonMethodWindow();
            window.Show();
        }

        private void SecantMethodButton_Click(object sender, RoutedEventArgs e)
        {
            SecantMethodWindow window = new SecantMethodWindow();
            window.Show();
        }

        private void CombinedMethodButton_Click(object sender, RoutedEventArgs e)
        {
            CombinedMethodWindow window = new CombinedMethodWindow();
            window.Show();
        }

        private void IterationsMethodButton_Click(object sender, RoutedEventArgs e)
        {
            IterationsMethodWindow window = new IterationsMethodWindow();
            window.Show();
        }

        private void DichotomyMethodButton_Click(object sender, RoutedEventArgs e)
        {
            DichotomyMethodWindow window = new DichotomyMethodWindow();
            window.Show();
        }

        private void LeftIntervalSideTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox) sender;
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

        private void Function1StDerative_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("1st derivative");
            tb.LostFocus += Handler.GetLostFocusEventHandler("1st derivative");
            Handler.GetLostFocusEventHandler("1st derivative")(sender, null);
        }

        private void Function2NdDerative_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("2nd derivative");
            tb.LostFocus += Handler.GetLostFocusEventHandler("2nd derivative");
            Handler.GetLostFocusEventHandler("2nd derivative")(sender, null);
        }

        private void EvaluateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Function expression = new Function(new[] { "x", "y" });
                expression.Parse(Function.Text);
                double x = (double)Parser.ParseInput(XValue.Text);
                double y = (double)Parser.ParseInput(YValue.Text);
                MessageBox.Show(expression.Evaluate(new[] { x, y }).ToString(), "Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void XValue_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("x");
            tb.LostFocus += Handler.GetLostFocusEventHandler("x");
            Handler.GetLostFocusEventHandler("x")(sender, null);
        }

        private void YValue_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.GotFocus += Handler.GetGotFocusEventHandler("y");
            tb.LostFocus += Handler.GetLostFocusEventHandler("y");
            Handler.GetLostFocusEventHandler("y")(sender, null);
        }
    }
}
