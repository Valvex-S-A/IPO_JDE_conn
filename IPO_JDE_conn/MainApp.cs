using API;
using IPO_JDE_conn.WebReference;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;


namespace IPO_JDE_conn
{
    public partial class MainApp : Form
    {
        public string logged = "";
        public int dg_doc_selected_row = -1;
        public int dg_akord_selected_row = -1;
        public DataGridViewSelectedRowCollection rows_to_update;
        byte[] bs;
        public MainApp(string _logged)
        {
            InitializeComponent();
            logged = _logged;
            db_raportyDataContext db = new db_raportyDataContext();

            var hale = from c in db.IPO_DOMYŚLNE_MAGAZYNies
                       orderby c.IPO_HALA
                       select c;

            comboBox1.DataSource = hale;
            dtp_from.Value = DateTime.Now.AddDays(-30);
            comboBox1.DisplayMember = "IPO_HALA";
            comboBox1.ValueMember = "IPO_HALA";
            comboBox2.DataSource = hale;
            comboBox2.DisplayMember = "IPO_HALA";
            comboBox2.ValueMember = "IPO_HALA";
            cb_PU.BackColor = Color.LightYellow;
            cb_RW.BackColor = Color.Orange;
            cb_PW.BackColor = Color.LightGreen;

            label30.Text = $"Stany do odśw:";
            
            label31.Text = $"Do zaks. w JDE:";
           
            label32.Text = $"Do przetw. IPO:  PW;  RW";
            label33.Text = DateTime.Now.ToShortTimeString();


        }

        private void MainApp_Load(object sender, EventArgs e)
        {
            this.Text = "Zalogowany: " + logged;

            if (logged == "apawlowski") button23.Enabled = true;
            else button23.Enabled = false;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Show_doc();

        }

        private void Show_doc()
        {
            dg_dok.DataSource = Get_doc();
            double ilosc = 0;
            double t_il = 0;
            foreach (DataGridViewRow row in this.dg_dok.Rows)
            {
                string ilosc_t = row.Cells[4].Value.ToString();
                t_il = double.Parse(ilosc_t);

                ilosc = ilosc + t_il;

            }
            lb_ilosc.Text = "Ilosc: " + Math.Round(ilosc, 3).ToString();
        }




        private dynamic Get_doc()
        {

            db_raportyDataContext db = new db_raportyDataContext();
            int date_from = dtp_from.Value.Year * 10000 + dtp_from.Value.Month * 100 + dtp_from.Value.Day;
            int date_to = dtp_to.Value.Year * 10000 + dtp_to.Value.Month * 100 + dtp_to.Value.Day;

            var doc = (from c in db.IPO_ZDAWKA_PW_4s
                       where c.DATA_ZMIANY.Value.Year * 10000 + c.DATA_ZMIANY.Value.Month * 100 + c.DATA_ZMIANY.Value.Day >= date_from


                       && c.DATA_ZMIANY.Value.Year * 10000 + c.DATA_ZMIANY.Value.Month * 100 + c.DATA_ZMIANY.Value.Day <= date_to
                       select new { c.ID, c.Nr_zlecenia_IPO, c.Nr_indeksu, c.Nazwa_pozycji, c.Ilosc, c.Magazyn_IPO, c.JM, c.RW_PW, c.DATA_ZMIANY, c.ZMIANA, Hala = c.HALA_PROD, c.Zaksiegowany_JDE, c.Data_utworzenia_poz, c.Czy_korygowany, Korekta_przez = c.Nr_seryjny, c.Powod_korekty, c.IPO_ID_POZYCJI });

            if (comboBox1.SelectedValue.ToString() != "[WSZYSTKIE]")
            {
                doc = doc.Where(c => c.Hala == comboBox1.SelectedValue.ToString());

            }
            if (rb1.Checked) doc = doc.Where(c => c.ZMIANA == "I");
            if (rb2.Checked) doc = doc.Where(c => c.ZMIANA == "II");
            if (rb3.Checked) doc = doc.Where(c => c.ZMIANA == "III");

            if (!cb_RW.Checked) doc = doc.Where(c => c.RW_PW != "RW");
            if (!cb_PW.Checked) doc = doc.Where(c => c.RW_PW != "PW");
            if (!cb_PU.Checked) doc = doc.Where(c => c.RW_PW != "PU");
            if (!string.IsNullOrEmpty(this.tb_nr_zl_ipo.Text))
            {
                int i;
                int.TryParse(this.tb_nr_zl_ipo.Text, out i);

                doc = doc.Where(c => c.Nr_zlecenia_IPO == i);
            }
            if (!string.IsNullOrEmpty(this.tb_index_ipo.Text))
            {
                doc = doc.Where(c => c.Nr_indeksu == this.tb_index_ipo.Text);
            }
            if (cb_error_only.Checked)
            { doc = doc.Where(c => (c.Korekta_przez != "OK" && c.Korekta_przez != "OK-")); }




            return doc;





        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dg_dok_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dg_doc_selected_row = e.RowIndex;
            if (!(dg_dok.CurrentCell is null))
            {
                if ((dg_dok.CurrentCell.RowIndex) != -1)
                {
                    string nr_zl = this.dg_dok.Rows[dg_dok.CurrentCell.RowIndex].Cells[1].Value.ToString();
                    string nr_indeks = this.dg_dok.Rows[dg_dok.CurrentCell.RowIndex].Cells[2].Value.ToString();
                    tb_nr_zl_ipo.Text = nr_zl;
                    tb_index_ipo.Text = nr_indeks;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {



            KORYGUJ_DOC(dg_doc_selected_row);
        }
        private void button11_Click(object sender, EventArgs e)
        {

            KORYGUJ_MAG(dg_doc_selected_row);
        }



        private void KORYGUJ_MAG(int _row)
        {
            if (dg_dok.CurrentRow.Index == -1) return;
            if (_row != -1)
            {
                string ID = this.dg_dok.Rows[_row].Cells[0].Value.ToString();
                long id = long.Parse(ID);

                Korekta_MAG mag = new Korekta_MAG(id);
                mag.ShowDialog();
                if (!mag.anuluj)
                {
                    db_raportyDataContext db = new db_raportyDataContext();
                    var rec = (from c in db.IPO_ZDAWKA_PWs
                               where c.ID == int.Parse(ID)
                               select c).Single();
                    var krec = Clone<IPO_ZDAWKA_PW>(rec);
                    double koszt_jn = (double)krec.Koszt_IPO / (krec.Ilosc == 0 ? 1 : (double)krec.Ilosc);

                    krec.Ilosc = krec.Ilosc * -1;
                    krec.Koszt_IPO = krec.Koszt_IPO * -1;
                    krec.Zaksiegowany_JDE = false;
                    krec.Czy_korygowany = true;
                    krec.Nr_seryjny = logged;
                    krec.Data_utworzenia_poz = DateTime.Now;
                    krec.Powod_korekty = $"zmiana magazynów przez {logged}";
                    db.IPO_ZDAWKA_PWs.InsertOnSubmit(krec);


                    foreach (var m in mag.magazyny)
                    {
                        var nrec = Clone<IPO_ZDAWKA_PW>(rec);
                        nrec.Ilosc = m.ilosc;
                        nrec.Magazyn_IPO = m.magazyn;
                        nrec.Koszt_IPO = m.ilosc * koszt_jn;
                        nrec.Zaksiegowany_JDE = false;
                        nrec.Czy_korygowany = true;
                        nrec.Nr_seryjny = logged;
                        nrec.Data_utworzenia_poz = DateTime.Now;
                        nrec.Powod_korekty = "zmiana magazynów";
                        db.IPO_ZDAWKA_PWs.InsertOnSubmit(nrec);
                        db.SubmitChanges();

                    }







                    db.SubmitChanges();
                }





            }


        }
        private void KORYGUJ_DOC(DataGridViewSelectedRowCollection wiersze)
        {


        }


        private void KORYGUJ_DOC(int _row)
        {
            if (dg_dok.CurrentRow.Index == -1) return;
            if (_row != -1)
            {
                string ID = this.dg_dok.Rows[_row].Cells[0].Value.ToString();
                db_raportyDataContext db = new db_raportyDataContext();
                var rec = (from c in db.IPO_ZDAWKA_PWs
                           where c.ID == int.Parse(ID)
                           select c).Single();
                KORYGUJ_DOC_ kor = new KORYGUJ_DOC_((int)rec.Nr_zlecenia_IPO, (int)rec.Ilosc);
                kor.ShowDialog();

                if (kor._nr_zlec != 0)
                {
                    var krec = Clone<IPO_ZDAWKA_PW>(rec);

                    double koszt_jn = (double)krec.Koszt_IPO / (krec.Ilosc == 0 ? 1 : (double)krec.Ilosc);

                    krec.Ilosc = krec.Ilosc * -1;
                    krec.Koszt_IPO = krec.Koszt_IPO * -1;
                    krec.Zaksiegowany_JDE = false;
                    krec.Czy_korygowany = true;
                    krec.Nr_seryjny = logged;
                    krec.Data_utworzenia_poz = DateTime.Now;
                    krec.Powod_korekty = logged + ":" + kor._komentarz;


                    var nrec = Clone<IPO_ZDAWKA_PW>(rec);
                    nrec.Ilosc = kor._nowa_ilosc;
                    nrec.Koszt_IPO = koszt_jn * kor._nowa_ilosc;

                    nrec.Zaksiegowany_JDE = false;
                    nrec.Czy_korygowany = true;
                    nrec.Data_utworzenia_poz = DateTime.Now;

                    nrec.Nr_seryjny = logged;
                    db.IPO_ZDAWKA_PWs.InsertOnSubmit(krec);
                    db.IPO_ZDAWKA_PWs.InsertOnSubmit(nrec);
                    rec.Czy_korygowany = true;
                    nrec.Powod_korekty = kor._komentarz;
                    db.SubmitChanges();


                }
                kor.Dispose();
                Show_doc();

            }
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

        private void ANULUJ_Click(object sender, EventArgs e)
        {
            ANULUJ_DOC(dg_doc_selected_row);
        }

        private void ANULUJ_DOC(int _row)
        {
            if (_row != -1)
            {
                string ID = this.dg_dok.Rows[_row].Cells[0].Value.ToString();
                db_raportyDataContext db = new db_raportyDataContext();
                var rec = (from c in db.IPO_ZDAWKA_PWs
                           where c.ID == int.Parse(ID)
                           select c).Single();

                var krec = Clone<IPO_ZDAWKA_PW>(rec);
                krec.Ilosc = krec.Ilosc * -1;
                krec.Koszt_IPO = krec.Koszt_IPO * -1;
                krec.Zaksiegowany_JDE = false;
                krec.Czy_korygowany = true;
                krec.Nr_seryjny = logged;
                krec.Data_utworzenia_poz = DateTime.Now;

                db.IPO_ZDAWKA_PWs.InsertOnSubmit(krec);

                rec.Czy_korygowany = true;
                db.SubmitChanges();
                Show_doc();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            POKAZ_AKORD();

        }

        private void POKAZ_AKORD()
        {
            db_raportyDataContext db = new db_raportyDataContext();
            int date_from = dtp_akord_od.Value.Year * 10000 + dtp_akord_od.Value.Month * 100 + dtp_akord_od.Value.Day;
            int date_to = dtp_akord_do.Value.Year * 10000 + dtp_akord_do.Value.Month * 100 + dtp_akord_do.Value.Day;
            var lista = (from c in db.IPO_Tasks_upds
                         where c.Czas_start.Value >= dtp_akord_od.Value &&
                         c.Czas_start.Value <= dtp_akord_do.Value
                         orderby c.Id_pracownika, c.Czas_start
                         select new { c.ID, c.Id_pracownika, c.Pracownik, c.Nazwa_operacji, c.Opis_pracy, c.Centrum_robocze, c.Indeks, c.Ilosc_wykonana, c.Ilosc_brak, c.Ilosc_planowana, c.Id_zlecenia, Czas_start = c.Czas_start.GetValueOrDefault(), Czas_Stop = c.Czas_stop.GetValueOrDefault(), c.Czas_realizacji, c.DATA_ZMIANY, c.ZMIANA, c.Hala, KOREKT = (c.korekt == null ? 0 : c.korekt), c.Task_Id }
                         );





            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                lista = lista.Where(c => c.Id_zlecenia == int.Parse(textBox1.Text));
            }
            if (!string.IsNullOrEmpty(KodDetalu.Text))
            {
                lista = lista.Where(c => c.Indeks == KodDetalu.Text);
            }
            if (!string.IsNullOrEmpty(NrPracownika.Text))
            {
                int i;
                int.TryParse(NrPracownika.Text, out i);
                lista = lista.Where(c => c.Id_pracownika == i);
            }
            if (comboBox2.SelectedValue.ToString() != "[WSZYSTKIE]")
            {
                lista = lista.Where(c => c.Hala == comboBox2.SelectedValue.ToString());

            }
            if (rba1.Checked) lista = lista.Where(c => c.ZMIANA == "I");
            if (rba2.Checked) lista = lista.Where(c => c.ZMIANA == "II");
            if (rba3.Checked) lista = lista.Where(c => c.ZMIANA == "III");


            dataGridView1.DataSource = lista;


        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            if (dg_akord_selected_row != -1)
            {
                int ID = int.Parse(dataGridView1.Rows[dg_akord_selected_row].Cells[0].Value.ToString());

                this.KORYGUJ_AKORD_(ID);
                POKAZ_AKORD();
            }
        }

        private void KORYGUJ_AKORD_(int ID)
        {

            KORYGUJ_AKORD akr = new KORYGUJ_AKORD(ID, logged);
            akr.ShowDialog();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dg_akord_selected_row = e.RowIndex;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            List<int> zam = new List<int>();

            foreach (string l in textBox2.Lines)
            {
                int l_nr = 0;
                if (int.TryParse(l, out l_nr))
                {
                    zam.Add(l_nr);

                }


            }






            if (zam.Count() != 0)
            {


                WebReference.Service1 client = new WebReference.Service1();
                // string test =  client.IPO_Update_order_sql(nr_zam);
                pdf_przewodnik.Gen_przew(zam, logged, cbKKC.Checked);

            }









        }



        public void CopyToClipboardWithHeaders(DataGridView _dgv)
        {

            _dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgv.MultiSelect = true;
            _dgv.SelectAll();
            //Copy to clipboard
            _dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = _dgv.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);


            _dgv.MultiSelect = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Document doc = new Document(PageSize.A4);
            string filename = "ZDAWKA_" + logged + "_" + comboBox1.SelectedValue.ToString() + DateTime.Now.ToString().Replace(':', ' ') + ".pdf";
            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fb = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);

            PdfWriter.GetInstance(doc, new FileStream("C:/temp/" + filename, FileMode.Create));
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = fb;
            title.Alignment = Element.ALIGN_CENTER;
            title.Add("ZDAWKA ZA " + dtp_from.Value.ToShortDateString());

            doc.Add(title);

            doc.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.CopyToClipboardWithHeaders(this.dg_dok);
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if ((int)dataGridView1.Rows[e.RowIndex].Cells[17].Value != 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Nowy_wpis nwp = new Nowy_wpis(logged);
            nwp.ShowDialog();
        }

        private void dg_dok_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if ((string)dg_dok.Rows[e.RowIndex].Cells[7].Value == "PW")
            {
                dg_dok.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;

            }

            if ((string)dg_dok.Rows[e.RowIndex].Cells[7].Value == "RW")
            {
                dg_dok.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;

            }

            if ((string)dg_dok.Rows[e.RowIndex].Cells[7].Value == "PU")
            {
                dg_dok.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;

            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            // pdf_przewodnik.GenPDFFileRaport(Program.LINQToDataTable(Get_doc()), logged);
            DialogResult dl = MessageBox.Show($"Czy chcesz pokazać raport błędów dla {comboBox1.SelectedValue.ToString()} , za dzień {dtp_to.Value.ToShortDateString()} ? "
                , "Zlecenia...", MessageBoxButtons.YesNo);

            if (dl == DialogResult.No || dl == DialogResult.Cancel) { return; }

            string wartosc = "Wartość, poniżej której ukrywamy różnice ";
            ShowInputDialog(ref wartosc);

            double.TryParse(wartosc, out double min_wartosc);

          

 

            List<string> lista_zlec = new List<string>();
            DateTime data_rap = dtp_to.Value.Date;

            foreach (DataGridViewRow r in dg_dok.Rows)
            {

                string nr_zlec = r.Cells[1].Value.ToString();

                if (r.Cells[7].Value.ToString().Trim() == "PW")
                { lista_zlec.Add(nr_zlec); }





            }
            var dlista_zlec = lista_zlec.Distinct().ToList<string>();

            Analiza_dzien anal = new Analiza_dzien(dlista_zlec, min_wartosc, true);
            anal.ShowDialog();


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dg_akord_selected_row != -1)
            {
                int ID = int.Parse(dataGridView1.Rows[dg_akord_selected_row].Cells[0].Value.ToString());
                db_raportyDataContext db = new db_raportyDataContext();
                var rec = (from c in db.IPO_Tasks_upds
                           where c.ID == ID
                           select c).Single();
                int task_id = (int)rec.Task_Id;
                kto_korygowal k = new kto_korygowal(task_id);
                k.ShowDialog();


            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CopyToClipboardWithHeaders(this.dataGridView1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string Item = this.dg_dok.Rows[dg_doc_selected_row].Cells[2].Value.ToString();
            string nr_zlec = this.tb_nr_zl_ipo.Text;
            Cardex c = new Cardex(Item, nr_zlec);

            c.ShowDialog();
            c.Dispose();


        }

        private void button13_Click(object sender, EventArgs e)
        {
            Nowe_PW pw = new Nowe_PW(logged);
            pw.ShowDialog();
            pw.Dispose();
        }

        private void dg_dok_MultiSelectChanged(object sender, EventArgs e)
        {
            rows_to_update = dg_dok.SelectedRows;

        }

        private void test()
        {
            string[] tablica = new string[] { "1", "2", "3", "a", "ab", "cc", "gg" };

            var tablicaa = tablica.Where(x => x.StartsWith("a"));


            foreach (string a in tablicaa)
            {



            }


        }


        private void button14_Click(object sender, EventArgs e)
        {
            Skasuj_zlecenie form = new Skasuj_zlecenie(logged);
            form.ShowDialog();
        }

        private void cb_PW_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            WebReference.Service1 srv = new WebReference.Service1();

            List<int> zam = new List<int>();

            foreach (string l in tb_zlecenia_upd.Lines)
            {
                int l_nr = 0;
                if (int.TryParse(l, out l_nr))
                {
                    if (l_nr != 0) zam.Add(l_nr);

                }

            }

            if (zam.Count != 0 && (cbNrZam.Checked || cbNrZlecF.Checked || cbPriorytet.Checked || cbAutop.Checked))
            {
                foreach (int z in zam)
                {
                    string Nr_zlec_F = "";
                    string Nr_zam = "";
                    int priorytet = -1;
                    bool auto = false;
                    decimal ilosc = 0;
                    bool tryb_f = checkBox1.Checked;


                    if (cbPriorytet.Checked) priorytet = int.Parse(cbox_prior.SelectedItem.ToString());
                    if (cbNrZlecF.Checked) Nr_zlec_F = tb_nr_zlec_f.Text;
                    if (cbNrZam.Checked) Nr_zam = tb_nr_zam.Text;
                    if (cbAutop.Checked) { auto = cb_autop_wl.Checked; decimal.TryParse(tb_auto_ilosc.Text, out ilosc); }


                    bool dodac_zlec = cb_nr_zam_dodaj.Checked;
                    string kom = srv.IPO_akt_dane_zlec(z, Nr_zlec_F, Nr_zam, priorytet, auto, ilosc, tryb_f, dodac_zlec);
                }




            }


        }

        private async void button16_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            button16.Enabled = false;
            await  System.Threading.Tasks.Task.Run(() => RozbijNaSkladowe());


            button16.Enabled = true;

            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sf.FileName, bs);
            }






        }

        private StringBuilder RozbijNaSkladoweJedenKod(string LITM, string okres, int qty, List<SLOWNIK_1> slownik)
        {
            StringBuilder sb = new StringBuilder();
            string[] delimiter = { Environment.NewLine };
            DB2008DataContext db = new DB2008DataContext();
            List<ListaBOM> total = new List<ListaBOM>();
            List<ListaBOM> Current = BOM.GetBom(qty, LITM);
            foreach (var l in Current)
            {
                total.Add(l);
            }
            var g = (from c in total
                     orderby c.main_grp, c.lnk_grp descending, c.level
                     select new { Typ = c.has_child ? "BOM" : "MAT", Wyrób = c.wyrob_l.Trim(), Składnik = c.skl_l, Nazwa = c.nazwa_s.Trim(), ILOSC_NORMA = c.ilosc_norma, ILOSC_ZAP = c.ilosc }).ToList();
            ;

            //var resp = (from c in g select new { c.Składnik, c.Typ }).Distinct();

            //g = g.Where(x => x.Typ == "BOM");

            


            var nresp = (from c in g
                         join d in slownik on c.Składnik.Trim() equals d.IMLITM.Trim()
                         //where d.KOD_PLAN == "OBRÓBKA" && d.KOLOR == "OBR"
                         select new { okres, Wyrob = LITM, Składnik = c.Składnik.Trim(), d.NAZWA, d.KOD_PLAN, d.KOLOR, c.ILOSC_ZAP, d.NR_RYS }).ToArray();

            //sb.AppendLine("ITM;NAZWA;KOD_PLANISTY;KOLOR;STKT;PARTIA_OPT;PARTIA_MIN;BOM/MAT");
            foreach (var l in nresp)
            {
                bool check = false;
                if (cb_a_galwanika.Checked && l.KOD_PLAN == "GALWANIKA") check = true;
                if (cb_a_brak.Checked && l.KOD_PLAN == "BRAK") check = true;
                if (cb_a_mgraff.Checked && l.KOD_PLAN == "MONT.GRAFF") check = true;
                if (cb_a_mvalvex.Checked && l.KOD_PLAN == "MONT.VALVEX") check = true;
                if (cb_a_obrobka.Checked && l.KOD_PLAN == "OBRÓBKA") check = true;
                if (cb_a_szlifiernia.Checked && l.KOD_PLAN == "SZLIFIERNIA") check = true;
                if (cb_a_odlewnia.Checked && l.KOD_PLAN == "ODLEWNIA") check = true;
                if (cb_a_po_usl.Checked && l.KOD_PLAN == "PO.USL") check = true;
                if (cb_a_prasownia.Checked && l.KOD_PLAN == "PRASOWNIA") check = true;


                if (check) sb.AppendLine($"{l.okres};{l.Wyrob};{l.Składnik};{l.KOLOR};{l.NR_RYS};{l.KOD_PLAN};{l.NAZWA};{l.ILOSC_ZAP}");

            }


            return sb;
        }



        private void RozbijNaSkladowe()
        {
            string[] delimiter = { Environment.NewLine };

            string[] Lines = textBox3.Text.Split(delimiter, StringSplitOptions.None);
            DB2008DataContext db = new DB2008DataContext();
            List<ListaBOM> total = new List<ListaBOM>();

            int l1 = 1, l2 = 1;
            foreach (string LITM in Lines)
            {

                label11.Text = "Przebieg 1: " + l1++.ToString() + " / " + Lines.Count().ToString();
                label11.Update();
                this.Refresh();
                List<ListaBOM> Current = BOM.GetBom(1, LITM);
                foreach (var l in Current)
                {
                    total.Add(l);
                }

            }

            var g = from c in total
                    orderby c.main_grp, c.lnk_grp descending, c.level
                    select new { Typ = c.has_child ? "BOM" : "MAT", Wyrób = c.wyrob_l, Składnik = c.skl_l, Nazwa = c.nazwa_s, ILOSC_NORMA = c.ilosc_norma, ILOSC_ZAP = c.ilosc }
                     ;

            var resp = (from c in g select new { c.Składnik, c.Typ }).Distinct();

            if (!cbBOM.Checked) resp = resp.Where(x => x.Typ != "BOM");
            if (!cbMAT.Checked) resp = resp.Where(x => x.Typ != "MAT");
            var sltemp = (from d in db.SLOWNIK_1s
                          select new { d.IMLITM, d.NAZWA, d.KOD_PLAN, d.KOLOR, d.IMSTKT, d.SeriaOptymalna, d.ZapasBezpieczenstwa }).ToList();


            var nresp = (from c in resp
                         join d in sltemp on c.Składnik.Trim() equals d.IMLITM.Trim()
                         select new { Składnik = c.Składnik.Trim(), d.NAZWA, d.KOD_PLAN, d.KOLOR, d.IMSTKT, d.SeriaOptymalna, d.ZapasBezpieczenstwa, c.Typ }).ToArray();



            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("BOM;KOD_PLANISTY;KOLOR;STKT;PARTIA_OPT;PARTIA_MIN");
            sb.AppendLine("ITM;NAZWA;KOD_PLANISTY;KOLOR;STKT;PARTIA_OPT;PARTIA_MIN;BOM/MAT");
            foreach (var l in nresp)
            {
                label11.Text = $"Przebieg 2: {l2++.ToString()} / {resp.Count().ToString()}";

                sb.AppendLine($"{l.Składnik};{l.NAZWA};{l.KOD_PLAN};{l.KOLOR};{l.IMSTKT};{l.SeriaOptymalna.ToString()};{l.ZapasBezpieczenstwa.ToString()};{l.Typ}");

            }
            string ms = sb.ToString();
            bs =
               Encoding.Unicode.GetBytes(ms);


        }

        private void button17_Click(object sender, EventArgs e)
        {
            string[] delimiter = { Environment.NewLine };

            string[] Lines = textBox4.Text.Split(delimiter, StringSplitOptions.None);
            DB2008DataContext db = new DB2008DataContext();
            bool status = false;
            status = radioButton1.Checked;
            WebReference.Service1 srv = new WebReference.Service1();

            int licznik = 1;
            foreach (string LITM in Lines)
            {
                bool autogrow = true;
                var kod_plan = db.SLOWNIK_1s.Where(x => x.IMLITM == LITM).First();
                switch (kod_plan.KOD_PLAN)
                {
                    case "PRASOWNIA":
                        {
                            if (!cbPrasownia.Checked) autogrow = false;

                            break;
                        }
                    case "ODLEWNIA":
                        {
                            if (!cbODLEWNIA.Checked) autogrow = false;

                            break;
                        }
                    case "OBRÓBKA":
                        {
                            if (!cbOBRÓBKA.Checked) autogrow = false;

                            break;
                        }
                    case "BRAK":
                        {
                            if (!cbBRAK.Checked) autogrow = false;

                            break;
                        }
                    case "MONT.GRAFF":
                        {
                            if (!cbMONTGRAFF.Checked) autogrow = false;

                            break;
                        }
                    case "GALWANIKA":
                        {
                            if (!cbGALWANIKA.Checked) autogrow = false;

                            break;
                        }
                    case "SZLIFIERNIA":
                        {
                            if (!cbSZLIFIERNIA.Checked) autogrow = false;

                            break;
                        }
                    case "MONT.VALVEX":
                        {
                            if (!cbMONTVALVEX.Checked) autogrow = false;

                            break;
                        }
                    case "PO.USL":
                        {
                            if (!cbPOUSL.Checked) autogrow = false;

                            break;
                        }
                    default:
                        {

                            break;
                        }
                }


                srv.IPO_Zmien_autoprodukcje_litm(status, autogrow, (int)kod_plan.IMITM, (int)kod_plan.SeriaOptymalna, (int)kod_plan.ZapasBezpieczenstwa);
                label9.Text = licznik++.ToString() + "/" + Lines.Count().ToString();
                label9.Refresh();
            }

            button16.Enabled = true;

        }

        private void button18_Click(object sender, EventArgs e)
        {

            DB2008DataContext db = new DB2008DataContext();

            int licznik = 1;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {


                if (!row.IsNewRow)
                {
                    try
                    {
                        label10.Text = licznik++.ToString() + "/" + dataGridView2.Rows.Count.ToString();
                        label10.Refresh();
                        string indeks = row.Cells[0].Value.ToString();
                        int partia_opt = 0, partia_min = 0, partia_max = 0, dni_dost = 0;

                        int.TryParse(row.Cells[2].Value.ToString(), out partia_opt);
                        int.TryParse(row.Cells[3].Value.ToString(), out partia_min);

                        string magazyn = row.Cells[1].Value.ToString().Trim();

                        int.TryParse(row.Cells[4].Value.ToString(), out partia_max);
                        int.TryParse(row.Cells[5].Value.ToString(), out dni_dost);

                        if (string.IsNullOrEmpty(magazyn)) magazyn = "PROD";


                        var item = (from c in db.SLOWNIK_1s where c.IMLITM == indeks select c).ToList().FirstOrDefault();

                        var mag_podst = (from c in db.IPO_MAGAZYN_PODSTAWOWY_PWs where c.LIITM == item.IMITM select c.LIMCU).ToList().FirstOrDefault();
                        //magazyn = mag_podst.Trim();
                        row.Cells[1].Value = magazyn.Trim();
                        var d4102s = (from c in db.F4102s where c.IBLITM == indeks && (new string[] { "62", "61", "PROD" }).Contains(c.IBMCU.Trim()) select c);
                       foreach (var d in d4102s)
                        {

                            
                            d.IBSAFE = partia_min * 10000;
                            d.IBRQMN = partia_opt * 10000;
                            d.IBRQMX = partia_max * 10000;
                            d.IBLTCM = dni_dost;
                            db.SubmitChanges();

                        }
                    }
                    catch 
                    {
                        MessageBox.Show("Sprawdz poprawność danych. Nie zaktualizowana pozycja " + indeks);
                    }

                }


            }
            dataGridView2.Rows.Clear();
             

        }

        private async void button19_Click(object sender, EventArgs e)
        {
            string[] delimiter = { Environment.NewLine };

            string[] Lines = textBox4.Text.Split(delimiter, StringSplitOptions.None);

            Control.CheckForIllegalCrossThreadCalls = false;
            await System.Threading.Tasks.Task.Run(() =>
            {
                DB2008DataContext db = new DB2008DataContext();




                bool status = false;
                status = radioButton1.Checked;
                WebReference.Service1 srv = new WebReference.Service1();
                WEB_IPO.IPO ipo = new WEB_IPO.IPO();    
                int licznik = 1;
                foreach (string LITM in Lines)
                {

                    label9.Text = $"{licznik.ToString()}/{Lines.Count()}";
                    System.Threading.Thread.Sleep(1000);
                    srv.IPOupdateItem_LITM(LITM);
                    ipo.Zmien_blokade_indeksu(LITM, false);


                    licznik++;
                }


            });


           



        }

        private void dataGridView3_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int iFail = 0, iRow = dataGridView3.CurrentCell.RowIndex;
                    int iCol = dataGridView3.CurrentCell.ColumnIndex;
                    DataGridViewCell oCell;

                    for (int n = 0; n < lines.Count() - 1; n++)
                        dataGridView3.Rows.Add();

                    foreach (string line in lines)
                    {
                        if (iRow < dataGridView3.RowCount && line.Length > 0)
                        {
                            string[] sCells = line.Split('\t');
                            for (int i = 0; i < sCells.GetLength(0); ++i)
                            {
                                if (iCol + i < this.dataGridView3.ColumnCount)
                                {
                                    oCell = dataGridView3[iCol + i, iRow];
                                    if (!oCell.ReadOnly)
                                    {
                                        if ((oCell.Value ?? "").ToString() != sCells[i])
                                        {
                                            oCell.Value = Convert.ChangeType(sCells[i],
                                                                  oCell.ValueType);
                                            oCell.Style.BackColor = Color.Tomato;
                                        }
                                        else
                                            iFail++;
                                        //only traps a fail if the data has changed 
                                        //and you are pasting into a read only cell
                                    }
                                }
                                else
                                { break; }
                            }

                            iRow++;
                        }
                        else
                        { break; }
                        if (iFail > 0)
                            MessageBox.Show(string.Format("{0} updates failed due" +
                                            " to read only column setting", iFail));
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("The data you pasted is in the wrong format for the cell");
                    return;
                }

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {

            WebReference.Service1 srv = new WebReference.Service1();

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {


                if (!row.IsNewRow)
                {
                    try
                    {
                        string tekst = "";
                        string Nr_zam = row.Cells[0].Value.ToString();
                        if (cb_ex_dodaj.Checked) tekst = "/" + row.Cells[1].Value.ToString();
                        else tekst = row.Cells[1].Value.ToString();
                        int nr_zlec = 0;
                        int.TryParse(Nr_zam, out nr_zlec);


                        string kom = srv.IPO_akt_dane_zlec(nr_zlec, "", tekst, -1, false, 0, false, cb_ex_dodaj.Checked);
                    }
                    catch { }
                }
            }
            dataGridView3.Rows.Clear();
            MessageBox.Show("Gotowe!");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var db = new DB2008DataContext();

            int licznik = 1;
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {

                if (!row.IsNewRow)
                {
                    string indeks_z = (row.Cells[0].Value ?? "").ToString();
                    string jm_z = (row.Cells[1].Value ?? "").ToString();
                    string indeks_do = (row.Cells[2].Value ?? "").ToString();
                    string jm_do = (row.Cells[3].Value ?? "").ToString();

                    string ilosc_z = (row.Cells[4].Value ?? "").ToString();
                    string ilosc_do = (row.Cells[5].Value ?? "").ToString();
                    string magazyn_z = (row.Cells[6].Value ?? "").ToString();
                    string magazyn_do = (row.Cells[7].Value ?? "").ToString();
                    string lokalizacja_z = (row.Cells[8].Value ?? "").ToString();
                    string lokalizacja_do = (row.Cells[9].Value ?? "").ToString();
                    string komentarz = (row.Cells[10].Value ?? "").ToString();

                    if (magazyn_z != "PROD") { row.Cells[10].Value = "Tylko na PROD"; continue; }
                    if (magazyn_do != "PROD") { row.Cells[10].Value = "Tylko na PROD"; continue; }
                

                    if (!string.IsNullOrEmpty(komentarz)) continue;

                    var check_ind_z = from c in db.SLOWNIK_1s
                                      where c.IMLITM == indeks_z
                                      select c;

                    if (check_ind_z.Count() != 1) { row.Cells[10].Value = "Zły indeks_z !!!"; continue; }
                    indeks_z = check_ind_z.First().IMLITM;


                    var check_ind_do = from c in db.SLOWNIK_1s
                                       where c.IMLITM == indeks_do
                                       select c;
                    
                    if (check_ind_do.Count() != 1) { row.Cells[10].Value = "Zły indeks_do !!!"; continue; }
                    indeks_do = check_ind_do.First().IMLITM;


                    var check_mag_z = from c in db.IPO_magazyny_IPO2JDE
                                      where c.LIMCU.Trim() == magazyn_z.Trim()
                                      select c;

                    if (check_mag_z.Count() == 0) { row.Cells[10].Value = "Zły magazyn_z !!!"; continue; }
                    magazyn_z = check_mag_z.First().LIMCU;



                    var check_mag_do = from c in db.IPO_magazyny_IPO2JDE
                                       where c.LIMCU.Trim() == magazyn_do.Trim()
                                       select c;

                    if (check_mag_do.Count() == 0) { row.Cells[10].Value = "Zły magazyn_do !!!"; continue; }
                    magazyn_do = check_mag_do.First().LIMCU;
                    var check_lok_z = from c in db.IPO_magazyny_IPO2JDE
                                      where c.LILOCN.Trim() == lokalizacja_z.Trim()
                                      select c;

                    if (check_lok_z.Count() == 0) { row.Cells[10].Value = "Zła lokalizacja_z !!!"; continue; }
                    lokalizacja_z = check_lok_z.First().LILOCN;

                    var check_lok_do = from c in db.IPO_magazyny_IPO2JDE
                                       where c.LILOCN.Trim() == lokalizacja_do.Trim()
                                       select c;

                    if (check_lok_do.Count() == 0) { row.Cells[10].Value = "Zła lokalizacja_do !!!"; continue; }
                    lokalizacja_do = check_lok_do.First().LILOCN;
                    double _i_ilosc_z = 0; double _i_ilosc_do = 0;
                    double.TryParse(ilosc_z, out _i_ilosc_z);
                    double.TryParse(ilosc_do, out _i_ilosc_do);
                    if (_i_ilosc_z == 0 || _i_ilosc_do == 0) { row.Cells[10].Value = "Zła ilość !!!"; continue; }

                    trans.trans tr = new trans.trans();

                    if (lokalizacja_z == "") lokalizacja_z = String.Empty;
                    if (lokalizacja_do == "") lokalizacja_do = String.Empty;
                    string wynik = tr.H7Raport_tmp(indeks_z, indeks_do, _i_ilosc_z, jm_z, _i_ilosc_do, jm_do, magazyn_z, lokalizacja_z, magazyn_do, lokalizacja_do, "magazyn");
                    //string wynik = tr.GraffBis_Poprawa_JM(indeks_z,jm_z, indeks_do,jm_do, _i_ilosc_z,_i_ilosc_do, magazyn_z, magazyn_do, lokalizacja_z, lokalizacja_do, logged);
                    row.Cells[10].Value = wynik;

                }

            }
        }

        private void b_symbol_Click(object sender, EventArgs e)
        {

            WebReference.Service1 s = new WebReference.Service1();
            foreach (string l in tb_symbol.Lines)
            {


                if (!string.IsNullOrEmpty(l)) s.IPO_zmien_symbol(l, textBox5.Text);

            }
            MessageBox.Show("GOTOWE!");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Czy na pewno chcesz ten dokument zaksięgować ponownie?", "...", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dl == DialogResult.No) return;

            if (dg_doc_selected_row != -1)
            {
                string ID = this.dg_dok.Rows[dg_doc_selected_row].Cells[0].Value.ToString();
                db_raportyDataContext db = new db_raportyDataContext();
                var rec = (from c in db.IPO_ZDAWKA_PWs
                           where c.ID == int.Parse(ID)
                           select c).Single();

                rec.Zaksiegowany_JDE = false;
                rec.Data_ksiegowania_JDE = DateTime.Now;
                rec.Czy_korygowany = true;
                db.SubmitChanges();
                MessageBox.Show("Zaksięgowano. Po 3 minutach sprawdź czy OK.");
            }

        }

        private void button24_Click(object sender, EventArgs e)
        {
            var db = new DB2008DataContext();

            int licznik = 1;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {

                if (!row.IsNewRow)
                {


                    string indeks_z = (row.Cells[0].Value ?? "").ToString();
                    string indeks_do = (row.Cells[1].Value ?? "").ToString();
                    string ilosc = (row.Cells[2].Value ?? "").ToString();

                    string magazyn_z = (row.Cells[3].Value ?? "").ToString();

                    string lokalizacja_z = (row.Cells[4].Value ?? "").ToString();

                    string komentarz = (row.Cells[5].Value ?? "").ToString();

                    if (!string.IsNullOrEmpty(komentarz)) continue;

                    var check_ind_z = from c in db.SLOWNIK_1s
                                      where c.IMLITM == indeks_z
                                      select c;

                    if (check_ind_z.Count() != 1) { row.Cells[5].Value = "Zły indeks_z !!!"; continue; }

                    var check_ind_do = from c in db.SLOWNIK_1s
                                       where c.IMLITM == indeks_do
                                       select c;

                    if (check_ind_do.Count() != 1) { row.Cells[5].Value = "Zły indeks_do !!!"; continue; }

                    var check_mag_z = from c in db.IPO_magazyny_IPO2JDE
                                      where c.LIMCU.Trim() == magazyn_z.Trim()
                                      select c;

                    if (check_mag_z.Count() == 0) { row.Cells[5].Value = "Zły magazyn_z !!!"; continue; }





                    var check_lok_z = from c in db.IPO_magazyny_IPO2JDE
                                      where c.LILOCN.Trim() == lokalizacja_z.Trim()
                                      select c;

                    if (check_lok_z.Count() == 0) { row.Cells[5].Value = "Zła lokalizacja_z !!!"; continue; }





                    int _i_ilosc = 0;
                    int.TryParse(ilosc, out _i_ilosc);

                    if (_i_ilosc == 0) { row.Cells[5].Value = "Zła ilość !!!"; continue; }

                    //sprawdz stan mag.
                    var db2008 = new DB2008DataContext();
                    var stan = from c in db2008.IPO_STANies
                               where c.LITM.Trim() == indeks_z.Trim()
                               && c.MAG_ZAK == 0 && c.MAG.Trim() == magazyn_z.Trim() && c.LOK.Trim() == lokalizacja_z.Trim()
                               select c;
                    if (stan.Count() == 0) { row.Cells[5].Value = "BRAK STANU!!!"; continue; }

                    if (stan.Count() != 0)
                    {
                        var st = stan.Sum(x => x.QTY_PODST);
                        if (st < _i_ilosc) { row.Cells[5].Value = "BRAK STANU!!!"; continue; }
                    }



                    trans.trans tr = new trans.trans();

                    if (lokalizacja_z == "") lokalizacja_z = String.Empty;

                    string wynik = tr.H7Raport(indeks_z, indeks_do, _i_ilosc, magazyn_z, lokalizacja_z, "KOOPERACJA");
                    row.Cells[5].Value = wynik;

                    DialogResult dl = MessageBox.Show("Czy założyć transfer z H7PROD na MWG?", "Transfer...", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    if (dl == DialogResult.Yes)
                    {


                        var id = tr.MWG_Utwórz_transfer_ze_H7_na_MWG(indeks_do, "SZ", _i_ilosc, "Transfer H7->MWG");
                        //double id = srv.Zaloz_transferM4("        PROD", "H7PROD              ", indeks_do, "         MWG", _i_ilosc, "SZ",  logged, "Transfer H7");
                        MessageBox.Show("Założono transfer na MWG: " + id.ToString());

                    }



                    var db1 = new dbtransDataContext();
                    H7_REKLASYFIKACJA rek = new H7_REKLASYFIKACJA();
                    rek.Data_utw = DateTime.Now;
                    rek.ilosc = _i_ilosc;
                    rek.indeks_do = indeks_do;
                    rek.indeks_z = indeks_z;
                    rek.lokalizacja_z = lokalizacja_z;
                    rek.magazyn_z = magazyn_z;
                    rek.User = logged;
                    rek.wynik = wynik;
                    db1.H7_REKLASYFIKACJAs.InsertOnSubmit(rek);
                    db1.SubmitChanges();
                    this.dateTimePicker1.Value = DateTime.Now;
                    button25_Click(null, null); //zaktualizuj dane.
                    dataGridView5.Rows[row.Index].Selected = true;
                }

            }
        }

        private void dataGridView4_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int iFail = 0, iRow = dataGridView4.CurrentCell.RowIndex;
                    int iCol = dataGridView4.CurrentCell.ColumnIndex;
                    DataGridViewCell oCell;

                    for (int n = 0; n < lines.Count() - 1; n++)
                        dataGridView4.Rows.Add();

                    foreach (string line in lines)
                    {
                        if (iRow < dataGridView4.RowCount && line.Length > 0)
                        {
                            string[] sCells = line.Split('\t');
                            for (int i = 0; i < sCells.GetLength(0); ++i)
                            {
                                if (iCol + i < this.dataGridView4.ColumnCount)
                                {
                                    oCell = dataGridView4[iCol + i, iRow];
                                    if (!oCell.ReadOnly)
                                    {
                                        if ((oCell.Value ?? "").ToString() != sCells[i])
                                        {
                                            oCell.Value = Convert.ChangeType(sCells[i],
                                                                  oCell.ValueType);
                                            oCell.Style.BackColor = Color.Tomato;
                                        }
                                        else
                                            iFail++;
                                        //only traps a fail if the data has changed 
                                        //and you are pasting into a read only cell
                                    }
                                }
                                else
                                { break; }
                            }

                            iRow++;
                        }
                        else
                        { break; }
                        if (iFail > 0)
                            MessageBox.Show(string.Format("{0} updates failed due" +
                                            " to read only column setting", iFail));
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("The data you pasted is in the wrong format for the cell");
                    return;
                }
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                var db = new dbtransDataContext();

                var lista = from c in db.H7_REKLASYFIKACJAs
                            where c.Data_utw.Value.Date == this.dateTimePicker1.Value.Date
                            select c;

                dataGridView5.Rows.Clear();
                foreach (var l in lista)
                {

                    var index = dataGridView5.Rows.Add();
                    dataGridView5.Rows[index].Cells[0].Value = l.indeks_z;
                    dataGridView5.Rows[index].Cells[1].Value = l.indeks_do;
                    dataGridView5.Rows[index].Cells[2].Value = l.ilosc;
                    dataGridView5.Rows[index].Cells[3].Value = l.magazyn_z;
                    dataGridView5.Rows[index].Cells[4].Value = l.lokalizacja_z;
                    dataGridView5.Rows[index].Cells[5].Value = l.wynik;
                    dataGridView5.Rows[index].Cells[6].Value = l.User;


                }

            }
            catch { }



        }

        private void button26_Click(object sender, EventArgs e)
        {
            var db = new dbtransDataContext();



            var palety = from c in db.PALETY62s
                         where c.Data_utw.Value.Date >= dateTimePicker2.Value.Date && c.Data_utw.Value.Date <= dateTimePicker3.Value.Date
                         select c;


            if (tb_filtr_palety.Text != string.Empty)
            {
                palety = palety.Where(x => x.Nr_palety.Contains(tb_filtr_palety.Text));

            }
            datagrid_palety.DataSource = palety;
            datagrid_palety.Update();

        }

        private void button27_Click(object sender, EventArgs e)
        {
            string typ = this.dg_dok.Rows[dg_doc_selected_row].Cells[7].Value.ToString();
            string nr_zl = this.dg_dok.Rows[dg_doc_selected_row].Cells[1].Value.ToString();
            KORYGUJ_ZLEC z = new KORYGUJ_ZLEC(nr_zl);
            z.ShowDialog();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //if (dg_doc_selected_row == -1) return;
            string nr_zl = tb_nr_zl_ipo.Text;
            Analiza anal = new Analiza(nr_zl, logged);
            anal.ShowDialog();
        }

        private void dg_dok_CurrentCellChanged(object sender, EventArgs e)
        {

           
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            int n = 1;
            StringBuilder all = new StringBuilder();
            all.AppendLine("okres;Wyrob;l.Składnik;l.KOLOR;NR_RYS;l.KOD_PLAN;l.NAZWA;l.ILOSC_ZAP");
            var db = new DB2008DataContext();


            List<SLOWNIK_1> slownik = (from d in db.SLOWNIK_1s
                          select d).ToList();


            foreach (DataGridViewRow row in dataGridView6.Rows)
               {

                   row.Cells[0].Style.BackColor = Color.White;
                   if (!row.IsNewRow)
                   {
                       int qty = 0;
                       int.TryParse(row.Cells[2].Value.ToString(), out qty);

                       StringBuilder t =  this.RozbijNaSkladoweJedenKod(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString(), qty, slownik);

                      all.AppendLine(t.ToString());



                   }
                   label16.Text = $"Rozbito {n++.ToString()} z {dataGridView6.Rows.Count }";
                   label16.Update();
                   label16.Invalidate();


               }
          

            bs = Encoding.Unicode.GetBytes(all.ToString());

            SaveFileDialog sf1 = new SaveFileDialog();
            if (sf1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sf1.FileName, bs);
            }


        }

        private void DataGridView6_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int iFail = 0, iRow = dataGridView6.CurrentCell.RowIndex;
                    int iCol = dataGridView6.CurrentCell.ColumnIndex;
                    DataGridViewCell oCell;

                    for (int n = 0; n < lines.Count() - 1; n++)
                        dataGridView6.Rows.Add();

                    foreach (string line in lines)
                    {
                        if (iRow < dataGridView6.RowCount && line.Length > 0)
                        {
                            string[] sCells = line.Split('\t');
                            for (int i = 0; i < sCells.GetLength(0); ++i)
                            {
                                if (iCol + i < this.dataGridView6.ColumnCount)
                                {
                                    oCell = dataGridView6[iCol + i, iRow];
                                    if (!oCell.ReadOnly)
                                    {
                                        if ((oCell.Value ?? "").ToString() != sCells[i])
                                        {
                                            oCell.Value = Convert.ChangeType(sCells[i],
                                                                  oCell.ValueType);
                                            oCell.Style.BackColor = Color.Tomato;
                                        }
                                        else
                                            iFail++;
                                        //only traps a fail if the data has changed 
                                        //and you are pasting into a read only cell
                                    }
                                }
                                else
                                { break; }
                            }

                            iRow++;
                        }
                        else
                        { break; }
                        if (iFail > 0)
                            MessageBox.Show(string.Format("{0} updates failed due" +
                                            " to read only column setting", iFail));
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("The data you pasted is in the wrong format for the cell");
                    return;
                }

            }
        }

        private void dataGridView2_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int iFail = 0, iRow = dataGridView2.CurrentCell.RowIndex;
                    int iCol = dataGridView2.CurrentCell.ColumnIndex;
                    DataGridViewCell oCell;

                    for (int n = 0; n < lines.Count() - 1; n++)
                        dataGridView2.Rows.Add();

                    foreach (string line in lines)
                    {
                        if (iRow < dataGridView2.RowCount && line.Length > 0)
                        {
                            string[] sCells = line.Split('\t');
                            for (int i = 0; i < sCells.GetLength(0); ++i)
                            {
                                if (iCol + i < this.dataGridView2.ColumnCount)
                                {
                                    oCell = dataGridView2[iCol + i, iRow];
                                    if (!oCell.ReadOnly)
                                    {
                                        if ((oCell.Value ?? "").ToString() != sCells[i])
                                        {
                                            oCell.Value = Convert.ChangeType(sCells[i],
                                                                  oCell.ValueType);
                                            oCell.Style.BackColor = Color.Tomato;
                                        }
                                        else
                                            iFail++;
                                        //only traps a fail if the data has changed 
                                        //and you are pasting into a read only cell
                                    }
                                }
                                else
                                { break; }
                            }

                            iRow++;
                        }
                        else
                        { break; }
                        if (iFail > 0) { }
                           // MessageBox.Show(string.Format("{0} updates failed due" +
                                  //          " to read only column setting", iFail));
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("The data you pasted is in the wrong format for the cell");
                    return;
                }

            }
        }

        private void button31_Click(object sender, EventArgs e)
        {

            DateTime selectedDate = dtmgraff.Value;
            DateTime selectedDateTo = dtmtograff.Value;
            UpdateGrid(selectedDate, selectedDateTo);

        }

        private void UpdateGrid(DateTime _date, DateTime _date_to)
        {

            if (_date.Date > _date_to.Date) return;

            db_raportyDataContext db = new db_raportyDataContext();
            var lista = (from c in db.RAPORT_MONTAZ_1s
                        where c.Data_start.Value.Date >= _date.Date && c.Data_start.Value.Date <= _date_to.Date orderby c.id descending
                        select new { c.id,c.KOD_PLAN, c.Norma_minuty_szt, Nr_stan = c.nr_stanowiska, c.Nazwa_pracownika, c.Data_start, c.Data_stop,c.typ_pracy, c.Nr_zlec_ipo, c.Indeks, c.Nazwa_zlecenia, c.zdawka_dobre, c.zdawka_zle,c.mnoznik_czasu_pracy, c.status, c.minuty_norma_, c.minuty_praca_, c.Komentarz }).Take(1500);


            if (!string.IsNullOrEmpty(tb_f_nazw.Text))
            {

                lista = lista.Where(c => c.Nazwa_pracownika.ToLower().Contains(tb_f_nazw.Text.ToLower()));
            }
            if (!string.IsNullOrEmpty(tb_f_nazwa.Text))
            {

                lista = lista.Where(c => c.Nazwa_zlecenia.ToLower().Contains(tb_f_nazwa.Text.ToLower()));
            }
            if (!string.IsNullOrEmpty(tb_f_nr_zl.Text))
            {

                int.TryParse(tb_f_nr_zl.Text, out int nr_zlec);

                lista = lista.Where(c => (int)c.Nr_zlec_ipo == nr_zlec);
            }


            dataGridView7.DataSource = lista;

            dataGridView7.AutoResizeColumns();
            dataGridView7.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView7.Update();






        }

        private void button32_Click(object sender, EventArgs e)
        {
            New_Graff_Rap gr = new New_Graff_Rap(0, logged, dtmgraff.Value);
            gr.ShowDialog();

            DateTime selectedDate = dtmgraff.Value;
            DateTime selectedDateTo = dtmtograff.Value;
            UpdateGrid(selectedDate, selectedDateTo);

        }

        private void button34_Click(object sender, EventArgs e)
        {


            if (dataGridView7.SelectedRows.Count < 1) return;
            db_raportyDataContext db = new db_raportyDataContext();
            var dialog = MessageBox.Show("UWAGA! Chcesz usunąć ten zapis???","???",MessageBoxButtons.YesNo);
                if (dialog == DialogResult.No) { return; }

            foreach (DataGridViewRow row in dataGridView7.SelectedRows)
            {


                int nr_rec = int.Parse(row.Cells[0].Value.ToString());
                try
                {
                    
                    var lista = from c in db.RAPORT_MONTAZs where c.id == nr_rec select c;
                    db.RAPORT_MONTAZs.DeleteAllOnSubmit(lista);
                    db.SubmitChanges();
                }
                catch { }
            }

                DateTime selectedDate = dtmgraff.Value;
                DateTime selectedDateTo = dtmtograff.Value;
                UpdateGrid(selectedDate, selectedDateTo);

            
        }
            private void button33_Click(object sender, EventArgs e)
        {
            if (dataGridView7.SelectedRows.Count == 1)
            {
                try
                {
                    int nr_rec = (int)dataGridView7.Rows[dataGridView7.CurrentCell.RowIndex].Cells[0].Value;
                    New_Graff_Rap gr = new New_Graff_Rap(nr_rec, logged, DateTime.Now);
                    gr.ShowDialog();

                    DateTime selectedDate = dtmgraff.Value;
                    DateTime selectedDateTo = dtmtograff.Value;
                    UpdateGrid(selectedDate, selectedDateTo);
                }
                catch { MessageBox.Show("Ten rekord jest w tracie pracy - najpierw ją zakończ."); }
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {

            Action kasuj_GRAFF_EU_ACTION = new Action(() => Kasuj_GRAFF_EU())  ;
            kasuj_GRAFF_EU_ACTION.Invoke();
            
             

            

        }

        private void Kasuj_GRAFF_EU()
        {
            button35.Enabled = false;
            DB2008DataContext db = new DB2008DataContext();
            var lista_do_skas = (from c in db.SLOWNIK_1s
                                 where c.KOD_PLAN == "MONT.GRAFF" && c.IMSRP1 == "WYG" && (c.IMSRP3 == "AGE" || c.IMSRP3 == "BGE") && (  c.ZapasBezpieczenstwa  + c.SeriaOptymalna + (c.StanMax ?? 0)  != 0)
                                 select c.IMLITM).ToList();
            int n = 1;
            int maxn = lista_do_skas.Count();
            DialogResult dl = MessageBox.Show($"Czy na pewno chcesz skasować MIN MAX na Graff Europa? Kodów jest {maxn.ToString()} pozycji!!!", "Kasowanie...", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                foreach (var item in lista_do_skas)
                {
                    var d4102s = (from c in db.F4102s where c.IBLITM.Trim().ToUpper() == item.Trim().ToUpper() select c);
                    foreach (var d in d4102s) { d.IBSAFE = 0; d.IBRQMN = 0; d.IBRQMX = 0; }
                    db.SubmitChanges();

                    label10.Text = $"kasowanie {n++} z {maxn}";
                    label10.Refresh();
                }
            }
            
            button35.Enabled = true;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            
        }

        private void button40_Click(object sender, EventArgs e)
        {
            WEB_IPO.IPO ipo = new WEB_IPO.IPO();
            List<string> do_rozdzialu = new List<string>(); 
            foreach (string l in tb_zlecenia_upd.Lines)
            {
                _ = int.TryParse(l, out int nr_z);

                string[] lista = ipo.ZrobBoosta(nr_z);
                foreach (string s in lista)
                {
                    do_rozdzialu.Add(s);

                }



            }
            textBox4.Text = "";
            textBox4.Lines = do_rozdzialu.Distinct().ToArray();
            tabControl1.SelectedIndex = 3;
            MessageBox.Show($"Boost zrobiony - odśwież stany magazynowe na zakładce autoprodukcja  {Environment.NewLine}Do rozdziału - {do_rozdzialu.Distinct().Count()} pozycji.");

        }
        
        public string _litm = "";
        public int _nr_zlec = 0;
        public int _ilosc_do_podzial=0;
        public int _ilosc_druga = 0;
        public string _nr_zam = "";
        public DateTime _termin_k;
        public int _ilosc_total = 0;

        private void button41_Click(object sender, EventArgs e)
        {
            db_raportyDataContext db = new db_raportyDataContext();
            int.TryParse(textBox6.Text, out int nr_zlec);
            textBox7.Text = "";
            label25.Text = " i ...";

            var zlec = from c in db.IPO_ZLECENIAs where c.id == nr_zlec select c;

            if (zlec.Count() == 1)
            {
                var z = zlec.First();
                lb_nazwa_z_r.Text = z.NAZWA;
                lb_termin_r.Text = z.terminDostawy.Date.ToShortDateString();
                lb_nr_zam_r.Text = z.nr_zam_klienta;
                label28.Text = z.Indeks_zlecenia.Trim();
                lb_ilosc_r.Text = z.ilosc_zam.ToString();
                lb_status_r.Text = z.Status_zam.Trim();
                if (z.Status_zam == "Oczekujące" || z.Status_zam == "W planie")
                {
                    _litm = z.Indeks_zlecenia.Trim();
                    _nr_zlec = z.id;
                    _ilosc_total = z.ilosc_zam;
                    //button42.Enabled = true;
                    _termin_k = z.terminDostawy;
                    _nr_zam = z.nr_zam_klienta;
                }
                if (z.Status_zam == "W planie")
                { button46.Enabled = true;  } else { button46.Enabled = false; }


            }



        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox7.Text, out int ilosc);
            try
            {

                if (ilosc < 1 || ilosc == _ilosc_total) { this.button42.Enabled = false; return; }
                if (ilosc > _ilosc_total)
                {
                    _ilosc_do_podzial = ilosc;
                    _ilosc_druga = 0;
                    this.button42.Text = "Powiększ:";
                    label25.Text = "...";
                    this.button42.Enabled = true;

                }
                else
                {
                    this.button42.Text = "Podziel na :";
                    this.button42.Enabled = true;
                    label25.Text = $" i  {_ilosc_total - ilosc} ";
                    _ilosc_do_podzial =  ilosc;
                    _ilosc_druga = _ilosc_total - ilosc;
                }


            }
            catch { }



        }

        private void button42_Click(object sender, EventArgs e)
        {

            if (_nr_zlec != 0 )
            {
                DialogResult dl =   MessageBox.Show($"UWAGA! Nastąpi odrzucenie zlecenia i założenie nowych zlecen na {_ilosc_do_podzial} szt. oraz {_ilosc_druga} szt.","...",MessageBoxButtons.YesNo);

                if (dl == DialogResult.Yes)
                {
                    WEB_IPO.IPO ip = new WEB_IPO.IPO();
                    //ip.Odrzuc_zlecenie(_nr_zlec, "test");
                    WebReference.Service1 srv = new WebReference.Service1();
                    string test1 = srv.IPO_CreateOrder(_termin_k, _litm, _ilosc_do_podzial, false, 1, 2, _nr_zam + "$", $"{logged} - zl {_nr_zlec}", logged);
                    if (_ilosc_druga > 0) srv.IPO_CreateOrder(_termin_k, _litm, _ilosc_druga, false, 1, 2, _nr_zam, $"{logged} - zl {_nr_zlec}", logged);

                    ip.Odrzuc_zlecenie(_nr_zlec, "test");
                }
            }




            button42.Enabled = false;
            _nr_zlec = 0; _ilosc_do_podzial = 0; _ilosc_druga = 0;
            lb_nazwa_z_r.Text = "...";
            lb_termin_r.Text = "...";
            lb_nr_zam_r.Text = "...";
            lb_ilosc_r.Text = "...";
            textBox7.Text = "";
            label25.Text = " i ...";
            button46.Enabled = false;
            Update_zamowienia_dg8();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            Update_zamowienia_dg8(); button46.Enabled = false;
        }

        private void Update_zamowienia_dg8()
        {
            
            try
            {
                
                db_raportyDataContext db = new db_raportyDataContext();

                var nlista = (from c in db.API_zamowienia_produkcjis orderby c.dataUtworzenia descending select c).ToList();
                if (tb_utworzyl.Text != "") nlista = nlista.Where(x => x.Utworzyl.ToLower().Contains(tb_utworzyl.Text.ToLower())).ToList();

                if (tb_indeks_r.Text != "") nlista = nlista.Where(x => x.nrZleceniaF.ToLower().Contains(tb_indeks_r.Text.ToLower())).ToList();
                if (cb_nieakt.Checked) nlista = nlista.Where(x => x.Stan.Trim() == "DO_URUCHOMIENIA").ToList();
                label44.Text = nlista.Count.ToString() + " poz.";
                dataGridView8.DataSource = nlista;
                dataGridView8.Update();
            }
            catch (Exception)
            {

                
            }
            
        }

        private void CompleteIPOTask(int task_id)
        {
            IPO_PRACE_ZEWN.Prace_zewn_API pr = new IPO_PRACE_ZEWN.Prace_zewn_API();
            try
            {
                pr.StartTask(task_id, DateTime.Now);
            }
            catch { }

            System.Threading.Thread.Sleep(3000);
            try
            {
                pr.EndTask(task_id);
            }
            catch { }



        }

        private void button46_Click(object sender, EventArgs e)
        {
            this.button46.Enabled = false;
            db_raportyDataContext db = new db_raportyDataContext();
            panel1.BackColor = Color.Red;
            panel1.Update();
            var prace = from c in db.API_prace_zewns where c.zlecenie == _nr_zlec && (c.Stan_pracy == "W planie" || c.Stan_pracy == "W toku") select c.id;

            IPO_PRACE_ZEWN.Prace_zewn_API pr = new IPO_PRACE_ZEWN.Prace_zewn_API();

            foreach (var praca in prace) CompleteIPOTask(praca);
            

                
            

            
            button41_Click(null, null);
            panel1.BackColor = Color.Aquamarine;
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView8_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var id = dataGridView8.Rows[dataGridView8.CurrentRow.Index].Cells[10].Value.ToString();
                textBox6.Text = id.Trim();
            }
            catch { }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView8.Rows[dataGridView8.CurrentRow.Index].Cells[10].Value;

                DialogResult dl = MessageBox.Show($"UWAGA! - księgowanie zlecenia {id} !!! Czy jesteś pewien?", "...", MessageBoxButtons.YesNo);
                if (dl == DialogResult.Yes)
                {
                    db_raportyDataContext db = new db_raportyDataContext();
                    var prace = from c in db.API_prace_zewns where c.zlecenie == id  && c.Stan_pracy == "W planie" select c.id;
                    IPO_PRACE_ZEWN.Prace_zewn_API pr = new IPO_PRACE_ZEWN.Prace_zewn_API();
                    foreach (var praca in prace)
                    {
                        CompleteIPOTask(praca);

                    }

                }
                Update_zamowienia_dg8();

            }
            catch { }

        }

        private void button44_Click_1(object sender, EventArgs e)
        {
            
            WebReference.Service1 srv = new WebReference.Service1();
            try
            {
                var items = dataGridView8.SelectedRows;

                foreach (DataGridViewRow item in items)
                {
                    var id = item.Cells[0].Value.ToString();
                    srv.IPO_DeleteOrder(id);
                }

                MessageBox.Show("Zlecenia skasowane");
                Update_zamowienia_dg8();

                 
            }
            catch { }
        }

        private void button45_Click_1(object sender, EventArgs e)
        {
            try
            {
               // tb_zlecenia_upd.Text = "";

                  var id = dataGridView8.Rows[dataGridView8.CurrentRow.Index].Cells[10].Value.ToString();
                dataGridView8.Rows[dataGridView8.CurrentRow.Index].DefaultCellStyle.BackColor = Color.LightBlue;
                tb_zlecenia_upd.Text += id + Environment.NewLine;
                button46.Enabled = false;
                tabControl1.SelectedIndex = 2;

            }
            catch { }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            db_raportyDataContext db = new db_raportyDataContext();
            WebReference.Service1 srv = new WebReference.Service1();
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {
                if ( (row.Cells[11].Value ?? "" ).ToString() == "Zakończone" || (row.Cells[11].Value ?? "").ToString() == "Odrzucone")
                {
                    var id = row.Cells[0].Value.ToString();
                    srv.IPO_DeleteOrder(id);

                }


            }
            Update_zamowienia_dg8();

        }

        private void button49_Click(object sender, EventArgs e)
        {

            try
            {
                //dataGridView9.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
                db_raportyDataContext db = new db_raportyDataContext();

                var zlec = (from c in db.IPO_ZLECENIAs
                           where c.Indeks_zlecenia.Trim().Contains(tb_znajdz_zlec_r.Text.Trim()) && new string[] { "W planie", "Oczekujące","W toku" }.Contains(c.Status_zam)
                           select new { c.ipo_nr_zlec, c.ilosc_zam, c.Status_zam, c.NAZWA, c.nr_zam_klienta,c.Indeks_zlecenia }).ToList();
                dataGridView9.DataSource = zlec;
            }
            catch { }

        }

        private void button50_Click(object sender, EventArgs e)
        {
            try
            {
                tb_zlecenia_upd.Text = "";

                var id = dataGridView9.Rows[dataGridView9.CurrentRow.Index].Cells[0].Value.ToString();
                //dataGridView8.Rows[dataGridView8.CurrentRow.Index].DefaultCellStyle.BackColor = Color.LightBlue;
                tb_zlecenia_upd.Text += id + Environment.NewLine;
                button46.Enabled = false;
                tabControl1.SelectedIndex = 2;

            }
            catch { }
        }

        private void dataGridView9_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var id = dataGridView9.Rows[dataGridView9.CurrentRow.Index].Cells[0].Value.ToString();
                textBox6.Text = id;
            }
            catch { }
        }

        private void dataGridView9_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                dataGridView9.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
                //dataGridView9.AutoResizeColumns();
            }
            catch { }
        }

        private void dataGridView8_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                dataGridView8.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
               // dataGridView8.AutoResizeColumns();
            }
            catch { }
        }

        private void dataGridView8_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

            for (int n = 0; n < dataGridView8.Columns.Count; n++)
            {
                if ( (dataGridView8.Rows[e.RowIndex].Cells[12].Value ?? "").ToString()  == "True") dataGridView8.Rows[e.RowIndex].Cells[n].Style.Font = new System.Drawing.Font(dataGridView8.Font, FontStyle.Bold);
            }
            if ((dataGridView8.Rows[e.RowIndex].Cells[11].Value ?? "").ToString() == "W planie") dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
            if ((dataGridView8.Rows[e.RowIndex].Cells[11].Value ?? "").ToString() == "Oczekujące") dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            if ((dataGridView8.Rows[e.RowIndex].Cells[11].Value ?? "").ToString() == "") dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
            if ((dataGridView8.Rows[e.RowIndex].Cells[11].Value ?? "").ToString() == "Zakończone") dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
            if ((dataGridView8.Rows[e.RowIndex].Cells[11].Value ?? "").ToString() == "Odrzucone") dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
            if ((dataGridView8.Rows[e.RowIndex].Cells[11].Value ?? "").ToString() == "W toku") dataGridView8.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;



        }

        private void dataGridView7_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
          //  if ((dataGridView7.Rows[e.RowIndex].Cells[14].Value ?? "").ToString() == "w_toku") { dataGridView7.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow; }
          //  if ( (dataGridView7.Rows[e.RowIndex].Cells[13].Value ?? "").ToString() == "" && (dataGridView7.Rows[e.RowIndex].Cells[14].Value ?? "").ToString() == "stop") { dataGridView7.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red; }
          //  if ((dataGridView7.Rows[e.RowIndex].Cells[1].Value ?? "").ToString() != "MONT.GRAFF") { dataGridView7.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red; }
          //  if ((dataGridView7.Rows[e.RowIndex].Cells[7].Value ?? "").ToString() == "DNIÓWKA") { dataGridView7.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue; }

        }

        private void button51_Click(object sender, EventArgs e)
        {
            Importer t = new Importer(logged);
            t.ShowDialog();
        }

        private void button52_Click(object sender, EventArgs e)
        {
            try
            {
                // tb_zlecenia_upd.Text = "";

                int.TryParse(dataGridView8.Rows[dataGridView8.CurrentRow.Index].Cells[10].Value.ToString(), out int id);
                var dl = MessageBox.Show("Czy odrzucić zlecenie " + id, "...", MessageBoxButtons.YesNo);

                if (dl == DialogResult.Yes)
                {
                    WEB_IPO.IPO w = new WEB_IPO.IPO();
                    w.Odrzuc_zlecenie(id,logged);

                }

            }
            catch { }
        }

        private void button53_Click(object sender, EventArgs e)
        {

            try
            {
                DB2008DataContext db = new DB2008DataContext();
                db_raportyDataContext dbr = new db_raportyDataContext();
                var stany_do_odsw = db.IPO_to_updates.Select(x => x.ITM).Distinct().Count();
                label30.Text = $"Stany do odśw: {stany_do_odsw}";
                var dok_do_zaks = dbr.IPO_ZDAWKA_PWs.Where(x => x.Zaksiegowany_JDE == false).Count();
                label31.Text = $"Do zaks. w JDE: {dok_do_zaks}";
                var lista_rw = dbr.API_Wydanias.Where(c => c.dataZatwierdzenia == null).Count();
                var lista_pw = dbr.API_Przyjecias.Where(c => c.dataZatwierdzenia == null).Count();
                label32.Text = $"Do przetw. IPO: {lista_pw} PW;  {lista_rw} RW";
                label33.Text = DateTime.Now.ToShortTimeString();
            }
            catch { }
        }

        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(250, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Podaj:";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void button54_Click(object sender, EventArgs e)
        {

            var dl = MessageBox.Show("Czy na pewno chcesz odrzucić zamówienia aktywne?", "...", MessageBoxButtons.YesNo);
                if (dl == DialogResult.No ) { return; }
            db_raportyDataContext db = new db_raportyDataContext();
            WebReference.Service1 srv = new WebReference.Service1();
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {
                if ((row.Cells[7].Value ?? "").ToString().Trim() == "AKTYWNE" )
                {
                    var id = row.Cells[0].Value.ToString();
                    srv.IPO_DeleteOrder(id);

                }


            }
            Update_zamowienia_dg8();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Refresh_prace_szlif(tbfnazwa.Text, tbfcechap.Text, tbfkolor.Text, tbfindeks.Text, tbfstatus.Text, tbfnrzlec.Text, tbfzam.Text, tbfcechaa.Text, tbfcechab.Text, tbfcechaq.Text);
        }

        private void Refresh_prace_szlif(string f_nazwa, string f_cechaP, string fkolor, string findeks, string fstan, string fnrzlec, string fzamowienie,string f_cechaA, string f_cechaB, string f_cechaQ )
        {


            var db = new db_raportyDataContext();

            var items = (from c in db.API_prace_zewns
                         where (c.okprac == "Szlif_galw_" || c.okprac == "Szlifiernia") && c.KOLOR.Trim().Contains(fkolor) && c.NAZWA.Contains(f_nazwa) &&
                         c.cecha_A.ToLower().Trim().Contains(f_cechaA.ToLower()) &&
                         c.cecha_B.ToLower().Trim().Contains(f_cechaB.ToLower()) &&
                         c.cecha_P.ToLower().Trim().Contains(f_cechaP.ToLower()) &&
                         c.cecha_Q.ToLower().Trim().Contains(f_cechaQ.ToLower()) &&
                         c.Indeks_zlecenia.ToLower().Trim().Contains(findeks.ToLower()) &&
                         c.Stan_pracy.ToLower().Trim().Contains(fstan.ToLower()) &&
                         c.zlecenie.ToString().Contains(fnrzlec) && c.nr_zam_klienta.Trim().ToLower().Contains(fzamowienie)
                         orderby c.wsp descending
                         select new { c.zlecenie, c.Stan_pracy, c.Indeks_zlecenia, c.NAZWA, 
                             c.ilosc_zam, c.iloscWe, c.KOLOR, c.nr_zam_klienta, c.boost,
                             c.cecha_A, c.cecha_B, c.cecha_P, c.cecha_Q, c.dataGraniczna,
                             c.dataUtworzenia, c.GALW_TECHN }).ToList();

            if (checkBox2.Checked)
            {
                items = items.Where(x => x.KOLOR.ToLower().Trim() == tbfkolor.Text.Trim().ToLower()).ToList();
            }

            if (checkBox3.Checked)
            {
                items = items.Where(x => x.cecha_A.ToLower().Trim() == tbfcechaa.Text.Trim().ToLower()).ToList();
            }

            dataGridView10.DataSource = items;
            lb_pozyucji.Text = $"POZYCJI: {items.Count()}";




        }

        private void dataGridView10_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                lb_pozyucji.Text = $"POZYCJI: {dataGridView10.Rows.Count}, zaznaczono: {dataGridView10.SelectedRows.Count}";
            }
            catch (Exception)
            {

                
            }


            
        }

        private void button56_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView10.SelectedRows.Count > 0)
                {

                    //sprawdz statusy
                    foreach (DataGridViewRow row in dataGridView10.SelectedRows)
                    {
                        if (row.Cells[1].Value.ToString() != "W planie")
                        {
                            MessageBox.Show("Sprawdź zaznaczenie - nie wszystkie zlecenia są w planie!!!");

                            return;
                            
                        }
                        if (row.Cells[7].Value.ToString().Contains(": PO") && !row.Cells[7].Value.ToString().Contains("BER") && !row.Cells[11].Value.ToString().Contains("WER"))
                        {
                            MessageBox.Show("Sprawdź zaznaczenie - nie wszystkie zlecenia PO są zweryfikowane!!!");

                            return;

                        }



                    }

                    int n = 0;
                        var db = new db_raportyDataContext();
                    var dl = MessageBox.Show($"Czy na pewno wystartować te zlecenia? Startujesz {dataGridView10.SelectedRows.Count} pozycji", "???", MessageBoxButtons.YesNo);
                    if (dl == DialogResult.No) return;
                    IPO_PRACE_ZEWN.Prace_zewn_API pr = new IPO_PRACE_ZEWN.Prace_zewn_API();
                    foreach (DataGridViewRow row in dataGridView10.SelectedRows)
                    {
                        string nr_zlec = row.Cells[0].Value.ToString();
                        int.TryParse(nr_zlec, out int _nr_zlec);
                        var id = from c in db.API_prace_zewns where c.zlecenie == _nr_zlec select c.id;
                        foreach (var i in id)
                        {
                            if (row.Cells[1].Value.ToString() == "W planie") pr.StartTask(i, DateTime.Now.AddMinutes(30));
                            lb_pozyucji.Text = $"Startuję zlecenie {_nr_zlec}  -  {++n} z {dataGridView10.SelectedRows.Count}";
                        }
                    }
                }
            }
            catch (Exception) { }
            finally { Refresh_prace_szlif(tbfnazwa.Text, tbfcechap.Text, tbfkolor.Text, tbfindeks.Text, tbfstatus.Text, tbfnrzlec.Text, tbfzam.Text, tbfcechaa.Text, tbfcechab.Text, tbfcechaq.Text); }
             
        }

        private void button57_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView10.SelectedRows.Count > 0)
                {

                    //sprawdz statusy
                    foreach (DataGridViewRow row in dataGridView10.SelectedRows)
                    {
                        if (row.Cells[1].Value.ToString() != "W_toku")
                        {
                            MessageBox.Show("Sprawdź zaznaczenie - nie wszystkie zlecenia są w planie!!!");

                            return;

                        }
                    }

                    int n = 0;
                    var db = new db_raportyDataContext();
                    var dl = MessageBox.Show($"Czy na pewno zakończyć te zlecenia? Stopujesz {dataGridView10.SelectedRows.Count} pozycji", "???", MessageBoxButtons.YesNo);
                    if (dl == DialogResult.No) return;
                    IPO_PRACE_ZEWN.Prace_zewn_API pr = new IPO_PRACE_ZEWN.Prace_zewn_API();
                    foreach (DataGridViewRow row in dataGridView10.SelectedRows)
                    {
                        string nr_zlec = row.Cells[0].Value.ToString();
                        int.TryParse(nr_zlec, out int _nr_zlec);
                        var id = from c in db.API_prace_zewns where c.zlecenie == _nr_zlec select c.id;
                        foreach (var i in id)
                        {
                            if (row.Cells[1].Value.ToString() == "W_toku") pr.EndTask(i);
                            lb_pozyucji.Text = $"Kończę zlecenie {_nr_zlec}  -  {++n} z {dataGridView10.SelectedRows.Count}";
                        }
                    }
                }
            }
            catch (Exception) { }
            finally { Refresh_prace_szlif(tbfnazwa.Text, tbfcechap.Text, tbfkolor.Text, tbfindeks.Text, tbfstatus.Text, tbfnrzlec.Text, tbfzam.Text, tbfcechaa.Text, tbfcechab.Text, tbfcechaq.Text); }

        }
        


        private string OznaczOperacjeSzlifCechaB(string litm_galw, int nr_zlec)
        {

            DB2008DataContext db2008 = new DB2008DataContext();
            var kod = db2008.SLOWNIK_1s.Where(c => c.IMLITM.Trim() == litm_galw).FirstOrDefault();

            string kod_plan = kod.KOD_PLAN;
            int nmax = 5; //pieć razy w dół
            try
            {

                while (kod_plan != "SZLIFIERNIA" || nmax == 0)
                {
                    var rozpis_zlecenia = (from c in db2008.IPO_Rozpis_mats
                                           where c.wyrob_l.Trim() == litm_galw.Trim()
                                           select c).First();

                    litm_galw = rozpis_zlecenia.skladnik_l.Trim();
                    kod_plan = db2008.SLOWNIK_1s.Where(c => c.IMLITM.Trim() == litm_galw).Select(c => c.KOD_PLAN).FirstOrDefault();
                    nmax--;
                }
                

            }
            catch { }

            var marszruta = from c in db2008.IPO_Marszruties where c.Indeks.Trim() == litm_galw select c;
            bool szlif = false;
            bool poler = false;
            bool szczotki = false;
            foreach (var op in marszruta)
            {
                if (op.Operacja.Trim().ToLower().Contains("szlif")) szlif = true;
                if (op.Operacja.Trim().ToLower().Contains("poler")) poler = true;
                if (op.Operacja.Trim().ToLower().Contains("szczo")) szczotki = true;
            }



            string zwrot = $"{(szlif ? "SZL" : "___")}/{(poler ? "POL" : "___")}/{(szczotki ? "SZC" : "___")}";
            WebReference.Service1 srv = new WebReference.Service1();
            srv.IPO_akt_dane_zlec_cechy(nr_zlec, null, zwrot, null, null);

            return zwrot;

        }
        private void button58_Click(object sender, EventArgs e)
        {

            StringBuilder strb = new StringBuilder();
            var db = new db_raportyDataContext();

            strb.Append("<head><style>table, th, td {border: 1px solid black; border-collapse: collapse; line-height: 30px;}</style></head>");
            strb.Append($"<h2 style = 'text-align: center'> <span style='background-color: #914c53; color: #ffffff; padding: 0 3px;'>Lista pobrania z {DateTime.Now}</span></h2>");
            strb.Append("<br><br><table><tbody>");
            strb.Append("<tr><td>Nr zlecenia</td><td>Indeks mat.</td><td>Ilość</td><td>Lokalizacja</td><td>Nazwa</td><td>CECHA_A</td><td>Kod_zlec</td></tr>");

            foreach (DataGridViewRow dr in dataGridView10.SelectedRows)
            {
                _ =int.TryParse(dr.Cells[0].Value.ToString(), out int nr_zlec);
                string Nazwa = dr.Cells[3].Value.ToString().Trim();
                string CECHAA = dr.Cells[9].Value.ToString();
                //string CECHAB = dr.Cells[10].Value.ToString();
                string nr_zam = dr.Cells[7].Value.ToString();

                var lista_pobr = (from c in db.IPO_ZDAWKA_PWs where c.Nr_zlecenia_IPO == nr_zlec select c).ToList();
                foreach (var item in lista_pobr)
                {
                    strb.Append($"<tr><td>{dr.Cells[0].Value.ToString()}</td><td>{item.Nr_indeksu.Trim()}</td><td>{item.Ilosc ?? 0}</td><td>{item.Magazyn_IPO.Trim()}</td><td>{Nazwa}</td><td>{CECHAA}</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='http://sp2013:10001/barcode128.ashx?kod={ dr.Cells[0].Value.ToString() }'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
                }


                if (lista_pobr.Count == 0)
                {
                    strb.Append($"<tr><td>{dr.Cells[0].Value.ToString()}</td><td>BRAK!</td><td>BRAK!</td><td>BRAK!</td><td>{Nazwa}</td><td>{CECHAA}</td> <td>{nr_zam}</td></tr>");
                }

            }


            strb.Append("<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>");

            strb.Append("</tbody></table>");

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.MarginBottom = 10;
            converter.Options.MarginLeft = 10;


            string fname = "Pobranie-"+$"{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.pdf";
            SelectPdf.PdfDocument pdf = converter.ConvertHtmlString(strb.ToString());
            pdf.Save(Path.GetTempPath() + $@"\{fname}");
             
            SaveFileDialog sf = new SaveFileDialog();
            sf.DefaultExt = "pdf";
            sf.FileName = fname;
            bs = File.ReadAllBytes(Path.GetTempPath() + $@"\{fname}");


            if (sf.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sf.FileName, bs);
            }
            File.Delete(Path.GetTempPath() + $@"\{fname}");
            Process.Start(sf.FileName);
        }

        private async void button59_Click(object sender, EventArgs e)
        {
            var dl = MessageBox.Show($"Uwaga! TO DŁUGO TRWA. Chcesz oznaczyć {dataGridView10.SelectedRows.Count} pozycji. Czy na pewno?", "...", MessageBoxButtons.YesNo);
            if (dl == DialogResult.No) return;


            Control.CheckForIllegalCrossThreadCalls = false;
            await System.Threading.Tasks.Task.Run(() =>
            {
                int licznik = 0;

                foreach (DataGridViewRow dr in dataGridView10.SelectedRows)
                {

                    try
                    {
                        string litm = dr.Cells[2].Value.ToString();
                        int.TryParse(dr.Cells[0].Value.ToString(), out int nr_zlec);
                        lb_pozyucji.Text = $"oznaczam zlecenie {nr_zlec} - {++licznik}/{dataGridView10.SelectedRows.Count} ";
                        
                        if (!dr.Cells[10].Value.ToString().Contains("/")) OznaczOperacjeSzlifCechaB(litm, nr_zlec);
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                }
            });

        }

        private async void button60_Click(object sender, EventArgs e)
        {
            string wartosc = "Podaj wartość pola Cecha A";
            ShowInputDialog(ref wartosc);

            int licznik = 0;
            WebReference.Service1 srv = new Service1();

            DataGridViewSelectedRowCollection srows = dataGridView10.SelectedRows;

            Control.CheckForIllegalCrossThreadCalls = false;
            await System.Threading.Tasks.Task.Run(() =>
            {
                foreach (DataGridViewRow dr in srows)
                {

                    try
                    {
                        string litm = dr.Cells[2].Value.ToString();
                        int.TryParse(dr.Cells[0].Value.ToString(), out int nr_zlec);
                        lb_pozyucji.Text = $"oznaczam zlecenie {nr_zlec} - {++licznik}/{dataGridView10.SelectedRows.Count} ";

                        srv.IPO_akt_dane_zlec_cechy(nr_zlec, wartosc, null, null, null);
                    }
                    catch (Exception)
                    { }
                }
            });
        }

        private void button61_Click(object sender, EventArgs e)
        {
            if (dataGridView10.SelectedRows.Count != 1) return;
            foreach (DataGridViewRow dr in dataGridView10.SelectedRows)
            {

                string litm = dr.Cells[0].Value.ToString();
                textBox6.Text = litm;


            }
            tabControl1.SelectedIndex = 9;

        }

        private void button62_Click(object sender, EventArgs e)
        {
            db_raportyDataContext db = new db_raportyDataContext();
            Service1 srv = new Service1();

            string typ_zam = "";
            foreach (DataGridViewRow row in dataGridView10.SelectedRows)
            {
                string zam = (row.Cells[7].Value ?? "").ToString();
                bool OK = true;
                if (zam.Contains("PO") && !zam.Contains("BER")) typ_zam = "PO"; else typ_zam = "SO";

                int.TryParse(row.Cells[0].Value.ToString(), out int nr_zlec);
                var mat = from c in db.API_materialy_zlecenias where c.Nr_zlecenia == nr_zlec select c;
                foreach (var m in mat)
                {
                    var zop_mat = (from c in db.IPO_MATERIALY_ZOPs where c.IPO_LITM.Trim() == m.Indeks_mat.Trim() select c).FirstOrDefault();
                    var stan = db.INW_STANY_SPIS_ONLINEs.Where(c => c.PJLITM.Trim() == m.Indeks_mat.Trim() && !c.mag.Contains("IZ")).Sum(x => x.ILOSC);

                    if (((decimal)stan - (decimal)m.ilosc_planowana < (decimal)zop_mat.StanMinimalny && typ_zam == "PO")) OK = false;
                    MessageBox.Show($"Typ zlecenia: {typ_zam} \nDla materiału {m.Indeks_mat} :\nStan mag: {stan} \nStan min: {zop_mat.StanMinimalny} \nPotrzeby: {m.ilosc_planowana}\nMożna wyprodukować: { ((decimal)stan-zop_mat.StanMinimalny<0 ? 0 : (decimal)stan - zop_mat.StanMinimalny) } \nCzy OK? {(OK ? "TAK" : "NIE")} ");
                    if (OK && typ_zam == "PO") { srv.IPO_akt_dane_zlec_cechy(nr_zlec, null, null, $"WER : {logged}-{DateTime.Now.ToShortDateString()}", null); }
                    if (!OK && typ_zam == "PO") { srv.IPO_akt_dane_zlec_cechy(nr_zlec, null, null, $"PODZIEL NA ", ((decimal)stan - zop_mat.StanMinimalny < 0 ? "0" : ((decimal)stan - zop_mat.StanMinimalny).ToString())); }
                    row.Cells[11].Value = $"WER : {logged}-{DateTime.Now.ToShortDateString()}";
              
                     Refresh_prace_szlif(tbfnazwa.Text, tbfcechap.Text, tbfkolor.Text, tbfindeks.Text, tbfstatus.Text, tbfnrzlec.Text, tbfzam.Text, tbfcechaa.Text, tbfcechab.Text, tbfcechaq.Text);
                }





            }
        }

        private void button63_Click(object sender, EventArgs e)
        {
            Local_API api = new Local_API();
            int task_id = int.Parse(tbfindeks.Text);
            int qty = int.Parse(tbfzam.Text);

            api.RaportPracy(task_id, 1, "2", qty, "");




            //api.StartPracy(int.Parse(tbfindeks.Text), "2268", null);

        }

        private void dataGridView7_SelectionChanged(object sender, EventArgs e)
        {
            label45.Text = dataGridView7.SelectedRows.Count.ToString();
        }
    }

}
