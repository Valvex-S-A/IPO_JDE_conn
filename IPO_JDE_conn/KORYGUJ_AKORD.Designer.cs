namespace IPO_JDE_conn
{
    partial class KORYGUJ_AKORD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KORYGUJ_AKORD));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_data_stop = new System.Windows.Forms.DateTimePicker();
            this.tb_opis_pracy = new System.Windows.Forms.TextBox();
            this.tb_nazwa_operacji = new System.Windows.Forms.TextBox();
            this.tb_ilosc_brak = new System.Windows.Forms.TextBox();
            this.tb_ilosc = new System.Windows.Forms.TextBox();
            this.dtp_data_start = new System.Windows.Forms.DateTimePicker();
            this.dtp_czas_stop = new System.Windows.Forms.DateTimePicker();
            this.dtp_czas_start = new System.Windows.Forms.DateTimePicker();
            this.lb_stop = new System.Windows.Forms.Label();
            this.lb_start = new System.Windows.Forms.Label();
            this.lb_opis_pracy = new System.Windows.Forms.Label();
            this.lb_nazwa_oper = new System.Windows.Forms.Label();
            this.lb_ilosc_brak = new System.Windows.Forms.Label();
            this.lb_ilosc = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbIndeks = new System.Windows.Forms.TextBox();
            this.lb_indeks = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbIndeks);
            this.groupBox1.Controls.Add(this.lb_indeks);
            this.groupBox1.Controls.Add(this.dtp_data_stop);
            this.groupBox1.Controls.Add(this.tb_opis_pracy);
            this.groupBox1.Controls.Add(this.tb_nazwa_operacji);
            this.groupBox1.Controls.Add(this.tb_ilosc_brak);
            this.groupBox1.Controls.Add(this.tb_ilosc);
            this.groupBox1.Controls.Add(this.dtp_data_start);
            this.groupBox1.Controls.Add(this.dtp_czas_stop);
            this.groupBox1.Controls.Add(this.dtp_czas_start);
            this.groupBox1.Controls.Add(this.lb_stop);
            this.groupBox1.Controls.Add(this.lb_start);
            this.groupBox1.Controls.Add(this.lb_opis_pracy);
            this.groupBox1.Controls.Add(this.lb_nazwa_oper);
            this.groupBox1.Controls.Add(this.lb_ilosc_brak);
            this.groupBox1.Controls.Add(this.lb_ilosc);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 229);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dtp_data_stop
            // 
            this.dtp_data_stop.Checked = false;
            this.dtp_data_stop.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_data_stop.Location = new System.Drawing.Point(250, 193);
            this.dtp_data_stop.Name = "dtp_data_stop";
            this.dtp_data_stop.ShowCheckBox = true;
            this.dtp_data_stop.Size = new System.Drawing.Size(157, 20);
            this.dtp_data_stop.TabIndex = 16;
            // 
            // tb_opis_pracy
            // 
            this.tb_opis_pracy.Location = new System.Drawing.Point(250, 100);
            this.tb_opis_pracy.Name = "tb_opis_pracy";
            this.tb_opis_pracy.Size = new System.Drawing.Size(245, 20);
            this.tb_opis_pracy.TabIndex = 15;
            // 
            // tb_nazwa_operacji
            // 
            this.tb_nazwa_operacji.Location = new System.Drawing.Point(250, 72);
            this.tb_nazwa_operacji.Name = "tb_nazwa_operacji";
            this.tb_nazwa_operacji.Size = new System.Drawing.Size(245, 20);
            this.tb_nazwa_operacji.TabIndex = 13;
            this.tb_nazwa_operacji.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // tb_ilosc_brak
            // 
            this.tb_ilosc_brak.Location = new System.Drawing.Point(250, 46);
            this.tb_ilosc_brak.Name = "tb_ilosc_brak";
            this.tb_ilosc_brak.Size = new System.Drawing.Size(245, 20);
            this.tb_ilosc_brak.TabIndex = 12;
            // 
            // tb_ilosc
            // 
            this.tb_ilosc.Location = new System.Drawing.Point(250, 20);
            this.tb_ilosc.Name = "tb_ilosc";
            this.tb_ilosc.Size = new System.Drawing.Size(245, 20);
            this.tb_ilosc.TabIndex = 11;
            // 
            // dtp_data_start
            // 
            this.dtp_data_start.Checked = false;
            this.dtp_data_start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_data_start.Location = new System.Drawing.Point(250, 167);
            this.dtp_data_start.Name = "dtp_data_start";
            this.dtp_data_start.ShowCheckBox = true;
            this.dtp_data_start.Size = new System.Drawing.Size(157, 20);
            this.dtp_data_start.TabIndex = 9;
            this.dtp_data_start.ValueChanged += new System.EventHandler(this.dtp_data_start_ValueChanged);
            // 
            // dtp_czas_stop
            // 
            this.dtp_czas_stop.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_czas_stop.Location = new System.Drawing.Point(413, 193);
            this.dtp_czas_stop.Name = "dtp_czas_stop";
            this.dtp_czas_stop.ShowUpDown = true;
            this.dtp_czas_stop.Size = new System.Drawing.Size(82, 20);
            this.dtp_czas_stop.TabIndex = 8;
            // 
            // dtp_czas_start
            // 
            this.dtp_czas_start.Checked = false;
            this.dtp_czas_start.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_czas_start.Location = new System.Drawing.Point(413, 167);
            this.dtp_czas_start.Name = "dtp_czas_start";
            this.dtp_czas_start.ShowUpDown = true;
            this.dtp_czas_start.Size = new System.Drawing.Size(82, 20);
            this.dtp_czas_start.TabIndex = 7;
            // 
            // lb_stop
            // 
            this.lb_stop.AutoSize = true;
            this.lb_stop.Location = new System.Drawing.Point(188, 193);
            this.lb_stop.Name = "lb_stop";
            this.lb_stop.Size = new System.Drawing.Size(56, 13);
            this.lb_stop.TabIndex = 6;
            this.lb_stop.Text = "Czas_stop";
            // 
            // lb_start
            // 
            this.lb_start.AutoSize = true;
            this.lb_start.Location = new System.Drawing.Point(188, 167);
            this.lb_start.Name = "lb_start";
            this.lb_start.Size = new System.Drawing.Size(56, 13);
            this.lb_start.TabIndex = 5;
            this.lb_start.Text = "Czas_start";
            // 
            // lb_opis_pracy
            // 
            this.lb_opis_pracy.Location = new System.Drawing.Point(9, 100);
            this.lb_opis_pracy.Name = "lb_opis_pracy";
            this.lb_opis_pracy.Size = new System.Drawing.Size(235, 20);
            this.lb_opis_pracy.TabIndex = 4;
            this.lb_opis_pracy.Text = "label1";
            this.lb_opis_pracy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_nazwa_oper
            // 
            this.lb_nazwa_oper.Location = new System.Drawing.Point(12, 72);
            this.lb_nazwa_oper.Name = "lb_nazwa_oper";
            this.lb_nazwa_oper.Size = new System.Drawing.Size(232, 20);
            this.lb_nazwa_oper.TabIndex = 2;
            this.lb_nazwa_oper.Text = "label1";
            this.lb_nazwa_oper.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_ilosc_brak
            // 
            this.lb_ilosc_brak.Location = new System.Drawing.Point(9, 46);
            this.lb_ilosc_brak.Name = "lb_ilosc_brak";
            this.lb_ilosc_brak.Size = new System.Drawing.Size(235, 20);
            this.lb_ilosc_brak.TabIndex = 1;
            this.lb_ilosc_brak.Text = "label1";
            this.lb_ilosc_brak.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_ilosc
            // 
            this.lb_ilosc.Location = new System.Drawing.Point(6, 20);
            this.lb_ilosc.Name = "lb_ilosc";
            this.lb_ilosc.Size = new System.Drawing.Size(238, 23);
            this.lb_ilosc.TabIndex = 0;
            this.lb_ilosc.Text = "label1";
            this.lb_ilosc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "ANULUJ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(434, 249);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "ZAPISZ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbIndeks
            // 
            this.tbIndeks.Location = new System.Drawing.Point(250, 129);
            this.tbIndeks.Name = "tbIndeks";
            this.tbIndeks.Size = new System.Drawing.Size(245, 20);
            this.tbIndeks.TabIndex = 18;
            // 
            // lb_indeks
            // 
            this.lb_indeks.Location = new System.Drawing.Point(9, 129);
            this.lb_indeks.Name = "lb_indeks";
            this.lb_indeks.Size = new System.Drawing.Size(235, 20);
            this.lb_indeks.TabIndex = 17;
            this.lb_indeks.Text = "label1";
            this.lb_indeks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // KORYGUJ_AKORD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 286);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KORYGUJ_AKORD";
            this.Text = "KORYGUJ_AKORD";
            this.Load += new System.EventHandler(this.KORYGUJ_AKORD_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_stop;
        private System.Windows.Forms.Label lb_start;
        private System.Windows.Forms.Label lb_opis_pracy;
        private System.Windows.Forms.Label lb_nazwa_oper;
        private System.Windows.Forms.Label lb_ilosc_brak;
        private System.Windows.Forms.Label lb_ilosc;
        private System.Windows.Forms.TextBox tb_nazwa_operacji;
        private System.Windows.Forms.TextBox tb_ilosc_brak;
        private System.Windows.Forms.TextBox tb_ilosc;
        private System.Windows.Forms.DateTimePicker dtp_data_start;
        private System.Windows.Forms.DateTimePicker dtp_czas_stop;
        private System.Windows.Forms.DateTimePicker dtp_czas_start;
        private System.Windows.Forms.TextBox tb_opis_pracy;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dtp_data_stop;
        private System.Windows.Forms.TextBox tbIndeks;
        private System.Windows.Forms.Label lb_indeks;
    }
}