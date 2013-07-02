using RememberFormLocation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RememberFormLocation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool LoadDone;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDone = true;

            Console.WriteLine("Load " + new { Settings.Default.FooLeft });

            if (Settings.Default.FooTop != 0)
            {
                var IsNaN = double.IsNaN((double)Settings.Default.FooTop);

                if (!IsNaN)
                    this.Top = Settings.Default.FooTop;

            }

            if (Settings.Default.FooLeft != 0)
            {
                var IsNaN = double.IsNaN((double)Settings.Default.FooLeft);

                if (!IsNaN)
                {
                    this.Left = Settings.Default.FooLeft;
                }

                Console.WriteLine("Load " + new { Left });
            }


            var sizeString = (string)Settings.Default["Foo"];
            if (!string.IsNullOrEmpty(sizeString))
            {
                // Load { sizeString = NaN }
                // Load { sizeString = <size Width="null" Height="null"/> }

                Console.WriteLine("Load " + new { sizeString });
                var size = XElement.Parse(sizeString);
                Console.WriteLine("Load " + new { size });

                this.Width = int.Parse(size.Attribute("Width").Value);
                this.Height = int.Parse(size.Attribute("Height").Value);

            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (LoadDone)
            {
                Settings.Default.FooLeft = this.Left;
                Settings.Default.FooTop = this.Top;
            }

            Console.WriteLine("FormClosing " + new { Settings.Default.FooLeft });

            Settings.Default["Foo"] = new XElement(
                "size",
                new XAttribute("Width", "" + Width),
                new XAttribute("Height", "" + Height)
            ).ToString();

        }
    }
}
