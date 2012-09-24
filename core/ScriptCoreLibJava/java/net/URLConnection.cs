using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using java.io;

namespace java.net
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/net/URLConnection.html
    // http://developer.android.com/reference/java/net/URLConnection.html
	[Script(IsNative = true)]
	public abstract class URLConnection
	{
		/// <summary>
		/// Returns the value of the named header field.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string getHeaderField(string name)
		{
			return default(string);
		}

        public InputStream getInputStream()
        {
            return default(InputStream);
        }

        public OutputStream getOutputStream()
        {
            return default(OutputStream);
        }

        public void addRequestProperty(string key, string value)
        {
        }

        public void setDoOutput(bool dooutput)
        {
        }

        public void setDoInput(bool doinput)
        {
        }

        public void setRequestProperty(string key, string value)
        {
        }

        public void setUseCaches(bool usecaches)
        {
        }


	}
}
