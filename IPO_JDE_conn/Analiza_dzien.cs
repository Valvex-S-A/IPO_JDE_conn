using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPO_JDE_conn
{
    public partial class Analiza_dzien : Form
    {
        private List<Item> lista_do_gv = new List<Item>();
        private List<string> _lista_zl = new List<string>();
        double _min_wartosc = 0; 

        public Analiza_dzien(List<string> lst, double min_wartosc, bool opakowania)
        {

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            _lista_zl = lst;

            label1.Text = $"UWAGA! Do rozbicia jest {lst.Count.ToString()} pozycji!";
            _min_wartosc = min_wartosc;

            


           
        }

        private void Analiza_dzien_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int n = 1;
            foreach (var poz in _lista_zl)
            {
                label1.Text = $"Rozbijam {n++}/{_lista_zl.Count()}";
                label1.Refresh();
                this.Invalidate();

                try
                {
                    var lista = Analiza_zlec.Lista_pozycji(int.Parse(poz));

                    foreach (var poz1 in lista)
                    {


                        if (Math.Abs(poz1.Różnica_wartość) >_min_wartosc)
                        lista_do_gv.Add(poz1);


                    }



                }
                catch { }

                


            }

            dataGridView1.DataSource = lista_do_gv;
            dataGridView1.Refresh();

        }
    }
}
