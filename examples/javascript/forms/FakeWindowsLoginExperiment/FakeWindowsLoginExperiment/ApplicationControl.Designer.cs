using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FakeWindowsLoginExperiment
{
    public partial class ApplicationControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.applicationWebService1 = new FakeWindowsLoginExperiment.ApplicationWebService();
            this.rootViewSampleComponent1 = new FakeWindowsLoginExperiment.Library.Designers.Samples.RootViewSampleComponent();
            this.rootDesignedComponent1 = new FakeWindowsLoginExperiment.Library.Designers.Samples.RootDesignedComponent();
            this.exampleComponent1 = new FakeWindowsLoginExperiment.Library.Designers.Samples.ExampleComponent();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(310, 258);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Logout";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }

        private Button button1;
        private Button button2;
        private ApplicationWebService applicationWebService1;
        private Library.Designers.Samples.RootViewSampleComponent rootViewSampleComponent1;
        private Library.Designers.Samples.RootDesignedComponent rootDesignedComponent1;
        private Library.Designers.Samples.ExampleComponent exampleComponent1;

    }
}
