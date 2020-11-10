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
    public partial class Importer : Form
    {
        public string _logged;

        public Importer(string logged)
        {
            InitializeComponent();
            _logged = logged;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int iFail = 0, iRow = dataGridView1.CurrentCell.RowIndex;
                    int iCol = dataGridView1.CurrentCell.ColumnIndex;
                    DataGridViewCell oCell;

                    for (int n = 0; n < lines.Count() - 1; n++)
                        dataGridView1.Rows.Add();

                    foreach (string line in lines)
                    {
                        if (iRow < dataGridView1.RowCount && line.Length > 0)
                        {
                            string[] sCells = line.Split('\t');
                            for (int i = 0; i < sCells.GetLength(0); ++i)
                            {
                                if (iCol + i < this.dataGridView1.ColumnCount)
                                {
                                    oCell = dataGridView1[iCol + i, iRow];
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

        private void button1_Click(object sender, EventArgs e)
        {

            WebReference.Service1 srv = new WebReference.Service1();
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (!(r.Cells[0].Value is null) && !(r.Cells[4].Value ?? "").ToString().StartsWith("#")   )
                {
                    string litm = r.Cells[0].Value.ToString();
                    

                    DateTime.TryParse(r.Cells[3].Value.ToString(), out DateTime _dt);
                    int.TryParse(r.Cells[1].Value.ToString(), out int _ilosc);
                    string nr_zam = r.Cells[2].Value.ToString();
                    string opis = (r.Cells[4].Value ?? ""  ).ToString();
                    if (opis.StartsWith("#")) continue;


                    string test1 = srv.IPO_CreateOrder(_dt, litm, _ilosc, false, 1, 2, nr_zam,opis, _logged);



                    r.Cells[4].Value = "#" + test1.Substring(1, 10);
                }
            }
        }
    }
}
