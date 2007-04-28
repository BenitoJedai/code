using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace gameclient.source.js.Controls
{
    using shared;

    [Script]
    public class LayeredTextBox
    {
        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
        }

        public event EventHandler Cancel;
        public event EventHandler<string> Send;

        public readonly IHTMLDiv Control = new IHTMLDiv();

        [Script]
        public class LayersGroup
        {
            public readonly IHTMLInput Text = new IHTMLInput(HTMLInputTypeEnum.text, "");
            public readonly IHTMLDiv Canvas = new IHTMLDiv();

        }

        public readonly LayersGroup Layers = new LayersGroup();


        public LayeredTextBox()
        {
            Control.appendChild(
                Layers.Canvas,
                Layers.Text
            );

            this.Control.style.overflow = IStyle.OverflowEnum.hidden;
            this.Layers.Canvas.style.overflow = IStyle.OverflowEnum.hidden;
            this.Layers.Text.style.overflow = IStyle.OverflowEnum.hidden;
            this.Layers.Text.style.border = "0";
            this.Layers.Text.style.backgroundColor = Color.Transparent;

            InitializeText();
        }

        void InitializeText()
        {
            this.Layers.Text.onblur +=
                delegate
                {
                    if (this._IsVisible)
                    {

                        Helper.Invoke(this.Cancel);
                        this.Hide();
                    }
                };


            this.Layers.Text.onkeypress +=
                delegate(IEvent ev)
                {
                    if (this._IsVisible)
                    {
                        if (ev.IsEscape)
                        {

                            Helper.Invoke(this.Cancel);
                            this.Hide();

                            return;
                        }

                        if (ev.IsReturn)
                        {
                            if (this.Value == "")
                            {
                                Helper.Invoke(this.Cancel);
                            }
                            else
                            {
                                Helper.Invoke(this.Send, this.Value);
                            }
                            this.Hide();

                            return;
                        }
                    }
                };
        }

        public string Value
        {
            get { return this.Layers.Text.value; }
            set { this.Layers.Text.value = value; }
        }

        public Rectangle CurrentLocation = new Rectangle();

        public void SetLocation(Rectangle r)
        {
            CurrentLocation = r;

            Control.style.SetLocation(r);

            Layers.Text.style.SetLocation(0, 0, r.Width, r.Height);
            Layers.Canvas.style.SetLocation(0, 0, r.Width, r.Height);
        }

        public void ShowAndFocus()
        {
            this._IsVisible = true;


            this.Control.style.display = IStyle.DisplayEnum.block;
            this.Layers.Text.focus();
        }

        public event EventHandler AfterHide;

        public void Hide()
        {
            this._IsVisible = false;

            this.Layers.Text.value = "";

            this.Control.style.display = IStyle.DisplayEnum.none;

            Helper.Invoke(AfterHide);
        }
    }

}
