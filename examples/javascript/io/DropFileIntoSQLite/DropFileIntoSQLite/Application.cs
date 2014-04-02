using DropFileIntoSQLite.Design;
using DropFileIntoSQLite.HTML.Pages;
using DropFileIntoSQLite.Library;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Library;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DropFileIntoSQLite
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


                    var types = evt.dataTransfer.types == null ? 0 : evt.dataTransfer.types.Length;

                    if (evt.dataTransfer.types != null)
                        foreach (var type in evt.dataTransfer.types.AsEnumerable())
                        {
                            Console.WriteLine(
                                new { type }
                                );
                        }

                    var items = evt.dataTransfer.items == null ? 0u : evt.dataTransfer.items.length;
                    var files = evt.dataTransfer.files == null ? 0u : evt.dataTransfer.files.length;



                    Console.WriteLine("ondragover: " +
                        new
                        {

                            types,
                            items,
                            files
                        }
                    );
                };


            Native.document.body.ondragleave +=
                delegate
                {
                    page.Header.style.color = JSColor.None;
                };

            #region DetectCanvasFromBytesExperiment
            Action<Form, WebBrowser, string, string, long> DetectCanvasFromBytesExperiment =
                (ff, web, ContentValue, src, ContentBytesLength) =>
                {
                    web.Navigated +=
                           async delegate
                           {
                               if (ContentValue != "png.png")
                                   return;

                               // X:\jsc.svn\examples\javascript\canvas\CanvasFromBytes\CanvasFromBytes\Application.cs
                               //Console.WriteLine("interesting, is it one of ours? " + new { ContentValue });
                               ff.Text = "interesting, is it one of ours? " + new { ContentValue };

                               var csrci = new IHTMLImage { src = src };

                               //await csrci.async.onlo
                               await csrci;

                               if (csrci.width != csrci.height)
                                   return;


                               var w = csrci.width;


                               // do the reverse
                               var z = new CanvasRenderingContext2D(w, w);
                               z.drawImage(csrci, 0, 0, w, w);
                               //z.canvas.AttachToDocument();

                               // whats the bytes?

                               var zbytes = z.bytes;

                               ff.Text = "will decode " + new { zbytes.Length, ContentBytesLength }.ToString();

                               #region decode
                               var decodebytes = await Task.Factory.StartNew(
                                   new { zbytes },
                                   scope =>
                                   {
                                       // Native.Console.WriteLine += ?

                                       // { Length = 2053956, u4 = 3c68746d } 

                                       Console.WriteLine(
                                        new
                                        {
                                            scope.zbytes.Length,

                                            u4 = new[] { 
                                            scope.zbytes[0 * 4 + 0],
                                            scope.zbytes[0 * 4 +1],
                                            scope.zbytes[0 * 4 +2],
                                            scope.zbytes[0 * 4 +3]
                                        }.ToHexString()
                                        }
                                       );


                                       // Uncaught Error: InvalidOperationException: { MethodToken = dAAABv_a4OTKgLfLC20SaJA } function is not available at { href =

                                       var wwbytes = new byte[scope.zbytes.Length / 4];
                                       var wi = 0;

                                       for (int i = 0; i < scope.zbytes.Length; i += 4)
                                       {
                                           // that be the red
                                           //wwbytes[wi] = scope.zbytes[i];

                                           // bet we need alpha
                                           wwbytes[wi] = scope.zbytes[i + 3];
                                           wi++;
                                       }



                                       return wwbytes;
                                   }
                               );
                               #endregion


                               var html = Encoding.UTF8.GetString(decodebytes);

                               ff.Text = "decoded " + new { html.Length, ContentBytesLength }.ToString();

                               //Console.WriteLine(new { html });


                               // um hide old data.
                               web.Hide();

                               var xweb = new WebBrowser { Dock = DockStyle.Fill };
                               xweb.AttachTo(ff);
                               xweb.DocumentText = html;
                               ff.Text = "!decoded " + new { html.Length, ContentBytesLength }.ToString();
                           };

                };
            #endregion


            Native.document.body.ondrop +=
                evt =>
                {
                    //if (evt.dataTransfer == null)
                    //    return;

                    var types = evt.dataTransfer.types == null ? 0 : evt.dataTransfer.types.Length;

                    var items = evt.dataTransfer.items == null ? 0u : evt.dataTransfer.items.length;
                    var files = evt.dataTransfer.files == null ? 0u : evt.dataTransfer.files.length;


                    Console.WriteLine("ondrop: " +
                        new
                        {

                            types,
                            items,
                            files
                        }
                    );


                    page.Header.style.color = JSColor.None;


                    //var xfiles = evt.dataTransfer.files.AsEnumerable().Concat(
                    //    from x in evt.dataTransfer.items.AsEnumerable()
                    //    let f = x.getAsFile()
                    //    where f != null
                    //    select f
                    //);

                    #region DataTable
                    if (evt.dataTransfer.items != null)
                    {
                        // X:\jsc.svn\examples\javascript\DragDataTableIntoCSVFile\DragDataTableIntoCSVFile\Application.cs

                        evt.dataTransfer.items.AsEnumerable().Where(
                            x =>

                                x.type.ToLower() ==

                                // let jsc type system sort it out?
                                // how much reflection does jsc give us nowadays?
                                typeof(DataTable).Name.ToLower()

                        ).WithEach(
                            async x =>
                            {
                                // http://www.whatwg.org/specs/web-apps/current-work/multipage/dnd.html#dfnReturnLink-0
                                var DataTable_xml = await x.getAsString();

                                var DataTable = StringConversionsForDataTable.ConvertFromString(DataTable_xml);

                                var ff = new Form { Text = new { typeof(DataTable).Name }.ToString() };

                                var g = new DataGridView { DataSource = DataTable, Dock = DockStyle.Fill };

                                ff.Controls.Add(g);


                                ff.Show();

                            }
                        );
                    }
                    #endregion

                    #region files
                    evt.dataTransfer.files.AsEnumerable().WithEachIndex(
                        (f, index) =>
                        {
                            Console.WriteLine(
                                new
                                {

                                    f.name,
                                    f.size,
                                    f.lastModifiedDate
                                }
                            );

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

                            #region image
                            var i = default(IHTMLImage);

                            if (f.type.StartsWith("image/"))
                            {
                                // um would we have a timing issue here?
                                f.ToDataURLAsync(
                                    src =>
                                    {
                                        i = new IHTMLImage { src = src }.AttachTo(fc);
                                        i.style.width = "100%";

                                        i.InvokeOnComplete(
                                            delegate
                                            {

                                                ff.ClientSize = new System.Drawing.Size(
                                                    // keep it reasonable!
                                                    i.width.Min(600),
                                                    i.height.Min(400)
                                                );

                                            }
                                        );
                                    }
                                );
                            }
                            #endregion

                            // http://html5doctor.com/drag-and-drop-to-server/

#if FUTURE
                            service.XUpload(f, delegate { });
#endif


                            var d = new FormData();

                            d.append("foo", f, f.name);

                            var xhr = new IXMLHttpRequest();

                            xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/upload");

                            #region InvokeOnComplete
                            xhr.InvokeOnComplete(
                                delegate
                                {
                                    Console.WriteLine("upload complete!");

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


                                            // "X:\jsc.svn\examples\javascript\canvas\CanvasFromBytes\png.png"
                                            DetectCanvasFromBytesExperiment(
                                                ff,
                                                web,
                                                 f.name,
                                                src,
                                               (long)f.size
                                            );

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
                            #endregion


                            //------WebKitFormBoundaryDmGHAZzeMBbcD5mu
                            //Content-Disposition: form-data; name="foo"; filename="FlashHeatZeeker.UnitPedControl.ApplicationSprite.swf"
                            //Content-Type: application/x-shockwave-flash


                            //------WebKitFormBoundaryDmGHAZzeMBbcD5mu--

                            Console.WriteLine("before upload...");
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
                    (__ContentKey, ContentBytesLength, ContentValue, Left, Top, Width, Height) =>
                    {

                        var ff = new Form();
                        ff.PopupInsteadOfClosing(HandleFormClosing: false);


                        ff.Text = new { __ContentKey, ContentValue, ContentBytesLength }.ToString();


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



                        //var fc = ff.GetHTMLTargetContainer();
                        var src = "/io/" + __ContentKey;

                        //var i = new IHTMLImage { src = src }.AttachTo(fc);
                        //i.style.width = "100%";

                        var web = new WebBrowser { Dock = DockStyle.Fill };

                        web.AttachTo(ff);

                        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.WebBrowser.add_DocumentCompleted(System.Windows.Forms.WebBrowserDocumentCompletedEventHandler)]
                        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
                        //script: error JSC1000: error at DropFileIntoSQLite.Application+<>c__DisplayClass2e.<.ctor>b__15,
                        // assembly: V:\DropFileIntoSQLite.Application.exe
                        // type: DropFileIntoSQLite.Application+<>c__DisplayClass2e, DropFileIntoSQLite.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

                        //web.DocumentCompleted +=
                        //    delegate
                        //    {



                        //};

                        DetectCanvasFromBytesExperiment(
                            ff,
                            web,
                            ContentValue,
                            src,
                            ContentBytesLength
                        );


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



            // can we write about this?
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


            new About().Show();
        }

    }

    public static class X
    {
        //public static IEnumerable<File> AsEnumerable(this FileList f)
        //{
        //    return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        //}

        public static IEnumerable<DataTransferItem> AsEnumerable(this DataTransferItemList f)
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
