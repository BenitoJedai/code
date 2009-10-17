using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.Library.Analytics
{
	public class AnalyticsForStatCounterArguments
	{
		public string sc_project;
		public string security;
		public string assembly;

		public Uri ToUri()
		{
			return new Uri(
				"http://c.statcounter.com/" + this.sc_project + @"/0/" + this.security + "/0/"
			);
		}
	}

}
