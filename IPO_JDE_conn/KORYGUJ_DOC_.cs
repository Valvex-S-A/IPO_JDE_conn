using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPO_JDE_conn
{
    public partial class KORYGUJ_DOC_ : Form
    {

        public int _nr_zlec;
        public double _stara_ilosc;
        public double _nowa_ilosc;
        public string _komentarz;

        public KORYGUJ_DOC_(int nr_zlec,double ilosc)
        {
            InitializeComponent();
            _nr_zlec = nr_zlec;
            _stara_ilosc = ilosc;
            label1.Text = "KOREKTA ILOŚIOWA ZLECENIA NR:" + _nr_zlec.ToString() ;
        }

        private void KORYGUJ_DOC__Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            _nr_zlec = 0;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length<6)
            {
                MessageBox.Show("PODAJ POWÓD KOREKTY - min 6 znaków!!!");
                return;

            }

            if (double.TryParse(this.textBox1.Text, out _nowa_ilosc))
            {
                _komentarz = this.textBox2.Text;
                this.Close(); }
            else { MessageBox.Show("PODAJ POPRAWNĄ ILOŚĆ!!!"); }
        }
    }
}
