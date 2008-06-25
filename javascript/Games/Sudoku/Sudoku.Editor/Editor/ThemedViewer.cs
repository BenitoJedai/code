using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using Sudoku.Transform;
using ScriptCoreLib.Shared.Drawing;

namespace Sudoku.Editor
{
    [Script, ScriptApplicationEntryPoint]
    class ThemedViewer
    {
        public ThemedViewer()
        {
            Assets.Level1.DownloadToString(
                text =>
                {
                    var e = new SudokuFile(text);

                 
                    e.Mappings.Randomize();

                    ToTable(e);
                }
            );
        }

        private static void ToTable(SudokuFile e)
        {
            var t = new IHTMLTable();

            t.cellPadding = 0;
            t.cellSpacing = 0;

            t.style.borderWidth = "1px";
            t.style.borderStyle = "solid";
            t.style.borderColor = Color.Gray;

            var b = t.AddBody();

            for (int y = 1; y < 10; y++)
            {
                var r = b.AddRow();

                for (int x = 1; x < 10; x++)
                {
                    var s = e[x, y];

                    var c = r.AddColumn();

                    //c.style.width = "28px";
                    //c.style.height = "28px";

                    c.style.width = "2em";
                    c.style.height = "2em";

                    c.style.borderWidth = "1px";
                    c.style.borderStyle = "solid";
                    c.style.borderColor = Color.Gray;

                    c.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.center;
                    c.style.verticalAlign = "middle";

                    if (x % 3 == 1)
                        c.style.borderLeftColor = Color.Black;

                    if (y % 3 == 1)
                        c.style.borderTopColor = Color.Black;

                    if (x % 3 == 0)
                        c.style.borderRightColor = Color.Black;

                    if (y % 3 == 0)
                        c.style.borderBottomColor = Color.Black;


                    c.innerText = e[x, y].ToString();

                    if (s.Hidden)
                    {
                        // no-break space
                    }
                    else
                    {
                        c.style.fontWeight = "bold";
                    }

                }
            }

            t.AttachToDocument();
        }

        static ThemedViewer()
        {
            typeof(ThemedViewer).Spawn();
        }
    }
}
