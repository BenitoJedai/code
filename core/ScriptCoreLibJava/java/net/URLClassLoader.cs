using ScriptCoreLib;

using java.security;
using java.net;
using java.lang;
using java.util;

namespace java.net
{
	// http://java.sun.com/j2se/1.5.0/docs/api/java/net/URLClassLoader.html
	/// <summary>
	/// This class loader is used to load classes and resources from a search
	/// path of URLs referring to both JAR files and directories. Any URL that
	/// ends with a '/' is assumed to refer to a directory. Otherwise, the URL
	/// is assumed to refer to a JAR file which will be opened as needed.
	/// </summary>
	[Script(IsNative=true)]
	public class URLClassLoader : SecureClassLoader
	{
		#region nested types


		#endregion

		#region fields


		#endregion

		#region constructors

		/// <summary>
		/// Constructs a new URLClassLoader for the specified URLs using the
		/// default delegation parent <code>ClassLoader</code>.
		/// </summary>
		public URLClassLoader(params URL[] @urls)
		{
		}
        ///// <summary>
        ///// Constructs a new URLClassLoader for the specified URLs, parent
        ///// class loader, and URLStreamHandlerFactory.
        ///// </summary>
        //public URLClassLoader(URL[] @urls, ClassLoader @parent, URLStreamHandlerFactory @factory)
        //{
        //}
		/// <summary>
		/// Constructs a new URLClassLoader for the given URLs.
		/// </summary>
		public URLClassLoader(URL[] @urls, ClassLoader @parent)
		{
		}

		#endregion

		#region methods

		/// <summary>
		/// Creates a new instance of URLClassLoader for the specified
		/// URLs and default parent class loader.
		/// </summary>
		public virtual URLClassLoader newInstance(params URL[] @urls)
		{
			return default(URLClassLoader);
		}

		/// <summary>
		/// Creates a new instance of URLClassLoader for the specified
		/// URLs and parent class loader.
		/// </summary>
		public virtual URLClassLoader newInstance(URL[] @urls, ClassLoader @parent)
		{
			return default(URLClassLoader);
		}

		/// <summary>
		/// Finds the resource with the specified name on the URL search path.
		/// </summary>
		public virtual URL findResource(string @name)
		{
			return default(URL);
		}

		/// <summary>
		/// Returns an Enumeration of URLs representing all of the resources
		/// on the URL search path having the specified name.
		/// </summary>
		public virtual Enumeration findResources(string @name)
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Returns the search path of URLs for loading classes and resources.
		/// </summary>
		public virtual URL[] getURLs()
		{
			return default(URL[]);
		}


		#endregion

	}
}
