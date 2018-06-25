using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace WindowsFormsApp2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private bool mouseDown;
        private Point lastLoc;
        private void Form1_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLoc = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                this.Location = new Point((this.Location.X - lastLoc.X) + e.X, (this.Location.Y - lastLoc.Y) + e.Y);
                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void button2_Click(object sender, EventArgs e) {
            Logger.Log("kk eae men");
            if (textBox1.Text != "" && textBox2.Text != "") {
                Logger.Log("kk eae men");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void label2_Click(object sender, EventArgs e) {
            var salt = Encryption.GenerateSalt();
            var saltb = Convert.FromBase64String(salt);
            int ite = 1000;
            var pass = "L7k%6a@3M1";
            var hash = Convert.ToBase64String(Encryption.GetPbkdf2Bytes(pass, saltb, ite, 255));
            MessageBox.Show(hash + "   :   " + salt);
            textBox1.Text = hash;
            textBox2.Text = salt;
        }

        private void label2_MouseHover(object sender, EventArgs e) {
            
        }

        private void button1_MouseHover(object sender, EventArgs e) {
            button1.Text = "•";
        }

        private void button1_MouseLeave(object sender, EventArgs e) {
            button1.Text = "○";
        }

        private void button1_MouseEnter(object sender, EventArgs e) {
            button1.Text = "•";
        }

        private void Form1_Load(object sender, EventArgs e) {
            if (!Database.Init()) {
                MessageBox.Show("Erro ao se conectar ao banco de dados!");
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_MouseEnter(object sender, EventArgs e) {
            button3.Text = "_";
        }

        private void button3_MouseLeave(object sender, EventArgs e) {
            button3.Text = "_";
        }
    }
}
