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
    public partial class Nowe_PW : Form
    {


        public double itm;
        public int nr_zlec_IPO;
        public string litm;
        public double qty;
        public string mag_dom;
        public string user;

        public Nowe_PW(string _user)
        {
            InitializeComponent();
            this.button1.Enabled = false;
            user = _user;
        }

        private void button1_Click(object sender, EventArgs e) //zapisz rekord
        {
            WebReference.Service1 srv = new WebReference.Service1();


            




            var db2008 = new DB2008DataContext();
            var kod = (from c in db2008.SLOWNIK_1s where c.IMLITM.Trim() == litm.Trim()
                      select c).First();


            db_raportyDataContext db = new db_raportyDataContext();
            var nrec = new IPO_ZDAWKA_PW();

            nrec.Czy_korygowany = true;
            nrec.Data_utworzenia_poz = DateTime.Now;
            nrec.Ilosc = qty;
            nrec.IPO_ID_POZYCJI = -1;
            nrec.ITM = itm.ToString();
            nrec.typ = 0;
            nrec.Zaksiegowany_JDE = false;
            nrec.JM = kod.JM_PROD.Trim();
            nrec.Kod_zlecenia_klienta = tb_litm.Text;
            nrec.Koszt_IPO = 0;
            nrec.Koszt_mat_IPO = 0;
            nrec.Magazyn_IPO = mag_dom;
            nrec.Nazwa_pozycji = lb_nazwa.Text;
            nrec.Nr_indeksu = litm;
            nrec.Nr_zam_klienta = litm;
            nrec.Nr_seryjny = "";
            nrec.Nr_zam_klienta = "";
            nrec.Nr_zlecenia_IPO = nr_zlec_IPO;
            nrec.Powod_korekty = "DODANE " + DateTime.Now.ToString() +  " RĘCZNIE PRZEZ " + user;

            if (rbPW.Checked) nrec.RW_PW = "PW";

            if (rbRW.Checked) nrec.RW_PW = "RW";

            if (rbPU.Checked) { nrec.RW_PW = "PU"; nrec.typ = 1; }





            db.IPO_ZDAWKA_PWs.InsertOnSubmit(nrec);


            db.SubmitChanges();
            this.Close();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB2008DataContext db = new DB2008DataContext();
            WebReference.Service1 srv = new WebReference.Service1();
            double.TryParse(this.tb_qty.Text, out qty);
            int.TryParse(this.tb_nr_zlec.Text, out nr_zlec_IPO);


            var zlecenie = srv.IPO_GET_ORDER(nr_zlec_IPO);

            double itm_zl = 0;
            double.TryParse(zlecenie.item_id, out itm_zl);


        var mat_itm = from c in db.SLOWNIK_1s
                       where c.IMLITM == this.tb_litm.Text
                       select new { c.IMITM, c.NAZWA,c.KOLOR};

            var wyr_itm = from c in db.SLOWNIK_1s
                          where c.IMITM == mat_itm.First().IMITM
                          // where c.IMITM == itm_zl
                          select new { c.IMITM, c.NAZWA, c.KOLOR };



            if (mat_itm.Count() == 1 &&  wyr_itm.Count() ==1 && zlecenie.ipo_order_id>0)
            {

                var mag = from c in db.IPO_MAGAZYN_PODSTAWOWY_PWs
                          where c.LIITM ==  wyr_itm.First().IMITM
                          select c;

                mag_dom = mag.First().mag_ipo;
                lb_mag.Text = mag_dom.Trim();
               


                if (qty != 0 && nr_zlec_IPO != 0) this.button1.Enabled = true;
                lb_nazwa.Text = mat_itm.First().NAZWA;
                itm = mat_itm.First().IMITM;
                litm = tb_litm.Text;
               


            }

                       

        }

        private void tb_litm_TextChanged(object sender, EventArgs e)
        {
            lb_nazwa.Text = "...";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
