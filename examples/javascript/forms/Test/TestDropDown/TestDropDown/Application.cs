using TestDropDown;
using TestDropDown.Design;
using TestDropDown.HTML.Pages;
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

namespace TestDropDown
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();

            var x = new IHTMLDiv();

            x.style.backgroundColor = "rgba(255,255,255,1.0)";
            x.style.position = IStyle.PositionEnum.absolute;
            x.style.height = "auto";
            x.style.width = "100%";
            x.style.top = "100%";
            x.style.zIndex = 999;


            x.AttachTo(content.textBox1.GetHTMLTarget());

            x.Hide();
            Action close = delegate
            {
                x.Hide();
            };

            Action<string> add = name =>
                new Option { name = name }.With(
                    o =>
                    {

                        o.AttachTo(x);

                        o.div.css.hover.style.backgroundColor = "green";

                        o.div.onclick +=
                            delegate
                            {
                                Console.WriteLine(o.name.innerText);
                                content.textBox1.Text = o.name.innerText;
                                close();
                                //content.textBox1.Text = o.name;
                            };
                    }
                );


            //content.checkBox1.CheckedChanged += delegate { x.Show(content.checkBox1.Checked); };
            content.textBox1.GotFocus += delegate
            {
                x.Show();
            };


            add("foo");
            add("Marge Simpson");

            new TheOtherOption { }.With(
                o =>
                {
                    o.BackColor = Color.Transparent;

                    o.GetHTMLTarget().With(
                        div =>
                        {
                            div.AttachTo(x);


                            div.style.position = IStyle.PositionEnum.relative;
                        }
                    );

                }
            );

            add("bar");

            var ls = new Option { name = "Lisa Simpson EUR" }.With(o =>
            {
                o.div.css.hover.style.backgroundColor = "rgba(118, 207, 140, 0.7)";

                o.div.onclick +=
                    delegate
                    {
                        content.textBox1.Text = o.name.innerText;
                        close();
                    };
            });

            content.textBox2.AddDropDownOptions(
                new Option { name = "hello" },
                new TheOtherOption { },
                new Option { name = "world" },
                ls

            );

        }

    }
}

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class XOptionDropDownExtensions
    {
        public static IHTMLDiv AddDropDownOptions(this TextBox that, params object[] e)
        {
            var x = new IHTMLDiv();

            x.style.backgroundColor = "rgba(255,255,255,1.0)";
            x.style.position = IStyle.PositionEnum.absolute;
            x.style.height = "auto";
            x.style.width = "100%";
            x.style.top = "100%";
            x.style.zIndex = 999;

            var c = that.GetHTMLTarget();

            x.AttachTo(c);

            x.Hide();

            that.GotFocus += delegate
            {
                x.Show();
            };

            that.Leave += delegate
            {
                x.Hide();
            };

            e.AsEnumerable().WithEach(
                k =>
                {
                    {
                        //var o = k as UserControl;
                        var o = k as Control;
                        if (o != null)
                        {
                            o.BackColor = Color.Transparent;

                            o.GetHTMLTarget().With(
                                div =>
                                {
                                    div.AttachTo(x);

                                    div.style.width = "auto";
                                    div.style.position = IStyle.PositionEnum.relative;
                                }
                            );

                            return;
                        }
                    }

                    {
                        var o = (INodeConvertible<IHTMLDiv>)k;
                        {
                            o.AttachTo(x);
                         
                            //o.div.css.hover.style.backgroundColor = "yellow";
                        }
                    }
                }
            );

            return x;
        }
    }
}