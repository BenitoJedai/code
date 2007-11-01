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
        class __IXMLDocument_IE
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
            if (IActiveX.IsSupported)
                return ((__IXMLDocument_IE)(object)this).selectSingleNode(path);

            return (INode)
                new IFunction("elementPath", @"
       var xpe = new XPathEvaluator();
           var nsResolver = xpe.createNSResolver( this.ownerDocument == null ? this.documentElement : this.ownerDocument.documentElement);
           var results = xpe.evaluate(elementPath,this,nsResolver,XPathResult.FIRST_ORDERED_NODE_TYPE, null);
           return results.singleNodeValue;             
            ").apply( this, path);
        }

        public INode[] selectNodes(string path)
        {
            if (IActiveX.IsSupported)
                return ((__IXMLDocument_IE)(object)this).selectNodes(path);

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

        [Script(OptimizedCode = @"
try
{
            return document.implementation.createDocument('', name, null);
}
catch (ex)
{
var z = new ActiveXObject('Microsoft.XMLDOM');
    z.documentElement = z.createElement(name);

            return z;
}


        ")]
        static public IXMLDocument InternalConstructor(string name)
        {
            return default(IXMLDocument);
        }





        [Script(OptimizedCode = @"

  if (typeof XMLSerializer != 'undefined') {
    return new XMLSerializer().serializeToString(node);
  }
  else if (typeof node.xml != 'undefined') {
    return node.xml;
  }
  else {
    return '';
  }
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
