using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLButtonElement.idl

    [Script(InternalConstructor = true)]
    public class IHTMLButton : IHTMLElement<IHTMLButton>
    {
        public bool disabled;

        #region Constructor

        public IHTMLButton()
        {
            // InternalConstructor
        }

        public IHTMLButton(IHTMLDocument doc)
        {
            // InternalConstructor
        }

        static IHTMLButton InternalConstructor()
        {
            return (IHTMLButton)((object)new IHTMLElement(HTMLElementEnum.button));
        }

        static IHTMLButton InternalConstructor(IHTMLDocument doc)
        {
            return (IHTMLButton)((object)new IHTMLElement(HTMLElementEnum.button, doc));
        }

        #endregion


        #region Constructor

        public IHTMLButton(string e)
        {
            // InternalConstructor
        }

        static IHTMLButton InternalConstructor(string e)
        {
            IHTMLButton b = new IHTMLButton();

            b.appendChild(e);

            return b;
        }

        #endregion









        [Obsolete]
        public static IHTMLButton Create(string p, System.Action h)
        {
            var b = new IHTMLButton(p);

            b.onclick += (e) => Helper.Invoke(h);
            b.AttachToDocument();

            return b;
        }


        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\ToolStrip\ToolStripButton.cs
        public static implicit operator IHTMLButton(Type x)
        {
            return new IHTMLButton { className = x.Name };
        }

        public static implicit operator IHTMLButton(XElement x)
        {
            // what if its not a button?
            return (IHTMLButton)x.AsHTMLElement();
        }

        public static implicit operator IHTMLButton(string x)
        {
            // what if its not a button?
            return new IHTMLButton { innerText = x };
        }






        #region async
        [Script]
        public new class Tasks : IHTMLElement.Tasks<IHTMLButton>
        {

            [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
            public Task<IEvent> onclick
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var i = that;

                    var y = new TaskCompletionSource<IEvent>();
                    //i.InvokeOnComplete(y.SetResult);

                    var old = new { i.disabled };

                    i.disabled = false;

                    i.onclick +=
                        e =>
                        {
                            if (old == null)
                                return;

                            i.disabled = old.disabled;

                            old = null;

                            y.SetResult(e);
                        };

                    return y.Task;
                    //return y.Task.GetAwaiter();
                }
            }
        }

        [System.Obsolete("is this the best way to expose events as async?")]
        public new Tasks async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks { that = this };
            }
        }
        #endregion
    }
}
