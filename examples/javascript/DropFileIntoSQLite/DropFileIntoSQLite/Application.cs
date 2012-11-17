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
using DropFileIntoSQLite.Design;
using DropFileIntoSQLite.HTML.Pages;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Windows.Forms;
using System.Media;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using DropFileIntoSQLite.Library;

namespace DropFileIntoSQLite
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
            // http://html5doctor.com/drag-and-drop-to-server/

            Native.Document.body.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.PreventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.


                    page.Header.style.color = JSColor.Green;

                };


            Native.Document.body.ondragleave +=
                delegate
                {
                    page.Header.style.color = JSColor.None;
                };

            Native.Document.body.ondrop +=
                evt =>
                {
                    //if (evt.dataTransfer == null)
                    //    return;



                    page.Header.style.color = JSColor.None;

                    #region dataTransfer
                    evt.dataTransfer.types.WithEach(
                        x =>
                        {
                            Console.WriteLine(x);

                            SystemSounds.Beep.Play();
                            //Console.Beep();

                            if (x == "text/uri-list")
                            {
                                var src = evt.dataTransfer.getData(x);

                                if (src != "about:blank")
                                {
                                    Console.WriteLine(new { src });

                                    new Form { Text = src }.With(
                                        f =>
                                        {
                                            var w = new WebBrowser { Dock = DockStyle.Fill }.AttachTo(f);

                                            w.Navigate(src);

                                            f.Show();
                                        }
                                    );

                                }
                            }

                            if (x == "text/plain")
                            {
                                var DocumentText = evt.dataTransfer.getData(x);

                                Console.WriteLine(new { DocumentText });

                                if (DocumentText.StartsWith("javascript:"))
                                {
                                    var host = DocumentText.SkipUntilOrEmpty("href='").TakeUntilOrEmpty("'");

                                    new Form { Text = "Application " + host }.With(
                                        f =>
                                        {
                                            new IHTMLAnchor
                                            {
                                                href = host,
                                                innerText = "Go to " + host
                                            }.AttachTo(f.GetHTMLTargetContainer()).style.display = IStyle.DisplayEnum.block;

                                            new IHTMLAnchor
                                            {
                                                href = DocumentText,
                                                innerText = "Launch " + host
                                            }.AttachTo(f.GetHTMLTargetContainer()).style.display = IStyle.DisplayEnum.block;

                                            f.Show();
                                        }
                                    );
                                }
                            }

                            if (x == "text/html")
                            {
                                var DocumentText = evt.dataTransfer.getData(x);

                                //Console.WriteLine(new { DocumentText });



                                new Form { Text = x }.With(
                                    f =>
                                    {
                                        var w = new WebBrowser { Dock = DockStyle.Fill }.AttachTo(f);

                                        //((IHTMLIFrame)w.GetHTMLTarget()).contentWindow.onload +=
                                        //    delegate
                                        //    {
                                        //        ((IHTMLIFrame)w.GetHTMLTarget()).contentWindow.document.DesignMode = true;
                                        //    };

                                        w.DocumentText = DocumentText;



                                        f.Show();
                                    }
                                );
                            }
                        }
                    );
                    #endregion



                    #region files
                    evt.dataTransfer.files.AsEnumerable().WithEachIndex(
                        (f, index) =>
                        {
                            var ff = new Form();


                            ff.Text = new { f.type, f.name, f.size }.ToString();


                            ff.Show();

                            ff.MoveTo(
                                evt.CursorX + 32 * index,
                                evt.CursorY + 24 * index
                            );

                            var fc = ff.GetHTMLTargetContainer();

                            fc.title = ff.Text;

                            if (f.type.StartsWith("image/"))
                            {
                                f.ToDataURLAsync(
                                    src =>
                                    {
                                        var i = new IHTMLImage { src = src }.AttachTo(fc);

                                        i.InvokeOnComplete(
                                            delegate
                                            {
                                                ff.ClientSize = new System.Drawing.Size(i.width, i.height);
                                            }
                                        );
                                    }
                                );
                            }

                            // http://html5doctor.com/drag-and-drop-to-server/

#if FUTURE
                            service.XUpload(f, delegate { });
#endif


                            var d = new FormData();

                            d.append("foo", f, f.name);

                            var xhr = new IXMLHttpRequest();

                            xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/upload");

                            xhr.InvokeOnComplete(
                                delegate
                                {
                                    SystemSounds.Beep.Play();

                                    //Console.Beep();
                                    XElement.Parse(xhr.responseText).Elements("ContentKey").WithEach(
                                        ContentKey =>
                                        {

                                            ff.FormClosing +=
                                                delegate
                                                {
                                                    service.DeleteFileAsync(ContentKey.Value,
                                                        delegate
                                                        {

                                                        }
                                                    );
                                                };
                                        }
                                    );
                                }
                            );

                            xhr.send(d);
                        }
                    );
                    #endregion


                    // let's disable other handlers
                    //evt.dataTransfer = null;

                    evt.stopPropagation();
                    evt.stopImmediatePropagation();

                    evt.PreventDefault();
                };

            {
                var index = 0;
                service.EnumerateFilesAsync("",
                    (ContentKey, ContentBytesLength) =>
                    {
                        var ff = new Form();


                        ff.Text = new { ContentKey, ContentBytesLength }.ToString();


                        ff.Show();

                        ff.MoveBy(
                            32 * index,
                            24 * index
                        );

                        index++;

                        var fc = ff.GetHTMLTargetContainer();
                        var src = "/io/" + ContentKey;

                        var i = new IHTMLImage { src = src }.AttachTo(fc);

                        i.InvokeOnComplete(
                            delegate
                            {
                                ff.ClientSize = new System.Drawing.Size(i.width, i.height);
                            }
                        );

                        ff.FormClosing +=
                            delegate
                            {
                                service.DeleteFileAsync(ContentKey,
                                    delegate
                                    {

                                    }
                                );
                            };
                    }
                );
            }


            #region CreateDialogAt
            var CreateDialogAt =
                new
                {
                    Dialog = default(IHTMLDiv),
                    Content = default(IHTMLDiv),
                    Width = default(string)
                }
                .ToFunc(
                (Point pos, string width) =>
                {
                    var dialog = new IHTMLDiv();

                    dialog.style.SetLocation(pos.X, pos.Y);

                    dialog.style.backgroundColor = Color.Gray;
                    dialog.style.padding = "1px";

                    var caption = new IHTMLDiv().AttachTo(dialog);

                    caption.style.backgroundColor = Color.Blue;
                    caption.style.width = width;
                    caption.style.height = "0.5em";
                    caption.style.cursor = IStyle.CursorEnum.move;

                    var drag = new DragHelper(caption);

                    drag.Position = pos;
                    drag.Enabled = true;
                    drag.DragMove +=
                        delegate
                        {
                            dialog.style.SetLocation(drag.Position.X, drag.Position.Y);
                        };

                    var _content = new IHTMLDiv().AttachTo(dialog);

                    _content.style.textAlign = IStyle.TextAlignEnum.center;
                    _content.style.backgroundColor = Color.White;
                    _content.style.padding = "1px";

                    dialog.AttachToDocument();

                    return new { Dialog = dialog, Content = _content, Width = width };
                }
            );
            #endregion

            var toolbar = CreateDialogAt(new Point(2, 2), "8em");
            // [blocked] The page at https://developer.mozilla.org/en-US/docs/CSS/linear-gradient ran insecure content from http://192.168.1.101:5830/view-source.

            #region bookmark launcher
            new IHTMLAnchor
            {
                href = @"javascript:
((function(h,i)
{ 
    

    var a=-1, 
    b='onreadystatechange', 
    c=document.getElementsByTagName('HEAD')[0], 
    d, 
    e, 
    f, 
    g=c.childNodes, 
    d; 

    e=document.createElement('base');   
    e.href='%%';
    c.appendChild(e);     

    d = function () 
    {  
        next: while (1)  
        {   
            a++;     
            if (a ==h.length)   
            {   
                i();    
                return;   
            }     

            /*
            for (f=0;f<g.length;f++)   
            {   
                var v =g[f];    
                var w =h[a];       

                if (v.nodeName =='SCRIPT')    
                    if (v.src ==w || v.src.substr(v.src.length - w.length - 1,w.length + 1) =='/' + w)    
                        continue next;   
            } */      
            e=document.createElement('SCRIPT');   
            e.src='%%' + h[a];     
            e[b in e?b:'onload']=  function()    
            {       
                var f=e.readyState;    
                if(f==null||f=='loaded'||f=='complete')     
                    d();    
            };  
 
        c.appendChild(e);     
        return;  
    } 
}; 
d();

}

)(['view-source'],function(){}))".Replace("%%", Native.Document.location + ""),

                innerText = "Bookmark launcher"
            }.AttachTo(toolbar.Content);
            #endregion

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );


            new About().Show();
        }

    }

    public static class X
    {
        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }

        public static void ToDataURLAsync(this Blob f, Action<string> y)
        {
            var reader = new FileReader();

            reader.onload = IFunction.Of(
                delegate
                {
                    var base64 = (string)reader.result;

                    y(base64);

                }
            );

            // Read in the image file as a data URL.
            reader.readAsDataURL(f);
        }
    }

}
