using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{

    [Script(Implements = typeof(XNode))]
    internal class __XNode : __XObject
    {
        [Obsolete("not available for web workers?")]
        internal INode InternalValue;

        public override string ToString()
        {
            return IXMLDocument.ToXMLString(InternalValue);
        }

        internal override XElement InternalGetParent()
        {
            return (XElement)(object)new __XElement
            {
                InternalValue = this.InternalValue.parentNode
            };
        }

        public void Remove()
        {
            this.InternalValue.parentNode.removeChild(InternalValue);
        }


        public void __adoptNode(__XNode e)
        {
            #region we need this workaround to bring html alive

            if (Expando.InternalIsMember(this.InternalValue, "innerHTML"))
            {
                if (!Expando.InternalIsMember(e.InternalValue, "innerHTML"))
                {
                    // adding XElement into HTML aren't we?

                    var swap = new IHTMLDiv();

                    // will this trigger scripts?
                    //swap.innerHTML = e.ToString();
                    swap.innerHTML = IXMLDocument.ToXMLString(e.InternalValue);

                    e.InternalValue = swap.firstChild;
                    return;
                }
            }
            #endregion

            if (e.InternalValue.ownerDocument != this.InternalValue.ownerDocument)
            {
                var ownerDocument = this.InternalValue.ownerDocument;

                try
                {
                    // IE does not implement adoptNode yet
                    e.InternalValue = ownerDocument.adoptNode(e.InternalValue);
                }
                catch
                {

                }
            }
        }

        public void ReplaceWith(object content)
        {
            var x = (content as __XNode);
            if (x != null)
            {



                __adoptNode(x);

                this.InternalValue.parentNode.replaceChild(
                    x.InternalValue,
                    this.InternalValue
                );
            }
        }

    }
}
