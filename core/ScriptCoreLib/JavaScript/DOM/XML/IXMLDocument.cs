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
    [Script(InternalConstructor=true)]
    public class IXMLDocument : IDocument<IXMLElement>
    {
        public IXMLDocument(string name) { }
    
        public INode selectSingleNode(string path)
        {
            return default(INode);
        }

        public INode[] selectNodes(string path)
        {
            return default(INode[]);

        }

        [Script(OptimizedCode= @"
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





        [Script(OptimizedCode=@"

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

        [Script(OptimizedCode=@"

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
