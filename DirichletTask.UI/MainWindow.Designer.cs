namespace DirichletTask.UI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.lblNumber = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtKappa = new System.Windows.Forms.TextBox();
            this.lblKappa = new System.Windows.Forms.Label();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.lblX1 = new System.Windows.Forms.Label();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.lblX2 = new System.Windows.Forms.Label();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDx = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAlpha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBeta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTabulateToFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1064, 475);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(897, 25);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 38);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(16, 15);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(16, 13);
            this.lblNumber.TabIndex = 2;
            this.lblNumber.Text = "n:";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(57, 12);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(100, 20);
            this.txtNumber.TabIndex = 3;
            // 
            // txtKappa
            // 
            this.txtKappa.Location = new System.Drawing.Point(57, 43);
            this.txtKappa.Name = "txtKappa";
            this.txtKappa.Size = new System.Drawing.Size(100, 20);
            this.txtKappa.TabIndex = 5;
            // 
            // lblKappa
            // 
            this.lblKappa.AutoSize = true;
            this.lblKappa.Location = new System.Drawing.Point(16, 46);
            this.lblKappa.Name = "lblKappa";
            this.lblKappa.Size = new System.Drawing.Size(40, 13);
            this.lblKappa.TabIndex = 4;
            this.lblKappa.Text = "kappa:";
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(234, 12);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(100, 20);
            this.txtX1.TabIndex = 7;
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.Location = new System.Drawing.Point(193, 15);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(21, 13);
            this.lblX1.TabIndex = 6;
            this.lblX1.Text = "x1:";
            // 
            // txtX2
            // 
            this.txtX2.Location = new System.Drawing.Point(234, 43);
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(100, 20);
            this.txtX2.TabIndex = 9;
            // 
            // lblX2
            // 
            this.lblX2.AutoSize = true;
            this.lblX2.Location = new System.Drawing.Point(193, 46);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(18, 13);
            this.lblX2.TabIndex = 8;
            this.lblX2.Text = "x2";
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(419, 12);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(100, 20);
            this.txtY1.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(378, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "y1";
            // 
            // txtY2
            // 
            this.txtY2.Location = new System.Drawing.Point(419, 43);
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(100, 20);
            this.txtY2.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(378, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "y2";
            // 
            // txtDx
            // 
            this.txtDx.Location = new System.Drawing.Point(609, 12);
            this.txtDx.Name = "txtDx";
            this.txtDx.Size = new System.Drawing.Size(100, 20);
            this.txtDx.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(568, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "dx:";
            // 
            // txtAlpha
            // 
            this.txtAlpha.Location = new System.Drawing.Point(609, 43);
            this.txtAlpha.Name = "txtAlpha";
            this.txtAlpha.Size = new System.Drawing.Size(100, 20);
            this.txtAlpha.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(568, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "alpha:";
            // 
            // txtBeta
            // 
            this.txtBeta.Location = new System.Drawing.Point(791, 12);
            this.txtBeta.Name = "txtBeta";
            this.txtBeta.Size = new System.Drawing.Size(100, 20);
            this.txtBeta.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(750, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "beta:";
            // 
            // btnTabulateToFile
            // 
            this.btnTabulateToFile.Location = new System.Drawing.Point(977, 25);
            this.btnTabulateToFile.Name = "btnTabulateToFile";
            this.btnTabulateToFile.Size = new System.Drawing.Size(75, 38);
            this.btnTabulateToFile.TabIndex = 20;
            this.btnTabulateToFile.Text = "Tabulate";
            this.btnTabulateToFile.UseVisualStyleBackColor = true;
            this.btnTabulateToFile.Click += new System.EventHandler(this.btnTabulateToFile_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 561);
            this.Controls.Add(this.btnTabulateToFile);
            this.Controls.Add(this.txtBeta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAlpha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDx);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtY2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtY1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtX2);
            this.Controls.Add(this.lblX2);
            this.Controls.Add(this.txtX1);
            this.Controls.Add(this.lblX1);
            this.Controls.Add(this.txtKappa);
            this.Controls.Add(this.lblKappa);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainWindow";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtKappa;
        private System.Windows.Forms.Label lblKappa;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.TextBox txtX2;
        private System.Windows.Forms.Label lblX2;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtY2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAlpha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBeta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTabulateToFile;
    }
}

