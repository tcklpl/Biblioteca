﻿namespace WindowsFormsApp2 {
    partial class SysWOW64 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // SysWOW64
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SysWOW64";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SysWOW64";
            this.Load += new System.EventHandler(this.SysWOW64_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SysWOW64_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SysWOW64_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SysWOW64_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}