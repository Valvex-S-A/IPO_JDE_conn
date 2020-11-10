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
    public partial class Analiza : Form
    {


        int nr_zlec;
        string mag_zlec;
        string user_name;
        string kod_wyr;
        public Analiza(string _nr_zlec, string user)
        {
            InitializeComponent();



            nr_zlec = int.Parse(_nr_zlec);
            user_name = user;
            var srv = new WebReference.Service1();

            var db = new db_raportyDataContext();
            var db2008 = new DB2008DataContext();
            var zlecenie = srv.IPO_GET_ORDER(nr_zlec);

            double itm_zl = 0;
            double.TryParse(zlecenie.item_id, out itm_zl);

            var wyr_itm = (from c in db2008.SLOWNIK_1s
                          where c.IMITM == itm_zl
                          select new { c.IMITM, c.NAZWA, c.KOLOR, c.IMLITM}).FirstOrDefault();

            kod_wyr = wyr_itm.IMLITM.Trim();


            var mag = (from c in db2008.IPO_MAGAZYN_PODSTAWOWY_PWs
                       where c.LIITM == wyr_itm.IMITM
                       select c).FirstOrDefault();
            

           
            mag_zlec = mag.mag_ipo;


            Update_grid();

        }

        public void Update_grid()
        {
            var db1 = new db_raportyDataContext();
            var nlinie = from c in db1.IPO_ZDAWKA_PWs
                        where c.Nr_zlecenia_IPO == nr_zlec
                        select c;


            var n_linie = Analiza_zlec.Lista_pozycji(nr_zlec);
            double nilosc_zlec = (double)(from c in nlinie where c.RW_PW == "PW" select c.Ilosc).Sum();
            label1.Text = kod_wyr + " : " + nilosc_zlec.ToString() + " SZT" + " ; Korekty na magazyn: " + mag_zlec;
            dataGridView1.DataSource = n_linie;
            dataGridView1.Update();

            return;

            var srv = new WebReference.Service1();

            var db = new db_raportyDataContext();
            var db2008 = new DB2008DataContext();

            //skasuj stare rozpisy
            var do_skas = from c in db.IPO_ZDAWKA_PW_NORMAs where c.Nr_zlecenia_IPO == nr_zlec select c;
            db.IPO_ZDAWKA_PW_NORMAs.DeleteAllOnSubmit(do_skas);
            db.SubmitChanges();


            //znajdz magazyn domyślny dla zlecenia




            var linie = from c in db.IPO_ZDAWKA_PWs
                        where c.Nr_zlecenia_IPO == nr_zlec
                        select c;
            //wyceń materiały - nie przejmujemy się jednostkami.
            foreach (var l in linie)
            {
                var tkw = (from c in db.słownik_TKWs where c.Indeks.Trim() == l.Nr_indeksu.Trim() select c.Koszt).FirstOrDefault();

                if (l.RW_PW == "PW") l.Koszt_IPO = Math.Round((double)tkw * (double)l.Ilosc, 3);
                if (l.RW_PW == "RW") l.Koszt_IPO = Math.Round((double)tkw * (double)-l.Ilosc, 3);
                if (l.RW_PW == "PU") l.Koszt_IPO = Math.Round((double)tkw * (double)-l.Ilosc, 3);

                db.SubmitChanges();

            }

            double ilosc_zlec = (double)(from c in linie where c.RW_PW == "PW" select c.Ilosc).Sum();

            // utwórz zlecenie w tabeli przejściowej.
            label1.Text = kod_wyr + " : " + ilosc_zlec.ToString() + " SZT" + " ; Korekty na magazyn: " + mag_zlec;
            srv.Dodaj_rozpis_do_Tabeli(nr_zlec, 0, ilosc_zlec);
           
            // wyceń rozpis
            var linie_norma = from c in db.IPO_ZDAWKA_PW_NORMAs
                              where c.Nr_zlecenia_IPO == nr_zlec && c.RW_PW == "RW"
                              select c;
            //wyceń materiały - nie przejmujemy się jednostkami.
            foreach (var l in linie_norma)
            {
                var tkw = (from c in db.słownik_TKWs where c.Indeks.Trim() == l.Nr_indeksu.Trim() select c.Koszt).FirstOrDefault();

                if (l.RW_PW == "PW") l.Koszt_IPO = Math.Round((double)tkw * (double)l.Ilosc, 3);
                if (l.RW_PW == "RW") l.Koszt_IPO = Math.Round((double)tkw * (double)-l.Ilosc, 3);
                if (l.RW_PW == "PU") l.Koszt_IPO = Math.Round((double)tkw * (double)-l.Ilosc, 3);

                db.SubmitChanges();

            }

            var total = (from c in linie where c.RW_PW == "RW" select new { TYP = "JDE", c.Nr_zlecenia_IPO, Indeks = c.Nr_indeksu.Trim(), c.Nazwa_pozycji, c.Ilosc, c.Koszt_IPO })
           .Concat(from g in linie_norma select new { TYP = "NORMA", g.Nr_zlecenia_IPO, Indeks = g.Nr_indeksu.Trim(), g.Nazwa_pozycji, g.Ilosc, g.Koszt_IPO }).ToArray();

            // zgrupuj
            List<Item> lst = new List<Item>();
            //utwórz listę indeksów
            var lista_indeksow = (from c in total select c.Indeks).Distinct();

            foreach (var i in lista_indeksow)
            {
                Item itm = new Item();

                var tmp = from c in total where c.Indeks == i select c;
                itm.Nr_indeksu = i;
                itm.Nazwa = tmp.First().Nazwa_pozycji;
                itm.Ilość_JDE = (double)(from c in tmp where c.TYP == "JDE" select c.Ilosc).Sum();
                itm.Wartość_JDE = (double)(from c in tmp where c.TYP == "JDE" select c.Koszt_IPO).Sum();
                itm.Ilość_NORMA = (double)(from c in tmp where c.TYP == "NORMA" select c.Ilosc).Sum();
                itm.Wartość_NORMA = (double)(from c in tmp where c.TYP == "NORMA" select c.Koszt_IPO).Sum();
                itm.Różnica_ilość = Math.Round(itm.Ilość_JDE + itm.Ilość_NORMA);
                itm.Różnica_wartość = Math.Round(itm.Wartość_JDE + itm.Wartość_NORMA, 3);

                lst.Add(itm);
            }



            dataGridView1.DataSource = lst;
            dataGridView1.Update();


        }



        class Item
        {
            public string Nr_indeksu { get; set; }
            public string Nazwa { get; set; }
            public double Ilość_JDE { get; set; }
            public double Ilość_NORMA { get; set; }
            public double Wartość_JDE { get; set; }
            public double Wartość_NORMA { get; set; }
            public double Różnica_ilość { get; set; }
            public double Różnica_wartość { get; set; }
        }


            


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Analiza_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {



            if (textBox1.Text == "" || dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Podaj jakąś ilość lub zaznacz jeden wiersz do poprawy!!!", "błąd");
                return;


            }
            double.TryParse(textBox1.Text,out double qty);

            string litm = (string)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value;
            string nazwa = (string)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value;




            var db2008 = new DB2008DataContext();
            var kod = (from c in db2008.SLOWNIK_1s where c.IMLITM.Trim() == litm.Trim()
                       select c).First();



            DialogResult dl = MessageBox.Show("Czy zaksięgować detal\n" + litm + " " + nazwa + "\n RW na magazyn " + mag_zlec + " Ilosc:" + qty.ToString() + " " + kod.JM_PROD.Trim(), "ksiegowanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dl == DialogResult.No) return;

            db_raportyDataContext db = new db_raportyDataContext();
            var nrec = new IPO_ZDAWKA_PW();

            nrec.Czy_korygowany = true;
            nrec.Data_utworzenia_poz = DateTime.Now;
            nrec.Ilosc = qty;
            nrec.IPO_ID_POZYCJI = -1;
            nrec.ITM = kod.IMITM.ToString();
            nrec.typ = 0;
            nrec.Zaksiegowany_JDE = false;
            nrec.JM = kod.JM_PROD.Trim();
            nrec.Kod_zlecenia_klienta = kod_wyr;
            nrec.Koszt_IPO = 0;
            nrec.Koszt_mat_IPO = 0;
            nrec.Magazyn_IPO = mag_zlec;
            nrec.Nazwa_pozycji = nazwa;
            nrec.Nr_indeksu = litm;
            nrec.Nr_zam_klienta = litm;
            nrec.Nr_seryjny = "";
            nrec.Nr_zam_klienta = "";
            nrec.Nr_zlecenia_IPO = nr_zlec;
            nrec.Powod_korekty = "DODANE " + DateTime.Now.ToString() + " RĘCZNIE PRZEZ " + user_name ;
            nrec.RW_PW = "RW";







            db.IPO_ZDAWKA_PWs.InsertOnSubmit(nrec);
            db.SubmitChanges();
            this.textBox1.Text = "";

            this.Update_grid();
        }
    }
}
