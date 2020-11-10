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
    public partial class New_Graff_Rap : Form
    {
        int _id_rec = 0;
        int _nr_zlec = 0;
        string _nazwa_zlec;
        string _index;
        string _user;
        DateTime _cdt;

        public New_Graff_Rap(int id_rec, string logged_user, DateTime currentDate)
        {
            InitializeComponent();
            _id_rec = id_rec;
            _user = logged_user;
            _cdt = currentDate;
            var db = new db_raportyDataContext();

            TimeSpan tsp = dt_czas_stop.Value - dt_czas_stop.Value;

            label4.Text = tsp.TotalMinutes.ToString("0.0");

            var pracownicy = from c in db.API_Pracownicies
                             where c.grupa == 67 orderby c.nazwisko
                             select new { c.id, NAZWA = $"{c.nazwisko} {c.imie}" };

            comboBox1.DataSource = pracownicy;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "NAZWA";
            comboBox4.SelectedIndex = 0;


            List<string> typ_zl = new List<string>();
            typ_zl.Add("MONTAŻ");
            typ_zl.Add("PAKOWANIE");
            typ_zl.Add("MONTAŻ_PAKOWANIE");
            typ_zl.Add("DNIÓWKA");
            comboBox3.DataSource = typ_zl;

            List<string> status_zl = new List<string>();
            status_zl.Add("w_toku");
            status_zl.Add("stop");
            status_zl.Add("pauza");
            comboBox2.DataSource = status_zl;
            dateTimePicker1.Value = _cdt.Date;
            dateTimePicker2.Value = _cdt.Date;
            if (id_rec == 0 ) { return; } //zakończ - w przeciwnym wypadku zaktualizuj formatkę...
            dateTimePicker2.Enabled = false;

            var rec = (from c in db.RAPORT_MONTAZs where c.id == id_rec select c).First();
            dateTimePicker1.Value = rec.Data_start.Value.Date;
            dateTimePicker2.Value = rec.Data_start.Value.Date;

            dt_czas_start.Value = rec.Data_start.Value;
            dt_czas_stop.Value = rec.Data_stop.Value;
            tsp = dt_czas_stop.Value - dt_czas_start.Value;

            label4.Text = tsp.TotalMinutes.ToString("0.0");



            comboBox1.SelectedValue = rec.id_prac;
            comboBox3.SelectedItem = rec.typ_pracy;
            comboBox2.SelectedItem = rec.status;
            comboBox4.SelectedItem = rec.nr_stanowiska;

            textBox2.Text = rec.zdawka_dobre.ToString();
            textBox3.Text = rec.zdawka_zle.ToString();

            textBox1.Text = rec.Nr_zlec_ipo.ToString();

        }

        private void dt_czas_stop_ValueChanged(object sender, EventArgs e)
        {
            if (dt_czas_stop.Value < dt_czas_start.Value)
            {
              //  dt_czas_stop.Value = dt_czas_start.Value.AddSeconds(1);


            }

            TimeSpan tsp = dt_czas_stop.Value - dt_czas_start.Value;

            label4.Text = tsp.TotalMinutes.ToString("0.0");
        }

        private void New_Graff_Rap_Load(object sender, EventArgs e)
        {

        }

        private void dt_czas_start_ValueChanged(object sender, EventArgs e)
        {
            if (dt_czas_start.Value > dt_czas_stop.Value)
            {
               // dt_czas_start.Value = dt_czas_stop.Value.AddSeconds(-1);

            }

            TimeSpan tsp = dt_czas_stop.Value - dt_czas_start.Value;

            label4.Text = tsp.TotalMinutes.ToString("0.0");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var db = new db_raportyDataContext();

            int nr_zl = 0;
            int.TryParse(textBox1.Text, out nr_zl);
            if (nr_zl ==0 && comboBox3.SelectedItem.ToString() == "DNIÓWKA") { button2.Enabled = true; return; }

            if (nr_zl == 0) { label10.Text = "BŁĄD!!!"; return; }

            var zl = (from c in db.IPO_ZLECENIAs
                     where c.ipo_nr_zlec == nr_zl
                     select c).ToList();

            if (zl.Count() == 0 ) { label10.Text = "BŁĄD!!!"; return; }
            label10.Text = zl.FirstOrDefault().NAZWA;
            _index = zl.FirstOrDefault().Indeks_zlecenia;
            _nr_zlec = nr_zl;
            _nazwa_zlec = zl.FirstOrDefault().NAZWA;
            button2.Enabled = true;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var db = new db_raportyDataContext();

            if (comboBox3.SelectedItem.ToString() == "DNIÓWKA"  &&  dateTimePicker1.Value.Date>dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Podaj dobrą datę zakończenia dniówki - nie może być młodsza niż data start ");
                    return;
            }

            if (_id_rec != 0 && dateTimePicker1.Value.Date != dateTimePicker2.Value.Date)
            {

                MessageBox.Show("Nie możesz przy edycji rekordu dodawać więcej dniówki niż jeden dzień!");
                return;
            }

            RAPORT_MONTAZ rpm;

            TimeSpan tms = dateTimePicker2.Value - dateTimePicker1.Value;


            for (int n = 0; n <= tms.TotalDays; n++)
            {

                if (_id_rec == 0) { rpm = new RAPORT_MONTAZ(); }
                else
                {
                    rpm = (from c in db.RAPORT_MONTAZs where c.id == _id_rec select c).FirstOrDefault();
                    RAPORT_MONTAZ_KOREKTY rkor = new RAPORT_MONTAZ_KOREKTY();
                    rkor.Data_start = rpm.Data_start;
                    rkor.Data_stop = rpm.Data_stop;
                    rkor.guid = rpm.guid;
                    rkor.id_prac = rpm.id_prac;
                    rkor.Indeks = rpm.Indeks;
                    rkor.minuty_norma = rpm.minuty_norma;
                    rkor.minuty_norma_ = rpm.minuty_norma_;
                    rkor.minuty_praca = rpm.minuty_praca;
                    rkor.minuty_praca_ = rpm.minuty_praca_;
                    rkor.mnoznik_czasu_pracy = rpm.mnoznik_czasu_pracy;
                    rkor.Nazwa_pracownika = rpm.Nazwa_pracownika;
                    rkor.Nazwa_zlecenia = rpm.Nazwa_zlecenia;
                    rkor.nr_stanowiska = rpm.nr_stanowiska;
                    rkor.Nr_zlec_ipo = rpm.Nr_zlec_ipo;
                    rkor.status = rpm.status;
                    rkor.typ_pracy = rpm.typ_pracy;
                    rkor.zdawka_dobre = rpm.zdawka_dobre;
                    rkor.zdawka_zle = rpm.zdawka_zle;
                    rkor.Komentarz = rpm.id.ToString();
                    db.RAPORT_MONTAZ_KOREKTies.InsertOnSubmit(rkor);
                    db.SubmitChanges();


                }

                rpm.Data_start = dateTimePicker1.Value.Date.AddDays(n) + dt_czas_start.Value.TimeOfDay;
                rpm.Data_stop = dateTimePicker1.Value.Date.AddDays(n) + dt_czas_stop.Value.TimeOfDay;
                if (_id_rec == 0)
                {
                    rpm.guid = Guid.NewGuid();
                    rpm.Komentarz = $"Dodane {DateTime.Now} przez {_user}";
                    rpm.mnoznik_czasu_pracy = 1;
                }
                else 
                {
                    rpm.Komentarz = $"Zmodyfikowane {DateTime.Now} przez {_user}";
                    



                }
                rpm.id_prac = (int)comboBox1.SelectedValue;
                rpm.Indeks = _index;
                
                
                   

                rpm.minuty_norma = 0;
                rpm.minuty_norma_ = 0;
                int.TryParse(textBox1.Text, out int nr_zlec);

                rpm.Nr_zlec_ipo = _nr_zlec;
                TimeSpan? tsm = (rpm.Data_stop - rpm.Data_start);

                rpm.minuty_praca = (int)tsm.Value.TotalMinutes;
                rpm.minuty_praca_ = (decimal)tsm.Value.TotalMinutes;
                
                rpm.Nazwa_pracownika = comboBox1.Text;
                rpm.typ_pracy = comboBox3.SelectedItem.ToString();
                rpm.Nazwa_zlecenia = label10.Text;

                int.TryParse(textBox2.Text, out int dobre);
                rpm.zdawka_dobre = dobre;

                int.TryParse(textBox3.Text, out int zle);
                rpm.zdawka_zle = zle;



                rpm.status = comboBox2.SelectedItem.ToString();
                rpm.nr_stanowiska = comboBox4.SelectedItem.ToString();

                if (_id_rec == 0) db.RAPORT_MONTAZs.InsertOnSubmit(rpm);
                db.SubmitChanges();

            }
            this.Close();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "DNIÓWKA")
                dateTimePicker2.Enabled = true;
            else
                dateTimePicker2.Enabled = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() != "DNIÓWKA")
                dateTimePicker2.Value = dateTimePicker1.Value;
            
              
        }
    }
}
