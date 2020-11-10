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
    public partial class KORYGUJ_AKORD : Form
    {
        int id_rec;
        string login;
        int task_id;

        public KORYGUJ_AKORD(int _id_rec, string _login)
        {
            InitializeComponent();
            id_rec = _id_rec;
            login = _login;
            

        }

        private void KORYGUJ_AKORD_Load(object sender, EventArgs e)
        {

            db_raportyDataContext db = new db_raportyDataContext();
            var rec = (from c in db.IPO_Tasks_upds
                       where c.ID == id_rec
                       select c).Single();
            task_id = (int)rec.Task_Id;

            dtp_data_start.Value = (DateTime)rec.Czas_start;
            dtp_data_stop.Value =  (DateTime)rec.Czas_stop;

            dtp_czas_start.Value = (DateTime)rec.Czas_start;
            dtp_czas_stop.Value =  (DateTime)rec.Czas_stop;

            dtp_data_start.Checked = false;
            dtp_data_stop.Checked = false;

            lb_ilosc.Text = "ILOŚĆ: (" + rec.Ilosc_wykonana.ToString() + ")";
            lb_ilosc_brak.Text = "ILOŚĆ BRAK: (" + rec.Ilosc_brak.ToString() + ")";
            lb_nazwa_oper.Text = "NAZWA_OPER: (" + rec.Nazwa_operacji + ")";
            lb_indeks.Text = "INDEKS: (" + rec.Indeks +  ")";


            lb_opis_pracy.Text = "OPIS PRACY: (" + rec.Opis_pracy  + ")";
            groupBox1.Text = "KOREKTA dla :" + rec.Pracownik;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtp_data_start_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //waliduj dane
            double check;

            if (!string.IsNullOrEmpty(tb_ilosc.Text))
            {
                if (!Double.TryParse(tb_ilosc.Text, out check)) { MessageBox.Show("POPRAW ILOŚĆ!!!"); return; }
            }
            if (!string.IsNullOrEmpty(tb_ilosc_brak.Text))
            {
                if (!Double.TryParse(tb_ilosc_brak.Text, out check)) { MessageBox.Show("POPRAW ILOŚĆ_BRAK!!!"); return; }
            }

            string nstartc;
            nstartc = dtp_data_start.Value.ToString("yyyy-MM-dd") + " " + dtp_czas_start.Value.ToLongTimeString();
            string nstopc;
            nstopc = dtp_data_stop.Value.ToString("yyyy-MM-dd") + " " + dtp_czas_stop.Value.ToLongTimeString();

            if (DateTime.Parse(nstopc)< DateTime.Parse(nstartc)) { MessageBox.Show("CZAS STOP WIĘKSZY OD START - POPRAW!!!"); return; }




            db_raportyDataContext db = new db_raportyDataContext();



            if (!string.IsNullOrEmpty(tbIndeks.Text))
            {

                DB2008DataContext db1 = new DB2008DataContext();
                var itm = from c in db1.SLOWNIK_1s
                          where c.IMLITM == tbIndeks.Text
                          select c;

                if (itm.Count() !=1) { MessageBox.Show("Podaj poprawny indeks!!!"); return;   }

                
                var _itm = itm.First();

                var to_delete1 = from c in db.IPO_Tasks_korektas
                                where c.Task_id == task_id && c.Typ_korekty == "INDEKS_ITM"
                                select c;
                db.IPO_Tasks_korektas.DeleteAllOnSubmit(to_delete1);
                db.SubmitChanges();
                IPO_Tasks_korekta kor1 = new IPO_Tasks_korekta();
                kor1.Typ_korekty = "INDEKS_ITM";
                kor1.Task_id = task_id;
                kor1.Dane = _itm.IMITM.ToString();
                kor1.Utworzony_przez = login;
                kor1.Utworzony = DateTime.Now;
                db.IPO_Tasks_korektas.InsertOnSubmit(kor1);
                db.SubmitChanges();
                





                var to_delete = from c in db.IPO_Tasks_korektas
                                where c.Task_id == task_id && c.Typ_korekty == "INDEKS"
                                select c;
                db.IPO_Tasks_korektas.DeleteAllOnSubmit(to_delete);
                db.SubmitChanges();
                IPO_Tasks_korekta kor = new IPO_Tasks_korekta();
                kor.Typ_korekty = "INDEKS";
                kor.Task_id = task_id;
                kor.Dane = tbIndeks.Text;
                kor.Utworzony_przez = login;
                kor.Utworzony = DateTime.Now;
                db.IPO_Tasks_korektas.InsertOnSubmit(kor);
                db.SubmitChanges();
                db.UpdateJDE(task_id);

            }



            //najpierw usuń stare zapisy
            if (!string.IsNullOrEmpty(tb_ilosc.Text))
            {
                
               
                var to_delete = from c in db.IPO_Tasks_korektas
                                where c.Task_id == task_id && c.Typ_korekty == "ILOSC"
                                select c;
                db.IPO_Tasks_korektas.DeleteAllOnSubmit(to_delete);
                db.SubmitChanges();
                IPO_Tasks_korekta kor = new IPO_Tasks_korekta();
                kor.Typ_korekty = "ILOSC";
                kor.Task_id = task_id;
                kor.Dane = tb_ilosc.Text;
                kor.Utworzony_przez = login;
                kor.Utworzony = DateTime.Now;
                db.IPO_Tasks_korektas.InsertOnSubmit(kor);
                db.SubmitChanges();
                db.UpdateJDE(task_id);
              
            }

            if (!string.IsNullOrEmpty(tb_ilosc_brak.Text))
            {
               

                var to_delete = from c in db.IPO_Tasks_korektas
                                where c.Task_id == task_id && c.Typ_korekty == "ILOSC_BRAK"
                                select c;
                db.IPO_Tasks_korektas.DeleteAllOnSubmit(to_delete);
                db.SubmitChanges();
                IPO_Tasks_korekta kor = new IPO_Tasks_korekta();
                kor.Typ_korekty = "ILOSC_BRAK";
                kor.Task_id = task_id;
                kor.Dane = tb_ilosc_brak.Text;
                kor.Utworzony_przez = login;
                kor.Utworzony = DateTime.Now;
                db.IPO_Tasks_korektas.InsertOnSubmit(kor);
                db.SubmitChanges();
                db.UpdateJDE(task_id);

            }
            if (!string.IsNullOrEmpty(tb_nazwa_operacji.Text))
            {
                var to_delete = from c in db.IPO_Tasks_korektas
                                where c.Task_id == task_id && c.Typ_korekty == "NAZWA_OPERACJI"
                                select c;
                db.IPO_Tasks_korektas.DeleteAllOnSubmit(to_delete);
                db.SubmitChanges();
                IPO_Tasks_korekta kor = new IPO_Tasks_korekta();
                kor.Typ_korekty = "NAZWA_OPERACJI";
                kor.Task_id = task_id;
                kor.Dane = tb_nazwa_operacji.Text;
                kor.Utworzony_przez = login;
                kor.Utworzony = DateTime.Now;
                db.IPO_Tasks_korektas.InsertOnSubmit(kor);
                db.SubmitChanges();
                db.UpdateJDE(task_id);

            }

             
            if (!string.IsNullOrEmpty(tb_opis_pracy.Text))
            {
                var to_delete = from c in db.IPO_Tasks_korektas
                                where c.Task_id == task_id && c.Typ_korekty == "OPIS_PRACY"
                                select c;
                db.IPO_Tasks_korektas.DeleteAllOnSubmit(to_delete);
                db.SubmitChanges();
                IPO_Tasks_korekta kor = new IPO_Tasks_korekta();
                kor.Typ_korekty = "OPIS_PRACY";
                kor.Task_id = task_id;
                kor.Dane = tb_opis_pracy.Text;
                kor.Utworzony_przez = login;
                kor.Utworzony = DateTime.Now;
                db.IPO_Tasks_korektas.InsertOnSubmit(kor);
                db.SubmitChanges();
                db.UpdateJDE(task_id);

            }

            if (dtp_data_start.Checked)
            {

                string nstart;
                nstart = dtp_data_start.Value.ToShortDateString() + " " + dtp_czas_start.Value.ToLongTimeString();
                



                var to_delete = from c in db.IPO_Tasks_korektas
                                where c.Task_id == task_id && c.Typ_korekty == "START"
                                select c;
                db.IPO_Tasks_korektas.DeleteAllOnSubmit(to_delete);
                db.SubmitChanges();
                IPO_Tasks_korekta kor = new IPO_Tasks_korekta();
                kor.Typ_korekty = "START";
                kor.Task_id = task_id;
                kor.Dane = nstart;
                kor.Utworzony_przez = login;
                kor.Utworzony = DateTime.Now;
                db.IPO_Tasks_korektas.InsertOnSubmit(kor);
                db.SubmitChanges();
                db.UpdateJDE(task_id);

            }

            if (dtp_data_stop.Checked)
            {

                string nstop;
                nstop = dtp_data_stop.Value.ToShortDateString() + " " + dtp_czas_stop.Value.ToLongTimeString();




                var to_delete = from c in db.IPO_Tasks_korektas
                                where c.Task_id == task_id && c.Typ_korekty == "STOP"
                                select c;
                db.IPO_Tasks_korektas.DeleteAllOnSubmit(to_delete);
                db.SubmitChanges();
                IPO_Tasks_korekta kor = new IPO_Tasks_korekta();
                kor.Typ_korekty = "STOP";
                kor.Task_id = task_id;
                kor.Dane = nstop;
                kor.Utworzony_przez = login;
                kor.Utworzony = DateTime.Now;
                db.IPO_Tasks_korektas.InsertOnSubmit(kor);
                db.SubmitChanges();
                db.UpdateJDE(task_id);
            }
            this.Close();

        }
    }
}
