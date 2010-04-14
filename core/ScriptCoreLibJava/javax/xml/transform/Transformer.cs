// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.transform.Transformer

using ScriptCoreLib;
using java.lang;
using java.util;

namespace javax.xml.transform
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/transform/Transformer.html
	[Script(IsNative = true)]
	public abstract class Transformer
	{
		/// <summary>
		/// Default constructor is protected on purpose.
		/// </summary>
		public Transformer()
		{
		}

		/// <summary>
		/// Clear all parameters set with setParameter.
		/// </summary>
		abstract public void clearParameters();

		/// <summary>
		/// Get the error event handler in effect for the transformation.
		/// </summary>
		abstract public ErrorListener getErrorListener();

		/// <summary>
		/// Get a copy of the output properties for the transformation.
		/// </summary>
		abstract public Properties getOutputProperties();

		/// <summary>
		/// Get an output property that is in effect for the
		/// transformation.
		/// </summary>
		abstract public string getOutputProperty(string @name);

		/// <summary>
		/// Get a parameter that was explicitly set with setParameter
		/// or setParameters.
		/// </summary>
		abstract public object getParameter(string @name);

		/// <summary>
		/// Get an object that will be used to resolve URIs used in
		/// document(), etc.
		/// </summary>
		abstract public URIResolver getURIResolver();

		/// <summary>
		/// Set the error event listener in effect for the transformation.
		/// </summary>
		abstract public void setErrorListener(ErrorListener @listener);

		/// <summary>
		/// Set the output properties for the transformation.
		/// </summary>
		abstract public void setOutputProperties(Properties @oformat);

		/// <summary>
		/// Set an output property that will be in effect for the
		/// transformation.
		/// </summary>
		abstract public void setOutputProperty(string @name, string @value);

		/// <summary>
		/// Add a parameter for the transformation.
		/// </summary>
		abstract public void setParameter(string @name, object @value);

		/// <summary>
		/// Set an object that will be used to resolve URIs used in
		/// document().
		/// </summary>
		abstract public void setURIResolver(URIResolver @resolver);

		/// <summary>
		/// Process the source tree to the output result.
		/// </summary>
		abstract public void transform(Source @xmlSource, Result @outputTarget);

	}
}
