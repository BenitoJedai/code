using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace ShellWithPing.Library
{
    public partial class ConsoleWindow : Form
    {
        public bool DisableFormClosingHandler;


        public ConsoleWindow()
        {
            InitializeComponent();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var crlf = "\r\n";

            if (!textBox1.Text.Contains(crlf))
                crlf = "\n";

            if (textBox1.Text.Contains(crlf))
            {
                var x = textBox1.Text.TakeUntilIfAny(crlf);

                textBox1.Text = "";

                History.AddRange(
                    HistoryDown.ToArray()
                );
                HistoryDown.Clear();
                if (!string.IsNullOrEmpty(HistoryPending))
                {
                    History.Add(HistoryPending);
                    HistoryPending = "";
                }

                History.Add(x);
                AppendLine("[" + History.Count + "] " + x);

                if (AtCommand != null)
                    AtCommand(x, y => AppendLine(y));

            }
        }

        public string HistoryPending;
        public List<string> History = new List<string>();
        public List<string> HistoryDown = new List<string>();

        public event Action<string, Action<string>> AtCommand;

        public ConsoleWindow AppendLine(string x)
        {
            // Additional information: Invoke or BeginInvoke cannot be called on a control until the window handle has been created.
            if (Loaded)
            {
                this.Invoke(
                    new Action(
                        delegate
                        {
                            InternalAppendLine(x);
                        }
                    )
                );
            }
            else
            {
                this.Load +=
                    delegate
                    {
                        AppendLine(x);
                    };
            }


            return this;
        }

        public void Clear(string DefaultText = "")
        {
            label1.Text = DefaultText;
            textBox2.Text = label1.Text;
        }

        private ConsoleWindow InternalAppendLine(string x)
        {

            label1.Text += "\r\n" + x;
            textBox2.Text = label1.Text;
            textBox2.SelectionStart = textBox2.Text.Length - 1;

            // does this work?
            textBox2.ScrollToCaret();
            
            //textBox2.Select(textBox2.Text.Length - 1, 1);
            textBox1.Focus();


            return this;
        }

        bool Loaded = false;
        private void ConsoleWindow_Load(object sender, EventArgs e)
        {
            Loaded = true;
            this.BackColor = Color.Black;

            //label1.Hide();
            textBox2.Left = label1.Left;
            textBox1.Left = label1.Left;

            textBox1.Top = label1.Bottom;

            textBox2.Top = label1.Top;
            textBox2.Height = label1.Height;

            label2.Top = label1.Bottom;

            label1.Text = "";
            textBox2.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Visible = !label2.Visible;
        }

        private void ConsoleWindow_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void label1_SizeChanged(object sender, EventArgs e)
        {
            textBox2.Height = label1.Height + 8;

            textBox1.Top = textBox2.Bottom;
            label2.Top = textBox2.Bottom;
        }

        public Color Color
        {
            set
            {
                this.textBox1.ForeColor = value;
                this.textBox2.ForeColor = value;
                this.label2.ForeColor = value;
            }
        }

        private void ConsoleWindow_Resize(object sender, EventArgs e)
        {
            textBox1.Width = this.ClientSize.Width - textBox1.Left;
            textBox2.Width = this.ClientSize.Width - textBox2.Left;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (History.Any())
                {
                    var x = History.Last();
                    HistoryPending = x;
                    HistoryDown.Add(this.textBox1.Text);

                    //var a = new { this.textBox1.SelectionStart, this.textBox1.SelectionLength };
                    this.textBox1.Text = x;
                    this.textBox1.Select(this.textBox1.Text.Length, 0);

                    History.RemoveAt(History.Count - 1);
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (HistoryDown.Any())
                {
                    var x = HistoryDown.Last();
                    History.Add(this.textBox1.Text);

                    HistoryPending = x;
                    this.textBox1.Text = x;
                    this.textBox1.Select(this.textBox1.Text.Length, 0);

                    HistoryDown.RemoveAt(HistoryDown.Count - 1);
                }
            }

            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                this.textBox1.Text += "\r\n";
            }
        }


    }
}
