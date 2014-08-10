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
using TestAsyncAssignArrayToEnumerable;
using TestAsyncAssignArrayToEnumerable.Design;
using TestAsyncAssignArrayToEnumerable.HTML.Pages;

namespace TestAsyncAssignArrayToEnumerable
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
            // X:\jsc.svn\examples\javascript\Test\TestAssignArrayToEnumerable\TestAssignArrayToEnumerable\Application.cs

            Invoke();
        }

        //     Unhandled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Reflection.TargetException: Non-static field requires a target.
        //        at System.Reflection.RtFieldInfo.CheckConsistency(Object target)
        //at System.Reflection.RtFieldInfo.InternalGetValue(Object obj, StackCrawlMark& stackMark)
        //at System.Reflection.RtFieldInfo.GetValue(Object obj)
        //at jsx.reflector.ReflectorWindow.<InitializeAddField>b__6d3(FieldInfo SourceField, TextBlock Header, Boolean qualify) in x:\jsc.internal.git\compiler\jsx.reflector\ReflectorWindow.AddField.cs:line 344
        //at jsx.reflector.ReflectorWindow.<InitializeAddField>b__6e1(FieldInfo SourceField, ItemCollection nodes, Boolean qualify) in x:\jsc.internal.git\compiler\jsx.reflector\ReflectorWindow.AddField.cs:line 377
        //at jsx.reflector.ReflectorWindow.<InitializeAddField>b__6d2(FieldInfo SourceField, ItemCollection nodes) in x:\jsc.internal.git\compiler\jsx.reflector\ReflectorWindow.AddField.cs:line 44
        //at jsx.reflector.ReflectorWindow.<>c__DisplayClass9ee.<>c__DisplayClassa0a.<>c__DisplayClassa24.<>c__DisplayClassa3f.<InitializeAddType>b__98b(FieldInfo SourceField) in x:\jsc.internal.git\compiler\jsx.reflector\ReflectorWindow.AddType.cs:line 1578

        async void Invoke()
        {
            IEnumerable<object> collection = new[] { new object() };

            await Task.Yield();

            //  d = c.bwQABoBf2jWIHILvaqtMig();
            foreach (var item in collection)
            {
                Console.WriteLine(new { item });
            }
        }
    }
}
