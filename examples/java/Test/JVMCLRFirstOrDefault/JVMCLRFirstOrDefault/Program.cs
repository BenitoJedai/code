using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRFirstOrDefault
{
    enum xlong : long { }

    static class Program
    {
        class item
        {
            public xlong value;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            try
            {
                //- javac
                //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRFirstOrDefault\Program.java
                //java\JVMCLRFirstOrDefault\Program.java:31: error: method Of in class __SZArrayEnumerator_1<T#2> cannot be applied to given types;
                //            num1 = __Enumerable.<Long>FirstOrDefault(__SZArrayEnumerator_1.<Long>Of(numArray0));
                //                                                                          ^
                //  required: T#1[]
                //  found: long[]
                //  reason: actual argument long[] cannot be converted to Long[] by method invocation conversion
                //  where T#1,T#2 are type-variables:
                //    T#1 extends Object declared in method <T#1>Of(T#1[])
                //    T#2 extends Object declared in class __SZArrayEnumerator_1

                //var e = new long[0];
                {
                    var e = new item[0];


                    var z = e.Select(x => x.value).FirstOrDefault();

                    Console.WriteLine(new { z });
                }

                {
                    var e = new[] { new item { value = 
                        
                        //Error	1	Cannot implicitly convert type 'int' to 'JVMCLRFirstOrDefault.xlong'. An explicit conversion exists (are you missing a cast?)	X:\jsc.svn\examples\java\Test\JVMCLRFirstOrDefault\JVMCLRFirstOrDefault\Program.cs	68	56	JVMCLRFirstOrDefault
                        (JVMCLRFirstOrDefault.xlong)
                        666 } };


                    var z = e.Select(x => x.value).FirstOrDefault();

                    Console.WriteLine(new { z });
                }
            }
            catch
            {
                Console.WriteLine("NOK");
            }


            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
