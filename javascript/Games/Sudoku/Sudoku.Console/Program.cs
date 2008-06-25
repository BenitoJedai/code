using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Sudoku.Transform;

namespace Sudoku.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("web/assets/Sudoku.Editor/Level1.txt");

            var t = new SudokuFile(data);


            t.ToConsole();


            t.Mappings.Rotated = true;

            t.ToConsole();

        }
    }
}
