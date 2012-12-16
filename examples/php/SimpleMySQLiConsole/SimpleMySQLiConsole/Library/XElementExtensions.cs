using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

namespace SimpleMySQLiConsole.Library
{
    static class XElementExtensions
    {
        public static Form ToForm(this XElement xml)
        {
            var f = new Form
            {
                Text = "SQL results"
            };


            var g = new DataGridView();

            g.Dock = DockStyle.Fill;

            f.Controls.Add(g);

            g.Show();


            g.Columns.AddRange(
                xml.Element("th").Elements().Select(
                    td => new DataGridViewTextBoxColumn { HeaderText = td.Value }
                ).ToArray()
            );

            g.Rows.AddRange(
                xml.Elements("tr").Select(
                    tr =>
                        new DataGridViewRow().With(
                            r =>
                            {
                                r.Cells.AddTextRange(
                                    tr.Elements().Select(td => td.Value).ToArray()
                                );
                            }
                        )
                ).ToArray()
            );

            f.Show();
            f.GetHTMLTarget().AttachTo(Native.Document.body.parentNode);

            return f;
        }
    }
}
