using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestMD5Experiment;
using TestMD5Experiment.Design;
using TestMD5Experiment.HTML.Pages;

namespace TestMD5Experiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //script: error JSC1000: No implementation found for this native method, please implement [System.UInt32.ToString(System.String)]
            //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
            //script: error JSC1000: error at MD5.Digest.ToString,
            // assembly: T:\TestMD5Experiment.Application.exe
            // type: MD5.Digest, TestMD5Experiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0014
            //  method:System.String ToString()

            //script: error JSC1000: opcode unsupported - [0x0002] ldind.u4   +1 -1{[0x0001] ldarg.1    +1 -0}

            //script: error JSC1000: error at MD5.MD5.PerformTransformation,
            // assembly: U:\TestMD5Experiment.Application.exe
            // type: MD5.MD5, TestMD5Experiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0002
            //  method:Void PerformTransformation(UInt32 ByRef, UInt32 ByRef, UInt32 ByRef, UInt32 ByRef)

            //02000015 MD5.MD5
            //{ Location =
            // assembly: U:\TestMD5Experiment.Application.exe
            // type: MD5.MD5, TestMD5Experiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x003e
            //  method:MD5.Digest CalculateMD5Value() }
            //script: error JSC1000: Method: CalculateMD5Value, Type: MD5.MD5; emmiting failed : System.NotImplementedException: { ParameterType = System.UInt32&, p = [0x003e] call

            //arg[0] is typeof System.Char[]
            //no matching prototype
            //script: error JSC1000: error at MD5.MD5.get_Value,
            // assembly: U:\TestMD5Experiment.Application.exe
            // type: MD5.MD5, TestMD5Experiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0034
            //  method:System.String get_Value()

            var a = new MD5.MD5();

            //a.FingerPrint
            a.Value = "";

            // a.FingerPrint = "D41D8CD98F00B204E9800998ECF8427E"
            // { FingerPrint = FFFFFFA3FFFFFFF8FFFFFFA1FFFFFFBD }
            new IHTMLPre { new { a.FingerPrint } }.AttachToDocument();

        }

    }
}
