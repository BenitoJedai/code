using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.xml.transform;
using javax.xml.transform.dom;
using java.io;
using javax.xml.transform.stream;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XNode))]
	internal class __XNode : __XObject
	{
		public org.w3c.dom.Node InternalValue;

        virtual protected void InternalEnsureElement()
        {
        }

		public override string ToString()
		{
			// http://faq.javaranch.com/java/DocumentToString
			var r = default(string);

			try
			{
                this.InternalEnsureElement();

				Source source = new DOMSource(this.InternalValue);
				StringWriter stringWriter = new StringWriter();
				Result result = new StreamResult(stringWriter);
				TransformerFactory factory = TransformerFactory.newInstance();

                
				Transformer transformer = factory.newTransformer();

				transformer.transform(source, result);

				r = stringWriter.getBuffer().toString();

                var IsDocument = (this is __XDocument);
                if (!IsDocument)
                {
                    // hack.

                    {
                        var prefix = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

                        if (r.StartsWith(prefix))
                            r = r.Substring(prefix.Length);
                    }

                    {
                        var prefix = "\r\n";

                        if (r.StartsWith(prefix))
                            r = r.Substring(prefix.Length);
                    }
                }
			}
			catch // (csharp.ThrowableException exc)
			{
                // The input node can not be null for a DOMSource for newTemplates!

                //throw new NotSupportedException(exc.Message);

                throw;
            }

			return r;
		}

	}
}
