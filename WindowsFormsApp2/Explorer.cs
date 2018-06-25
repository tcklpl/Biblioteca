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
    public partial class Explorer : Form {
        DataTable ds;
        public Explorer(DataTable ds) {
            InitializeComponent();
            this.ds = ds;
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }

        private void Explorer_Load(object sender, EventArgs e) {
            dataGridView1.DataSource = ds;
        }

        private void button2_Click(object sender, EventArgs e) {
            dataGridView1.Refresh();
        }
    }
}
