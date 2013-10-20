using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.DOM.XML
{
	/// <summary>
	/// http://www.howtocreate.co.uk/tutorials/jsexamples/importingXML.html
	/// http://www.faqts.com/knowledge_base/view.phtml/aid/34769/fid/616
	/// http://webfx.eae.net/dhtml/xmlextras/demo.html
	/// http://sarissa.sourceforge.net/doc/
	/// </summary>
	[Script(InternalConstructor = true)]
	public class IXMLDocument : IDocument<IXMLElement>
	{
		public IXMLDocument(string name) { }

		// http://weblogs.asp.net/ssargent/archive/2007/01/25/selectsinglenode-and-firefox.aspx

		[Script(HasNoPrototype = true)]
		class __IXMLDocument_Native
		{
			public INode selectSingleNode(string path)
			{
				return default(INode);
			}

			public INode[] selectNodes(string path)
			{
				return default(INode[]);

			}
		}

		[Script(DefineAsStatic = true)]
		public INode selectSingleNode(string path)
		{
			var native = ((__IXMLDocument_Native)(object)this);

			if (IActiveX.IsSupported)
				return native.selectSingleNode(path);

			// opera: http://www.opera.com/docs/changelogs/windows/902/
			if (Expando.InternalIsMember(this, "selectSingleNode"))
				return native.selectSingleNode(path);

			return (INode)
				new IFunction("elementPath", @"
       var xpe = new XPathEvaluator();
           var nsResolver = xpe.createNSResolver( this.ownerDocument == null ? this.documentElement : this.ownerDocument.documentElement);
           var results = xpe.evaluate(elementPath,this,nsResolver,XPathResult.FIRST_ORDERED_NODE_TYPE, null);
           return results.singleNodeValue;             
            ").apply(this, path);
		}

		public INode[] selectNodes(string path)
		{
			var native = ((__IXMLDocument_Native)(object)this);

			if (IActiveX.IsSupported)
				return native.selectNodes(path);

			if (Expando.InternalIsMember(this, "selectNodes"))
				return native.selectNodes(path);

			return (INode[])
				new IFunction("sXPath", @"
    var oEvaluator = new XPathEvaluator();
    var oResult = oEvaluator.evaluate(sXPath, this, null, XPathResult.ORDERED_NODE_ITERATOR_TYPE, null);
    var aNodes = new Array();

    if (oResult != null) 
    {
        var oElement = oResult.iterateNext();

        while(oElement) 
        {
            aNodes.push(oElement);
            oElement = oResult.iterateNext();
        }
    }

    return aNodes;
").apply(this, path);

		}



		static public IXMLDocument InternalConstructor(string name)
		{
			// http://social.msdn.microsoft.com/Forums/en-US/jscript/thread/fc6618f1-5130-47de-9840-c66af68d6c85
			var r = default(IXMLDocument);

			if (IActiveX.IsSupported)
			{
				r = (IXMLDocument)IActiveX.TryCreate(
					//"Microsoft.XMLDOM"
					"msxml2.DOMDocument.6.0"
				);
				
				var root = r.createElement(name);

				r.documentElement = root;
			}
			else
			{
				r = (IXMLDocument)Native.Document.implementation.createDocument("", name, null);
			}

			if (r == null)
				throw new System.NotSupportedException();

			return r;
		}




        // ie 11 has XMLSerializer but does not support it
		[Script(OptimizedCode = @"

  if (typeof node.xml != 'undefined') {
    return node.xml;
  }

  if (typeof XMLSerializer != 'undefined') {
    return new XMLSerializer().serializeToString(node);
  }

    return '';
")]
		public static string ToXMLString(INode node)
		{
			return default(string);
		}

		[Script(OptimizedCode = @"

 var xmlDocument = null;
  if (typeof DOMParser != 'undefined') {
    xmlDocument = new DOMParser().parseFromString(xml,
'application/xml');
  }
  else if (typeof ActiveXObject != 'undefined') {
    /*@cc_on @*/
    /*@if (@_jscript_version >= 5)
    try {
      xmlDocument = new ActiveXObject('Microsoft.XMLDOM');
      xmlDocument.loadXML(xml);
    }
    catch (e) { }
    @end @*/  
  }
  return xmlDocument;
")]
		public static IXMLDocument Parse(string xml)
		{
			return default(IXMLDocument);
		}

		[Script(DefineAsStatic = true)]
		public string ToXMLString()
		{
			return IXMLDocument.ToXMLString(documentElement);
		}
	}
}
