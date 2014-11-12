using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XContainer.cs

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
            InternalValueInitialize();


            #region int
            // X:\jsc.svn\examples\javascript\LINQ\test\vb\TestXMLSelect\TestXMLSelect\Application.vb

            //   i = !(b instanceof wNCFl85B5DOD64VaExqsNg);
            //if (content is int)
            if (ScriptCoreLib.JavaScript.Runtime.Expando.IsNativeNumberObject(content))
            {
                if (this.InternalValue == null)
                {
                    // web worker mode? do we need to store elements on our own?
                }
                else
                {
                    this.InternalValue.appendChild(
                        this.InternalValue.ownerDocument.createTextNode(Convert.ToString((int)content))
                    );
                }

                return;
            }
            #endregion


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
                var IncomingXElement = (__XElement)(object)(content as XElement);
                if (IncomingXElement != null)
                {
                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                    }
                    else
                    {
                        // X:\jsc.svn\examples\javascript\Test\TestAttachXElementToDocument\TestAttachXElementToDocument\Application.cs

                        if (IncomingXElement.InternalValue == null)
                        {
                            IncomingXElement.InternalValue = this.InternalValue.ownerDocument.createElement(
                                IncomingXElement.InternalElementName.LocalName
                            );

                            // missing content?
                        }

                        // http://stackoverflow.com/questions/1811116/ie-support-for-dom-importnode
                        // The solution to all of my problems was to not use a DOM method after all, and instead use my own implementation. Here, in all of its glory, is my final solution to the importNode() problem coded in a cross-browser compliant way: (Line wraps marked » —Ed.)

                        //__adoptNode(NewXElement);

                        __XContainer.InternalRebuildDocument(this, IncomingXElement);


                        //ie will complain. why?
                        // does it expect adobtion?

                        this.InternalValue.appendChild(IncomingXElement.InternalValue);
                    }

                    return;
                }
            }
            #endregion


            // what is it?
            throw new NotImplementedException();
        }


        public override void InternalValueInitialize()
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
        }


        #endregion



        public IEnumerable<XNode> Nodes()
        {
            return this.InternalElement.childNodes.Select(
                item =>
                {
                    //Console.WriteLine(new { item });

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
            // X:\jsc.svn\examples\javascript\Test\TestXElementAdd\TestXElementAdd\Application.cs

            Console.WriteLine("Elements " + new { name });

            return this.Elements().Where(
                k =>
                {
                    Console.WriteLine("Elements " + new { name, k.Name.LocalName });

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

        public IEnumerable<XElement> Descendants()
        {
            // X:\jsc.svn\examples\javascript\svg\DEAGELForecast\DEAGELForecast\Application.cs
            return this.Elements().SelectMany(k => k.DescendantsAndSelf());


        }

    }
}
