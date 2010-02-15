using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Web;
using System;
using System.Net;

namespace UltraTutorial03
{
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			new IHTMLDiv { innerHTML = "Hello world" }.AttachToDocument();

			var GetTime = new IHTMLButton { innerText = "GetTime" }.AttachToDocument();

			GetTime.onclick +=
				delegate
				{
					new AlphaWebService().GetTime("[client time]: " + DateTime.Now + " [server time]",
						x =>
						{
							new IHTMLDiv { innerText = x }.AttachToDocument();
						}
					);
				};

			var url = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text, "http://example.com");

			url.AttachToDocument();

			
			var DownloadData = new IHTMLButton { innerText = "DownloadData" }.AttachToDocument();

			DownloadData.onclick +=
				delegate
				{
					new AlphaWebService().DownloadData(url.value,
						x =>
						{
							new IHTMLPre { innerText = url.value + Environment.NewLine + Environment.NewLine + x }.AttachToDocument();
						}
					);
				};
		}
	}

	public delegate void GetTimeResult(string e);
	public delegate void DownloadDataResult(string e);

	public sealed class AlphaWebService
	{
		public void GetTime(string prefix, GetTimeResult result)
		{
			result(prefix + ": " + DateTime.Now);
		}

		public void DownloadData(string url, DownloadDataResult result)
		{
			var c = new WebClient();

			result(c.DownloadString(url));
		}
	}

}
