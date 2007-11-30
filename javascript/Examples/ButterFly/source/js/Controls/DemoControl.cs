using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace ButterFly.source.js.Controls
{

    //delegate void XZC<T>(T t);

    //[Script]
    //delegate X XZCX<T, X>(T t, X x);

    [Script, ScriptApplicationEntryPoint(IsClickOnce = true)]
    public class DemoControl //: SpawnControlBase
    {
        // public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }

        //static int SimpleCall(string e, int i)
        //{
        //    Console.WriteLine("xxx");

        //    return 0;
        //}

        //static void A(global::System.MulticastDelegate m)
        //{
        //}

        //static void A(global::System.Delegate m)
        //{

        //}

        public DemoControl()
        {
            Control.AttachToDocument();
            Control.appendChild("There is a buttrfly under your mouse. Can you see it? :)");

            Butterfly.Spawn(Control);
        }

        public DemoControl(IHTMLElement e)
        //: base(e)
        {
            //var z = new XZCX<string, int>(SimpleCall);

            //z += SimpleCall;

            //z("44", 88);

            e.insertNextSibling(Control);

            Control.appendChild("There is a buttrfly under your mouse. Can you see it? :)");

            Butterfly.Spawn(Control);


        }


        static DemoControl()
        {
            typeof(DemoControl).SpawnTo(i => new DemoControl(i));
        }
    }


}
