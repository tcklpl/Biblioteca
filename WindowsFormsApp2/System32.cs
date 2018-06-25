using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2 {
    public partial class System32 : Form {
        public System32() {
            InitializeComponent();
        }

        private bool mouseDown;
        private Point lastLoc;
        private String cds = "";

        private void System32_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLoc = e.Location;
        }

        private void System32_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                this.Location = new Point((this.Location.X - lastLoc.X) + e.X, (this.Location.Y - lastLoc.Y) + e.Y);
                this.Update();
            }
        }

        private void System32_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void button1_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_MouseEnter(object sender, EventArgs e) {
            button1.Text = "•";
        }

        private void button1_MouseLeave(object sender, EventArgs e) {
            button1.Text = "○";
        }

        private void System32_Load(object sender, EventArgs e) {
            Database.Init();
        }

        private void button4_Click(object sender, EventArgs e) {
            cds = "buqui";
            dataGridView1.DataSource = Database.FetchTable("buqui");
            button4.BackColor = Color.FromArgb(70, 70, 70);
            button5.BackColor = Color.FromArgb(60, 60, 60);
            button6.BackColor = Color.FromArgb(60, 60, 60);
            button9.BackColor = Color.FromArgb(60, 60, 60);
        }

        private void button5_Click(object sender, EventArgs e) {
            cds = "emprestimos";
            dataGridView1.DataSource = Database.FetchTable("emprestimos");
            button4.BackColor = Color.FromArgb(60, 60, 60);
            button5.BackColor = Color.FromArgb(70, 70, 70);
            button6.BackColor = Color.FromArgb(60, 60, 60);
            button9.BackColor = Color.FromArgb(60, 60, 60);
        }

        private void button6_Click(object sender, EventArgs e) {
            cds = "users";
            dataGridView1.DataSource = Database.FetchTable("users");
            button4.BackColor = Color.FromArgb(60, 60, 60);
            button5.BackColor = Color.FromArgb(60, 60, 60);
            button6.BackColor = Color.FromArgb(70, 70, 70);
            button9.BackColor = Color.FromArgb(60, 60, 60);
        }

        private void button2_Click(object sender, EventArgs e) {
            if (cds != "") {
                SysWOW64 s = new SysWOW64(cds, QueryType.INSERT);
                s.Show();
                s.FormClosed += (se, ea) => { dataGridView1.DataSource = Database.FetchTable(cds); };
            }
        }

        private void button7_Click(object sender, EventArgs e) {
            if (dataGridView1.SelectedRows.Count == 1) {
                int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                SysWOW64 s = new SysWOW64(cds, QueryType.UPDATE, id);
                s.Show();
                s.FormClosed += (se, ea) => { dataGridView1.DataSource = Database.FetchTable(cds); };
            }
        }

        private void button8_Click(object sender, EventArgs e) {
            if (dataGridView1.SelectedRows.Count == 1) {
                if (MessageBox.Show("Você está certo?", "Tem certeza?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                    Database.removeFromTable(cds, id);
                    dataGridView1.DataSource = Database.FetchTable(cds);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e) {
            cds = "clientes";
            dataGridView1.DataSource = Database.FetchTable("clientes");
            button4.BackColor = Color.FromArgb(60, 60, 60);
            button5.BackColor = Color.FromArgb(60, 60, 60);
            button6.BackColor = Color.FromArgb(60, 60, 60);
            button9.BackColor = Color.FromArgb(70, 70, 70);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e) {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows[0].Cells[0].Value.ToString() == "" || dataGridView1.SelectedRows.Count > 1) {
                button7.Enabled = false;
                button8.Enabled = false;
                button7.ForeColor = Color.Black;
                button8.ForeColor = Color.Black;
            } else {
                button7.Enabled = true;
                button8.Enabled = true;
                button7.ForeColor = Color.White;
                button8.ForeColor = Color.White;
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e) {
            if (dataGridView1.DataSource != null) {
                comboBox1.Items.Clear();
                button2.Enabled = true;
                button2.ForeColor = Color.White;
                foreach (String s in Database.getColumnsName(cds)) {
                    comboBox1.Items.Add(s);
                }
                comboBox1.SelectedIndex = 0;
                comboBox1.ForeColor = Color.White;
            } else {
                button2.Enabled = false;
                button2.ForeColor = Color.White;
                comboBox1.Items.Clear();
            }
        }

        private void button10_Click(object sender, EventArgs e) {
            SystemResources s = new SystemResources();
            s.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            if (cds == "") return;
            dataGridView1.ClearSelection();
            if (textBox1.Text.Count() > 0) {
                List<String> kkk = Database.getColumnsName(cds);
                int j = 0;
                for (int i = 0; i < kkk.Count; i++) {
                    if (kkk[i] == comboBox1.Text) j = i;
                }
                foreach (DataGridViewRow row in dataGridView1.Rows) {
                    if (row.Cells[j].Value.ToString().ToLower().Contains(textBox1.Text.ToLower())) {
                        row.Selected = true;
                    }
                }
            }
        }
    }
}
