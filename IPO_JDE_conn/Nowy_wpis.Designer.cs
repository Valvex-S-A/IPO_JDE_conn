namespace IPO_JDE_conn
{
    partial class Nowy_wpis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Nowy_wpis));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dd_operacja = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_ilosc_brak = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_ilosc = new System.Windows.Forms.TextBox();
            this.dtp_stop_czas = new System.Windows.Forms.DateTimePicker();
            this.dtp_stop_data = new System.Windows.Forms.DateTimePicker();
            this.dtp_start_czas = new System.Windows.Forms.DateTimePicker();
            this.dtp_start_data = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.ddPracownik = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_nr_masz = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_opis = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_nr_zlecenia = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_id_pracownika = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.CausesValidation = false;
            this.groupBox1.Controls.Add(this.tb_id_pracownika);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.dd_operacja);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tb_ilosc_brak);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_ilosc);
            this.groupBox1.Controls.Add(this.dtp_stop_czas);
            this.groupBox1.Controls.Add(this.dtp_stop_data);
            this.groupBox1.Controls.Add(this.dtp_start_czas);
            this.groupBox1.Controls.Add(this.dtp_start_data);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ddPracownik);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_nr_masz);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_opis);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_nr_zlecenia);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 430);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dodatkowy zapis do akordu";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dd_operacja
            // 
            this.dd_operacja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dd_operacja.FormattingEnabled = true;
            this.dd_operacja.Location = new System.Drawing.Point(145, 69);
            this.dd_operacja.Name = "dd_operacja";
            this.dd_operacja.Size = new System.Drawing.Size(141, 21);
            this.dd_operacja.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(84, 310);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Ilość brak";
            // 
            // tb_ilosc_brak
            // 
            this.tb_ilosc_brak.Location = new System.Drawing.Point(146, 307);
            this.tb_ilosc_brak.Name = "tb_ilosc_brak";
            this.tb_ilosc_brak.Size = new System.Drawing.Size(141, 20);
            this.tb_ilosc_brak.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(75, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "CZAS STOP";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 218);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "CZAS START";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ilość sztuk";
            // 
            // tb_ilosc
            // 
            this.tb_ilosc.Location = new System.Drawing.Point(145, 279);
            this.tb_ilosc.Name = "tb_ilosc";
            this.tb_ilosc.Size = new System.Drawing.Size(141, 20);
            this.tb_ilosc.TabIndex = 14;
            // 
            // dtp_stop_czas
            // 
            this.dtp_stop_czas.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_stop_czas.Location = new System.Drawing.Point(301, 241);
            this.dtp_stop_czas.Name = "dtp_stop_czas";
            this.dtp_stop_czas.ShowUpDown = true;
            this.dtp_stop_czas.Size = new System.Drawing.Size(83, 20);
            this.dtp_stop_czas.TabIndex = 13;
            // 
            // dtp_stop_data
            // 
            this.dtp_stop_data.Location = new System.Drawing.Point(145, 241);
            this.dtp_stop_data.Name = "dtp_stop_data";
            this.dtp_stop_data.Size = new System.Drawing.Size(150, 20);
            this.dtp_stop_data.TabIndex = 12;
            // 
            // dtp_start_czas
            // 
            this.dtp_start_czas.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_start_czas.Location = new System.Drawing.Point(301, 215);
            this.dtp_start_czas.Name = "dtp_start_czas";
            this.dtp_start_czas.ShowUpDown = true;
            this.dtp_start_czas.Size = new System.Drawing.Size(83, 20);
            this.dtp_start_czas.TabIndex = 11;
            // 
            // dtp_start_data
            // 
            this.dtp_start_data.Location = new System.Drawing.Point(145, 215);
            this.dtp_start_data.Name = "dtp_start_data";
            this.dtp_start_data.Size = new System.Drawing.Size(150, 20);
            this.dtp_start_data.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Pracownik";
            // 
            // ddPracownik
            // 
            this.ddPracownik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddPracownik.FormattingEnabled = true;
            this.ddPracownik.Location = new System.Drawing.Point(145, 162);
            this.ddPracownik.Name = "ddPracownik";
            this.ddPracownik.Size = new System.Drawing.Size(141, 21);
            this.ddPracownik.TabIndex = 8;
            this.ddPracownik.SelectedIndexChanged += new System.EventHandler(this.ddPracownik_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nr maszyny";
            // 
            // tb_nr_masz
            // 
            this.tb_nr_masz.Location = new System.Drawing.Point(145, 133);
            this.tb_nr_masz.Name = "tb_nr_masz";
            this.tb_nr_masz.Size = new System.Drawing.Size(141, 20);
            this.tb_nr_masz.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Opis operacji";
            // 
            // tb_opis
            // 
            this.tb_opis.Location = new System.Drawing.Point(145, 100);
            this.tb_opis.Name = "tb_opis";
            this.tb_opis.Size = new System.Drawing.Size(141, 20);
            this.tb_opis.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Grupa operacji";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nr zlecenia";
            // 
            // tb_nr_zlecenia
            // 
            this.tb_nr_zlecenia.Location = new System.Drawing.Point(145, 39);
            this.tb_nr_zlecenia.Name = "tb_nr_zlecenia";
            this.tb_nr_zlecenia.Size = new System.Drawing.Size(141, 20);
            this.tb_nr_zlecenia.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(353, 449);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "ZAPISZ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 449);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 2;
            this.button2.Text = "ANULUJ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(67, 192);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Id Pracownika";
            // 
            // tb_id_pracownika
            // 
            this.tb_id_pracownika.Location = new System.Drawing.Point(145, 190);
            this.tb_id_pracownika.Name = "tb_id_pracownika";
            this.tb_id_pracownika.Size = new System.Drawing.Size(74, 20);
            this.tb_id_pracownika.TabIndex = 22;
            // 
            // Nowy_wpis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 494);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Nowy_wpis";
            this.Text = "Nowy_wpis";
            this.Load += new System.EventHandler(this.Nowy_wpis_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_ilosc;
        private System.Windows.Forms.DateTimePicker dtp_stop_czas;
        private System.Windows.Forms.DateTimePicker dtp_stop_data;
        private System.Windows.Forms.DateTimePicker dtp_start_czas;
        private System.Windows.Forms.DateTimePicker dtp_start_data;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ddPracownik;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_nr_masz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_opis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_nr_zlecenia;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_ilosc_brak;
        private System.Windows.Forms.ComboBox dd_operacja;
        private System.Windows.Forms.TextBox tb_id_pracownika;
        private System.Windows.Forms.Label label10;
    }
}