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
using ScriptCoreLib.JavaScript.Runtime;
using System.Dynamic;
using System.Collections.Generic;

namespace com.abstractatech.notez
{

    public class MyLocalStorage
    {
        public Dictionary<string, string> InternalStorage = new Dictionary<string, string>();

        public IEnumerable<string> Keys
        {
            get
            {
                return InternalStorage.Keys.OrderBy(k => k);
            }
        }

        public Action<string, string> AtSetItem;
        public Action<string> AtRemove;

        public void Remove(string key)
        {
            InternalStorage.Remove(key);

            if (AtRemove != null)
                AtRemove(key);
        }

        public string this[string key]
        {
            set
            {
                if (InternalStorage.ContainsKey(key))
                    if (InternalStorage[key] == value)
                        return;

                InternalStorage[key] = value;

                if (AtSetItem != null)
                    AtSetItem(key, value);
            }
            get
            {
                return InternalStorage[key];
            }
        }
    }


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
            // localStorage not available on android webview!
            //E/Web Console( 3751): Uncaught TypeError: Cannot set property '20130329 Hello world' of null at http://192.168.1.107:25459/view-source:32300


            var localStorage = new MyLocalStorage
            {

                AtRemove = service.remove_LocalStorage,
                AtSetItem = service.set_LocalStorage
            };

            #region done
            Action done = delegate
                {



                    var hh = new HorizontalSplit
                    {
                        Minimum = 0.05,
                        Maximum = 0.95,
                        Value = 0.2,
                    };

                    hh.Container.AttachToDocument();



                    hh.Split.LeftScrollable = new IHTMLDiv { className = "SidebarForButtons" };


                    var CreateNew = new IHTMLButton { innerText = "+ create new", className = "SidebarButton" }.AttachTo(
                  hh.Split.LeftScrollable
                 );



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
                    text.DesignerContainer.style.top = "3em";
                    text.DesignerContainer.style.right = "0px";
                    text.DesignerContainer.style.bottom = "3em";
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
                    text.SourceContainer.style.top = "3em";
                    text.SourceContainer.style.right = "0px";
                    text.SourceContainer.style.bottom = "3em";
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

                    var oldtitle = "";

                    Action DoRefresh = delegate { };

                    Action DoCreateNew = delegate
                    {
                        oldtitle = "";

                        #region default text
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
                        #endregion


                        DoRefresh();
                    };

                    CreateNew.onclick +=
                        delegate
                        {
                            DoCreateNew();
                        };




                    var buttons = new List<IHTMLButton>();

                    Action DoShowSomething = delegate
                    {
                        if (buttons.Count == 0)
                        {
                            DoCreateNew();
                        }
                        else
                        {
                            oldtitle = buttons.First().innerText;

                            text.InnerHTML = localStorage[oldtitle];
                        }
                    };


                    var remove = text.AddButton(null, "Remove this document",
                          delegate
                          {
                              var button = buttons.FirstOrDefault(k => k.innerText == oldtitle);

                              if (button == null)
                                  return;

                              //Native.Window.localStorage.removeItem(button.innerText);
                              localStorage.Remove(button.innerText);


                              button.Orphanize();
                              buttons.Remove(button);

                              DoShowSomething();
                          }
                      );

                    IHTMLElement remove_element = remove;
                    remove_element.style.Float = IStyle.FloatEnum.right;

                    text.BottomToolbar.appendChild(remove_element);


                    #region DoRefresh
                    DoRefresh = delegate
                   {
                       // what has changed
                       // text not default anymore?
                       // title change?

                       var xml = text.Document.body.AsXElement();

                       xml.Elements().FirstOrDefault().With(
                           TitleElement =>
                           {

                               // is there a buttn with old title?

                               if (oldtitle != "")
                               {
                                   var button = buttons.FirstOrDefault(k => k.innerText == oldtitle);

                                   if (button == null)
                                   {
                                       button = new IHTMLButton { className = "SidebarButton" }.AttachTo(
                                              hh.Split.LeftScrollable
                                           );

                                       button.onclick +=
                                           delegate
                                           {
                                               oldtitle = "";


                                               text.InnerHTML =
                                                   localStorage[button.innerText];
                                               //Native.Window.localStorage[button.innerText];
                                               DoRefresh();

                                           };

                                       buttons.Add(button);
                                   }

                                   button.innerText = TitleElement.Value;

                                   if (oldtitle != TitleElement.Value)
                                       localStorage.Remove(oldtitle);

                                   //Native.Window.localStorage.removeItem(oldtitle);

                                   //Native.Window.localStorage[oldtitle] = null;
                               }


                               localStorage[TitleElement.Value] = text.InnerHTML;
                               //Native.Window.localStorage[TitleElement.Value] = text.InnerHTML;
                               oldtitle = TitleElement.Value;
                               //Console.WriteLine("TitleElement: " + TitleElement.Value);
                           }
                       );

                       // whats the title?
                   };
                    #endregion





                    //var localStorage_keys = new List<string>();

                    //for (uint i = 0; i < Native.Window.localStorage.length; i++)
                    //{
                    //    var button_text = Native.Window.localStorage.key(i);

                    //    localStorage_keys.Add(button_text);
                    //}

                    foreach (var button_text in localStorage.Keys)
                    {


                        var button = new IHTMLButton
                        {
                            className = "SidebarButton",
                            innerText = button_text
                        }.AttachTo(
                            hh.Split.LeftScrollable
                        );

                        button.onclick +=
                            delegate
                            {
                                oldtitle = "";


                                text.InnerHTML =
                                    //Native.Window.localStorage[button.innerText];
                                localStorage[button.innerText];

                                DoRefresh();

                            };

                        Console.WriteLine(new { button_text });

                        buttons.Add(button);
                    }



                    new ScriptCoreLib.JavaScript.Runtime.Timer(
                        t =>
                        {
                            DoRefresh();



                        }
                    ).StartInterval(1000);


                    DoShowSomething();
                };
            #endregion





            service.get_LocalStorage(
                //add_localStorage: (key, value) => Native.Window.localStorage[key] = value,
                add_localStorage: (key, value) => localStorage[key] = value,

                done: done
            );

        }

    }






}
