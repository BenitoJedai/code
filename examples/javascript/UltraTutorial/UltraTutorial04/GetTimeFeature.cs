using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace UltraTutorial04
{
	public delegate void GetTimeResult(string e);

	partial class AlphaWebService 
	{
		public void GetTime(string prefix, GetTimeResult result)
		{
			result(prefix + ": " + DateTime.Now);
		}

	}

	partial class UltraApplication
	{
		private static void AddButtonForGetTime()
		{
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
}
