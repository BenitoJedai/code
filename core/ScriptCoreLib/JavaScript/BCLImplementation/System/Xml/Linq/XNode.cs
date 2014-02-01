using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{

    [Script(Implements = typeof(XNode))]
    internal class __XNode : __XObject
    {
        [Obsolete("not available for web workers?")]
        internal INode InternalValue;

        public virtual void InternalValueInitialize()
        {
 
        }

        public override string ToString()
        {
            InternalValueInitialize();

            // X:\jsc.svn\examples\javascript\svg\SVGCSSContent\SVGCSSContent\Application.cs

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


        //public void __adoptNode(__XNode e)
        //{
        //    if (e.InternalValue.ownerDocument == this.InternalValue.ownerDocument)
        //        return;


        //    // is ie too helpful
        //    // and returns html document from a xmlhttprequest?



        //    #region we need this workaround to bring html alive

        //    if (Expando.InternalIsMember(this.InternalValue, "innerHTML"))
        //        if (!Expando.InternalIsMember(e.InternalValue, "innerHTML"))
        //        {
        //            // adding XElement into HTML aren't we?

        //            var swap = new IHTMLDiv();

        //            // will this trigger scripts?
        //            //swap.innerHTML = e.ToString();
        //            swap.innerHTML = IXMLDocument.ToXMLString(e.InternalValue);

        //            e.InternalValue = swap.firstChild;
        //            return;
        //        }

        //    #endregion

        //}


        internal static void InternalRebuildDocument(__XNode that, __XElement IncomingXElement)
        {
            if (IncomingXElement.InternalValue.ownerDocument == that.InternalValue.ownerDocument)
                return;
            // due to IE!
            //Console.WriteLine(" ok, force import manually. swap documents");


            var IncomingXElementAttributes = IncomingXElement.Attributes().Select(a => new { a.Name, a.Value }).ToArray();
            var IncomingXElementNodes = IncomingXElement.Nodes().ToArray();

            // first reset the underlying node

            IncomingXElement.InternalValue = that.InternalValue.ownerDocument.createElement(
                  IncomingXElement.InternalValue.nodeName
            );

            foreach (var item in IncomingXElementAttributes)
            {
                IncomingXElement.Add(
                    new XAttribute(item.Name, item.Value)
                );
            }

            IncomingXElement.Add(IncomingXElementNodes);

            //Console.WriteLine(" ok, force import manually done!");
        }

        public void ReplaceWith(object content)
        {
            // http://msdn.microsoft.com/en-us/library/system.xml.linq.xnode.replacewith.aspx
            // what if this or content is anything but __XNode?

            var that = this as __XNode;
            if (that != null)
            {

                var IncomingXElement = (content as __XElement);
                if (IncomingXElement != null)
                {

                    var parentNode = this.InternalValue.parentNode;

                    __XContainer.InternalRebuildDocument(that, IncomingXElement);


                    parentNode.replaceChild(
                        IncomingXElement.InternalValue,
                        that.InternalValue
                    );
                }
            }
        }

    }
}
