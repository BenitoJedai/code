using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeWindowsLoginExperiment.Library.Designers
{


    // RootDesignerView is a simple control that will be displayed  
    // in the designer window. 
    //public class RootDesignerView : UserControl
    public class RootDesignerView : UserControl
    {
        //jsc.meta.Library.ScriptResourceWriter ref1;

        //global::jsc.Library.VirtualDictionaryBase ref2;

        ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images.Google_Chrome ref0 = new ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images.Google_Chrome();



        private Timer timer1;
        private System.ComponentModel.IContainer components;
        private Button button1;
        private Timer timer2;
        private Button button2;
        private WebBrowser webBrowser1;
        private Button button3;
        private SampleRootDesigner m_designer;

        public RootDesignerView()
        {
            InitializeComponent();
        }

        public RootDesignerView(SampleRootDesigner designer)
        {
            m_designer = designer;
            //BackColor = Color.Blue;
            ////Font = new Font(Font.FontFamily.Name, 24.0f);
            //Font = new Font(Font.Name, 24.0f);

            InitializeComponent();

        }

        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    base.OnPaint(pe);

        //    // Draws the name of the component in large letters.
        //    pe.Graphics.DrawString(
        //        "jsc: " + m_designer.Component.Site.Name
        //        , Font, Brushes.Yellow, ClientRectangle);
        //}

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "goo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(100, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "goo";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(19, 64);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(268, 201);
            this.webBrowser1.TabIndex = 2;
            this.webBrowser1.Url = new System.Uri("http://my.jsc-solutions.net", System.UriKind.Absolute);
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(247, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "goo";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // RootDesignerView
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "RootDesignerView";
            this.Size = new System.Drawing.Size(697, 521);
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form { FormBorderStyle = FormBorderStyle.SizableToolWindow }.Show();

        }
    }

}
