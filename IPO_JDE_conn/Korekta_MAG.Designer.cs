namespace IPO_JDE_conn
{
    partial class Korekta_MAG
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dg_mag = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_do_rozp = new System.Windows.Forms.Label();
            this.lb_rozpisane = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg_mag)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "ANULUJ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // dg_mag
            // 
            this.dg_mag.AllowUserToAddRows = false;
            this.dg_mag.AllowUserToDeleteRows = false;
            this.dg_mag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_mag.Location = new System.Drawing.Point(12, 12);
            this.dg_mag.Name = "dg_mag";
            this.dg_mag.Size = new System.Drawing.Size(670, 174);
            this.dg_mag.TabIndex = 1;
            this.dg_mag.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_mag_CellContentClick);
            this.dg_mag.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_mag_CellEndEdit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "DO ROZPISANIA:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ROZPISANE:";
            // 
            // lb_do_rozp
            // 
            this.lb_do_rozp.AutoSize = true;
            this.lb_do_rozp.Location = new System.Drawing.Point(123, 193);
            this.lb_do_rozp.Name = "lb_do_rozp";
            this.lb_do_rozp.Size = new System.Drawing.Size(10, 13);
            this.lb_do_rozp.TabIndex = 4;
            this.lb_do_rozp.Text = "-";
            // 
            // lb_rozpisane
            // 
            this.lb_rozpisane.AutoSize = true;
            this.lb_rozpisane.Location = new System.Drawing.Point(123, 222);
            this.lb_rozpisane.Name = "lb_rozpisane";
            this.lb_rozpisane.Size = new System.Drawing.Size(10, 13);
            this.lb_rozpisane.TabIndex = 5;
            this.lb_rozpisane.Text = "-";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(557, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "ZAPISZ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Korekta_MAG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 325);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lb_rozpisane);
            this.Controls.Add(this.lb_do_rozp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dg_mag);
            this.Controls.Add(this.button1);
            this.Name = "Korekta_MAG";
            this.Text = "Korekta_MAG";
            this.Load += new System.EventHandler(this.Korekta_MAG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_mag)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridView dg_mag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_do_rozp;
        private System.Windows.Forms.Label lb_rozpisane;
        private System.Windows.Forms.Button button2;
    }
}