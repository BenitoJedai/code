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
using ChromeEarth;
using ChromeEarth.Design;
using ChromeEarth.HTML.Pages;

namespace ChromeEarth
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
            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                chrome.Notification.DefaultTitle = "Earth";
                //chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );

                return;
            }
            #endregion

            //{ trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = Void InvokeAsync(System.String, System.Func`2[System.String,System.Threading.Tasks.Task]), DeclaringType = ChromeTCPServer.TheServer, Location =
            // assembly: X:\jsc.svn\examples\javascript\chrome\apps\ChromeEarth\ChromeEarth\bin\Debug\Chrome Web Server Styled Form.dll
            // type: ChromeTCPServer.TheServerWithStyledForm, Chrome Web Server Styled Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x00bb
            //  method:Void Invoke(System.String, Int32, Int32, System.Action`1[ScriptCoreLib.JavaScript.Extensions.FormStyler]), ex = System.NullReferenceException: Object reference not set to an instance of an object.
            //   at jsc.ILInstruction.GetExpectedType(Int32 )
            //   at jsc.ILInstruction.GetExpectedType()
            //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass11b.<WriteSwitchRewrite>b__b4(ILRewriteContext e)
            //   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.?? .    (ILRewriteContext )
            //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass130.<>c__DisplayClass140.<WriteSwitchRewrite>b__e6(ILGenerator flow_il)


            new WebGLEarthByBjorn.Application(null);

        }

    }
}
