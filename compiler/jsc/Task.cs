using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace jsc
{
    class Task : IDisposable
    {
        class Color : IDisposable   
        {
            ConsoleColor x;

            public Color(ConsoleColor c)
            {
                x = Console.ForegroundColor;

                Console.ForegroundColor = c;
            }

            #region IDisposable Members

            public void Dispose()
            {
                Console.ForegroundColor = x;

            }

            #endregion
        }
        
        static int _ident;

        string _name;
        bool _enabled;

        public static bool Enabled;

        static public void Error(string e, params object[] a)
        {
            using (new Color(ConsoleColor.Red))
            {
                if (a.Length == 0)
                    Console.Error.WriteLine(@"script: error JSC1000: " + e);
                else
                    Console.Error.WriteLine(@"script: error JSC1000: " + e, a);
            }

        }

        static public void Warning(string e, params object[] a)
        {
            using (new Color(ConsoleColor.Yellow))
            {
                Console.Error.WriteLine(@"script: warning JSC1000: " + e, a);

            }

        }

        public Task(string name) : this(name, null)
        {
        }

        [DebuggerNonUserCode]
        static public void Fail(ILInstruction i)
        {
            if (i != null)
            {
                Error("error at {0}.{1}, {2}", i.OwnerMethod.DeclaringType.FullName,  i.OwnerMethod.Name, i.Location);

            }
            Console.WriteLine("*** Compler cannot continue... press enter to quit.");
            Console.ReadLine();

            if (Debugger.IsAttached)
                Debugger.Break();

			throw new InvalidOperationException();
        }

        public Task(string name, string desc)
        {
            _enabled = Enabled;

            _name = name;
            //if (_enabled)
            //{

            //    WriteIdent();

            //    using (new Color(ConsoleColor.DarkCyan))
            //    {
            //        Console.Write("<");
            //    }

            //    Console.Write(_name);

            //    if (desc != null)
            //    {
            //        Console.Write(" [");

            //        using (new Color(ConsoleColor.Green))
            //        {
            //            Console.Write(desc);
            //        }

            //        Console.Write("]");
            //    }

            //    using (new Color(ConsoleColor.DarkCyan))
            //    {
            //        Console.Write(">");
            //    }

            //    Console.WriteLine();
            //}

            _ident++;

        }

        static void WriteIdent()
        {
            
            //Console.Write(new string(' ', _ident * 2));
        }

        public static void WriteLine(string e)
        {
            //if (Enabled)
            //{
            //    WriteIdent();

            //    Console.WriteLine(e);
            //}
        }


        public static void WriteLine(string e,params object[] a)
        {
            //if (Enabled)
            //{
            //    WriteIdent();

            //    Console.WriteLine(e, a);
            //}
        }

        #region IDisposable Members

        public void Dispose()
        {

            _ident--;

            //if (_enabled)
            //{
            //    Console.WriteLine();

            //    WriteIdent();

            //    using (new Color(ConsoleColor.DarkCyan))
            //    {
            //        Console.Write("</");
            //    }

            //    Console.Write(_name);

            //    using (new Color(ConsoleColor.DarkCyan))
            //    {
            //        Console.Write(">");
            //    }

            //    Console.WriteLine();
            //}

        }

        #endregion
}
}
