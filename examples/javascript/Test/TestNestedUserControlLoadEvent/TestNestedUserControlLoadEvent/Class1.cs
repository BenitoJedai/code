using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestNestedUserControlLoadEvent
{
    class Class1 :UserControl
    {
        private Button button1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }


        public Class1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Class1
            // 
            this.Controls.Add(this.button1);
            this.Name = "Class1";
            this.Load += new System.EventHandler(this.Class1_Load);
            this.ResumeLayout(false);

        }

        private void Class1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Class1_Load");

            button1.Text = "load event";
        }
    }
}
