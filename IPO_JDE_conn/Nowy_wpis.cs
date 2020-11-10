using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPO_JDE_conn
{
    public partial class Nowy_wpis : Form
    {

        string _logged = "";
        public Nowy_wpis(string logged)
        {
            InitializeComponent();

            db_raportyDataContext db = new db_raportyDataContext();

            var prac = (from c in db.IPO_Tasks
                        orderby c.Pracownik
                        select new { c.Id_pracownika, c.Pracownik }).Distinct().OrderBy(c => c.Pracownik);

            ddPracownik.DataSource = prac;
            ddPracownik.ValueMember = "Id_pracownika";
            ddPracownik.DisplayMember = "Pracownik";

            var oper = (from c in db.IPO_Tasks select new { c.Nazwa_operacji }).Distinct().OrderBy(g => g.Nazwa_operacji);
            dd_operacja.DataSource = oper;
            dd_operacja.DisplayMember = "Nazwa_operacji";
            _logged = logged;


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Nowy_wpis_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebReference.Service1 srv = new WebReference.Service1();
            //Dodaj nowy rekord do bazy...
            DB2008DataContext db2 = new DB2008DataContext();
            double il_ok = 0;
            double il_br = 0;
            int nr_zlec = 0;
            string Indeks = "";


            double.TryParse(tb_ilosc.Text, out il_ok);
            double.TryParse(tb_ilosc_brak.Text, out il_br);
            int.TryParse(tb_nr_zlecenia.Text, out nr_zlec);


            

            db_raportyDataContext db = new db_raportyDataContext();


            var task_id = db.IPO_GET_TASK_ID().First();


            var zlec = srv.IPO_GET_ORDER(nr_zlec);


            var idx = from c in db2.SLOWNIK_1s
                         where c.IMITM == double.Parse(zlec.item_id)
                         select c;

            if (idx.Count() == 1) Indeks = idx.First().IMLITM.Trim();
             


            if (zlec.ipo_order_id == 0) { MessageBox.Show("Popraw nr zlecenia!!!"); return; }

            Regex reg = new Regex("[1-9][0-9]-");
            if (!reg.IsMatch(tb_opis.Text)) { MessageBox.Show("Popraw opis pracy musi zawierac nr operacji i znak -  , np 10-na gotowo"); return; }


            reg = new Regex("[1-9][0-9][0-9]");

            if (!reg.IsMatch(tb_nr_masz.Text)) { MessageBox.Show("Popraw nr maszyny!!! "); return; }
             


            IPO_Task tsk = new IPO_Task();


            DateTime start = dtp_start_data.Value.Date + dtp_start_czas.Value.TimeOfDay;
            DateTime stop = dtp_stop_data.Value.Date + dtp_stop_czas.Value.TimeOfDay;

            if (start > stop) { MessageBox.Show("Popraw daty i godziny!!! Start nie może być poźniej niż stop!!!"); return; }


            tsk.Czas_planowany = 0;
            tsk.Czas_realizacji =  (int)(stop - start).TotalMinutes;
            tsk.Czas_start = start;
            tsk.Czas_stop = stop;

            tsk.Id_maszyny = tb_nr_masz.Text;
            tsk.Id_pracownika = ddPracownik.SelectedValue.ToString();
            tsk.Pracownik = ddPracownik.Text;

            if (tb_id_pracownika.Text != "")
            {

               

                string nazwa = srv.IPO_get_user(tb_id_pracownika.Text);


                tsk.Id_pracownika = tb_id_pracownika.Text;
                tsk.Pracownik = nazwa;
            }


            if (tsk.Pracownik == "BŁĄD!!!")
            { MessageBox.Show("Popraw Id pracownika!!!");  return;  }
            tsk.Id_zlecenia = nr_zlec;
            tsk.Ilosc_brak = il_br;
            tsk.Ilosc_wykonana = il_ok;
            tsk.Indeks = Indeks;
            tsk.Indeks_ITM = int.Parse(zlec.item_id);
            tsk.Task_Id = task_id.IPO_TASK_ID_KOREKTA;
            tsk.Ilosc_planowana = 0;
            tsk.Nr_zamowienia = _logged;

            tsk.Opis_pracy = tb_opis.Text;
            tsk.Nazwa_operacji = dd_operacja.Text;
            db.IPO_Tasks.InsertOnSubmit(tsk);
            db.SubmitChanges();


            this.Close();
        }

        private void ddPracownik_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
