using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Web;
using System;

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
		}
	}

	public delegate void GetTimeResult(string e);

	public sealed class AlphaWebService
	{
		public void GetTime(string prefix, GetTimeResult result)
		{
			result(prefix + ": " + DateTime.Now);
		}
	}
}
