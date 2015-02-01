﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        // tested by
        // X:\jsc.svn\examples\c\Test\TestConsoleWriteLine\TestConsoleWriteLine\Program.cs


        public static ConsoleColor ForegroundColor
        {
            set
            {
                windows_h.SetConsoleTextAttribute(
                    windows_h.GetStdHandle(windows_h.STD_OUTPUT_HANDLE)
                    , (int)value);
            }
        }

        public static void Beep()
        {
            windows_h.Beep(400, 200);
        }

        public static void Beep(int f, int d)
        {
            windows_h.Beep(f, d);
        }

        public static void Write(double i)
        {
            stdio_h.printf("%g", __arglist(i));
        }

        public static void Write(int i)
        {
            stdio_h.printf("%d", __arglist(i));
        }

        public static void Write(char c)
        {
            stdio_h.putchar(c);

            // http://stackoverflow.com/questions/14680252/difference-between-puts-and-printf-in-c-while-using-sleep
            //stdio_h.fflush();

        }

        public static void Write(string e)
        {
            stdio_h.printf("%s", __arglist(e));


            //foreach (var c in e)
            //{
            //    Write(c);
            //}
        }

        public static void WriteLine()
        {
            WriteLine("");
        }

        public static void WriteLine(int e)
        {
            Write(e);
            WriteLine();
        }
        public static void WriteLine(string e)
        {
            stdio_h.puts(e);
        }
    }

}
