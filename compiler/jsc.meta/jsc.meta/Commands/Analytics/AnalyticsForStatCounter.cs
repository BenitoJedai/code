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
using jsc.Library.Analytics;

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
			//Debugger.Launch();

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

				rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
					"jsc->" +  Path.GetFileNameWithoutExtension( assembly.Name)
				},

				merge = new RewriteToAssembly.MergeInstruction[] {
					"jsc"
				},


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

	
	}
}
