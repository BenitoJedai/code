using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;
using ScriptCoreLib.Shared.Maze;

namespace TestMazeGenerator
{

    static class Program
    {

        class Feedback : MazeGenerator.IFeedback
        {
            public void Invoke(string e)
            {
                //Console.WriteLine(e);
            }

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..

            // generic parameter needs to be moved..
            //enumerable_10 = __Enumerable.AsEnumerable(__SZArrayEnumerator_1<String>.Of(stringArray3));

            Console.WriteLine("hi!");


            Console.WriteLine("maze...");

            var maze = new MazeGenerator(20, 20, new Feedback());

            var w = new BlockMaze(maze);
            
            for (int iy = 0; iy < w.Height; iy += 2)
            {
                for (int i = 0; i < w.Width; i++)
                {
                    var v0 = w.Walls[i][iy];
                    var v1 = false;

                    if (iy + 1 < w.Height)
                        v1 = w.Walls[i][iy + 1];

                    //Console.Write("" + v.ToString("x2"));



                    if (v0)
                    {
                        if (v1)
                            Console.Write("█");
                        else
                            Console.Write("▀");
                    }
                    else
                    {
                        if (v1)
                            Console.Write("▄");
                        else
                            Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }

            System.Console.WriteLine("done");





            System.Console.WriteLine("jvm");


            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );

        }


    }

    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain(
             StringAction ListMethods = null
            )
        {
            System.Console.WriteLine(XML);

            MessageBox.Show("it works?!?");
        }
    }

}
