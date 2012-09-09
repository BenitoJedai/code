using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.PHP.DOM;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(XContainer))]
    internal abstract class __XContainer : __XNode
    {
        public __XName InternalElementName;

        public DOMElement InternalElement
        {
            get
            {
                DOMElement e = (DOMElement)this.InternalValue;
                return e;
            }
        }

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
                var doc = new DOMDocument();

                var item = doc.createElement(this.InternalElementName.LocalName);
                doc.appendChild(item);

                this.InternalValue = doc.documentElement;
            }


            #region string
            {
                var e = content as string;

                if (e != null)
                {
                    var text = this.InternalElement.ownerDocument.createTextNode(e);

                    this.InternalElement.appendChild(text);
                    return;
                }
            }
            #endregion


            #region XAttribute
            {
                var e = (__XAttribute)(object)(content as XAttribute);
                if (e != null)
                {
                    var CurrentValue = e.Value;

                    e.InternalElement = this;
                    e.Value = CurrentValue;
                    return;
                }
            }
            #endregion

            #region XElement
            {
                var e = (__XElement)(object)(content as XElement);
                if (e != null)
                {
                    if (e.InternalElement == null)
                    {
                        e.InternalValue = this.InternalElement.ownerDocument.createElement(e.InternalElementName.LocalName);
                    }
                    else
                    {
                        __adoptNode(e);
                    }

                    this.InternalElement.appendChild(e.InternalElement);

                    return;
                }
            }
            #endregion

            throw new NotImplementedException();
        }

        private void __adoptNode(__XElement e)
        {
            if (e.InternalValue.ownerDocument != this.InternalValue.ownerDocument)
            {
                var ownerDocument = this.InternalValue.ownerDocument;

                try
                {
                    // IE does not implement adoptNode yet
                    e.InternalValue = ownerDocument.importNode(e.InternalValue, true);
                }
                catch
                {

                }
            }
        }

        #region Elements
        public XElement Element(XName name)
        {
            return Elements(name).FirstOrDefault();
        }

        public IEnumerable<XElement> Elements(XName name)
        {
            return this.Elements().Where(k => k.Name.LocalName == name.LocalName);
        }

        public IEnumerable<XElement> Elements()
        {
            var e = InternalElement;
            var a = new List<XElement>();

            for (int i = 0; i < e.childNodes.length; i++)
			{
                var item = e.childNodes.item(i);

                // http://www.php.net/manual/en/dom.constants.php
                if (item.nodeType == (int)ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.ElementNode)
                    a.Add(
                        (XElement)(object)new __XElement(null, null) { InternalValue = item }
                    );

            }

            return a;

        }
        #endregion

    }
}
