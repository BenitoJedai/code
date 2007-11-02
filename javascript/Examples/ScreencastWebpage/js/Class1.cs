using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
//using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using global::System.Collections.Generic;
using System;
//using ScriptCoreLib.JavaScript.Runtime;

namespace ScreencastWebpage.js
{
    static class FooClass
    {
        delegate void FooDelegate();

        static event FooDelegate Bar;


        //private int myVar;

        //public int MyProperty
        //{
        //    get { return myVar; }
        //    set { myVar = value; }
        //}
	

 

 

 

 

        static event FooDelegate BarSpecial
        {
            add
            {
                Bar += value;
            }
            remove
            {
                Bar -= value;
            }
        }

        static void Foo()
        {
            FooClass.Bar += LoadWebPage_Handler;

            int bar = 0;

            LoadWebPage("http://example.com", () => bar++);
        }

        static void LoadWebPage(string url, Action done)
        {
            LoadWebPage("http://example.com", new Action(LoadWebPage_Handler));
            LoadWebPage("http://example.com", LoadWebPage_Handler);
            LoadWebPage("http://example.com",
                delegate() { Console.WriteLine("done!"); }
                );
            LoadWebPage("http://example.com", () => Console.WriteLine("done!"));
        }

        static void LoadWebPage_Handler()
        {
            Console.WriteLine("done!");
        }


    }


    [Script]
    public static class ScreencastWebpage
    {
        [Script]
        public class DataItem
        {
            public string text;
            public string iframe;
            public string swf;
            public int width;
            public int height;
        }

        [Script(ExternalTarget = "ScreencastWebpage_Data")]
        public static DataItem[] Data;


        #region Loader
        [Script]
        public class LoaderControl
        {
            public IHTMLElement Control;

            public LoaderControl(IHTMLElement e)
            {
                Control = e;
            }

            public string Text
            {
                get
                {
                    return Control.firstChild.nodeValue;
                }
                set
                {
                    Control.firstChild.nodeValue = value;
                }
            }

            public void Show(string e)
            {
                Text = e;
                Control.Show();


            }

            public void Hide(string e)
            {
                Text = e;
                ScriptCoreLib.JavaScript.Runtime.Fader.FadeOut(Control, 1000, 300);
            }
        }

        public static LoaderControl Loader
        {
            get
            {
                return new LoaderControl(Native.Document.getElementById("ScreencastWebpage_Loader"));
            }
        }


        #endregion

        public static IHTMLDiv Dock
        {
            get
            {
                return (IHTMLDiv)(Native.Document.getElementById("ScreencastWebpage_Dock"));
            }
        }

        static IHTMLDiv LoadFlash(string url, int width, int height)
        {
            var t = @"
<OBJECT classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0' WIDTH='" + width + "' HEIGHT='" + height + @"' >
 <PARAM NAME=movie VALUE='" + url + @"'> <PARAM NAME=quality VALUE=high> <PARAM NAME=bgcolor VALUE='#FFFFFF'> 
 <EMBED src='" + url + @"' quality=high bgcolor='#FFFFFF'   WIDTH='" + width + "' HEIGHT='" + height + @"' TYPE='application/x-shockwave-flash' PLUGINSPAGE='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash'>
  </EMBED>
 </OBJECT>";

            return new IHTMLDiv(t);
        }



        static void LoadWebPage(string url, Action<IHTMLIFrame> done)
        {
            var i = new IHTMLIFrame();

            i.style.display = IStyle.DisplayEnum.none;

            i.onload +=
                delegate
                {
                    done(i);
                };

            i.src = url;

            i.attachToDocument();
        }









        static ScreencastWebpage()
        {



            Native.Window.onload +=
                delegate
                {
                    Action Ready = () => Loader.Hide("Ready!");

                    Ready();

                    Action<int, IHTMLDiv, IHTMLButton> Play =
                        delegate(int index, IHTMLDiv t, IHTMLButton btn)
                        {
                            var x = Data[index];

                            btn.disabled = true;
                            Loader.Show("Loading... " + index);


                            LoadWebPage(x.iframe,
                                delegate(IHTMLIFrame i)
                                {
                                    i.Dispose();

                                    Ready();

                                    t.removeChildren();
                                    t.Show();

                                    var clear = new IHTMLButton("Close");

                                    clear.onclick +=
                                        delegate
                                        {
                                            btn.disabled = false;

                                            t.removeChildren();
                                            t.Hide();
                                        };


                                    t.appendChild(clear);

                                    t.appendChild(LoadFlash(x.swf, x.width, x.height));
                                }
                            );
                        };


                    var fieldset = new IHTMLElement(IHTMLElement.HTMLElementEnum.div);
                    var legend = new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, "Choose a screencast to watch");


                    var ol = new IHTMLElement(IHTMLElement.HTMLElementEnum.ol);

                    fieldset.appendChild(legend, ol);

                    var autoload = default(Action);

                    for (int i = 0; i < Data.Length; i++)
                    {
                        var x_index = i;
                        var x = Data[x_index];

                        var area = new IHTMLDiv();

                        area.Hide();


                        var view = new IHTMLButton("View");

                        var anchor = new IHTMLAnchor("");

                        anchor.name = "" + x_index;

                        Action PlayThis =
                            delegate
                            {
                                ScriptCoreLib.JavaScript.Runtime.Timer.DoAsync(
                                    delegate
                                    {
                                        Native.Document.location.hash = "" + x_index;
                                        //Native.Window.scrollTo(anchor.offsetTop, anchor.offsetLeft);

                                        Play(x_index, area, view);
                                    }
                                );
                            };

                        view.onclick +=
                            delegate
                            {
                                PlayThis();
                            };


                        var download = new IHTMLButton("Download");

                        download.onclick +=
                            delegate
                            {
                                Native.Document.location.href = x.iframe;
                            };

                        Func<IHTMLBreak> clear =
                            delegate
                            {
                                var br = new IHTMLBreak();

                                br.style.clear = "both";

                                return br;
                            };


                        ol.appendChild(
                            new IHTMLElement(IHTMLElement.HTMLElementEnum.li,
                                anchor,
                                new IHTMLLabel(x.text, view),
                                new IHTMLBreak(),
                                view,
                                download,
                                clear(),
                                area,
                                clear()
                            )
                        );

                        if ("#" + x_index == Native.Document.location.hash)
                            autoload = PlayThis;
                    }

                    Dock.appendChild(fieldset);

                    if (autoload != null)
                        autoload();
                };
        }
    }

}
