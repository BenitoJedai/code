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

namespace AvalonFlashLinqToObjects
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

            var label1 = new TextBlock
            {
                Text = "Enter a list of names separated by commas",
            }.AttachTo(this).MoveTo(8, 8);

            var users = new TextBox
            {
                AcceptsReturn = true,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                Width = 300,
                Height = 50,
                Text = "_martin, mike, mac, ken, neo, zen, jay, morpheous, trinity, Agent Smith, _psycho",
            }.AttachTo(this).MoveTo(8, 8 + 32);



            var label2 = new TextBlock
            {
                Text = "Enter a partial name to be found from the list above.",
            }.AttachTo(this).MoveTo(8, 8 + 32  + 50 + 8);

            var filter = new TextBox
            {
                Width = 300,
                Text = "psy",
            }.AttachTo(this).MoveTo(8, 8 + 32 + 50 + 8 + 16);

            var label3 = new TextBlock
            {
                Text = "Enter a partial name to make the entry special",
            }.AttachTo(this).MoveTo(8, 8 + 32 + 50 + 8 + 58);

            var filter2 = new TextBox
            {
                Width = 300,
                Text = "a",
            }.AttachTo(this).MoveTo(8, 8 + 32 + 50 + 8 + 16 + 58);



            var label4 = new TextBlock
            {
                Text = "Results",
            }.AttachTo(this).MoveTo(8, 8 + 32 + 50 + 8 + 58 + 58);

            var result = new TextBox
            {
                AcceptsReturn = true,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),

                Width = 300,
                Height = 50,
                Text = "?",
                IsReadOnly = true,
            }.AttachTo(this).MoveTo(8, 8 + 32 + 50 + 8 + 16 + 58 + 58);

            Action Update =
                delegate
                {
                    var user_filter = filter.Text.Trim().ToLower();
                    var user_filter2 = filter2.Text.Trim().ToLower();

                    result.Clear();

                    var __users = users.Text.Split(',');


                    var query = from i in __users
                                where i.ToLower().Contains(user_filter)
                                let name = i.Trim()
                                let isspecial = i.ToLower().Contains(user_filter2)
                                orderby isspecial ascending, name.Length descending, name
                                select new { isspecial, length = name.Length, name };

                    foreach (var v in query)
                    {
                        var m = "match: " + v;

                        if (v.isspecial)
                            m = m.ToUpper();

                        result.AppendText(m + Environment.NewLine);
                    }
                };

            Update();

            users.TextChanged += delegate { Update(); };
            filter.TextChanged += delegate { Update(); };
            filter2.TextChanged += delegate { Update(); };
        }

    }
}
