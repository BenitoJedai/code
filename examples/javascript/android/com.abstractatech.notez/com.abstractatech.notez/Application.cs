using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.notez.Design;
using com.abstractatech.notez.HTML.Pages;
using ScriptCoreLib.JavaScript.Controls;

namespace com.abstractatech.notez
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {












            var hh = new HorizontalSplit
            {
                Minimum = 0.05,
                Maximum = 0.95,
                Value = 0.2,
            };

            hh.Container.AttachToDocument();


            hh.Split.RightScrollable = new IHTMLDiv();

            hh.Split.RightScrollable.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            hh.Split.RightScrollable.style.width = "100%";
            hh.Split.RightScrollable.style.height = "100%";



            var text = new TextEditor(hh.Split.RightScrollable);

            text.ContainerForBorders.style.border = "";

            text.Control.style.position = IStyle.PositionEnum.absolute;
            text.Control.style.left = "0px";
            text.Control.style.top = "0px";
            text.Control.style.right = "0px";
            text.Control.style.bottom = "0px";



            #region DesignerContainer
            text.DesignerContainer.style.position = IStyle.PositionEnum.absolute;
            text.DesignerContainer.style.left = "0px";
            text.DesignerContainer.style.top = "2em";
            text.DesignerContainer.style.right = "0px";
            text.DesignerContainer.style.bottom = "2em";
            text.DesignerContainer.style.height = "";

            text.Frame.style.position = IStyle.PositionEnum.absolute;
            text.Frame.style.left = "0px";
            text.Frame.style.top = "0px";
            //text.Frame.style.right = "0px";
            //text.Frame.style.bottom = "0px";
            text.Frame.style.width = "100%";
            text.Frame.style.height = "100%";
            #endregion


            #region SourceContainer
            text.SourceContainer.style.position = IStyle.PositionEnum.absolute;
            text.SourceContainer.style.left = "0px";
            text.SourceContainer.style.top = "2em";
            text.SourceContainer.style.right = "0px";
            text.SourceContainer.style.bottom = "2em";
            text.SourceContainer.style.height = "";

            text.TextArea.style.position = IStyle.PositionEnum.absolute;
            text.TextArea.style.left = "0px";
            text.TextArea.style.top = "0px";
            //text.Frame.style.right = "0px";
            //text.Frame.style.bottom = "0px";
            text.TextArea.style.width = "100%";
            text.TextArea.style.height = "100%";
            #endregion

            text.BottomToolbarContainer.style.position = IStyle.PositionEnum.absolute;
            text.BottomToolbarContainer.style.left = "0px";
            text.BottomToolbarContainer.style.right = "0px";
            text.BottomToolbarContainer.style.bottom = "0px";


            var now = DateTime.Now;


            var yyyy = now.Year;
            var mm = now.Month;
            var dd = now.Day;


            var yyyymmdd = yyyy
                + mm.ToString().PadLeft(2, '0')
                + dd.ToString().PadLeft(2, '0');


            text.InnerHTML = @"

<div><font face='Verdana' size='5' color='#0000fc'>" + yyyymmdd + @" This is a header</font></div><div><br /></div><blockquote style='margin: 0 0 0 40px; border: none; padding: 0px;'></blockquote><font face='Verdana'>This is your content.</font>

            ";

        }

    }
}
