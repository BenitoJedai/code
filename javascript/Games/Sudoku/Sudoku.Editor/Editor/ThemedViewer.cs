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
using ScriptCoreLib.JavaScript.DOM;

namespace Sudoku.Editor
{
    [Script, ScriptApplicationEntryPoint]
    class ThemedViewer
    {
        [Script]
        public class Cell
        {
            public int X;
            public int Y;

            public IHTMLSpan Text;
        }

        public ThemedViewer()
        {
            var body = Native.Document.body;

            body.style.backgroundColor = Color.Black;
            body.style.color = Color.FromRGB(0, 0xff, 00);
            body.style.fontFamily = IStyle.FontFamilyEnum.Consolas;

            var a = new List<Cell>();

            CreateTable(a);

            Assets.Level1.DownloadToString(
                text =>
                {
                    var e = new SudokuFile(text);

                 
                    e.Mappings.Randomize();

                    for (int y = 1; y < 10; y++)
                    {
                        for (int x = 1; x < 10; x++)
                        {
                            var s = e[x, y];
                            var c = a.Single(i => i.X == x && i.Y == y);

                            
                            if (s.Hidden)
                            {
                                // no-break space
                            }
                            else
                            {
                                c.Text.style.fontWeight = "bold";
                                c.Text.innerText = s.ToString();
                            }

                        }
                    }
                }
            );
        }

        private static void CreateTable(List<Cell> a)
        {
            var t = new IHTMLTable();

            t.cellPadding = 0;
            t.cellSpacing = 0;

            t.style.borderWidth = "1px";
            t.style.borderStyle = "solid";
            t.style.borderColor = Color.FromRGB(0, 0x80, 00);

            var b = t.AddBody();

            for (int y = 1; y < 10; y++)
            {
                var r = b.AddRow();

                for (int x = 1; x < 10; x++)
                {
                    var s = new Cell
                    {
                        X = x,
                        Y = y,
                        Text = new IHTMLSpan("\xA0")
                    };

                    a.Add(s);

                    var c = r.AddColumn(s.Text);


                    c.style.width = "2em";
                    c.style.height = "2em";

                    c.style.borderWidth = "1px";
                    c.style.borderStyle = "solid";
                    c.style.borderColor = Color.FromRGB(0, 0x80, 00);

                    c.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.center;
                    c.style.verticalAlign = "middle";

                    if (x % 3 == 1)
                        c.style.borderLeftColor = Color.FromRGB(0, 0xff, 00);

                    if (y % 3 == 1)
                        c.style.borderTopColor = Color.FromRGB(0, 0xff, 00);

                    if (x % 3 == 0)
                        c.style.borderRightColor = Color.FromRGB(0, 0xff, 00);

                    if (y % 3 == 0)
                        c.style.borderBottomColor = Color.FromRGB(0, 0xff, 00);





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
