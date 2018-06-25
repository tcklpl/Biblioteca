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
    public partial class SysWOW64 : Form {
        private String t;
        QueryType qt;
        private bool mouseDown;
        private Point lastLoc;
        int id;
        List<object> values;
        public SysWOW64(String table, QueryType qt, int id = 0) {
            InitializeComponent();
            t = table;
            this.qt = qt;
            this.id = id;
        }

        private void SysWOW64_Load(object sender, EventArgs e) {
            List<String> l = Database.getColumnsName(t);
            List<String> o = Database.getColumnsTypeName(t);
            l.Remove("Id");
            o.RemoveAt(0);
            var labels = new Dictionary<int, object>();
            var fields = new Dictionary<int, object>();
            int height = (l.Count * 30) + 110;
            int width = 380;
            this.Width = width;
            this.Height = height;
            this.StartPosition = FormStartPosition.CenterScreen;
            switch (qt) {
                // Insert
                case QueryType.INSERT:
                    
                    for (int i = 0; i < l.Count; i++) {
                        // Add Labels
                        Label l1 = new Label();
                        l1.Location = new Point(13, (i * 30) + 13);
                        l1.Text = l[i];
                        l1.ForeColor = Color.FromArgb(255, 255, 255);
                        l1.Size = new Size(100, 13);
                        labels.Add(i, l1);
                        Controls.Add(l1);
                        // Add Fields
                        switch (o[i]) {
                            case "varchar":
                                TextBox t1 = new TextBox();
                                t1.Location = new Point(113, (i * 30) + 13);
                                t1.Size = new Size(250, 10);
                                fields.Add(i, t1);
                                Controls.Add(t1);
                                break;
                            case "text":
                                TextBox t2 = new TextBox();
                                t2.Location = new Point(113, (i * 30) + 13);
                                t2.Size = new Size(250, 10);
                                t2.MaxLength = 999999999;
                                fields.Add(i, t2);
                                Controls.Add(t2);
                                break;
                            case "int":
                                NumericUpDown n1 = new NumericUpDown();
                                n1.Location = new Point(113, (i * 30) + 13);
                                n1.Size = new Size(250, 10);
                                n1.Maximum = 99999999999;
                                fields.Add(i, n1);
                                Controls.Add(n1);
                                break;
                            case "datetime":
                                DateTimePicker d1 = new DateTimePicker();
                                d1.Location = new Point(113, (i * 30) + 13);
                                d1.Size = new Size(250, 10);
                                fields.Add(i, d1);
                                Controls.Add(d1);
                                break;
                            case "date":
                                DateTimePicker d2 = new DateTimePicker();
                                d2.Location = new Point(113, (i * 30) + 13);
                                d2.Size = new Size(250, 10);
                                d2.Format = DateTimePickerFormat.Short;
                                fields.Add(i, d2);
                                Controls.Add(d2);
                                break;
                            case "bit":
                                CheckBox c1 = new CheckBox();
                                c1.Location = new Point(113, (i * 30) + 13);
                                fields.Add(i, c1);
                                Controls.Add(c1);
                                break;
                        }
                    }
                    break;

                case QueryType.UPDATE:

                    values = Database.getAllValuesFromId(t, id);

                    for (int i = 0; i < l.Count; i++) {
                        // Add Labels
                        Label l1 = new Label();
                        l1.Location = new Point(13, (i * 30) + 13);
                        l1.Text = l[i];
                        l1.ForeColor = Color.FromArgb(255, 255, 255);
                        l1.Size = new Size(100, 13);
                        labels.Add(i, l1);
                        Controls.Add(l1);
                        // Add Populated Fields
                        switch (o[i]) {
                            case "varchar":
                                TextBox t1 = new TextBox();
                                t1.Location = new Point(113, (i * 30) + 13);
                                t1.Size = new Size(250, 10);
                                t1.Text = (String)values[i];
                                fields.Add(i, t1);
                                Controls.Add(t1);
                                break;
                            case "text":
                                TextBox t2 = new TextBox();
                                t2.Location = new Point(113, (i * 30) + 13);
                                t2.Size = new Size(250, 10);
                                t2.MaxLength = 999999999;
                                t2.Text = (String)values[i];
                                fields.Add(i, t2);
                                Controls.Add(t2);
                                break;
                            case "int":
                                NumericUpDown n1 = new NumericUpDown();
                                n1.Location = new Point(113, (i * 30) + 13);
                                n1.Size = new Size(250, 10);
                                n1.Maximum = 99999999999;
                                n1.Value = (int)values[i];
                                fields.Add(i, n1);
                                Controls.Add(n1);
                                break;
                            case "datetime":
                                DateTimePicker d1 = new DateTimePicker();
                                d1.Location = new Point(113, (i * 30) + 13);
                                d1.Size = new Size(250, 10);
                                d1.Value = (DateTime)values[i];
                                fields.Add(i, d1);
                                Controls.Add(d1);
                                break;
                            case "date":
                                DateTimePicker d2 = new DateTimePicker();
                                d2.Location = new Point(113, (i * 30) + 13);
                                d2.Size = new Size(250, 10);
                                d2.Format = DateTimePickerFormat.Short;
                                d2.Value = (DateTime)values[i];
                                fields.Add(i, d2);
                                Controls.Add(d2);
                                break;
                            case "bit":
                                CheckBox c1 = new CheckBox();
                                c1.Location = new Point(113, (i * 30) + 13);
                                c1.Checked = (bool)values[i];
                                fields.Add(i, c1);
                                Controls.Add(c1);
                                break;
                        }
                    }

                    break;
            }

            Button b1 = new Button();
            b1.Location = new Point(13, Height - 80);
            b1.Size = new Size(110, 50);
            b1.Text = "Fechar";
            b1.Click += (s,ea) => { Close(); };
            b1.ForeColor = Color.FromArgb(255, 255, 255);
            Controls.Add(b1);

            Button b2 = new Button();
            b2.Location = new Point(134, Height - 80);
            b2.Size = new Size(110, 50);
            if (qt == QueryType.INSERT) {
                b2.Text = "Limpar";
            } else b2.Text = "Resetar";
            b2.Click += (s, ea) => {
                switch (qt) {
                    // reset on insert
                    case QueryType.INSERT:
                        for (int i = 0; i < fields.Count; i++) {
                            switch (o[i]) {
                                case "varchar":
                                    TextBox t1 = (TextBox)fields[i];
                                    t1.Clear();
                                    break;
                                case "text":
                                    TextBox t2 = (TextBox)fields[i];
                                    t2.Clear();
                                    break;
                                case "int":
                                    NumericUpDown n1 = (NumericUpDown)fields[i];
                                    n1.Value = 0;
                                    break;
                                case "datetime":
                                    DateTimePicker d1 = (DateTimePicker)fields[i];
                                    d1.Value = System.DateTime.Now;
                                    break;
                                case "date":
                                    DateTimePicker d2 = (DateTimePicker)fields[i];
                                    d2.Value = System.DateTime.Now;
                                    break;
                                case "bit":
                                    CheckBox c1 = (CheckBox)fields[i];
                                    c1.Checked = false;
                                    break;
                            }
                        }
                        break;
                    // reset on update
                    case QueryType.UPDATE:
                        for (int i = 0; i < fields.Count; i++) {
                            switch (o[i]) {
                                case "varchar":
                                    TextBox t1 = (TextBox)fields[i];
                                    t1.Text = (String)values[i];
                                    break;
                                case "text":
                                    TextBox t2 = (TextBox)fields[i];
                                    t2.Text = (String)values[i];
                                    break;
                                case "int":
                                    NumericUpDown n1 = (NumericUpDown)fields[i];
                                    n1.Value = (int)values[i];
                                    break;
                                case "datetime":
                                    DateTimePicker d1 = (DateTimePicker)fields[i];
                                    d1.Value = (DateTime)values[i];
                                    break;
                                case "date":
                                    DateTimePicker d2 = (DateTimePicker)fields[i];
                                    d2.Value = (DateTime)values[i];
                                    break;
                                case "bit":
                                    CheckBox c1 = (CheckBox)fields[i];
                                    c1.Checked = (bool)values[i];
                                    break;
                            }
                        }
                        break;
                }
            };
            b2.ForeColor = Color.FromArgb(255, 255, 255);
            Controls.Add(b2);

            Button b3 = new Button();
            b3.Location = new Point(255, Height - 80);
            b3.Size = new Size(110, 50);
            switch (qt) {
                case QueryType.INSERT:
                    b3.Text = "Inserir";
                    break;
                case QueryType.UPDATE:
                    b3.Text = "Atualizar";
                    break;
            }
            b3.Click += (s, ea) => {
                if (MessageBox.Show("Você está certo?", "Tem certeza?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    List<object> m = new List<object>();
                    bool ok = true;
                    for (int i = 0; i < l.Count; i++) {
                        if (!ok) {
                            MessageBox.Show("Preencha todos os campos!");
                            break;
                        }
                        switch (o[i]) {
                            case "varchar":
                                TextBox t1 = (TextBox)fields[i];
                                if (t1.Text == "" || t1.Text == null) ok = false;
                                m.Add(t1.Text);
                                break;
                            case "text":
                                TextBox t2 = (TextBox)fields[i];
                                if (t2.Text == "" || t2.Text == null) ok = false;
                                m.Add(t2.Text);
                                break;
                            case "int":
                                NumericUpDown n1 = (NumericUpDown)fields[i];
                                m.Add(n1.Value);
                                break;
                            case "date":
                                DateTimePicker d1 = (DateTimePicker)fields[i];
                                if (d1.Text == "" || d1.Text == null) ok = false;
                                m.Add(d1.Value.ToString("yyyy-MM-dd"));
                                break;
                            case "datetime":
                                DateTimePicker d2 = (DateTimePicker)fields[i];
                                if (d2.Text == "" || d2.Text == null) ok = false;
                                m.Add(d2.Value.ToString("yyyy-MM-dd hh:mm"));
                                break;
                            case "bit":
                                CheckBox c1 = (CheckBox)fields[i];
                                if (c1.Checked) m.Add(1); else m.Add(0);
                                break;
                        }
                    }
                    switch (qt) {
                        case QueryType.INSERT:
                            Database.insertOnTable(t, l, m);
                            Close();
                            break;
                        case QueryType.UPDATE:
                            Database.updateFromTable(t, id, l, m);
                            Close();
                            break;
                    }
                }
            };
            b3.ForeColor = Color.FromArgb(255, 255, 255);
            Controls.Add(b3);

        }

        private void SysWOW64_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLoc = e.Location;
        }

        private void SysWOW64_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                this.Location = new Point((this.Location.X - lastLoc.X) + e.X, (this.Location.Y - lastLoc.Y) + e.Y);
                this.Update();
            }
        }

        private void SysWOW64_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }
    }
}
