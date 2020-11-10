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
    public partial class KORYGUJ_ZLEC : Form
    {
        private string nr_zlec;
        private double il_pierwotna;

        public KORYGUJ_ZLEC(string _nr_zlec)
        {
            InitializeComponent();
            nr_zlec = _nr_zlec;
            WebReference.Service1 srv = new WebReference.Service1();

            var zlec = srv.IPO_GET_ORDER(int.Parse(nr_zlec));
            lb_nazwa_zlec.Text = zlec.name;
            lb_ilosc.Text = "ILOŚĆ PIERWOTNA:" + zlec.quantity;
            il_pierwotna = zlec.quantity;


        }

        private void KORYGUJ_ZLEC_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox1.Text, out double nowa_ilosc);
            if (nowa_ilosc ==0)
            {

                MessageBox.Show("Podaj Ilosc różną od zera!!!");
                return;
            }

            var db = new db_raportyDataContext();
            double wspl = Math.Round(nowa_ilosc / il_pierwotna, 3);
            int.TryParse(nr_zlec, out int nr_zlec_int);
            var linie = from c in db.IPO_ZDAWKA_PWs
                        where c.RW_PW == "RW" && c.Nr_zlecenia_IPO == nr_zlec_int
                        select c;

            foreach (var linia in linie)
            {
                var krec = Clone<IPO_ZDAWKA_PW>(linia);
                krec.Ilosc = -krec.Ilosc;
                krec.Powod_korekty = "KOREKTA ILOSCI ZLECENIA!";
                krec.Data_utworzenia_poz = DateTime.Now;
                krec.Czy_korygowany = true;
                krec.Zaksiegowany_JDE = false;
                db.IPO_ZDAWKA_PWs.InsertOnSubmit(krec);

                var nrec = Clone<IPO_ZDAWKA_PW>(linia);
                nrec.Ilosc = linia.Ilosc * wspl;
                nrec.Powod_korekty = "KOREKTA ILOSCI ZLECENIA!";
                nrec.Data_utworzenia_poz = DateTime.Now;
                nrec.Czy_korygowany = true;
                nrec.Zaksiegowany_JDE = false;
                db.IPO_ZDAWKA_PWs.InsertOnSubmit(nrec);
                db.SubmitChanges();

            }

            this.Close();
        }


        public static T Clone<T>(T source)
        {
            var dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            using (var ms = new System.IO.MemoryStream())
            {
                dcs.WriteObject(ms, source);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)dcs.ReadObject(ms);
            }
        }
    }
}
