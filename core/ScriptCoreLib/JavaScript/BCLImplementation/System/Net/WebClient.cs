using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net
{
	[Script(Implements = typeof(global::System.Net.WebClient))]
	internal class __WebClient
	{
		public event DownloadStringCompletedEventHandler DownloadStringCompleted;

		public void DownloadStringAsync(Uri address)
		{
			var e = new __DownloadStringCompletedEventArgs { Error = new Exception("Not implemented. (__WebClient.DownloadStringAsync)") };
			DownloadStringCompleted(null, (DownloadStringCompletedEventArgs)(object)e);
		}
	}
}
