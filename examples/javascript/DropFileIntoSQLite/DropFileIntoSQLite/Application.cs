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
            FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

            // http://html5doctor.com/drag-and-drop-to-server/

            #region ondrop
            Native.document.body.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.preventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.


                    page.Header.style.color = JSColor.Green;

                    Console.WriteLine("ondragover: " + new
                        {

                            types = evt.dataTransfer.types.Length,
                            files = evt.dataTransfer.files.length
                        }
                    );
                };


            Native.document.body.ondragleave +=
                delegate
                {
                    page.Header.style.color = JSColor.None;
                };

            Native.document.body.ondrop +=
                evt =>
                {
                    //if (evt.dataTransfer == null)
                    //    return;



                    page.Header.style.color = JSColor.None;



                    #region files
                    evt.dataTransfer.files.AsEnumerable().WithEachIndex(
                        (f, index) =>
                        {
                            var ff = new Form();
                            ff.PopupInsteadOfClosing(HandleFormClosing: false);



                            ff.Text = new { f.type, f.name, f.size }.ToString();


                            ff.Show();

                            ff.MoveTo(
                                evt.CursorX + 32 * index,
                                evt.CursorY + 24 * index
                            );

                            var fc = ff.GetHTMLTargetContainer();

                            fc.title = ff.Text;

                            var i = default(IHTMLImage);

                            if (f.type.StartsWith("image/"))
                            {
                                f.ToDataURLAsync(
                                    src =>
                                    {
                                        i = new IHTMLImage { src = src }.AttachTo(fc);
                                        i.style.width = "100%";

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
                                            var __ContentKey = (Table1_ContentKey)int.Parse(ContentKey.Value);

                                            var web = new WebBrowser { Dock = DockStyle.Fill };

                                            web.Hide();
                                            web.AttachTo(ff);


                                            var src = "/io/" + ContentKey.Value;

                                            if (i == null)
                                            {
                                                web.Show();
                                            }
                                            else
                                            {
                                                web.Navigated +=
                                                    delegate
                                                    {
                                                        i.Orphanize();
                                                        web.Show();
                                                    };
                                            }

                                            web.Navigate(src);


                                            //if (i != null)
                                            //{
                                            //    i.src = src;
                                            //}

                                            __ContentKey.SetLeft(ff.Left);
                                            __ContentKey.SetTop(ff.Top);

                                            ff.LocationChanged +=
                                                delegate
                                                {
                                                    __ContentKey.SetLeft(ff.Left);
                                                    __ContentKey.SetTop(ff.Top);
                                                };

                                            ff.SizeChanged +=
                                                delegate
                                                {
                                                    __ContentKey.SetWidth(ff.Width);
                                                    __ContentKey.SetHeight(ff.Height);
                                                };

                                            ff.FormClosing +=
                                                delegate
                                                {
                                                    __ContentKey
                                                        .Delete();
                                                };


                                            #region onmousewheel
                                            ff.GetHTMLTarget().With(
                                                ffh =>
                                                {
                                                    dynamic ffhs = ffh.style;
                                                    // http://css-infos.net/property/-webkit-transition
                                                    //ffhs.webkitTransition = "webkitTransform 0.3s linear";

                                                    ffh.onmousewheel +=
                                                        e =>
                                                        {
                                                            e.preventDefault();
                                                            e.stopPropagation();


                                                            if (e.WheelDirection > 0)
                                                            {
                                                                ff.Width = (int)(ff.Width * 1.1);
                                                                ff.Height = (int)(ff.Height * 1.1);
                                                            }
                                                            else
                                                            {
                                                                ff.Width = (int)(ff.Width * 0.9);
                                                                ff.Height = (int)(ff.Height * 0.9);
                                                            }

                                                        };

                                                }
                                            );
                                            #endregion

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

                    evt.preventDefault();
                };
            #endregion

            #region restore
            {
                var index = 0;

                default(Table1_ContentKey).WithEach(
                    (__ContentKey, ContentBytesLength, Left, Top, Width, Height) =>
                    {

                        var ff = new Form();
                        ff.PopupInsteadOfClosing(HandleFormClosing: false);


                        ff.Text = new { __ContentKey, ContentBytesLength }.ToString();


                        ff.Show();

                        if (Left > 0)
                            ff.MoveTo(
                                Left,
                                Top
                            );
                        else

                            ff.MoveBy(
                                32 * index,
                                24 * index
                            );

                        index++;

                        #region onmousewheel
                        ff.GetHTMLTarget().With(
                            ffh =>
                            {
                                dynamic ffhs = ffh.style;
                                // http://css-infos.net/property/-webkit-transition
                                //ffhs.webkitTransition = "webkitTransform 0.3s linear";

                                ffh.onmousewheel +=
                                    e =>
                                    {
                                        e.preventDefault();
                                        e.stopPropagation();

                                        if (e.WheelDirection > 0)
                                        {
                                            ff.Width = (int)(ff.Width * 1.1);
                                            ff.Height = (int)(ff.Height * 1.1);
                                        }
                                        else
                                        {
                                            ff.Width = (int)(ff.Width * 0.9);
                                            ff.Height = (int)(ff.Height * 0.9);
                                        }

                                    };

                            }
                        );
                        #endregion



                        var fc = ff.GetHTMLTargetContainer();
                        var src = "/io/" + __ContentKey;

                        //var i = new IHTMLImage { src = src }.AttachTo(fc);
                        //i.style.width = "100%";

                        var web = new WebBrowser { Dock = DockStyle.Fill };

                        web.AttachTo(ff);

                        web.Navigate(src);

                        if (Width > 0)
                            ff.SizeTo(
                               Width,
                               Height
                           );
                        //else
                        //    i.InvokeOnComplete(
                        //        delegate
                        //        {


                        //            ff.ClientSize = new System.Drawing.Size(i.width, i.height);

                        //        }
                        //    );

                        ff.LocationChanged +=
                            delegate
                            {
                                __ContentKey.SetLeft(ff.Left);
                                __ContentKey.SetTop(ff.Top);
                            };

                        ff.SizeChanged +=
                            delegate
                            {
                                __ContentKey.SetWidth(ff.Width);
                                __ContentKey.SetHeight(ff.Height);
                            };

                        ff.FormClosing +=
                            delegate
                            {
                                __ContentKey.Delete();
                            };
                    }
                );
            }
            #endregion




            #region bookmark launcher
            var href = @"javascript:
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

)(['view-source'],function(){}))".Replace("%%", Native.Document.location + "");

            page.Header.draggable = true;
            page.Header.ondragstart +=
                e =>
                {
                    e.dataTransfer.setData("text/uri-list", href);
                };


            IStyleSheet.Default["#Header:hover"].style.color = "red";
            IStyleSheet.Default["#Header:hover"].style.cursor = IStyle.CursorEnum.pointer;

            #endregion

            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);


            new About().Show();
        }

    }

    public static class X
    {
        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }


        [Obsolete("ScriptCoreLib.Async")]
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
