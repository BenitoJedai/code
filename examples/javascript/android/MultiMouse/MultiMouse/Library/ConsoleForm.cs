using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLiteWithDataGridView.Library
{
    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();
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

        public void InitializeConsoleFormWriter()
        {
            var f = this;

            var w = new ConsoleFormWriter();

            var o = Console.Out;

            Console.SetOut(w);

            w.AtWrite =
                x =>
                {
                    f.textBox1.AppendText(x);
                    f.textBox1.ScrollToCaret();

                    o.Write(x);
                };

            w.AtWriteLine =
                x =>
                {
                    WriteLine(x);

                    o.WriteLine(x);

                };
        }

        public void WriteLine(string x)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Text = x;
                return;
            }

            this.textBox1.AppendText(x + Environment.NewLine);

            // this seems to be a slow func based on chrome profiler
            this.textBox1.ScrollToCaret();
        }

        private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Normal;
                else
                {
                    this.WindowState = FormWindowState.Minimized;

                    this.textBox1.Clear();
                    this.textBox1.AppendText("Console cleared..." + Environment.NewLine);

                }
            }

        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }
    }
}
