using CSSTransform;
using CSSTransform.Design;
using CSSTransform.HTML.Pages;
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
using ScriptCoreLib.Shared.Lambda;

namespace CSSTransform
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

            Native.document.body.onclick +=
                e =>
                {
                    var element = (IHTMLElement)e.Element;


                    e.stopPropagation();

                    //onclick { element = [object HTMLHeadingElement], clientWidth = 1584, clientHeight = 37, CursorX = 695, CursorY = 51, OffsetX = 695, OffsetY = 51, scrollLeft = 0, scrollTop = 0, offsetLeft = 8, offsetWidth = 1584 }


                    Console.WriteLine(
                        "onclick " + new
                        {
                            element,
                            element.clientWidth,
                            element.clientHeight,

                            e.CursorX,
                            e.CursorY,

                            e.OffsetX,
                            e.OffsetY,

                            element.scrollLeft,
                            element.scrollTop,


                            element.offsetLeft,
                            element.offsetWidth
                        }
                    );
                };

            Native.document.body.onmouseover +=
                async e =>
                {
                    e.stopPropagation();

                    var element = (IHTMLElement)e.Element;

                    // string or eXpression to attribute?
                    element.css.after.content = "_";
                    element.css.after.style.color = "red";

                    // either or
                    var onclick = element.async.onclick;

                    // either or
                    //await element.async.onmouseout | onclick;

                    // script: error JSC1000: method was found, but too late: [WhenAny]
                    await Task.WhenAny(element.async.onmouseout, onclick);

                    //if (onclick)
                    if (onclick.IsCompleted)
                    {

                        element.css.after.style.color = "blue";
                        await Task.Delay(200);
                        element.css.after.style.color = "red";
                        await Task.Delay(200);

                        var f = new Form();
                        var c = new ApplicationControl { Dock = DockStyle.Fill };

                        c.AttachTo(f);


                        c.textBox1.TextChanged += delegate { element.css.before.content = c.textBox1.Text; };
                        c.textBox2.TextChanged += delegate { element.css.before.style.color = c.textBox2.Text; };
                        c.trackBar1.ValueChanged += delegate { element.css.style.transform = "scale(" + (c.trackBar1.Value * 0.02) + ")"; };


                        // can we have a html or svg line here connecting the form and element
                        // like visual studio debugger?
                        f.Show();
                    }

                    element.css.after.content = "";
                };




            //new IHTMLAnchor { "drag me" }.AttachTo(Native.document.documentElement).With(
            //    dragme =>
            //    {
            //        dragme.style.position = IStyle.PositionEnum.@fixed;
            //        dragme.style.left = "1em";
            //        dragme.style.bottom = "1em";

            //        dragme.style.zIndex = 1000;

            //        dragme.AllowToDragAsApplicationPackage();
            //    }
            //);
        }

    }
}
