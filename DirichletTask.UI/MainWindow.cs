using DirichletTask.UI.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DirichletTask.UI
{
    public partial class MainWindow : Form
    {
        MainViewModel _viewModel;
        private int _countOfLabels = 5;
        private int _startMargin;
        private List<Label> _currLabels;
        private bool _canPaint = false;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel();
            _currLabels = new List<Label>();
            CopyViewModelValuesToText();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_canPaint)
            {
                _viewModel.Height = pictureBox1.Height;
                _viewModel.Width = pictureBox1.Width;

                _viewModel.DrawCommand(e.Graphics);
            }
        }

        private void btnDraw_Click(object sender, System.EventArgs e)
        {
            _canPaint = true;
            CopyTextValuesToViewModel();
            PutLabels();
            pictureBox1.Refresh();
        }

        private void CopyTextValuesToViewModel()
        {
            _viewModel.Number = txtNumber.Text;
            _viewModel.Kappa = txtKappa.Text;
            _viewModel.X1 = txtX1.Text;
            _viewModel.X2 = txtX2.Text;
            _viewModel.Y1 = txtY1.Text;
            _viewModel.Y2 = txtY2.Text;
            _viewModel.Dx = txtDx.Text;
            _viewModel.Alpha = txtAlpha.Text;
            _viewModel.Beta = txtBeta.Text;
        }

        private void CopyViewModelValuesToText()
        {
            txtNumber.Text = _viewModel.Number;
            txtKappa.Text = _viewModel.Kappa;
            txtX1.Text = _viewModel.X1;
            txtX2.Text = _viewModel.X2;
            txtY1.Text = _viewModel.Y1;
            txtY2.Text = _viewModel.Y2;
            txtDx.Text = _viewModel.Dx;
            txtAlpha.Text = _viewModel.Alpha;
            txtBeta.Text = _viewModel.Beta;
        }

        private void PutLabels()
        {
            foreach (var label in _currLabels)
            {
                pictureBox1.Controls.Remove(label);
            }

            _currLabels.Clear();

            for (int i = 0; i <= _countOfLabels; ++i)
            {
                //vertical label
                var label = new Label();
                var y1 = double.Parse(_viewModel.Y1);
                var y2 = double.Parse(_viewModel.Y2);
                label.AutoSize = true;
                label.Text = (y2 - i * ((y2 - y1)/(double)_countOfLabels)).ToString();

                label.Location = new Point(20, _startMargin + i * (pictureBox1.Height / _countOfLabels));
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.Black;

                _currLabels.Add(label);
                pictureBox1.Controls.Add(label);
                label.BringToFront();

                //horizontal label
                var label2 = new Label();
                var x1 = double.Parse(_viewModel.X1);
                var x2 = double.Parse(_viewModel.X2);
                label2.AutoSize = true;
                label2.Text = (x1 + i * ((x2 - x1) / (double)_countOfLabels)).ToString();

                label2.Location = new Point(i * ((pictureBox1.Width) / _countOfLabels), pictureBox1.Height - 20);
                label2.BackColor = Color.Transparent;
                label2.ForeColor = Color.Black;

                _currLabels.Add(label2);
                pictureBox1.Controls.Add(label2);
                label2.BringToFront();
            }
        }

        private void btnTabulateToFile_Click(object sender, System.EventArgs e)
        {
            CopyTextValuesToViewModel();
            _viewModel.TabulateCommand();
        }
    }
}
