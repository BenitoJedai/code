using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace AvalonFlashXElementExample
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.LightGray;
            r.AttachTo(this);
            r.MoveTo(4, 4);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 8.0, this.Height - 8.0);



            var label2 = new TextBlock
            {
                Text = "Key",
            }.AttachTo(this).MoveTo(8, 8);

            var textBox1 = new TextBox
            {
                Width = 300,
                Text = "",
            }.AttachTo(this).MoveTo(8, 8 + 16);


            var label3 = new TextBlock
            {
                Text = "Value",
            }.AttachTo(this).MoveTo(8, 8 + 58);

            var textBox2 = new TextBox
            {
                Width = 300,
                Text = "",
            }.AttachTo(this).MoveTo(8, 8 + 16 + 58);



            var textBox3 = new TextBox
            {
                AcceptsReturn = true,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                Width = 300,
                Height = 50,
                Text = "",
            }.AttachTo(this).MoveTo(8, 8 + 128);

            var PartialUpdate = false;

            textBox1.TextChanged +=
                delegate
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
                };


            textBox2.TextChanged +=
                delegate
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
                };


            textBox3.TextChanged +=
               delegate
               {
                   if (PartialUpdate)
                       return;
                   PartialUpdate = true;

                   try
                   {
                       var doc = XElement.Parse(textBox3.Text);

                       textBox1.Text = doc.Attribute("Key").Value;
                       textBox2.Text = doc.Element("Value").Value;

                       textBox3.Foreground = Brushes.Black;
                   }
                   catch
                   {
                       textBox3.Foreground = Brushes.Red;
                   }


                   PartialUpdate = false;
               };

            textBox1.Text = "foo";
            textBox2.Text = "bar";
        }

    }
}
