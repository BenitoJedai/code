using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLButton : IHTMLElement
    {
        public bool disabled;

        #region Constructor
        
            public IHTMLButton()
            {
                // InternalConstructor
            }
            
            static IHTMLButton InternalConstructor()
            {
                return (IHTMLButton)((object)new IHTMLElement(HTMLElementEnum.button));
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


        public static IHTMLButton Create(string p, EventHandler h)
        {
            var b = new IHTMLButton(p);

            b.onclick += ( e) => Helper.Invoke(h);
            b.attachToDocument();

            return b;
        }
    }
}
