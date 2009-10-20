using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.Library.Analytics
{
	public static class AnalyticsForStatCounterImplementation
	{
		// this type will be copied to the user assembly

		public static void Invoke(AnalyticsForStatCounterArguments a)
		{
			// if we are using any types here we need to copy them
			// to local assembly too

			Console.WriteLine("analytics: ");
			//Console.WriteLine("  sc_project: " + a.sc_project);
			//Console.WriteLine("  security: " + a.security);
			Console.WriteLine("  assembly: " + a.assembly);

			// we are going to assume we have access to BCL
			// TcpClient and no more

			// any type we reference should be outside jsc.meta...

			new TrivialWebRequest
			{
				Referer = "http://analytics.zproxybuzz.info/AnalyticsForStatCounter/?t=" + DateTime.Now + @"&assembly=" + a.assembly,
				Target = a.ToUri()
			}.Invoke();

		}
	}

}
