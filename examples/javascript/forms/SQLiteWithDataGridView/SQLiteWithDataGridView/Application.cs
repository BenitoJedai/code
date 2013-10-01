using SQLiteWithDataGridView.Design;
using SQLiteWithDataGridView.HTML.Pages;
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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.Runtime;

namespace SQLiteWithDataGridView
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        //15f8:02:01 004d:0129 SQLiteWithDataGridView.Application define interface CSSMinimizeFormToSidebar::CSSMinimizeFormToSidebar.HTML.Pages.IApp
        //{ Location =
        // assembly: Y:\SQLiteWithDataGridView.Application\CSSMinimizeFormToSidebar.dll
        // type: CSSMinimizeFormToSidebar.ApplicationExtension, CSSMinimizeFormToSidebar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x0043
        //  method:CSSMinimizeFormToSidebar.HTML.Pages.IApp InitializeSidebarBehaviour(System.Windows.Forms.Form, Boolean) }
        //{ trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = CSSMinimizeFormToSidebar.HTML.Pages.IApp InitializeSidebarBehaviour(System.Windows.Forms.Form, Boolean), DeclaringType = CSSMinimizeFormToSidebar.ApplicationExtension, Location =
        // assembly: X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\bin\x86\ASPNET\SQLiteWithDataGridView.exe
        // type: SQLiteWithDataGridView.Application+<>c__DisplayClass4, SQLiteWithDataGridView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x001c
        //  method:Void <.ctor>b__0(SQLiteWithDataGridView.Library.GridForm), ex = System.MissingMethodException: Method not found: 'Void ScriptCoreLib.JavaScript.Extensions.INodeExtensions.Clear(ScriptCoreLib.JavaScript.DOM.INode)'.
        //   at System.ModuleHandle.ResolveMethod(RuntimeModule module, Int32 methodToken, IntPtr* typeInstArgs, Int32 typeInstCount, IntPtr* methodInstArgs, Int32 methodInstCount)


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            content.label1.Text = Native.Document.location.href;

            // this is mesed up on Galaxy S, why?
            //global::DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();

            //FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);

            @"SQLite With DataGridView".ToDocumentTitle();


            content.con.Left = 0;
            content.con.Top = Native.window.Height - content.con.Height;
            content.con.Opacity = 0.7;
            //content.con.PopupInsteadOfClosing();


            var once = false;

            content.NewForm +=
                f =>
                {
                    if (once)
                    {

                        //f.PopupInsteadOfClosing(HandleFormClosing: false);
                    }
                    else
                    {

                        once = true;
                        //f.DisableFormClosingHandler = true;

                        global::CSSMinimizeFormToSidebar.ApplicationExtension.InitializeSidebarBehaviour(
                            f
                        );
                    }
                };

            var canvas = new AvalonPromotionBrandIntro.ApplicationCanvas();

            canvas.TriggerOnClick = false;
            canvas.Background = Brushes.Transparent;

            canvas.AnimationAllWhite +=
                delegate
                {
                    Native.Document.body.style.backgroundColor = JSColor.None;
                };

            canvas.AnimationCompleted +=
                delegate
                {
                    ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.ToHTMLElement(
                        canvas
                    ).Orphanize();

                };

            canvas.PrepareAnimation()();

            canvas.AttachToContainer(Native.Document.body);

            canvas.AutoSizeTo(Native.Document.body);
        }

    }
}
