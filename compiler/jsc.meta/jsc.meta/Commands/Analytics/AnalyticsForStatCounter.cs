using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Commands.Rewrite;
using System.Net;
using jsc.meta.Library.Web;
using System.Net.Sockets;
using System.Reflection;
using System.Diagnostics;

namespace jsc.meta.Commands.Analytics
{
	public class AnalyticsForStatCounter
	{
		public FileInfo assembly;

		public DirectoryInfo staging;

		// we should implement our own analytics


		public AnalyticsForStatCounterArguments counter = new AnalyticsForStatCounterArguments
		{
			// do not reuse these parameters in your applications!
			// public stats: http://my.statcounter.com/project/standard/stats.php?project_id=5203272&guest=1
			// http://my.statcounter.com/project/standard2/csv/download_log_file.php?project_id=5203272


			sc_project = "5203272",
			security = "94d6fb4a",

		};

		public void Invoke()
		{
			// see: http://www.statcounter.com/terms.html
			// until we can merge we might want to just
			// create an analytic wrapper instead of rewrite
			// which is not fully there yet
			Debugger.Launch();

			AnalyticsForStatCounterImplementation.Invoke(
				new AnalyticsForStatCounterArguments
				{
					sc_project = counter.sc_project,
					security = counter.security,
					assembly = Assembly.GetExecutingAssembly().GetName().Name
				}
			);

			new RewriteToAssembly
			{
				assembly = assembly,
				staging = staging,
				codeinjecton = new Action<AnalyticsForStatCounterArguments>(AnalyticsForStatCounterImplementation.Invoke),
				codeinjectonparams =
					_assembly => new object[] 
					{ 
						new AnalyticsForStatCounterArguments
						{
							sc_project = counter.sc_project,
							security = counter.security,
							assembly = _assembly.GetName().Name
						}
					}
			}.Invoke();



		}

		public class AnalyticsForStatCounterArguments
		{
			public string sc_project;
			public string security;
			public string assembly;
		}

		public static class AnalyticsForStatCounterImplementation
		{
			// this type will be copied to the user assembly

			public static void Invoke(AnalyticsForStatCounterArguments a)
			{
				// if we are using any types here we need to copy them
				// to local assembly too

				Console.WriteLine("analytics: ");
				Console.WriteLine("  sc_project: " + a.sc_project);
				Console.WriteLine("  security: " + a.security);
				Console.WriteLine("  assembly: " + a.assembly);

				// we are going to assume we have access to BCL
				// TcpClient and no more

				// any type we reference should be outside jsc.meta...

				var uri = new Uri(
						"http://c.statcounter.com/" + a.sc_project + @"/0/" + a.security + "/0/"
				);

				var t = new TcpClient();

				t.Connect(uri.Host, uri.Port);

				//var w = new StreamWriter(t.GetStream());

				var w = new StringBuilder();

				w.AppendLine("GET" + " " + uri.PathAndQuery + " HTTP/1.1");
				w.AppendLine("Host: " + uri.Host);
				w.AppendLine("Connection: Close");

				// http://www.botsvsbrowsers.com/
				w.Append(@"User-Agent: jsc
Referer:  http://analytics.zproxybuzz.info/AnalyticsForStatCounter/?t=" + DateTime.Now + @"&assembly=" + a.assembly + @"
Accept:  */*
Accept-Encoding:  gzip,deflate
Accept-Language:  et-EE,et;q=0.8,en-US;q=0.6,en;q=0.4
Accept-Charset:  windows-1257,utf-8;q=0.7,*;q=0.3
");
				w.AppendLine();

				var data = Encoding.UTF8.GetBytes(w.ToString());

				t.GetStream().Write(data, 0, data.Length);

				// it will take up to a minute to show up
				t.Close();
			}
		}
	}
}
