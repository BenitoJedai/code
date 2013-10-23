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
using System.Threading;
using System.Text;

namespace JVMCLRBroadcastLogger
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
            //'JVMCLRBroadcastLogger.exe' (CLR v4.0.30319: JVMCLRBroadcastLogger.exe): Loaded 'X:\jsc.svn\examples\java\JVMCLRBroadcastLogger\JVMCLRBroadcastLogger\bin\Release\JVMCLRBroadcastLogger.exports'. Module was built without symbols.
            //The program '[12004] JVMCLRBroadcastLogger.exe' has exited with code 0 (0x0).
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   

            Console.WriteLine("ApplicationWebService cctor");

            if (__AndroidMulticast.value == null)
                __AndroidMulticast.value = new __AndroidMulticast(
                    value =>
                    {
                        Console.WriteLine(
                            "ApplicationWebService cctor: " +

                            new { value }

                        );

                        //ApplicationWebService cctor: { value = <string reason="" c="1" preview="" n="com.abstractatech.gamification.gir">Visit me at 192.168.43.7:25814</string> }
                        //ApplicationWebService cctor: { value = <string reason="preview" c="2" preview="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGAAAABgCAIAAABt+uBvAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAUBklEQVR4nN2dz5Mkx1XHv+9lVlX/mJ0fuzuS1kZCksMmrPCBCAEHMBEcIYAjEQQHgiB84z/hxh3wCQIHdoAPcMIYcAAH47BDQsi2tLJWsqwf+2tmdrq7qr }
                        //ApplicationWebService cctor: { value = <string reason="shake" c="3" preview="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGAAAABgCAIAAABt+uBvAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAUBklEQVR4nN2dz5Mkx1XHv+9lVlX/mJ0fuzuS1kZCksMmrPCBCAEHMBEcIYAjEQQHgiB84z/hxh3wCQIHdoAPcMIYcAAH47BDQsi2tLJWsqwf+2tmdrq7qrIy }
                        //ApplicationWebService cctor: { value = <string reason="shake" c="4" preview="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGAAAABgCAIAAABt+uBvAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAUBklEQVR4nN2dz5Mkx1XHv+9lVlX/mJ0fuzuS1kZCksMmrPCBCAEHMBEcIYAjEQQHgiB84z/hxh3wCQIHdoAPcMIYcAAH47BDQsi2tLJWsqwf+2tmdrq7qrIy }

                        try
                        {
                            var xml = XElement.Parse(value);
                            // ApplicationWebService cctor: { value = <string reason="shake" c="8" preview="data:image/png;base64,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" n="com.abstractatech.gamification.gir">Visit me at 192.168.43.7:25814</string> }


                            Console.WriteLine(new { xml });
                        }
                        catch (Exception ex)
                        {
                            // client error
                            System.Console.WriteLine(
                                "ApplicationWebService cctor error " +
                                new { ex.Message, ex.StackTrace }
                            );
                        }
                    }
            );


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
