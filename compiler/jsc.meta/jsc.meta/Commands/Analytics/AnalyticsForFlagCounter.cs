using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jsc.meta.Library.Analytics;
using System.Reflection;
using jsc.meta.Commands.Rewrite;
using System.IO;

namespace jsc.meta.Commands.Analytics
{
	public class AnalyticsForFlagCounter
	{
		public FileInfo assembly;

		public DirectoryInfo staging;


		// <a href="http://s04.flagcounter.com/more/hGm0">
		// <img src="http://s04.flagcounter.com/count/hGm0/bg=FFFFFF/txt=000000/border=CCCCCC/columns=2/maxflags=12/viewers=0/labels=0/" alt="free counters" border="0"></a>

		public AnalyticsForFlagCounterArguments counter = new AnalyticsForFlagCounterArguments
		{
			id = "hGm0"
		};


		public void Invoke()
		{
			AnalyticsForFlagCounterImplementation.Invoke(
				new AnalyticsForFlagCounterArguments
				{
					id = this.counter.id,
					assembly = Assembly.GetExecutingAssembly().GetName().Name
				},
				null
			);

			new RewriteToAssembly
			{
				assembly = assembly,
				staging = staging,

				rename = new RewriteToAssembly.NamespaceRenameInstructions [] {
					"jsc.meta->" +  Path.GetFileNameWithoutExtension( assembly.Name)
				},

				merge = new [] {
					"jsc.meta"
				},

				codeinjecton = new Action<AnalyticsForFlagCounterArguments, TrivialWebRequest>(AnalyticsForFlagCounterImplementation.Invoke),
				codeinjectonparams =
					_assembly => new object[] 
					{ 
						new AnalyticsForFlagCounterArguments
						{
							id = counter.id,
							assembly = _assembly.GetName().Name
						},
						null
					}
			}.Invoke();
		}

		public class AnalyticsForFlagCounterImplementation
		{
			public static void Invoke(AnalyticsForFlagCounterArguments a, TrivialWebRequest r_)
			{
				Console.WriteLine("analytics: ");
				Console.WriteLine("  id: " + a.id);
				Console.WriteLine("  assembly: " + a.assembly);


				new TrivialWebRequest
				{
					Referer = "http://analytics.zproxybuzz.info/AnalyticsForFlagCounter/?t=" + DateTime.Now + @"&assembly=" + a.assembly,
					Target = a.ToUri()
				}.Invoke();
			}
		}

		public class AnalyticsForFlagCounterArguments
		{
			public string id;
			public string assembly;

			public Uri ToUri()
			{
				return new Uri(
					"http://s04.flagcounter.com/count/" + this.id + @"/bg=FFFFFF/txt=000000/border=CCCCCC/columns=2/maxflags=12/viewers=0/labels=0/"
				);
			}
		}
	}
}
