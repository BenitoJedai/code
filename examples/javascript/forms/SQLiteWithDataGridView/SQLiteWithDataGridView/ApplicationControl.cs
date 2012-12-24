using SQLiteWithDataGridView;
using SQLiteWithDataGridView.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLiteWithDataGridView
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }


        class ConsoleFormWriter : TextWriter
        {
            public Action<string> AtWrite;
            public Action<string> AtWriteLine;

            public override void Write(string value)
            {
                AtWrite(value);
            }

            public override void WriteLine(string value)
            {
                AtWriteLine(value);

            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var f = new ConsoleForm();
            var w = new ConsoleFormWriter();

            var o = Console.Out;

            Console.SetOut(w);

            w.AtWrite =
                x =>
                {
                    f.textBox1.AppendText(x);
                    o.Write(x);
                    f.textBox1.ScrollToCaret();
                };

            w.AtWriteLine =
                x =>
                {
                    f.textBox1.AppendText(x + Environment.NewLine);
                    o.WriteLine(x);
                    f.textBox1.ScrollToCaret();
                };

            f.Show();

            Console.WriteLine("Console has been redirected!");
        }


        private void Table001_Click(object sender, System.EventArgs e)
        {
            new GridForm { service = this.applicationWebService1 }.Show();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

    }
}
