using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;


namespace MouseButtons.js
{
    [Script, ScriptApplicationEntryPoint]
    public class MouseButtons
    {
        public MouseButtons()
        {
            var div = new IHTMLDiv("Click here");

            div.style.backgroundColor = Color.Yellow;

            div.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

            div.AttachToDocument();



            div.onmousedown +=
                ev =>
                    WriteInfo(div, new { onmousedown = GetInfo(ev) });

            div.onmouseup +=
                ev =>
                    WriteInfo(div, new { onmouseup = GetInfo(ev) });

            div.onclick +=
                ev =>
                    WriteInfo(div, new { onclick = GetInfo(ev) });


        }

        private static void WriteInfo(IHTMLDiv div, object obj)
        {
            var text = new IHTMLDiv();

            text.innerText = obj.ToString();

            text.AttachTo(div);
        }

        private static object GetInfo(ScriptCoreLib.JavaScript.DOM.IEvent ev)
        {
            var x = Expando.Of(ev);
            object onmousedown = new
            {
                button = x["button"].GetValue(),
                which = x["which"].GetValue(),
                ev.MouseButton
            };
            return onmousedown;
        }

        static MouseButtons()
        {
            typeof(MouseButtons).SpawnTo(i => new MouseButtons());
        }

    }

}
