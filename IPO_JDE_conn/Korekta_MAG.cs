using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace IPO_JDE_conn
{
    public partial class Korekta_MAG : Form
    {

        public bool anuluj;
        public List<nowe_magazyny> magazyny;
        private long id;
        public Korekta_MAG(long ID)
        {
            InitializeComponent();
            anuluj = false;
            id = ID;
        }

        private void Korekta_MAG_Load(object sender, EventArgs e)
        {

            button2.Enabled = false;
            db_raportyDataContext db = new db_raportyDataContext();
            DB2008DataContext db2008 = new DB2008DataContext();
            var rec = (from c in db.IPO_ZDAWKA_PWs
                       where c.ID == id
                       select c).Single();
            var stany = from g in db2008.IPO_STANies
                        where g.LITM == rec.Nr_indeksu
                        select new { g.mag_ipo, DOSTEPNE = g.QTY, DO_POBRANIA = "" };
            lb_do_rozp.Text = rec.Ilosc.ToString();
            DataTable st = LINQResultToDataTable(stany);

            dg_mag.DataSource = st;
            dg_mag.Update();
            dg_mag.ReadOnly = false;
            dg_mag.Columns[2].ReadOnly = false;
            dg_mag.Columns[1].ReadOnly = true;
            dg_mag.Columns[0].ReadOnly = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.anuluj = true;
            this.Close();
        }

        public struct nowe_magazyny
        {
            public string magazyn;
            public double ilosc;


        }

        private void dg_mag_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void dg_mag_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double ilosc = 0;
            foreach (DataGridViewRow row in dg_mag.Rows)
            {
               
                double t = 0;
                if (!row.IsNewRow) double.TryParse(row.Cells[2].Value.ToString(),out t);

                ilosc = ilosc + t;
               

            }
            button2.Enabled = false;
            if (lb_do_rozp.Text == lb_do_rozp.Text) button2.Enabled = true;

            this.lb_rozpisane.Text = ilosc.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            magazyny = new List<nowe_magazyny>();


            foreach (DataGridViewRow row in dg_mag.Rows)
            {
                double ilosc = 0;
                if (!row.IsNewRow) double.TryParse(row.Cells[2].Value.ToString(), out ilosc);
                if (!string.IsNullOrEmpty(row.Cells[0].Value.ToString()) && ilosc !=0)
                {
                    nowe_magazyny nm = new nowe_magazyny();
                    nm.ilosc = ilosc;
                    nm.magazyn = row.Cells[0].Value.ToString();
                    magazyny.Add(nm);
                }


            }
            this.Close();

        }
    }
}
