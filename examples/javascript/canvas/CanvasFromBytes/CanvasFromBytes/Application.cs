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
using CanvasFromBytes;
using CanvasFromBytes.Design;
using CanvasFromBytes.HTML.Pages;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CanvasFromBytes
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\Test\TestPackageAsApplication\TestPackageAsApplication\Application.cs

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Console.WriteLine("before");
            {
                var c = new CanvasRenderingContext2D(256, 256);


                var bytes = new byte[256 * 256 * 4];

                for (int x = 0; x < 256; x++)
                {
                    for (int y = 0; y < 256; y++)
                    {
                        bytes[x * 4 + 0 + 256 * 4 * y] = (byte)x;
                        bytes[x * 4 + 1 + 256 * 4 * y] = (byte)y;
                        bytes[x * 4 + 2 + 256 * 4 * y] = 0xff;
                        bytes[x * 4 + 3 + 256 * 4 * y] = 0xcf;
                    }


                }

                var i = c.getImageData();

                i.data.set(bytes, 0);

                c.putImageData(i, 0, 0, 0, 0, 256, 256);

                c.canvas.AttachToDocument();

            }


            {
                var bytes = new byte[256 * 256 * 4];

                for (int x = 0; x < 256; x++)
                {
                    for (int y = 0; y < 256; y++)
                    {
                        bytes[x * 4 + 0 + 256 * 4 * y] = 0;
                        bytes[x * 4 + 1 + 256 * 4 * y] = 0;
                        bytes[x * 4 + 2 + 256 * 4 * y] = 0;

                        // alpha?
                        bytes[x * 4 + 3 + 256 * 4 * y] = (byte)x;
                    }


                }

                // 1824 of 65536
                // 0.02783203125
                // 262144

                var slider = new IHTMLInput
                {

                    type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range,
                    max = 256 * 256
                }.AttachToDocument();


                new IHTMLPre { 
                    () =>
                    new { 
                        bytes.Length,
                        md5 = bytes.ToMD5Bytes().ToHexString(), 
                        slider.valueAsNumber,
                        u4 = new [] { 
                            bytes[slider.valueAsNumber * 4 + 0],
                            bytes[slider.valueAsNumber * 4 +1],
                            bytes[slider.valueAsNumber * 4 +2],
                            bytes[slider.valueAsNumber * 4 +3]
                        }.ToHexString() }
                }.AttachToDocument();

                var c = new CanvasRenderingContext2D(256, 256) { bytes = bytes };

                c.canvas.AttachToDocument();

                // https://code.google.com/p/chromium/issues/detail?id=312187
                // save for disk saving
                // https://developer.mozilla.org/en-US/docs/Web/API/HTMLCanvasElement
                var csrc = c.canvas.toDataURL("image/png");
                var csrci = new IHTMLImage { src = csrc }.AttachToDocument();

                // do the reverse
                var z = new CanvasRenderingContext2D(256, 256);
                z.drawImage(csrci, 0, 0, 256, 256);
                z.canvas.AttachToDocument();

                // whats the bytes?

                var zbytes = z.bytes;

                new IHTMLPre { 
                    () =>
                    new {
                        zbytes.Length,
                        
                        md5 = zbytes.ToMD5Bytes().ToHexString() ,
                        slider.valueAsNumber,
                        u4 = new [] { 
                            zbytes[slider.valueAsNumber * 4 + 0],
                            zbytes[slider.valueAsNumber * 4 +1],
                            zbytes[slider.valueAsNumber * 4 +2],
                            zbytes[slider.valueAsNumber * 4 +3]
                        }.ToHexString() 
                    }
                }.AttachToDocument();


                // 00ff80cf vs 00ff7fcf
            }
            Console.WriteLine("after");


            new IHTMLButton { "make me the link " + new { Native.document.location.protocol } }.AttachToDocument().WhenClicked(
               makelink =>
               {
                   makelink.disabled = true;



                   #region PackageAsApplication
                   Action<IHTMLScript, XElement, Action<string>> PackageAsApplication =
                       (source0, xml, yield) =>
                       {
                           new IXMLHttpRequest(
                               ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                               source0.src,
                               handler: (IXMLHttpRequest r) =>
                               {
                                   // store hash
                                   xml.Add(new XElement("link", new XAttribute("rel", "location"), new XAttribute("href", Native.Document.location.hash)));


                                   xml.Add(
                                       new XElement("script",
                                           "/* source */"
                                       )
                                   );

                                   var data = "";


                                   Action later = delegate
                                   {

                                       data = data.Replace("/* source */", r.responseText);

                                   };


                                   //Native.Document.getElementsByTagName("link").AsEnumerable().ToList().ForEach(

                                   xml.Elements("link").ToList().ForEach(
                                       (XElement link, Action next) =>
                                       {
                                           #region style
                                           var rel = link.Attribute("rel");
                                           if (rel.Value != "stylesheet")
                                           {
                                               next();
                                               return;
                                           }

                                           var href = link.Attribute("href");

                                           var placeholder = "/* " + href.Value + " */";

                                           //page.DragHTM.innerText += " " + placeholder;


                                           xml.Add(new XElement("style", placeholder));

                                           new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET, href.Value,
                                               rr =>
                                               {

                                                   later += delegate
                                                   {


                                                       data = data.Replace(placeholder, rr.responseText);

                                                   };

                                                   Console.WriteLine("link Remove");
                                                   link.Remove();

                                                   next();
                                               }
                                           );

                                           #endregion
                                       }
                                   )(
                                       delegate
                                       {


                                           data = xml.ToString();
                                           Console.WriteLine("data: " + data);
                                           later();

                                           yield(data);
                                       }
                                   );
                               }
                           );

                       };
                   #endregion

                   // XMLHttpRequest cannot load file:///D:/view-source. Cross origin requests are only supported for HTTP. 



                   Console.WriteLine("before PackageAsApplication");
                   PackageAsApplication(

                       // how would we know our current source location?
                       new IHTMLScript { src = "view-source" },
                       XElement.Parse(AppSource.Text),
                       async data =>
                       {
                           Console.WriteLine("enter PackageAsApplication");
                           var bytes0 = Encoding.UTF8.GetBytes(data);
                           var w0 = (int)Math.Ceiling(Math.Sqrt(bytes0.Length));

                           var padding0 = (w0 * w0) - bytes0.Length;

                           // { Length = 2072331, padding0 = 1269 }
                           new IHTMLPre {
                               new { 
                                   bytes0.Length, 
                                   padding0
                               }
                           }.AttachToDocument();


                           var bytes = Encoding.UTF8.GetBytes(
                               data

                               +
                               Encoding.UTF8.GetString(new byte[padding0])

                               // too slow?
                               //+ new string(' ', padding0)
                               );

                           //var bytes = Encoding.ASCII.GetBytes(data);

                           // { Length = 2019412 }


                           // why wont intellisense work like excel, debugger
                           // and tell me the anwser without running
                           // the other option is to step in via F11

                           //  Math.Sqrt(2019412) 1421.0601676213432

                           var w = (int)Math.Ceiling(Math.Sqrt(bytes.Length));

                           //{ Length = 2049266, w = 1432 }
                           //{ Length = 8202496 }

                           new IHTMLPre {
                               new { 
                                   bytes.Length, 
                                   w, 
                                   
                                 u8 = new [] { 
                                        bytes[0 * 4 + 0],
                                        bytes[0 * 4 +1],
                                        bytes[0 * 4 +2],
                                        bytes[0 * 4 +3],

                                        bytes[1 * 4 + 0],
                                        bytes[1 * 4 +1],
                                        bytes[1 * 4 +2],
                                        bytes[1 * 4 +3]
                                    }.ToHexString() ,

                                   md5 = bytes.ToMD5Bytes().ToHexString() }
                           }.AttachToDocument();

                           // 77,828 bytes)
                           var wbyteswatch = Stopwatch.StartNew();

                           // { Length = 2069090, w = 1439, u8 = 3c68746d6c3e0a20, md5 = 4fa4af629b63b267da4d4a142c9cb171 }
                           // { Length = 2070721, md5 = 0186a2c2d01257cf4ba6a5524ec4743d, u8 = 3c68746d6c3e0a20 }


                           #region encode
                           var wbytes = await Task.Factory.StartNew(
                               new { bytes, w },
                               scope =>
                               {
                                   // Native.Console.WriteLine += ?

                                   // { Length = 2053956, u4 = 3c68746d } 

                                   Console.WriteLine(
                                    new
                                    {
                                        scope.bytes.Length,

                                        u4 = new[] { 
                                            scope.bytes[0 * 4 + 0],
                                            scope.bytes[0 * 4 +1],
                                            scope.bytes[0 * 4 +2],
                                            scope.bytes[0 * 4 +3]
                                        }.ToHexString()
                                    }
                                   );


                                   // Uncaught Error: InvalidOperationException: { MethodToken = dAAABv_a4OTKgLfLC20SaJA } function is not available at { href =
                                   var ww = scope.w;

                                   var wwbytes = new byte[ww * ww * 4];
                                   var wi = 0;

                                   for (int i = 0; i < wwbytes.Length; i += 4)
                                   {
                                       //bytes[x * 4 + 0 + 256 * 4 * y] = 0;
                                       //bytes[x * 4 + 1 + 256 * 4 * y] = 0;
                                       //bytes[x * 4 + 2 + 256 * 4 * y] = 0;

                                       // can we get a red picture?
                                       // unless we alpha everything out!
                                       // 692 KB!
                                       //wwbytes[i + 0] = (byte)(0xff);
                                       wwbytes[i + 0] = 0;
                                       wwbytes[i + 1] = 0;
                                       wwbytes[i + 2] = 0;
                                       // alpha
                                       wwbytes[i + 3] =

                                           // the bytes histogram seems
                                           // to be in the middle?
                                           scope.bytes[wi];

                                       // reverse alpha?
                                       //(byte)(255 - scope.bytes[wi]);
                                       //(byte)~scope.bytes[wi];

                                       wi++;
                                   }



                                   return wwbytes;
                               }
                           );
                           #endregion


                           //{ Length = 2071072, w = 1440, u4 = 3c68746d, md5 = 5c45e67b07d9045882dfe305e9bf23fd }
                           //{ Length = 8294400, ElapsedMilliseconds = 9971, u8 = ff00003cff00003c }

                           new IHTMLPre {
                               new { 
                                   wbytes.Length, wbyteswatch.ElapsedMilliseconds,
                                    u8 = new [] { 
                                        wbytes[0 * 4 + 0],
                                        wbytes[0 * 4 +1],
                                        wbytes[0 * 4 +2],
                                        wbytes[0 * 4 +3],

                                        wbytes[1 * 4 + 0],
                                        wbytes[1 * 4 +1],
                                        wbytes[1 * 4 +2],
                                        wbytes[1 * 4 +3]
                                    }.ToHexString() 
                               }
                           }.AttachToDocument();


                           new IHTMLPre {
                               new { wbytes.Length, wbyteswatch.ElapsedMilliseconds, md5 = wbytes.ToMD5Bytes().ToHexString() }
                           }.AttachToDocument();

                           // { Length = 8236900, ElapsedMilliseconds = 2476, md5 = 166c1b692fad548789a12a3ae5f0a7ae }

                           var c = new CanvasRenderingContext2D(w, w) { bytes = wbytes };

                           // this would touch our precious alpha channel!

                           //c.fillText(
                           //    "rgb channel for preview, while alpha channel has the data of "
                           //    + new { bytes.Length },
                           //    64, 64, c.canvas.width
                           //    );


                           c.canvas.style.backgroundColor = "yellow";
                           c.canvas.style.border = "1px solid red";
                           c.canvas.AttachToDocument();

                           // https://code.google.com/p/chromium/issues/detail?id=312187
                           // save for disk saving
                           // https://developer.mozilla.org/en-US/docs/Web/API/HTMLCanvasElement
                           var csrc = c.canvas.toDataURL("image/png");


                           // this wont help at all
                           // https://bugzilla.mozilla.org/show_bug.cgi?id=676619

                           var csrca = new IHTMLAnchor { download = "view-source.png", href = csrc, title = "view-source", innerText = "do not drag, click to download view-source.png" }.AttachToDocument();
                           // http://caniuse.com/download


                           var csrci = new IHTMLImage { src = csrc }.AttachToDocument();
                           csrci.style.backgroundColor = "yellow";
                           csrci.style.border = "1px solid red";

                           // um. time to reverse?


                           // do the reverse
                           var z = new CanvasRenderingContext2D(w, w);
                           z.drawImage(csrci, 0, 0, w, w);
                           z.canvas.AttachToDocument();

                           // whats the bytes?

                           var zbytes = z.bytes;

                           new IHTMLPre { 
                                new {
                                    zbytes.Length,
                        
                                    md5 = zbytes.ToMD5Bytes().ToHexString() ,
                                    u8 = new [] { 
                                        zbytes[0 * 4 + 0],
                                        zbytes[0 * 4 +1],
                                        zbytes[0 * 4 +2],
                                        zbytes[0 * 4 +3],

                                        zbytes[1 * 4 + 0],
                                        zbytes[1 * 4 +1],
                                        zbytes[1 * 4 +2],
                                        zbytes[1 * 4 +3]
                                    }.ToHexString() 
                                }
                            }.AttachToDocument();

                           // { Length = 8236900, md5 = 166c1b692fad548789a12a3ae5f0a7ae, u4 = 0000003c }

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

                           //{ Length = 8282884, md5 = ece6f7e935f0b424082445b273b8ed65, u4 = ff00003c }
                           //{ Length = 2070721, md5 = 47f7ec04a0b7cfc27e706583cc1a9bf7, u4 = 3c3c3c3c }

                           new IHTMLPre { 
                                new {
                                    decodebytes.Length,
                        
                                    md5 = decodebytes.ToMD5Bytes().ToHexString() ,
                                    u8 = new [] { 
                                        decodebytes[0 * 4 + 0],
                                        decodebytes[0 * 4 +1],
                                        decodebytes[0 * 4 +2],
                                        decodebytes[0 * 4 +3],

                                        decodebytes[1 * 4 + 0],
                                        decodebytes[1 * 4 +1],
                                        decodebytes[1 * 4 +2],
                                        decodebytes[1 * 4 +3]
                                    }.ToHexString() 
                                }
                            }.AttachToDocument();
                       }
                  );


               }
            );

        }

    }
}
