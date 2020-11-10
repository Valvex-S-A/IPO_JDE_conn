namespace IPO_JDE_conn
{
    partial class Skasuj_zlecenie
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRW = new System.Windows.Forms.CheckBox();
            this.cbPW = new System.Windows.Forms.CheckBox();
            this.cbPU = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_nr_zlecenia = new System.Windows.Forms.TextBox();
            this.Anuluj = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_nr_zlecenia);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbPU);
            this.groupBox1.Controls.Add(this.cbPW);
            this.groupBox1.Controls.Add(this.cbRW);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skasuj zlecenie";
            // 
            // cbRW
            // 
            this.cbRW.AutoSize = true;
            this.cbRW.Checked = true;
            this.cbRW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRW.Location = new System.Drawing.Point(6, 20);
            this.cbRW.Name = "cbRW";
            this.cbRW.Size = new System.Drawing.Size(45, 17);
            this.cbRW.TabIndex = 0;
            this.cbRW.Text = "RW";
            this.cbRW.UseVisualStyleBackColor = true;
            // 
            // cbPW
            // 
            this.cbPW.AutoSize = true;
            this.cbPW.Checked = true;
            this.cbPW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPW.Location = new System.Drawing.Point(6, 38);
            this.cbPW.Name = "cbPW";
            this.cbPW.Size = new System.Drawing.Size(44, 17);
            this.cbPW.TabIndex = 1;
            this.cbPW.Text = "PW";
            this.cbPW.UseVisualStyleBackColor = true;
            // 
            // cbPU
            // 
            this.cbPU.AutoSize = true;
            this.cbPU.Checked = true;
            this.cbPU.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPU.Location = new System.Drawing.Point(6, 57);
            this.cbPU.Name = "cbPU";
            this.cbPU.Size = new System.Drawing.Size(41, 17);
            this.cbPU.TabIndex = 2;
            this.cbPU.Text = "PU";
            this.cbPU.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nr zlecenia IPO";
            // 
            // tb_nr_zlecenia
            // 
            this.tb_nr_zlecenia.Location = new System.Drawing.Point(95, 81);
            this.tb_nr_zlecenia.Name = "tb_nr_zlecenia";
            this.tb_nr_zlecenia.Size = new System.Drawing.Size(100, 20);
            this.tb_nr_zlecenia.TabIndex = 4;
            // 
            // Anuluj
            // 
            this.Anuluj.Location = new System.Drawing.Point(19, 174);
            this.Anuluj.Name = "Anuluj";
            this.Anuluj.Size = new System.Drawing.Size(75, 23);
            this.Anuluj.TabIndex = 1;
            this.Anuluj.Text = "Anuluj";
            this.Anuluj.UseVisualStyleBackColor = true;
            this.Anuluj.Click += new System.EventHandler(this.Anuluj_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(222, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "SKASUJ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fragment indeksu";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 117);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            // 
            // Skasuj_zlecenie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 218);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Anuluj);
            this.Controls.Add(this.groupBox1);
            this.Name = "Skasuj_zlecenie";
            this.Text = "Skasuj_zlecenie";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_nr_zlecenia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbPU;
        private System.Windows.Forms.CheckBox cbPW;
        private System.Windows.Forms.CheckBox cbRW;
        private System.Windows.Forms.Button Anuluj;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}