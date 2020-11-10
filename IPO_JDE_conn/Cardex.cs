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
    public partial class Cardex : Form
    {
        public Cardex(string item, string nr_zlec)
        {

            InitializeComponent();
            var db = new DB2008DataContext();
            var dane = from c in db.IPO_CARDEXes
                       orderby c.data_transakcji descending
                       where c.nr_indeksu == item
                       select c;

            int _nr_zlec;
            int.TryParse(nr_zlec, out _nr_zlec);
            if (_nr_zlec !=0)
            {
                dane = dane.Where(c => c.nr_zl_DOCO == _nr_zlec);
            }
            




            this.dataGridView1.DataSource = dane;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

          
            



        }

        private void ZAKOŃCZ_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CopyToClipboardWithHeaders(this.dataGridView1);
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



    }
}
