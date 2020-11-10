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
    public partial class Skasuj_zlecenie : Form
    {
        public string logged;

        public Skasuj_zlecenie(string _logged)
        {
            logged = _logged;
            InitializeComponent();
        }

        private void Anuluj_Click(object sender, EventArgs e)
        {
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



        private void button1_Click(object sender, EventArgs e)
        {
            var db = new db_raportyDataContext();

            int nr_zlec = 0;

                int.TryParse(tb_nr_zlecenia.Text, out nr_zlec);

            var transakcje = from c in db.IPO_ZDAWKA_PWs
                             where c.Nr_zlecenia_IPO == nr_zlec
                             select c;

            if (!string.IsNullOrEmpty(textBox1.Text))
            {

                transakcje = transakcje.Where(x => x.Nr_indeksu.StartsWith(textBox1.Text));
            }


            if (cbRW.Checked)
            {
                var rw_do_skas = from c in transakcje where c.RW_PW == "RW" select c;

                foreach (var indeks in rw_do_skas)
                {
                    var suma_dla_indeksu = (from c in rw_do_skas where c.Nr_indeksu == indeks.Nr_indeksu select c.Ilosc).Sum();
                    if (suma_dla_indeksu != 0)
                    {
                        var krec = Clone<IPO_ZDAWKA_PW>(indeks);
                        double koszt_jn = (double)krec.Koszt_IPO / (krec.Ilosc == 0 ? 1 : (double)krec.Ilosc);

                        krec.Ilosc = krec.Ilosc * -1;
                        krec.Koszt_IPO = krec.Koszt_IPO * -1;
                        krec.Zaksiegowany_JDE = false;
                        krec.Czy_korygowany = true;
                        krec.Nr_seryjny = logged;
                        krec.Data_utworzenia_poz = DateTime.Now;
                        krec.Powod_korekty = "korekta do zera!!!";
                        db.IPO_ZDAWKA_PWs.InsertOnSubmit(krec);
                        db.SubmitChanges();
                    }
              
                }

            }

            if (cbPU.Checked)
            {
                var rw_do_skas = from c in transakcje where c.RW_PW == "PU" select c;

                foreach (var indeks in rw_do_skas)
                {
                    var suma_dla_indeksu = (from c in rw_do_skas where c.Nr_indeksu == indeks.Nr_indeksu select c.Ilosc).Sum();
                    if (suma_dla_indeksu != 0)
                    {
                        var krec = Clone<IPO_ZDAWKA_PW>(indeks);
                        double koszt_jn = (double)krec.Koszt_IPO / (krec.Ilosc == 0 ? 1 : (double)krec.Ilosc);


                        krec.Ilosc = krec.Ilosc * -1;
                        krec.Koszt_IPO = krec.Koszt_IPO * -1;
                        krec.Zaksiegowany_JDE = false;
                        krec.Czy_korygowany = true;
                        krec.Nr_seryjny = logged;
                        krec.Data_utworzenia_poz = DateTime.Now;
                        krec.Powod_korekty = "korekta do zera!!!";
                        db.IPO_ZDAWKA_PWs.InsertOnSubmit(krec);
                        db.SubmitChanges();
                    }

                }

            }


            if (cbPW.Checked)
            {
                var rw_do_skas = from c in transakcje where c.RW_PW == "PW" select c;

                foreach (var indeks in rw_do_skas)
                {
                    var suma_dla_indeksu = (from c in rw_do_skas where c.Nr_indeksu == indeks.Nr_indeksu select c.Ilosc).Sum();
                    if (suma_dla_indeksu != 0)
                    {
                        var krec = Clone<IPO_ZDAWKA_PW>(indeks);
                        double koszt_jn = (double)krec.Koszt_IPO / (krec.Ilosc == 0 ? 1 : (double)krec.Ilosc);

                        krec.Ilosc = krec.Ilosc * -1;
                        krec.Koszt_IPO = krec.Koszt_IPO * -1;
                        krec.Zaksiegowany_JDE = false;
                        krec.Czy_korygowany = true;
                        krec.Nr_seryjny = logged;
                        krec.Data_utworzenia_poz = DateTime.Now;
                        krec.Powod_korekty = "korekta do zera!!!";
                        db.IPO_ZDAWKA_PWs.InsertOnSubmit(krec);
                        db.SubmitChanges();
                    }

                }

            }

            this.Close();

        }
    }
}
