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

namespace JVMCLRjgpshell
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


            // link to a CAP project

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            //System.IO.DirectoryNotFoundException: Could not find a part of the path 'X:\jsc.svn\market\synergy\java\com.jgpshell\bin\Debug\jgpshell0.03\src\com\jgpshell\cardGridCom\CardGridUser.java'.
            //   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)

            //There are inline java source files to be compiled...
            //System.IO.DirectoryNotFoundException: Could not find a part of the path 'X:\jsc.svn\market\synergy\java\com.jgpshell\JVMCLRjgpshell\JVMCLRjgpshell\jgpshell0.03\src\com\jgpshell\cardGridCom\CardGridUser.java'.


            //enter jsc.jvmi
            //source: X:\jsc.svn\market\synergy\java\com.jgpshell\JVMCLRjgpshell\JVMCLRjgpshell\jgpshell0.03\lib\swing-layout-1.0.jar
            //jsc.jvmi ScriptCoreLib.SwitchToCLRContextAttribute
            //05d4:01:04 CreateToJARImportNatives { FileNameString = X:\jsc.svn\market\synergy\java\com.jgpshell\JVMCLRjgpshell\JVMCLRjgpshell\jgpshell0.03\lib\swing-layout-1.0.jar }
            //05d4:01:04 CreateToJARImportNatives { CreateToJARImportNativesCandidate = X:\jsc.svn\market\synergy\java\com.jgpshell\JVMCLRjgpshell\JVMCLRjgpshell\jgpshell0.03\lib\swing-layout-1.0.jar }
            //05d4:01:04 CreateToJARImportNatives Cache { FileNameString = X:\jsc.svn\market\synergy\java\com.jgpshell\JVMCLRjgpshell\JVMCLRjgpshell\jgpshell0.03\lib\swing-layout-1.0.jar, Input = X:\jsc.svn\market\synergy\java\com.jgpshell\JVMCLRjgpshell\JVMCLRjgpshell\jgpshell0.03\lib\swing-layout-1.0.jar }
            //System.IO.DirectoryNotFoundException: Could not find a part of the path 'X:\jsc.svn\market\synergy\java\com.jgpshell\JVMCLRjgpshell\JVMCLRjgpshell\jgpshell0.03\lib\swing-layout-1.0.jar'.
            //   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)

            //java -Djava.library.path=.\lib\ -classpath .\lib\swing-layout-1.0.jar:.\lib\mysql-connector-java-3.0.17-ga-bin.jar;%CLASSPATH%  com.jgpshell.shell.Shell %2 %3


            //005e mysql-connector-java-3.0.17-ga-bin org.gjt.mm.mysql.Driver
            //Closing types... mysql-connector-java-3.0.17-ga-bin
            //0002 mysql-connector-java-3.0.17-ga-bin create com.mysql.jdbc.AssertionFailedException
            //0007 mysql-connector-java-3.0.17-ga-bin create com.mysql.jdbc.OutputStreamWatcher
            //0008 mysql-connector-java-3.0.17-ga-bin create com.mysql.jdbc.Blob
            //System.TypeLoadException: Method 'getBinaryStream' in type 'com.mysql.jdbc.Blob' from assembly 'mysql-connector-java-3.0.17-ga-bin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
            //   at System.Reflection.Emit.TypeBuilder.TermCreateClass(RuntimeModule module, Int32 tk, ObjectHandleOnStack type)
            //   at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
            //   at System.Reflection.Emit.TypeBuilder.CreateType()
            //   at jsc.meta.Library.CreateToJARImportNatives.    . ?  .     (TypeBuilder )
            //System.TypeLoadException: Method 'getBinaryStream' in type 'com.mysql.jdbc.Blob' from assembly 'mysql-connector-java-3.0.17-ga-bin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
            //   at System.Reflection.Emit.TypeBuilder.TermCreateClass(RuntimeModule module, Int32 tk, ObjectHandleOnStack type)
            //   at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
            //   at System.Reflection.Emit.TypeBuilder.CreateType()

            Console.WriteLine("com.jgpshell.shell.Shell.main");

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRjgpshell\Program.java
            //java\JVMCLRjgpshell\Program.java:26: error: unreported exception ParseException; must be caught or declared to be thrown
            //        Shell.main(args);
            //                  ^

            var jpcsc = new FileInfo("jpcsc.dll");

            Console.WriteLine(new { jpcsc.FullName, jpcsc.Exists });


            try
            {

                //com.jgpshell.shell.Shell.main(args);
                com.jgpshell.xshell.XShell.main(args);

                //SCardListReaders: 0x8010002e, Cannot find a smart card reader.

                //--> No reader detected !
                //---Session ready.---
                //Welcome !
                //JGPShell v 0.1 !
            }
            catch (Exception ex)
            {
                //com.jgpshell.shell.Shell.main
                //---Initializing session ...---
                //Database error : Check the driver com.mysql.jdbc.Driver was not found !
                //--> MySQL connection failure.
                //{ Message = no jpcsc in java.library.path, StackTrace = java.lang.UnsatisfiedLinkError: no jpcsc in java.library.path
                //        at java.lang.ClassLoader.loadLibrary(Unknown Source)
                //        at java.lang.Runtime.loadLibrary0(Unknown Source)
                //        at java.lang.Runtime.loadLibrary(Unknown Source)
                //        at com.linuxnet.jpcsc.PCSC.<clinit>(PCSC.java:264)
                //        at com.linuxnet.jpcsc.Context.<clinit>(Context.java:17)
                //        at com.jgpshell.cardGridCom.CardGridUser.<init>(CardGridUser.java:34)
                //        at com.jgpshell.shell.Session.init(Session.java:144)
                //        at com.jgpshell.shell.Shell.init_shell(Shell.java:72)
                //        at com.jgpshell.shell.Shell.main(Shell.java:98)
                //        at JVMCLRjgpshell.Program.main(Program.java:30)
                // }

                Console.WriteLine(new { ex.Message, ex.StackTrace });

            }


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
