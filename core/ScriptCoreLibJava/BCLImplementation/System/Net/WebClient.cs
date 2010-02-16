using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
	[Script(Implements = typeof(global::System.Net.WebClient))]
	internal class __WebClient : __Component
	{
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
	}
}
