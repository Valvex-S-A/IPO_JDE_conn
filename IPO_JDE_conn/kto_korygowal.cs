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
    public partial class kto_korygowal : Form
    {


        public kto_korygowal(int TaskId)
        {
            InitializeComponent();
            db_raportyDataContext db = new db_raportyDataContext();
            var kor = from c in db.IPO_Tasks_korektas
                      where c.Task_id == TaskId
                      select new { c.Typ_korekty, c.Utworzony, c.Utworzony_przez, c.Dane };

            dg_kto_kor.DataSource = kor;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
