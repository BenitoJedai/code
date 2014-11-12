using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // https://github.com/mono/mono/blob/master/mcs/class/System.Xml.Linq/System.Xml.Linq/XNode.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XNode.cs

    [Script(Implements = typeof(XNode))]
    internal class __XNode : __XObject
    {
        [Obsolete("not available for web workers?")]
        internal INode InternalValue;

        public virtual void InternalValueInitialize()
        {

        }

        public override void InternalAddChanged(EventHandler<XObjectChangeEventArgs> e)
        {
            //X:\jsc.svn\examples\javascript\Test\TestXMLChangedEvent\TestXMLChangedEvent
            Console.WriteLine("Hello from internalAddChanged");
           
            new MutationObserver(
              new MutationCallback(
                  (MutationRecord[] mutations, MutationObserver observer) =>
                  {
                      //Console.WriteLine("Mutations len " + mutations.Length);

                      foreach (var m in mutations)
                      {
                          if (m.type == "childList")
                          {
                              //Console.WriteLine("Added mutation");

                              foreach (var addedNode in m.addedNodes)
                              {
                                  if (addedNode.nodeType == INode.NodeTypeEnum.ElementNode)
                                  {
                                      var addedElement = ((IHTMLElement)addedNode).AsXElement();

                                      e(addedElement, (XObjectChangeEventArgs)(object)new __XObjectChangeEventArgs(XObjectChange.Add));

                                  }
                              }
                          }
                          else if (m.type == "attributes")
                          {
                              //Console.WriteLine("Attribute mutation");
                              var target = ((IHTMLElement)(m.target)).AsXElement();
                              var attr = target.Attribute(m.attributeName);
                              e(attr, (XObjectChangeEventArgs)(object)new XObjectChangeEventArgs(XObjectChange.Value));
                          }
                          else if (m.type == "characterData")
                          {
                              //Console.WriteLine("Content mutation");
                              e(this, (XObjectChangeEventArgs)(object)new XObjectChangeEventArgs(XObjectChange.Name));
                          }
                          //else if (m.type == "subtree")
                          //{
                          //    e(this, (XObjectChangeEventArgs)(object)new XObjectChangeEventArgs(XObjectChange.Value));
                          //}
                      }
                  }
              )
          ).observe(InternalValue,
              new
              {
                  // Set to true if mutations to target's children are to be observed.
                  childList = true,
                  // Set to true if mutations to target's attributes are to be observed. Can be omitted if attributeOldValue and/or attributeFilter is specified.
                  attributes = true,
                  // Set to true if mutations to target's data are to be observed. Can be omitted if characterDataOldValue is specified.
                  characterData = true,
                  // Set to true if mutations to not just target, but also target's descendants are to be observed.
                  subtree = true,
              }
          );

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
