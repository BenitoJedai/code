using java.net;
using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRDataTableMerge
{

    static class Program
    {
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

            //            java.lang.Object, rt
            //'JVMCLRDataTableMerge.exe' (CLR v4.0.30319: JVMCLRDataTableMerge.exe): Loaded 'X:\jsc.svn\examples\java\JVMCLRDataTableMerge\JVMCLRDataTableMerge\bin\Release\JVMCLRDataTableMerge.exports'. Module was built without symbols.
            //The program '[12004] JVMCLRDataTableMerge.exe' has exited with code 0 (0x0).
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   

            Console.WriteLine("ApplicationWebService cctor");

            var o0 = ScriptedNotifications0.GetDataTable();
            var o1 = ScriptedNotifications.GetDataTable();

            var merge = new DataTable();

            merge.Merge(o0);
            merge.Merge(o1);

            Console.WriteLine("ApplicationWebService cctor done");

            CLRProgram.CLRMain();
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
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
