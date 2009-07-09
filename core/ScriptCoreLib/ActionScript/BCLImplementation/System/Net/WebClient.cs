using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using System.Net;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Net
{
	[Script(Implements = typeof(global::System.Net.WebClient))]
	internal class __WebClient
	{
		public event DownloadStringCompletedEventHandler DownloadStringCompleted;

		public void DownloadStringAsync(Uri address)
		{
			var request = new URLRequest(address.ToString());
			request.method = URLRequestMethod.GET;

			var loader = new URLLoader();
			loader.complete +=
				args =>
				{
					var e = new __DownloadStringCompletedEventArgs { Result = "" + loader.data };

					DownloadStringCompleted(null, (DownloadStringCompletedEventArgs)(object)e);
				};

			loader.ioError +=
				args =>
				{
					var e = new __DownloadStringCompletedEventArgs {  };
					DownloadStringCompleted(null, (DownloadStringCompletedEventArgs)(object)e);
				};


			loader.securityError +=
				args =>
				{
					var e = new __DownloadStringCompletedEventArgs { };
					DownloadStringCompleted(null, (DownloadStringCompletedEventArgs)(object)e);
				};

			loader.load(request);
		}
	}
}
