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
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Dynamic;
using System.Collections.Generic;
using com.abstractatech.wiki;
using System.Windows.Forms;

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
                // ignore
                if (string.IsNullOrWhiteSpace(key))
                    return;

                if (InternalStorage.ContainsKey(key))
                    if (InternalStorage[key] == value)
                        return;

                InternalStorage[key] = value;

                if (AtSetItem != null)
                    AtSetItem(key, value);
            }
            get
            {
                if (InternalStorage.ContainsKey(key))
                    return InternalStorage[key];

                return "";
            }
        }
    }


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebServiceWithReplay service = new ApplicationWebServiceWithReplay();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // localStorage not available on android webview!
            //E/Web Console( 3751): Uncaught TypeError: Cannot set property '20130329 Hello world' of null at http://192.168.1.107:25459/view-source:32300

            Console.WriteLine("serial 57770");

            "My Notez (loading...)".ToDocumentTitle();


            service.AtPendingActions +=
                count =>
                {
                    if (service.ServicePending.ElapsedMilliseconds > 500)
                    {
                        if (service.ServicePending.ElapsedMilliseconds > 4000)
                        {
                            "My Notez (offline)".ToDocumentTitle();
                            return;
                        }

                        "My Notez (pending)".ToDocumentTitle();
                        return;
                    }

                    "My Notez".ToDocumentTitle();
                };

            Native.Window.onbeforeunload +=
                e =>
                {
                    if (service.ServicePending.IsRunning)
                        e.Text = "The changes made here have not yet made it to the server.";
                };

            var storage = new MyLocalStorage
            {

                AtRemove =
                    x => service.remove_LocalStorage(x),

                AtSetItem =
                    (key, value) =>
                    {
                        service.set_LocalStorage(key, value);
                    }
            };

            Console.WriteLine("Do we have localStorage? [2]");

            Native.Window.localStorage.With(
                localStorage =>
                {
                    Console.WriteLine("This browser has localStorage. Lets sync with that. [2]");

                    for (uint i = 0; i < localStorage.length; i++)
                    {
                        var key = localStorage.key(i);
                        var value = localStorage[key];

                        storage[key] = value;
                    }

                    // jsc why aint ths working?
                    //storage.AtRemove += localStorage.removeItem;
                    storage.AtRemove += key => localStorage.removeItem(key);
                    storage.AtSetItem += (key, value) => { localStorage[key] = value; };

                }
            );

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

                    var ff = new Form
                    {
                        //StartPosition = FormStartPosition.Manual,
                        SizeGripStyle = SizeGripStyle.Hide

                    };


                    hh.Split.RightScrollable = new IHTMLDiv();

                    hh.Split.RightScrollable.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
                    hh.Split.RightScrollable.style.width = "100%";
                    hh.Split.RightScrollable.style.height = "100%";

                    ff.Show();


                    ff.PopupInsteadOfClosing(SpecialNoMovement: true
                        //, HandleFormClosing: false
                        //, NotifyDocked: AtResize
                        );

                    //var text = new TextEditor(hh.Split.RightScrollable);
                    var text = new TextEditor(ff.GetHTMLTargetContainer());

                    text.ContainerForBorders.style.border = "";

                    text.Control.style.position = IStyle.PositionEnum.absolute;
                    text.Control.style.left = "0px";
                    text.Control.style.top = "0px";
                    text.Control.style.right = "0px";
                    text.Control.style.bottom = "0px";


                    Native.Window.onresize +=
                        delegate
                        {
                            var TopToolbarHeight = text.TopToolbar.clientHeight;

                            //Console.WriteLine(new { TopToolbarHeight });

                            text.DesignerContainer.style.top = (TopToolbarHeight + 4) + "px";
                            text.SourceContainer.style.top = (TopToolbarHeight + 4) + "px";

                        };


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

                    #region DoCreateNew
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

                        string header = yyyymmdd + @" New Header " + storage.Keys.Count();


                        text.InnerHTML = @"
<div><font face='Verdana' size='5' color='#0000fc'>" + header + @"</font></div><div><br /></div><blockquote style='margin: 0 0 0 40px; border: none; padding: 0px;'></blockquote><font face='Verdana'>This is your content.</font>
            ";
                        #endregion



                        DoRefresh();
                    };

                    CreateNew.onclick +=
                        delegate
                        {
                            DoCreateNew();
                        };
                    #endregion




                    var buttons = new List<IHTMLButton>();

                    Action EitherCreateNewOrSelectFirst = delegate
                    {
                        if (buttons.Count == 0)
                        {
                            DoCreateNew();
                        }
                        else
                        {
                            if (buttons.Any(k => k.innerText == oldtitle))
                            {
                                //already selected
                            }
                            else
                            {
                                oldtitle = buttons.First().innerText;

                                text.InnerHTML = storage[oldtitle];
                            }
                        }
                    };


                    var remove = text.AddButton(null, "Remove this document",
                          delegate
                          {
                              var button = buttons.FirstOrDefault(k => k.innerText == oldtitle);

                              if (button == null)
                                  return;

                              //Native.Window.localStorage.removeItem(button.innerText);
                              storage.Remove(button.innerText);


                              button.Orphanize();
                              buttons.Remove(button);

                              EitherCreateNewOrSelectFirst();
                          }
                      );

                    IHTMLElement remove_element = remove;
                    remove_element.style.Float = IStyle.FloatEnum.right;

                    text.BottomToolbar.appendChild(remove_element);

                    #region new_SidebarButton
                    Func<IHTMLButton> new_SidebarButton =
                        delegate
                        {
                            var button = new IHTMLButton { className = "SidebarButton" }.AttachTo(
                                           hh.Split.LeftScrollable
                                        );

                            button.onclick +=
                                delegate
                                {
                                    oldtitle = "";
                                    text.InnerHTML = storage[button.innerText];
                                    DoRefresh();

                                };

                            button.oncontextmenu +=
                               e =>
                               {
                                   e.preventDefault();

                                   storage.Remove(button.innerText);


                                   button.Orphanize();
                                   buttons.Remove(button);

                                   EitherCreateNewOrSelectFirst();
                               };

                            buttons.Add(button);

                            return button;
                        };
                    #endregion

                    #region DoRefresh
                    DoRefresh = delegate
                   {
                       // what has changed
                       // text not default anymore?
                       // title change?


                       // document unloaded?
                       if (text.Document == null)
                           return;

                       var xml = text.Document.body.AsXElement();

                       // script: error JSC1000: No implementation found for this native method, please implement [static System.String.IsNullOrWhiteSpace(System.String)]

                       xml.Elements().FirstOrDefault(k => !string.IsNullOrWhiteSpace(k.Value)).With(
                           TitleElement =>
                           {
                               // take no action for no title
                               if (string.IsNullOrWhiteSpace(TitleElement.Value))
                                   return;

                               // is there a buttn with old title?


                               var button = buttons.FirstOrDefault(
                                   k =>
                                   {
                                       if (oldtitle == "")
                                       {
                                           return k.innerText == TitleElement.Value;
                                       }

                                       return k.innerText == oldtitle;
                                   }
                               );

                               if (button == null)
                               {
                                   button = new_SidebarButton();
                               }

                               button.innerText = TitleElement.Value;

                               buttons.WithEach(
                                   x => x.setAttribute("data-active", x == button)
                               );


                               if (oldtitle != "")
                               {
                                   if (oldtitle != TitleElement.Value)
                                       storage.Remove(oldtitle);


                               }


                               storage[TitleElement.Value] = text.InnerHTML;
                               oldtitle = TitleElement.Value;
                               //Console.WriteLine("TitleElement: " + TitleElement.Value);
                           }
                       );

                       // whats the title?
                   };
                    #endregion



                    foreach (var button_text in storage.Keys)
                    {
                        new_SidebarButton().innerText = button_text;
                    }



                    new ScriptCoreLib.JavaScript.Runtime.Timer(
                        t =>
                        {
                            DoRefresh();



                        }
                    ).StartInterval(500);


                    EitherCreateNewOrSelectFirst();
                };
            #endregion

            var tt = default(ScriptCoreLib.JavaScript.Runtime.Timer);

            Action done_timeout = delegate
            {
                if (done == null)
                    return;

                tt.Stop();
                done();
                done = null;
            };


            service.get_LocalStorage(
                //add_localStorage: (key, value) => Native.Window.localStorage[key] = value,
                add_localStorage:
                    (key, value) =>
                    {
                        // what if we are resuming from offline edit.
                        // merge?


                        // keep the one we got from localStorage, because it has longer entry?
                        if (storage[key].Length > value.Length)
                            return;

                        storage[key] = value;
                    },

                done: done_timeout
            );


            // either server responds in 2000 or we consider us offline...
            tt = new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    done_timeout();
                }
            );

            tt.StartTimeout(3000);

        }

    }






}
