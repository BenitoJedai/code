using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.net;
using java.io;

namespace OrcasJavaGoogleApplication.Server.Library
{
	[Script]
	public static class Extensions
	{
		public static string ToWebString(this Uri u)
		{
			var w = new StringBuilder();

			try
			{
				var url = new URL(u.ToString());
				var i = new InputStreamReader(url.openStream());
				var reader = new BufferedReader(i);


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
