using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using Sudoku.Transform;

namespace Sudoku.Editor
{
    [Script, ScriptApplicationEntryPoint]
    class Viewer
    {
        public Viewer()
        {
            Assets.Level1.DownloadToString(
                text =>
                {
                    var e = new SudokuFile(text);

                    new IHTMLCode(text).AttachToDocument();
                }
            );
        }

        static Viewer()
        {
            typeof(Viewer).Spawn();
        }
    }
}
