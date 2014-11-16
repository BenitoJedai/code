using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ScriptCoreLib;

namespace TestThreadStart
{
    [Script(Implements = typeof(global::System.Math))]
    internal class __Math
    {
        public static double Sin(double e)
        {
            return math_h.sin(e);
        }

        public static double Cos(double e)
        {
            return math_h.cos(e);
        }
    }

    [Script(Implements = typeof(global::System.IO.File))]
    internal class __File
    {
        // http://www.cplusplus.com/reference/clibrary/cstdio/fopen.html

        public static void WriteAllText(string path, string contents)
        {
            var handle = stdio_h.fopen(path, "w+");
            stdio_h.fputs(contents, handle);
            stdio_h.fclose(handle);
        }
    }

    [Script(Implements = typeof(global::System.String))]
    internal class __String
    {
        [Script(OptimizedCode = @"return e[o];")]
        internal static char StringChar(string e, int o)
        {
            return default(char);
        }

        public char get_Chars(int i)
        {
            return StringChar((string)(object)this, i);
        }

        public int Length
        {
            get
            {
                return string_h.strlen((string)(object)this);
            }
        }
    }

    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        public static ConsoleColor ForegroundColor
        {
            set
            {
                windows_h.SetConsoleTextAttribute(windows_h.GetStdHandle(windows_h.STD_OUTPUT_HANDLE), (int)value);
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
        }

        public static void Write(string e)
        {
            foreach (var c in e)
            {
                Write(c);
            }
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

    [Script(Implements = typeof(global::System.Threading.Thread))]
    internal class __Thread
    {
        //  implementation for System.Threading.Thread not found - Void .ctor(System.Threading.ThreadStart)

        __ThreadStart __ThreadStart;
        __ParameterizedThreadStart __ParameterizedThreadStart;


        public __Thread(ParameterizedThreadStart start)
        {
            // X:\jsc.svn\examples\c\Test\TestThreadStart\TestThreadStart\Program.cs
            __ParameterizedThreadStart = (__ParameterizedThreadStart)(object)start;
        }


        public void Start(object parameter)
        {
            Console.WriteLine("__Thread ParameterizedThreadStart");

            ///*newobj*/
            //malloc(4)[0] = parameter;
            //objectArray0 = ((void**)/*newobj*/ malloc(4));

            //var arglist = new object[1];

            //arglist[0] = parameter;



            //var arglist = new object[] { parameter };

            process_h._beginthread(__ParameterizedThreadStart._method, stack_size: 0, arglist: parameter);
        }


        public __Thread(ThreadStart e)
        {
            Console.WriteLine("__Thread ");
            __ThreadStart = (__ThreadStart)(object)e;
        }



        public void Start()
        {
            Console.WriteLine("__Thread Start");

            process_h._beginthread(__ThreadStart._method, stack_size: 0, arglist: null);
        }

        public static void Sleep(int p)
        {
            windows_h.Sleep(p);
        }
    }

    // delegates for C ?
    [Script(Implements = typeof(global::System.Threading.ThreadStart))]
    internal class __ThreadStart
    {
        public IntPtr _method;

        //implementation for System.Threading.ThreadStart not found - Void.ctor(System.Object, IntPtr)
        public __ThreadStart(object target, IntPtr method)
        {
            Console.WriteLine("__ThreadStart ");

            this._method = method;
        }
    }

    [Script(Implements = typeof(global::System.Threading.ParameterizedThreadStart))]
    internal class __ParameterizedThreadStart
    {
        public IntPtr _method;

        //implementation for System.Threading.ThreadStart not found - Void.ctor(System.Object, IntPtr)
        public __ParameterizedThreadStart(object target, IntPtr method)
        {
            Console.WriteLine("__ParameterizedThreadStart ");

            this._method = method;
        }
    }


    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T>
    {
        private int _size;

        T[] _list;

        public __List()
        {
            Allocate(0);
        }

        internal void Allocate(int i)
        {
            _list = new T[i];
            _size = i;
        }

        internal bool InBounds(int i)
        {
            return (i < 0 || i >= _size);
        }

        public T this[int i]
        {
            get
            {
                if (InBounds(i)) return default(T);

                return _list[i];
            }
            set
            {
                if (InBounds(i)) return;

                _list[i] = value;
            }
        }

        public int Count
        {
            get { return _size; }
            set
            {
                _size = value;

                _list = (T[])stdlib_h.realloc(_list, IntPtr.Size * _size);
            }
        }


        public void Add(T e)
        {
            int p = _size;

            Count++;

            this[p] = e;
        }

        /// <summary>
        /// releases internal list, and frees the list object itself
        /// </summary>
        public void Free()
        {
            Count = 0;

            stdlib_h.free(this);
        }


    }

    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
        public static int Size
        {
            [Script(OptimizedCode = "return sizeof(void*);")]
            get
            {
                return default(int);
            }
        }

        [Script(OptimizedCode = "return a==b;")]
        static public bool operator ==(__IntPtr a, __IntPtr b)
        {
            return default(bool);
        }

        [Script(OptimizedCode = "return a!=b;")]
        static public bool operator !=(__IntPtr a, __IntPtr b)
        {
            return default(bool);
        }

        public override bool Equals(object obj)
        {
            return this == obj as __IntPtr;
        }

        public override int GetHashCode()
        {
            return default(int);
        }
    }
}
