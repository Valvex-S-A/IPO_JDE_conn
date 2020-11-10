namespace IPO_JDE_conn
{
    partial class KORYGUJ_ZLEC
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
            this.button1 = new System.Windows.Forms.Button();
            this.lb_nazwa_zlec = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_ilosc = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "ANULUJ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_nazwa_zlec
            // 
            this.lb_nazwa_zlec.AutoSize = true;
            this.lb_nazwa_zlec.Location = new System.Drawing.Point(13, 13);
            this.lb_nazwa_zlec.Name = "lb_nazwa_zlec";
            this.lb_nazwa_zlec.Size = new System.Drawing.Size(35, 13);
            this.lb_nazwa_zlec.TabIndex = 1;
            this.lb_nazwa_zlec.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(429, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "UWAGA! KOREKTA DOTYCZY TYLKO RW.          PW i PU muszisz skorygować ręcznie";
            // 
            // lb_ilosc
            // 
            this.lb_ilosc.AutoSize = true;
            this.lb_ilosc.Location = new System.Drawing.Point(12, 46);
            this.lb_ilosc.Name = "lb_ilosc";
            this.lb_ilosc.Size = new System.Drawing.Size(42, 13);
            this.lb_ilosc.TabIndex = 3;
            this.lb_ilosc.Text = "lb_ilosc";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 82);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "NOWA ILOŚĆ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(338, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 33);
            this.button2.TabIndex = 6;
            this.button2.Text = "ZAKSIĘGUJ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // KORYGUJ_ZLEC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 204);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lb_ilosc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_nazwa_zlec);
            this.Controls.Add(this.button1);
            this.Name = "KORYGUJ_ZLEC";
            this.Text = "KORYGUJ_ZLEC";
            this.Load += new System.EventHandler(this.KORYGUJ_ZLEC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lb_nazwa_zlec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_ilosc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}