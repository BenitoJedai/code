using ScriptCoreLib;

using java.io;
using java.lang;

 
namespace java.net
{
    /// <summary>
    /// public final class URL
    /// </summary>
    [Script(IsNative = true)]
    public class URL
    {
        // Constructor Summary
        /// <summary>
        /// Creates a
        /// </summary>
        public URL(string spec)
        {
        }

        /// <summary>
        /// Creates a
        /// </summary>
        public URL(string protocol, string host, int port, string file)
        {
        }

        ///// <summary>
        ///// Creates a
        ///// </summary>
        //public URL(string protocol, string host, int port, string file, URLStreamHandler handler)
        //{
        //}

        /// <summary>
        /// Creates a URL from the specified
        /// </summary>
        public URL(string protocol, string host, string file)
        {
        }

        /// <summary>
        /// Creates a URL by parsing the given spec within a specified context.
        /// </summary>
        public URL(URL context, string spec)
        {
        }

        ///// <summary>
        ///// Creates a URL by parsing the given spec with the specified handler within a specified context.
        ///// </summary>
        //public URL(URL context, string spec, URLStreamHandler handler)
        //{
        //}

        //// Method Summary
        ///// <summary>
        ///// Compares this URL for equality with another object.
        ///// </summary>
        //public   bool   equals (object  obj)
        //{
        //    return default( bool  );
        //}

        /// <summary>
        /// Gets the authority part of this
        /// </summary>
        public   string   getAuthority ()
        {
            return default( string  );
        }

        /// <summary>
        /// Gets the contents of this URL.
        /// </summary>
        public   object   getContent ()
        {
            return default( object  );
        }

        /// <summary>
        /// Gets the contents of this URL.
        /// </summary>
        public   object   getContent (Class[] classes)
        {
            return default( object  );
        }

        /// <summary>
        /// Gets the default port number of the protocol associated with this
        /// </summary>
        public   int  getDefaultPort ()
        {
            return default( int );
        }

        /// <summary>
        /// Gets the file name of this
        /// </summary>
        public   string   getFile ()
        {
            return default( string  );
        }

        /// <summary>
        /// Gets the host name of this
        /// </summary>
        public   string   getHost ()
        {
            return default( string  );
        }

        /// <summary>
        /// Gets the path part of this
        /// </summary>
        public   string   getPath ()
        {
            return default( string  );
        }

        /// <summary>
        /// Gets the port number of this
        /// </summary>
        public   int  getPort ()
        {
            return default( int );
        }

        /// <summary>
        /// Gets the protocol name of this
        /// </summary>
        public   string   getProtocol ()
        {
            return default( string  );
        }

        /// <summary>
        /// Gets the query part of this
        /// </summary>
        public   string   getQuery ()
        {
            return default( string  );
        }

        /// <summary>
        /// Gets the anchor (also known as the "reference") of this
        /// </summary>
        public   string   getRef ()
        {
            return default( string  );
        }

        /// <summary>
        /// Gets the userInfo part of this
        /// </summary>
        public   string   getUserInfo ()
        {
            return default( string  );
        }

        /// <summary>
        /// Creates an integer suitable for hash table indexing.
        /// </summary>
        public   int  hashCode ()
        {
            return default( int );
        }

        ///// <summary>
        ///// Returns a
        ///// </summary>
        //public   URLConnection  openConnection ()
        //{
        //    return default( URLConnection );
        //}

        /// <summary>
        /// Opens a connection to this
        /// </summary>
        public   InputStream  openStream ()
        {
            return default( InputStream );
        }

        /// <summary>
        /// Compares two URLs, excluding the fragment component.
        /// </summary>
        public   bool   sameFile (URL other)
        {
            return default( bool  );
        }

        /// <summary>
        /// Sets the fields of the URL.
        /// </summary>
        public   void  set (string  protocol, string  host, int port, string  file, string  _ref)
        {
            return;
        }

        /// <summary>
        /// Sets the specified 8 fields of the URL.
        /// </summary>
        public   void  set (string  protocol, string  host, int port, string  authority, string  userInfo, string  path, string  query, string  _ref)
        {
            return;
        }

        ///// <summary>
        ///// Sets an application's
        ///// </summary>
        //public static  void  setURLStreamHandlerFactory (URLStreamHandlerFactory fac)
        //{
        //    return default( void );
        //}

        /// <summary>
        /// Constructs a string representation of this
        /// </summary>
        public   string   toExternalForm ()
        {
            return default( string  );
        }

        ///// <summary>
        ///// Constructs a string representation of this
        ///// </summary>
        //public   string   tostring ()
        //{
        //    return default( string  );
        //}


    }
}

