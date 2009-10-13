using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Commands.Rewrite;
using System.Net;
using jsc.meta.Library.Web;

namespace jsc.meta.Commands.Analytics
{
	public class AnalyticsForStatCounter
	{
		public FileInfo assembly;

		public DirectoryInfo staging;

		// we should implement our own analytics
		
		public class StatCounterParameters
		{
			// do not reuse these parameters in your applications!
			// public stats: http://my.statcounter.com/project/standard/stats.php?project_id=5203272&guest=1
			// http://my.statcounter.com/project/standard2/csv/download_log_file.php?project_id=5203272

			public string sc_project = "5203272";
			public string security = "94d6fb4a";
		}

		public StatCounterParameters counter = new StatCounterParameters();

		public void Invoke()
		{
			// see: http://www.statcounter.com/terms.html

			var AnalyticsGetImage = new Uri(
					"http://c.statcounter.com/" + counter.sc_project + @"/0/" + counter.security + "/0/"
			);

			var c = new BasicWebCrawler(AnalyticsGetImage.Host, AnalyticsGetImage.Port);

			c.HeaderWriter +=
				w =>
				{
					w.Write(@"UserAgent: Mozilla/5.0
Referer:  http://analytics.zproxybuzz.info/AnalyticsForStatCounter/" + this.assembly.Name + "?t=" + DateTime.Now + @"
Accept:  */*
Accept-Encoding:  gzip,deflate
Accept-Language:  et-EE,et;q=0.8,en-US;q=0.6,en;q=0.4
Accept-Charset:  windows-1257,utf-8;q=0.7,*;q=0.3
");
				};

			c.Crawl(AnalyticsGetImage.PathAndQuery);

			new RewriteToAssembly
			{
				assembly = assembly,
				staging = staging,
				codeinjecton = new Action<string, string>(AnalyticsForStatCounterImplementation.Invoke),
				codeinjectonparams = new object[] { counter.sc_project, counter.security }
			}.Invoke();



		}

		public static class AnalyticsForStatCounterImplementation
		{
			// this type will be copied to the user assembly

			public static void Invoke(string sc_project, string security)
			{
				// if we are using any types here we need to copy them
				// to local assembly too

				Console.WriteLine("codeinjection: ");
				Console.WriteLine("sc_project: " + sc_project);
				Console.WriteLine("security: " + security);

				// we are going to assume we have access to BCL
				// TcpClient and no more

				// any type we reference should be outside jsc.meta...
			}
		}
	}
}
