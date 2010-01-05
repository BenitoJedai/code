using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Net;

namespace OrcasMetaWebService1
{
	/// <summary>
	/// This class shows that we can even use DownloadString to work
	/// with external HTTP resources.
	/// </summary>
	public class ExampleCom 
	{
		public Uri Target
		{
			get
			{
				return new Uri("http://example.com");
			}
		}

		public string Text
		{
			get
			{
				var w = new WebClient();

				return w.DownloadString(this.Target);
			}
		}
	}
}
