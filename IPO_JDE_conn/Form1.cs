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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = this.button1;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String adPath = "LDAP://valvex.in";
            FormsAuth.LdapAuthentication lauth = new FormsAuth.LdapAuthentication(adPath);

            if (!lauth.IsAuthenticated("valvex.in", textBox1.Text, textBox2.Text)) { MessageBox.Show("Błędny login lub hasło"); }
            else
            {
                MainApp m = new MainApp(this.textBox1.Text);

                this.Hide();
                m.ShowDialog();
                this.Show();
                this.textBox2.Text = "";

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel1.BackColor == Color.Red)
            {
                panel1.BackColor = Color.Green;
                    this.timer1.Enabled = true;
                this.timer1.Start();

            }
            else
            {
                panel1.BackColor = Color.Red;
                this.timer1.Enabled = false;
                this.timer1.Stop();

            }
            

        }

        private void DrukujKKC()
        {
            WebReference.Service1 client = new WebReference.Service1();
            db_raportyDataContext db = new db_raportyDataContext();

            DateTime _to = DateTime.Now;
            DateTime _from = DateTime.Now.AddDays(0);

            int ito = _to.Year * 10000 + _to.Month * 100 + _to.Day;
            int ifrom = _from.Year * 10000 + _from.Month * 100 + _from.Day;
            string[] setts = client.IPO_GET_TASKS(_from, _to);

            var task = from c in db.IPO_KKC_WYDRUKIs
                       where c.DATA_WYST.Value.Day + c.DATA_WYST.Value.Month * 100 + c.DATA_WYST.Value.Year * 10000 >= ifrom &&
                        c.DATA_WYST.Value.Day + c.DATA_WYST.Value.Month * 100 + c.DATA_WYST.Value.Year * 10000 <= ito
                       select c.TASK_ID;

            foreach (string sett in setts)
            {

                if (!task.ToArray<int?>().Contains(int.Parse(sett.Split('@')[0])))
                {


                    IPO_KKC_WYDRUKI wyd = new IPO_KKC_WYDRUKI();
                    wyd.NR_ZLECENIA = int.Parse(sett.Split('@')[1]);
                    wyd.OPERACJA = sett.Split('@')[3];
                    wyd.TASK_ID = int.Parse(sett.Split('@')[0]);
                    wyd.USTAWIACZ = sett.Split('@')[2];
                    wyd.OPIS_OPER = sett.GetHashCode().ToString();
                    wyd.DATA_WYST = DateTime.Now;
                    db.IPO_KKC_WYDRUKIs.InsertOnSubmit(wyd);
                    if (sett.Split('@')[4].StartsWith(comboBox1.SelectedItem.ToString()))
                    {
                        db.SubmitChanges();
                        pdf_przewodnik.GenKKC((int)wyd.NR_ZLECENIA, wyd.USTAWIACZ, wyd.OPERACJA, sett.Split('@')[4]);
                    }
                }


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            this.timer1.Enabled = false;
            panel1.BackColor = Color.Red;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrukujKKC();
        }
    }
}
