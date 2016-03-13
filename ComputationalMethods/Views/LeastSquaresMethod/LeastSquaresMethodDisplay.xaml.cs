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
using Numerics.Integration;
using Numerics.Models;
using Numerics.Operators.Factory;
using Numerics.Structures;
using Numerics.Algorithms;
using Numerics.Algorithms.LinearSystemSolvers;

namespace ComputationalMethods.Views.LeastSquaresMethod
{
    public partial class LeastSquaresMethodDisplay : Window
    {
        //public Matrix<Func<double, double>> Wronskian = new Matrix<Func<double, double>>(3, 2);
        //public string outputString = "u(x) = a₁x(1-x)+a₂x²(1-x), where a₁={0}, a₂={1}";
        public Matrix<Func<double, double>> Wronskian = new Matrix<Func<double, double>>(3, 3);
        public string outputString = "u(x) = a₁x(1-x)+a₂x²(1-x)+a₃x³(1-x), where a₁={0}, a₂={1}, a₃={2}";

        public Func<double, double> F = x => -x;
        public LeastSquaresDataModel Model;
        public Func<double, double> p = x => 0;
        public Func<double, double> q = x => 1;
        public double a;
        public double b;
        public double eps;
        public LeastSquaresMethodSolver leastSquaresMethod;
        public ILinearSystemSolver solver = new GaussianSolver();

        public LeastSquaresMethodDisplay()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            Wronskian[0, 0] = x => x - x * x;
            Wronskian[1, 0] = x => 1 - 2 * x;
            Wronskian[2, 0] = x => -2;

            Wronskian[0, 1] = x => x * x - x * x * x;
            Wronskian[1, 1] = x => 2 * x - 3 * x * x;
            Wronskian[2, 1] = x => 2 - 6 * x;

            Wronskian[0, 2] = x => x * x * x - x * x * x * x;
            Wronskian[1, 2] = x => 3 * x * x - 4 * x * x * x;
            Wronskian[2, 2] = x => 6 * x - 12 * x * x;

            Model = new LeastSquaresDataModel
            {
                AFactory = new LinearDifferentialSecondOrderOperatorFactory(p, q),
                BasisWronskian = Wronskian,
                Epselon = eps,
                F = F,
                Integrator = new ChebyshevQuadrature(),
                IntervalEnd = b,
                IntervalStart = a                
            };
            leastSquaresMethod = new LeastSquaresMethodSolver(Model);
        }

        private void Evaluate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ParseInput();
                Initialize();
                leastSquaresMethod.BuildMatrix();
                solver.A = leastSquaresMethod.C;
                solver.B = leastSquaresMethod.B;
                solver.Solve();
                result.Content = string.Format(outputString, solver.X[0], solver.X[1], solver.X[2]);
                //result.Content = string.Format(outputString, solver.X[0], solver.X[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ParseInput()
        {
            a = double.Parse(aTextBox.Text);
            b = double.Parse(bTextBox.Text);
            eps = double.Parse(epsTextBox.Text);
        }
    }
}
