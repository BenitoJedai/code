using XElementExample;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XElementExample
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            if (PartialUpdate)
                return;

            PartialUpdate = true;
            textBox3.Text =
                new XElement("KeyValuePair",
                    new XAttribute("Key", textBox1.Text),
                    new XElement("Value", textBox2.Text)
                ).ToString();
            PartialUpdate = false;


        }

        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {
            if (PartialUpdate)
                return;

            PartialUpdate = true;
            textBox3.Text =
                new XElement("KeyValuePair",
                    new XAttribute("Key", textBox1.Text),
                    new XElement("Value", textBox2.Text)
                ).ToString();
            PartialUpdate = false;

        }

        public bool PartialUpdate;
        private void textBox3_TextChanged(object sender, System.EventArgs e)
        {
            if (PartialUpdate)
                return;

            PartialUpdate = true;
            try
            {
                var doc = XElement.Parse(textBox3.Text);

                textBox1.Text = doc.Attribute("Key").Value;
                textBox2.Text = doc.Element("Value").Value;

                textBox3.ForeColor = Color.Black;
            }
            catch
            {
                textBox3.ForeColor = Color.Red;

            }
            PartialUpdate = false;
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            this.textBox1.Text = "foo";
            this.textBox2.Text = "bar";
        }

    }
}
