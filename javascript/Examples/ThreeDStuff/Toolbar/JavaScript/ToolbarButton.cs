using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;

namespace Toolbar.JavaScript
{
    [Script]
    public class ToolbarButton
    {
        public IHTMLDiv Control;
        public int Counter;
        public bool IsDown;

        public static implicit operator bool(ToolbarButton e) { return e.IsActivated; }

        public bool IsActivated
        {
            get
            {
                return Counter % 2 == 1;
            }
        }

        public string Title { get { return Control.title; } set { Control.title = value; } }

        public ToolbarButton AttachTo(ToolbarDialog e)
        {
            Control.AttachTo(e.Control);

            return this;
        }

        public ToolbarButton()
        {

        }

        public ToolbarDialog Toolbar;

        public event Action<ToolbarButton> Clicked;

        public ToolbarButton(ToolbarDialog t, string img)
        {
            this.Toolbar = t;
            this.Toolbar.Buttons.Add(this);

            var btn = this;

            btn.Control = new IHTMLDiv();
            btn.IsDown = false;
            btn.Counter = 0;

            btn.Control.SetDialogColor(t.Color);

            btn.Control.style.background = "url(" + img + ") no-repeat";
            btn.Control.style.SetLocation(2 + 24 * (this.Toolbar.Buttons.Count - 1), 8, 22, 22);


            t.Grow();

            btn.Control.onclick +=
                ev =>
                {
                    RaiseClicked();
                };

            var onmouseup = default(ScriptCoreLib.Shared.EventHandler<IEvent>);


            btn.Control.onmousedown +=
                ev =>
                {
                    ev.StopPropagation();

                    btn.IsDown = true;
                    btn.Control.SetDialogColor(t.Color, false);

                    Native.Document.onmouseup += onmouseup;
                };


            onmouseup =
                ev =>
                {
                    if (btn.IsDown)
                    {
                        ev.StopPropagation();

                        btn.IsDown = false;
                        btn.Control.SetDialogColor(t.Color, true);

                        Native.Document.onmouseup -= onmouseup;
                    }
                };

            this.AttachTo(t);

        }

        public void RaiseClicked()
        {
            SilentClick();

            if (Clicked != null)
                Clicked(this);
        }

        public void SilentClick()
        {
            this.Counter++;

            if (IsActivated)
                Control.SetDialogColor(this.Toolbar.ActivatedButtonColor());
            else
                Control.SetDialogColor(this.Toolbar.Color);
        }
    }

}
