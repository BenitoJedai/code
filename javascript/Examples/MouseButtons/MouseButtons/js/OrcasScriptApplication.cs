﻿using ScriptCoreLib;
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
        // roslyn messed up something, InternalTarget null?
        // cctor misused?
        // know only anonymous types seem to misbehave.
        const string vs = "vs2015";


        public MouseButtons()
        {
            var div = new IHTMLDiv("\{vs} Click here");

            div.style.backgroundColor = Color.Yellow;

            div.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

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
                // dynamic?
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
