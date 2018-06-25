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
    public partial class SystemResources : Form {
        public SystemResources() {
            InitializeComponent();
        }
        int quant = 0;
        int emprestados = 0;

        private bool mouseDown;
        private Point lastLoc;

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            emprestados = 0;
            quant = 0;
            if (Database.getAllValuesFromId("buqui", (int)numericUpDown1.Value) != null) {
                label3.Text = Database.getAllValuesFromId("buqui", (int)numericUpDown1.Value)[0].ToString();
                quant = (int)Database.getAllValuesFromId("buqui", (int)numericUpDown1.Value)[6];

                List<object> empestimos = Database.getAllValuesOrAListOfThemUsingEspecificIndex("emprestimos", "idLivro", "" + numericUpDown1.Value, "Quant");
                if (empestimos != null) {
                    if (empestimos.Count > 0) {
                        foreach (int i in empestimos) {
                            emprestados += i;
                        }
                        quant -= emprestados;
                    }
                }
               

                label6.Text = emprestados > 0 ? "Disponível: " + quant + " | Emprestados: " + emprestados + " ↑" : "Disponível: " + quant + " | Emprestados: " + emprestados;
                numericUpDown3.Maximum = quant;
                label6.Cursor = Cursors.Hand;
            } else {
                label3.Text = "Id inválido";
                label6.Text = "Disponível: -- | Emprestados: --";
                numericUpDown3.Maximum = 0;
                label6.Cursor = Cursors.Arrow;
            }
        }

        private void SystemResources_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLoc = e.Location;
        }

        private void SystemResources_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                this.Location = new Point((this.Location.X - lastLoc.X) + e.X, (this.Location.Y - lastLoc.Y) + e.Y);
                this.Update();
            }
        }

        private void SystemResources_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
            if (Database.getAllValuesFromId("clientes", (int)numericUpDown2.Value) != null) {
                label4.Text = Database.getAllValuesFromId("clientes", (int)numericUpDown2.Value)[0].ToString();
            } else {
                label4.Text = "Id inválido";
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e) {
            if (numericUpDown3.Value <= quant && numericUpDown3.Value != 0) {
                button3.Enabled = true;
                button3.ForeColor = Color.White;
            } else {
                button3.Enabled = false;
                button3.ForeColor = Color.Black;
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            List<String> fields = new List<string>();
            fields.Add("DataS");
            fields.Add("Estado");
            fields.Add("idLivro");
            fields.Add("idCliente");
            fields.Add("Quant");
            List<object> values = new List<object>();
            values.Add(DateTime.Now);
            values.Add(1);
            values.Add(numericUpDown1.Value);
            values.Add(numericUpDown2.Value);
            values.Add(numericUpDown3.Value);
            Database.insertOnTable("emprestimos", fields, values);
            Close();
        }

        private void label6_MouseClick(object sender, MouseEventArgs e) {
            if (emprestados == 0) return;
            Explorer ec = new Explorer(Database.CustomFetchTable("SELECT c.Nome AS NomeCliente, l.Nome AS NomeLivro, e.DataS, e.Estado AS Emprestado, e.Quant FROM emprestimos e, buqui l, clientes c WHERE e.idLivro = l.Id AND e.idCliente = c.Id AND e.idLivro = " + numericUpDown1.Value));
            ec.Show();
        }
    }
}
