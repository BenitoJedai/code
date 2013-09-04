using HulaGirl.HTML.Images.FromAssets;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HulaGirl.source.js.Controls
{
    public class HulaGirl
    {

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }


        public HulaGirl(IHTMLElement e)
        {
            IHTMLImage[] Frames = new HTML.Pages.FramesImages().Images;


            e.insertNextSibling(Control);

            Control.innerHTML = "hello world (javascript) : " /* base.SpawnString*/;

            Control.onmouseover += delegate { Style.color = Color.Blue; };
            Control.onmouseout += delegate { Style.color = Color.None; };

            Style.cursor = IStyle.CursorEnum.pointer;

            var btn = IHTMLButton.Create("go!",
                    delegate
                    {
                        Control.innerHTML = "you clicked me!";

                    }
                );








            var img = Frames[52];

            img.AttachToDocument();

            var _width = 120;
            var _height = 100;
            var _zoom = 1.0;

            Native.Document.body.onmousewheel +=
                ev =>
                {
                    _zoom += 0.1 * ev.WheelDirection;

                    img.style.width = (_width * _zoom) + "px";
                    img.style.height = (_height * _zoom) + "px";
                };

            var index = 0;

            new Timer(
                delegate
                {

                    index++;

                    if (index >= Frames.Length)
                        index = 0;

                    img.Orphanize();
                    img = Frames[index].AttachToDocument();
                    img.style.width = (_width * _zoom) + "px";
                    img.style.height = (_height * _zoom) + "px";

                }, 0, 1000 / 24);



            //var bytes = frames.Sum(x => x.Length);
            //Console.WriteLine(new { bytes });


            // Error	4	The 'await' operator can only be used within an async method. Consider marking this method with the 'async' modifier and changing its return type to 'Task'.	X:\jsc.svn\examples\javascript\HulaGirl\HulaGirl\Library\HulaGirl.cs	89	26	HulaGirl
            //var frames = await Task.WhenAll(Frames.Select(k => k.bytes));



            new IHTMLButton { innerText = "gif" }.AttachToDocument().WhenClicked(
                 async xbtn =>
                 {
                     Action<int> y = xindex =>
                            {
                                xbtn.innerText = new { xindex, Frames.Length }.ToString();

                            };

                     Console.WriteLine("are we loaded yet? " + new { Frames.Length });

                     // Error	4	The 'await' operator can only be used within an async method. Consider marking this method with the 'async' modifier and changing its return type to 'Task'.	X:\jsc.svn\examples\javascript\HulaGirl\HulaGirl\Library\HulaGirl.cs	89	26	HulaGirl
                     //var bytes = await Task.WhenAll(frames);
                     //var bytes = await Task.WhenAll(Frames.Select(k => k.bytes));
                     //byte[][] bytes = await frames;

                     //frames = (from f in Frames select f.bytes).ToArray();
                     // why do we have to use static?
                     //bytes = await Task.WhenAll(frames);

                     // Error	5	Cannot await 'System.Collections.Generic.IEnumerable<System.Threading.Tasks.Task<byte[]>>'	X:\jsc.svn\examples\javascript\HulaGirl\HulaGirl\Library\HulaGirl.cs	117	34	HulaGirl
                     var bytes = await from f in Frames select f.bytes;
                     //bytes.DebuggerBreakIfMissing();

                     //Console.WriteLine("are we loaded yet? yes " + new { bytes.Length });

                     //Console.WriteLine(new { bytes.Length });



                     var a = new { Frames.First().width, Frames.First().height };
                     Console.WriteLine(new { a });







                     var src = await new GIFEncoderWorker(
                           a.width,
                           a.height,
                            delay: 1000 / 24,
                         //transparentColor: 0x0,
                           frames: bytes
                         ,
                           AtFrame: y
                       );



                     Console.WriteLine("done!");

                     new IHTMLImage { src = src }.AttachToDocument();
                 }
                 );


        }




    }



}
public static class X
{
    public static void DebuggerBreakIfMissing(this object i)
    {
        if (i == null)
            Debugger.Break();
    }


}
