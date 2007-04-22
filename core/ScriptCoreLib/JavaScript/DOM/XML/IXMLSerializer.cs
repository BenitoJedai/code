using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;


namespace ScriptCoreLib.JavaScript.DOM.XML
{
    using TypeNameResolver = Expando.TypeNameResolver;

    /// <summary>
    /// serializes to and from c# xml structure
    /// </summary>
    /// <typeparam name="TRoot"></typeparam>
    [Script]
    public class IXMLSerializer<TRoot> 
        where TRoot : class, new()
    {
        Expando KnownTypes = new Expando();

        public IXMLSerializer(params object[] k)
        {
            foreach (object x in k)
            {
                Expando o = Expando.Of(x);

                KnownTypes.SetMember(o.TypeMetaName, o);
            }
        }

        void SerializeTo(IXMLDocument doc, IXMLElement r, Expando x)
        {
            ExpandoMember[] c = x.GetFields();

            foreach (ExpandoMember m in c)
            {

                IXMLElement n = new IXMLElement(doc, m.Name);

                if (m.Self.IsString || m.Self.IsNumber)
                {
                    n.appendChild(new ITextNode(doc, m.Value));
                }
                else
                {
                    if (m.Self.IsBoolean)
                    {
                        n.appendChild(new ITextNode(doc, m.Value));
                    }
                    else if (m.Self.IsArray)
                    {
                        Expando[] a = m.Self.To<Expando[]>();

                        foreach (Expando i in a)
                        {
                            IXMLElement an = new IXMLElement(doc, i.TypeMetaName);

                            SerializeTo(doc, an, i);

                            n.appendChild(an);
                        }
                    }
                    else if (m.Self.IsObject && !m.Self.IsNull)
                    {
                        SerializeTo(doc, n, m.Self);
                    }
                }

                r.appendChild(n);
            
            }
        }

        public IXMLDocument Serialize(TRoot e)
        {
            IXMLDocument doc = new IXMLDocument(Expando.Of(e).TypeMetaName);

            SerializeTo(doc, doc.documentElement, Expando.Of(e));

            return doc;
        }

        Expando DeserializeTo(IXMLElement r, string name)
        {
            if (KnownTypes[name] == null)
                return null;

            Expando z = KnownTypes[name].CreateType();


            foreach (IXMLElement x in r.childNodes)
            {
                if (x.nodeType == INode.NodeTypeEnum.ElementNode)
                {
                    if (z.Metadata[x.nodeName] == null)
                    {
                        z.SetMember(x.nodeName, x.innerXML);
                    }
                    else
                    {
                        if (z.Metadata[x.nodeName].IsArray)
                        {
                            IArray<object> a = new IArray<object>();

                            foreach (IXMLElement xx in x.childNodes)
                            {
                                if (xx.nodeType == INode.NodeTypeEnum.ElementNode)
                                {
                                    a += DeserializeTo(xx, xx.nodeName);
                                }
                            }

                            z.SetMember(x.nodeName, a);
                        }
                        else
                        {
                            z.SetMember(x.nodeName, DeserializeTo(x, z.Metadata[x.nodeName].GetValue()));
                        }
                    }
                }
            }

            return z;
        }

        public TRoot Deserialize(IXMLHttpRequest r)
        {
            if (r == null)
                return null;

            return Deserialize(r.responseXML);
        }

        public TRoot Deserialize(IXMLDocument doc)
        {
            if (doc == null)
                return null;

            return DeserializeTo(doc.documentElement, doc.documentElement.nodeName).To<TRoot>();
        }

        



    }
}
