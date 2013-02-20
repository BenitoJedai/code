using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Documentation.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Documentation
{

    [Description("ScriptCoreLib.Ultra.Documentation. Write javascript, flash and java applets within a C# project.")]
    public sealed partial class Application
    {
        public Application(IApp a)
        {
            Native.Document.title = "ScriptCoreLib.Documentation";

            //IStyleSheet.Default["html"].style.With(
            //    style =>
            //    {
            //        //style.position = IStyle.PositionEnum.absolute;
            //        //style.left = "0px";
            //        //style.top = "0px";
            //        style.width = "100%";
            //        style.height = "100%";
            //    }
            //);

            //IStyleSheet.Default["body"].style.With(
            //    style =>
            //    {
            //        //style.position = IStyle.PositionEnum.absolute;
            //        //style.left = "0px";
            //        //style.top = "0px";
            //        style.width = "100%";
            //        style.height = "100%";
            //    }
            //);

            // android float


            //a.LoadingAnimation.FadeOut();


            //var xc = new IHTMLDiv();

            //xc.style.With(
            //     style =>
            //     {
            //         style.backgroundColor = "red";
            //         //style.position = IStyle.PositionEnum.absolute;

            //         //style.left = "0px";
            //         //style.top = "0px";

            //         //style.right = "0px";
            //         //style.bottom = "0px";


            //         style.width = "100%";
            //         style.height = "100%";
            //     }
            // );

            //xc.AttachToDocument();

            new DocumentationCompilationViewer();
        }

    }


}
