using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        // http://java.sun.com/javase/6/docs/api/java/text/Normalizer.html
        // http://stackoverflow.com/questions/1272032/java-utf-8-strange-behaviour
        // http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4038677
        // http://blogs.msdn.com/oldnewthing/archive/2005/08/29/457483.aspx

        [Script]
        public class __ConsoleOut : TextWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }

            public override void Write(string value)
            {
                __Console.InternalPrintStream.print(value);
            }

            public override void WriteLine(string value)
            {
                __Console.InternalPrintStream.println(value);
            }
        }

        static TextWriter InternalOut;
        public static TextWriter Out
        {
            get
            {
                if (InternalOut == null)
                    InternalOut = new __ConsoleOut();

                return InternalOut;
            }
        }

        public static void SetOut(global::System.IO.TextWriter newOut)
        {
            InternalOut = newOut;
        }

        static string InternalGetEnvironmentEncoding()
        {
            // http://en.wikipedia.org/wiki/Code_page

            // yay, this was fixed for java6...
            // http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4153167


            var u = default(string);

            try
            {
                // we cannot do this in applet?
                u = global::java.lang.JavaSystem.getProperty("file.encoding");
            }
            catch
            {

            }

            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLR1252Encoding\TestJVMCLR1252Encoding\Program.cs

            // default:
            if (string.IsNullOrEmpty(u))
                return "UTF-8";

            // translate Windows (ANSI) code pagesto IBM PC (OEM) code pages

            // baltic
            if ("Cp1257" == u) return "Cp775";

            // ...

            return u;
        }

        static global::java.io.PrintStream InternalPrintStreamCache;
        static global::java.io.PrintStream InternalPrintStream
        {
            get
            {
                try
                {
                    if (InternalPrintStreamCache == null)
                    {
                        var _stream = global::java.lang.JavaSystem.@out;
                        var _encoding = InternalGetEnvironmentEncoding();

                        InternalPrintStreamCache = new global::java.io.PrintStream(_stream, true, _encoding);
                    }
                }
                catch
                {
                    throw;
                }


                return InternalPrintStreamCache;
            }
        }

        public static void Beep()
        {
            global::java.awt.Toolkit.getDefaultToolkit().beep();
        }

        public static void Write(string p)
        {
            Out.Write(p);
        }

        public static void Write(char c)
        {
            Out.Write(new string(new char[] { c }));
        }


        public static void WriteLine()
        {
            Out.WriteLine("");
        }

        public static void WriteLine(object e)
        {
            Out.WriteLine("" + e);
        }

        public static void WriteLine(string e)
        {
            Out.WriteLine(e);
        }

        public static string ReadLine()
        {
            string z = null;

            try
            {
                var r0 = new global::java.io.InputStreamReader(global::java.lang.JavaSystem.@in);
                var r1 = new global::java.io.BufferedReader(r0);


                z = r1.readLine();
            }
            catch (Exception)
            {

            }

            return z;
        }


        // this is a nop
        public static ConsoleColor ForegroundColor { get; set; }
    }
}
