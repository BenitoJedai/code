using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;


using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;

#if BLOAT
namespace ScriptCoreLib.JavaScript.Controls
{
    [Script]
    public class MyInlineButton
    {
        public IHTMLSpan Control = new IHTMLSpan();

        public IHTMLImage Image;

        public event EventHandler<IEvent> Click;
        public event EventHandler<IEvent> MouseDown;

        
        public int PaddingX = 4;
        public int SpacingX = 0;

        private bool bVisible;

        public bool Visible
        {
            get { return bVisible; }
            set
            {
                bVisible = value;


                IHTMLElement.Show(bVisible, Control);


            }
        }



        private bool bEnabled;

        public bool Enabled
        {
            get { return bEnabled; }
            set
            {
                bEnabled = value;


                this.DoLayout();
            }
        }

        [Script(NoExeptions=true)]
        private void DoLayout()
        {
            if (this.Control.parentNode == null)
                return;



                if (Enabled)
                {
                    Control.style.cursor = IStyle.CursorEnum.pointer;

                    if (bMouseOver)
                    {
                        Control.style.backgroundColor = JSColor.System.Highlight;
                        Control.style.color = JSColor.System.HighlightText;
                    }
                    else
                    {
                        Control.style.backgroundColor = Color.None;
                        Control.style.color = Color.None;
                    }
                }
                else
                {
                    Control.style.cursor = IStyle.CursorEnum.@default;

                    if (bMouseOver)
                    {
                        Control.style.backgroundColor = JSColor.System.Highlight;

                    }
                    else
                    {

                            Control.style.backgroundColor = Color.None;

                    }

                    Control.style.color = JSColor.System.GrayText;
                }

        }

        bool bMouseOver = false;


        public string Text
        {
            get { return ControlTextNode.nodeValue; }
            set { ControlTextNode.nodeValue = value; }
        }

        ITextNode ControlTextNode = new ITextNode();
        ITextNode ControlTextNodeEmpty = new ITextNode("clickme");

 
        void Control_onmousedown(IEvent e)
        {
            if (Enabled)
            {
                if (MouseDown != null)
                    MouseDown(e);
            }
        }

        void Control_onclick(IEvent e)
        {
            if (Enabled)
            {
                if (Click != null)
                    Click(e);
            }
        }

        public MyInlineButton(string src, string text)
            : this(text)
        {


            Image = new IHTMLImage(src);
            Image.InvokeOnComplete(
                delegate
                {
                    Control.style.backgroundImage = "url(" + src + ")";
                    Control.style.backgroundPosition = PaddingX + "px center";
                    Control.style.backgroundRepeat = "no-repeat";
                    Control.style.paddingLeft = (Image.width + PaddingX * 2 + SpacingX) + "px";
                }
            );
        }

        public MyInlineButton(string text)
        {
            ControlTextNode.nodeValue = text;

            Control.appendChild(ControlTextNode);

            Control.onclick += new EventHandler<IEvent>(Control_onclick);
            Control.onmousedown += new EventHandler<IEvent>(Control_onmousedown);

            Control.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;
            Control.style.cursor = IStyle.CursorEnum.pointer;

            Control.style.padding = "2px";
            Control.style.marginLeft = "1px";
            Control.style.marginRight = "1px";

            Control.DisableContextMenu();
            Control.DisableSelection();

            Control.onmouseover +=
                delegate
                {
                    bMouseOver = true;

                    DoLayout();
                };

            Control.onmouseout +=
                delegate
                {
                    try
                    {
                        bMouseOver = false;

                        DoLayout();
                    }
                    catch
                    {

                    }


                };

            DoLayout();
        }


        public static implicit operator IHTMLElement(MyInlineButton e)
        {
            return e.Control;
        }
    }

}
#endif