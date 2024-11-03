using System;
using System.Collections.Generic;
using System.IO;
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
using Numerics.Algorithms;
using Numerics.Algorithms.LinearSystemSolvers;
using Numerics.Models;
using Numerics.Structures;

namespace ComputationalMethods.Views.LeastSquaresMethod
{
    /// <summary>
    /// Interaction logic for LinearLeastSquaresMethod.xaml
    /// </summary>
    public partial class LinearLeastSquaresMethod : Window
    {
        private Matrix<double> A;
        private Vector<double> B;
        private int M = 0;
        private int N = 0;
        LinearLeastSquaresDataModel Model;
        LinearLeastSquaresMethodSolver Solver;
        ILinearSystemSolver LinearSolver = new GaussianSolver();

        public LinearLeastSquaresMethod()
        {
            InitializeComponent();
            InitMethod("..\\..\\Content\\LeastSquaresInputData.txt");
            Solve();
            OutputResult("..\\..\\Content\\LeastSquaresOutputData.txt");
        }

        public void Solve()
        {
            Solver.BuildMatrix();
            LinearSolver.A = Solver.A;
            LinearSolver.B = Solver.B;
            LinearSolver.Solve();
        }

        public void InitMethod(string filename)
        {
            ParseFile(filename);
            Model = new LinearLeastSquaresDataModel
            {
                A = A,
                B = B,
                M = M,
                N = N
            };
            Solver = new LinearLeastSquaresMethodSolver(Model);
        }

        public void OutputResult(string filename)
        {
            var sb = new StringBuilder();
            using (var stream = new StreamWriter(filename))
            {
                for(int i = 0; i < N; ++i)
                {
                    stream.WriteLine(string.Format("x[{0}] = {1}", i, LinearSolver.X[i]));
                    sb.AppendFormat("x[{0}] = {1}, ", i, LinearSolver.X[i]);
                }
            }
            resultLabel.Content = sb.ToString();
        }

        public void ParseFile(string filename)
        {
            List<double[]> matrix = new List<double[]>();
            List<double> vector = new List<double>();
            using(var stream = new StreamReader(filename))
            {
                var line = stream.ReadLine();
                string[] splittedArray = line.Split(new char[] { ' ', '\t', '\n' });
                N = splittedArray.Length - 1;
                while(!string.IsNullOrEmpty(line))
                {
                    double[] lineValue = new double[N];
                    for(int i = 0; i < N; ++i)
                    {
                        lineValue[i] = double.Parse(splittedArray[i]);
                    }
                    matrix.Add(lineValue);
                    vector.Add(double.Parse(splittedArray[N]));
                    M++;
                    line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    splittedArray = line.Split(new char[] { ' ', '\t', '\n' });
                }
            }
            A = new Matrix<double>(M, N);
            B = new Vector<double>(M);
            for(int i = 0; i < M; ++i)
            {
                for(int j = 0; j < N; ++j)
                {
                    A[i, j] = matrix[i][j];
                }
                B[i] = vector[i];
            }
        }
    }
}
