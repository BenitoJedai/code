using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace Sudoku.Transform
{
    [Script]
    public class SudokuFile
    {
        [Script]
        public class Symbol
        {
            public int Value;

            public bool Hidden;

            public int X;
            public int Y;
        }

        public SudokuFile(string data)
        {
            this.data = data;


        }
    }
}
