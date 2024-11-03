using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Numerics.Integration;
using RPN;

namespace ComputationalMethods.Views.IntegrationMethods
{
    /// <summary>
    /// Interaction logic for IntegrationMainWindow.xaml
    /// </summary>
    public partial class IntegrationMainWindow : Window
    {
        private static int InitialN = 4;
        private static string ResultString = "Результат: {0}";
        private static string NString = "N: {0}";
        private static string TimeElapsedString = "Часу затрачено: {0} мс";

        private Function function;
        private double a;
        private double b;
        private double epselon;

        public IntegrationMainWindow()
        {
            InitializeComponent();
            function = new Function(new string[] { "x" });
        }

        private void Evaluate_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            RectanglesMethod integrator1 = new RectanglesMethod(InitialN);
            LeftRectanglesMethod integrator2 = new LeftRectanglesMethod(InitialN);
            RightRectanglesMethod integrator3 = new RightRectanglesMethod(InitialN);
            TrapezodialRule integrator4 = new TrapezodialRule(InitialN);
            SimpsonRule integrator5 = new SimpsonRule(InitialN);
            ThreeEightsRule integrator6 = new ThreeEightsRule(3);
            ChebyshevQuadrature integrator7 = new ChebyshevQuadrature(5);
            GaussianIntegration integrator8 = new GaussianIntegration(5);

            ParseInput();
            DoTask(rectanglesCheckBox, rectanglesRes, rectanglesN, rectanglesTime, integrator1);
            DoTask(leftRectanglesCheckBox, leftRectanglesRes, leftRectanglesN, leftRectanglesTime, integrator2);
            DoTask(rightRectanglesCheckBox, rightRectanglesRes, rightRectanglesN, rightRectanglesTime, integrator3);
            DoTask(trapezodialRuleCheckBox, trapezodialRes, trapezodialN, trapezodialTime, integrator4);
            DoTask(simpsonRuleCheckBox, simpsonRes, simpsonN, simpsonTime, integrator5);
            DoTask(threeEightsRuleCheckBox, threeEightsRuleRes, threeEightsRuleN, threeEightsRuleTime, integrator6);
            DoTask(chebyshevCheckBox, chebyshevRes, chebyshevN, chebyshevTime, integrator7);
            DoTask(gaussianCheckBox, gaussianRes, gaussianN, gaussianTime, integrator8);

            try
            {
                Function analyticalFunction = new Function(new[] { "x" });
                //analyticalFunction.Parse(analyticalFunctionTextBox.Text);
                //analyticalSolutionRes.Content = string.Format("Analytical Result: {0}", analyticalFunction.Evaluate(new double[]{b}) - analyticalFunction.Evaluate(new double[] {a}));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DoTask(CheckBox checkBox, Label res, Label n, Label time, IIntegrator integrator)
        {
            if (checkBox.IsChecked != null && (bool)checkBox.IsChecked)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                res.Content = string.Format(ResultString, function.Integrate(integrator, a, b, epselon));
                stopwatch.Stop();
                n.Content = string.Format(NString, integrator.GetSegmentsCount());
                time.Content = string.Format(TimeElapsedString, stopwatch.ElapsedMilliseconds);
            }
        }

        private void ClearOutput()
        {
            rectanglesRes.Content = string.Empty;
            rectanglesN.Content = string.Empty;
            rectanglesTime.Content = string.Empty;

            leftRectanglesRes.Content = string.Empty;
            leftRectanglesN.Content = string.Empty;
            leftRectanglesTime.Content = string.Empty;

            rightRectanglesRes.Content = string.Empty;
            rightRectanglesN.Content = string.Empty;
            rightRectanglesTime.Content = string.Empty;

            trapezodialN.Content = string.Empty;
            trapezodialRes.Content = string.Empty;
            trapezodialTime.Content = string.Empty;

            simpsonN.Content = string.Empty;
            simpsonRes.Content = string.Empty;
            simpsonTime.Content = string.Empty;

            threeEightsRuleN.Content = string.Empty;
            threeEightsRuleRes.Content = string.Empty;
            threeEightsRuleTime.Content = string.Empty;

            chebyshevN.Content = string.Empty;
            chebyshevRes.Content = string.Empty;
            chebyshevTime.Content = string.Empty;

            gaussianN.Content = string.Empty;
            gaussianRes.Content = string.Empty;
            gaussianTime.Content = string.Empty;
        }

        private void ParseInput()
        {
            function.Parse(functionTextBox.Text);
            a = double.Parse(leftBoundaryTextBox.Text);
            b = double.Parse(rightBoundaryTextBox.Text);
            epselon = double.Parse(epselonTextBox.Text);
        }
    }
}
