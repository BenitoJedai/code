using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(XContainer))]
    internal abstract class __XContainer : __XNode
    {
        public __XName InternalElementName;

        public IXMLElement InternalElement
        {
            get
            {
                IXMLElement e = (IXMLElement)this.InternalValue;
                return e;
            }
        }





        #region Add
        public void Add(params object[] content)
        {
            foreach (var item in content)
            {
                this.Add(item);
            }
        }

        public void Add(object content)
        {
            if (this.InternalValue == null)
            {
                if (Native.window == null)
                {

                    // what if we are running in a web worker?
                    // then we dont have the DOM xml available!
                    // tested by
                    // X:\jsc.svn\examples\javascript\Test\TestSolutionBuilder\TestSolutionBuilderV1\Application.cs
                }
                else
                {



                    var doc = new IXMLDocument(this.InternalElementName.LocalName);


                    this.InternalValue = doc.documentElement;
                }
            }

            #region string
            {
                var e = content as string;

                if (e != null)
                {
                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                    }
                    else
                    {
                        this.InternalValue.appendChild(
                            this.InternalValue.ownerDocument.createTextNode(e)
                        );
                    }

                    return;
                }
            }
            #endregion

            #region XText
            {
                var e = content as XText;

                if (e != null)
                {
                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                    }
                    else
                    {

                        this.InternalValue.appendChild(
                            this.InternalValue.ownerDocument.createTextNode(e.Value)
                        );
                    }

                    return;
                }
            }
            #endregion


            #region XComment
            {
                var e = content as XComment;

                if (e != null)
                {
                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                    }
                    else
                    {

                        this.InternalValue.appendChild(
                            this.InternalValue.ownerDocument.createComment(e.Value)
                        );
                    }

                    return;
                }
            }
            #endregion


            #region XAttribute
            {
                var e = (__XAttribute)(object)(content as XAttribute);
                if (e != null)
                {
                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                    }
                    else
                    {

                        var CurrentValue = e.Value;

                        e.InternalElement = this;
                        e.Value = CurrentValue;
                    }

                    return;
                }
            }
            #endregion


            #region XElement
            {
                var e = (__XElement)(object)(content as XElement);
                if (e != null)
                {
                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                    }
                    else
                    {

                        if (e.InternalValue == null)
                        {
                            e.InternalValue = this.InternalValue.ownerDocument.createElement(e.InternalElementName.LocalName);
                        }
                        else
                        {
                            __adoptNode(e);
                        }

                        this.InternalValue.appendChild(e.InternalValue);
                    }

                    return;
                }
            }
            #endregion


            // what is it?
            throw new NotImplementedException();
        }
        #endregion



        public IEnumerable<XNode> Nodes()
        {
            return this.InternalElement.childNodes.Select(
                item =>
                {
                    if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.ElementNode)
                        return (XNode)(object)new __XElement(null, null) { InternalValue = item };

                    if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.TextNode)
                        return (XNode)(object)new __XText() { InternalValue = item };

                    if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.CommentNode)
                        return (XNode)(object)new __XComment(null) { InternalValue = item };

                    // what is it?

                    throw new NotImplementedException();
                }
            ).Where(k => k != null);
        }

        #region Elements
        public XElement Element(XName name)
        {
            return Elements(name).FirstOrDefault();
        }

        public IEnumerable<XElement> Elements(XName name)
        {
            //Console.WriteLine("Elements " + new { name });
            return this.Elements().Where(
                k =>
                {
                    //Console.WriteLine("Elements " + new { name, k.Name.LocalName });

                    return k.Name.LocalName == name.LocalName;
                }
            );
        }

        public IEnumerable<XElement> Elements()
        {
            var e = InternalElement;
            var a = new List<XElement>();

            foreach (var item in e.childNodes)
            {
                if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.ElementNode)
                {
                    var x = new __XElement(null, null) { InternalValue = item };

                    a.Add(x);
                }

            }

            return a;

        }
        #endregion

        public void RemoveNodes()
        {
            if (this.InternalElement == null)
                return;

            var p = this.InternalElement.firstChild;

            while (p != null)
            {
                this.InternalElement.removeChild(p);

                p = this.InternalElement.firstChild;
            }
        }
    }
}
