using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Diagnostics;

namespace UltraWebApplication
{
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			new IHTMLDiv { innerHTML = "Hello world!" }.AttachToDocument();

			{
				var btn = new IHTMLButton { innerText = "UltraWebService" }.AttachToDocument();

				btn.onclick +=
					delegate
					{

						new UltraWebService().GetTime("time: ",
							result =>
							{
								new IHTMLDiv { innerText = result }.AttachToDocument();

							}
						);

					};
			}

		}

	}

	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			Debugger.Break();

			result(x + DateTime.Now);
		}
	}

}