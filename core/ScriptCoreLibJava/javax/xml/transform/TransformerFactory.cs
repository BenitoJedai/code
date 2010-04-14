// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.TransformerFactory

using ScriptCoreLib;
using java.lang;

namespace javax.xml.transform
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/TransformerFactory.html
	[Script(IsNative = true)]
	public abstract class TransformerFactory
	{
		/// <summary>
		/// Default constructor is protected on purpose.
		/// </summary>
		public TransformerFactory()
		{
		}

		/// <summary>
		/// Get the stylesheet specification(s) associated
		/// via the xml-stylesheet processing instruction (see
		/// http://www.w3.org/TR/xml-stylesheet/) with the document
		/// document specified in the source parameter, and that match
		/// the given criteria.
		/// </summary>
		abstract public Source getAssociatedStylesheet(Source @source, string @media, string @title, string @charset);

		/// <summary>
		/// Allows the user to retrieve specific attributes on the underlying
		/// implementation.
		/// </summary>
		abstract public object getAttribute(string @name);

		/// <summary>
		/// Get the error event handler for the TransformerFactory.
		/// </summary>
		abstract public ErrorListener getErrorListener();

		/// <summary>
		/// Look up the value of a feature.
		/// </summary>
		abstract public bool getFeature(string @name);

		/// <summary>
		/// Get the object that is used by default during the transformation
		/// to resolve URIs used in document(), xsl:import, or xsl:include.
		/// </summary>
		abstract public URIResolver getURIResolver();

		/// <summary>
		/// Obtain a new instance of a <code>TransformerFactory</code>.
		/// </summary>
		static public TransformerFactory newInstance()
		{
			return default(TransformerFactory);
		}

		/// <summary>
		/// Process the Source into a Templates object, which is a
		/// a compiled representation of the source.
		/// </summary>
		abstract public Templates newTemplates(Source @source);

		/// <summary>
		/// Create a new Transformer object that performs a copy
		/// of the source to the result.
		/// </summary>
		abstract public Transformer newTransformer();

		/// <summary>
		/// Process the Source into a Transformer object.
		/// </summary>
		abstract public Transformer newTransformer(Source @source);

		/// <summary>
		/// Allows the user to set specific attributes on the underlying
		/// implementation.
		/// </summary>
		abstract public void setAttribute(string @name, object @value);

		/// <summary>
		/// Set the error event listener for the TransformerFactory, which
		/// is used for the processing of transformation instructions,
		/// and not for the transformation itself.
		/// </summary>
		abstract public void setErrorListener(ErrorListener @listener);

		/// <summary>
		/// Set an object that is used by default during the transformation
		/// to resolve URIs used in xsl:import, or xsl:include.
		/// </summary>
		abstract public void setURIResolver(URIResolver @resolver);

	}
}
