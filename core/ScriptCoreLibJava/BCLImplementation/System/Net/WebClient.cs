using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using java.net;
using java.io;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
	[Script(Implements = typeof(global::System.Net.WebClient))]
	internal class __WebClient : __Component
	{
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs

        public Encoding Encoding { get; set; }

        public WebHeaderCollection Headers { get; set; }

		public string DownloadString(string u)
		{
			return DownloadString(new Uri(u));
		}

		public string DownloadString(Uri u)
		{
			var w = new StringBuilder();

			try
			{
				var url = new java.net.URL(u.ToString());
				var i = new java.io.InputStreamReader(url.openStream(), "UTF-8");
				var reader = new java.io.BufferedReader(i);

				// can't we just read to the end?
				var line = reader.readLine();
				while (line != null)
				{
					w.AppendLine(line);

					line = reader.readLine();
				}
				reader.close();
			}
			catch
			{
				// oops
			}

			return w.ToString();
		}
        
        public string UploadString(string u, string method, string data)
        {
            return UploadString(new Uri(u), data, method);
        }
        
        public string UploadString(Uri u, string method, string data)
        {
            var w = new StringBuilder();

            HttpURLConnection conn = null;

            try
            {
                var url = new java.net.URL(u.ToString());

                conn = (HttpURLConnection)url.openConnection();
                conn.setDoOutput(true);
                conn.setDoInput(true);
                conn.setInstanceFollowRedirects(false);
                conn.setRequestMethod(method);
                conn.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
                conn.setRequestProperty("charset", "utf-8");
                conn.setRequestProperty("content-length", "" + data.Length);
                conn.setUseCaches(false);

                try
                {

                    if (Headers != null)
                    {
                        foreach (string key in Headers.AllKeys)
                            conn.addRequestProperty(key, Headers[key]);
                    }
                }
                catch (Exception e)
                {
                    //System.Console.WriteLine("ERROR: Failed to write headers. Exception was:" + e.Message);
                }

                DataOutputStream wr = new DataOutputStream(conn.getOutputStream());
                wr.writeBytes(data);
                wr.flush();
                wr.close();



                //var i = new java.io.InputStreamReader(url.openStream(), "UTF-8");
                var i = new java.io.InputStreamReader(conn.getInputStream(), "UTF-8");
                var reader = new java.io.BufferedReader(i);

                // can't we just read to the end?
                var line = reader.readLine();
                while (line != null)
                {
                    w.AppendLine(line);

                    line = reader.readLine();
                }
                reader.close();
            }
            catch
            {
                // oops
            }

            if (conn != null)
                conn.disconnect();

            return w.ToString();
        }


	}
}
