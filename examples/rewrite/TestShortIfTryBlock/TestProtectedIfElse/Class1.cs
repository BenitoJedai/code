using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace TestProtectedIfElse
{
    public class Class1
    {
        public static MethodInfo Foo(object x)
        {
            try
            {
                if (x is MethodInfo)
                    return (MethodInfo)x;
                else
                    return null;
            }
            catch (ArgumentException e)
            {

                // not a method nor a ctor
                return null;
            }
            catch (Exception __exc)
            {
                throw;
            }
            catch 
            {
                throw;
            }
            finally
            {
                Console.WriteLine();
            }
            Console.WriteLine();

        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Action<string[]> _ = Program.Main;

            Debug.Assert(Class1.Foo(null) == null);
            Debug.Assert(Class1.Foo(_.Method) != null);

            Console.WriteLine("done!");
        }
    }
}
