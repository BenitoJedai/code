using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;

namespace Sudoku.Editor
{
    [Script, ScriptApplicationEntryPoint]
    class Viewer
    {
        public Viewer()
        {

        }

        static Viewer()
        {
            typeof(Viewer).Spawn();
        }
    }
}
