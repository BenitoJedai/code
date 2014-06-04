using TestBindingSource;
using TestBindingSource.Design;
using TestBindingSource.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TestBindingSource
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        //rem start /MIN /WAIT cmd /C c:\util\jsc\bin\jsc.meta.exe RewriteToJavaScriptDocument /assembly:"$(TargetFileName)" /AttachDebugger:false  /DisableWebServiceJava:true /DisableWebServicePHP:true /DisableWebServiceAndroid:true /DisableWebServiceChrome:true /FilterTo:"$(SolutionDir)"

        // [0x00000002] = { Name = "GetElementType", ReturnType = {System.Type}, GetParameterTypes = {System.Type[0x00000000]} }
        // VirtualMethod_ = { TargetMethod = {ScriptCoreLib.Shared.Data.Diagnostics.IQueryDescriptor GetDescriptor()}, InterfaceMethod = {ScriptCoreLib.Shared.Data.Diagnostics.IQueryDescriptor GetDescriptor()} }
        // DeclaringType = {Name = "DateToCount" FullName = "TestBindingSource.Data.Visualizationz+DateToCount"}
        // VirtualMethod_ = { TargetMethod = {TestBindingSource.Data.VisualizationzDateToCountKey Insert(TestBindingSource.Data.VisualizationzDateToCountRow)}, InterfaceMethod = {TestBindingSource.Data.VisualizationzDateToCountKey Insert(TestBindingSource.Data.VisualizationzDateToCoun...


        //will skip DefineVersionInfoResource
        //1888:01:01 RewriteToAssembly error: System.NotSupportedException: Type 'TestBindingSource.Data.VisualizationzDateToCountRow' was not completed.
        //   at System.Reflection.Emit.ModuleBuilder.PreSave(String fileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
        //   at System.Reflection.Emit.AssemblyBuilder.SaveNoLock(String assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
        //   at System.Reflection.Emit.AssemblyBuilder.Save(String assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)

        // Additional information: Type 'ScriptCoreLib.Shared.Data.Diagnostics.IQueryDescriptor' was not completed.
        // Additional information: Type 'TestBindingSource.Data.VisualizationzDateToCountStrategy' was not completed.
        // Additional information: Type 'ScriptCoreLib.Shared.Data.Diagnostics.IQueryDescriptor' was not completed.

        // DeclaringType = {Name = "VisualizationzDateToCountRow" FullName = "TestBindingSource.Data.VisualizationzDateToCountRow"}
        // SourceMethod = {TestBindingSource.Data.VisualizationzDateToCountRow op_Implicit(System.Data.DataRow)}
        // i = {[0x00ae] call       +1 -1{[0x00a9] call       +1 -1{[0x00a4] call       +1 -2{[0x009e] ldarg.0    +1 -0} {[0x009f] ldstr      [Timestamp]+1 -0} } } }
        // SourceType = {Name = "StringConversionsForStopwatch" FullName = "ScriptCoreLib.Library.StringConversionsForStopwatch"}
        // script: error JSC1000: No implementation found for this native method, please implement [static System.Diagnostics.StopwatchExtensions.CreateStopwatchAtElapsed(System.TimeSpan)]

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            this.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
