using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Ultra.Library.Delegates;
using System.ComponentModel;

namespace ScriptCoreLib.Ultra.Components.Volatile
{
	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}

		public void Serve(WebServiceHandler h)
		{
			if (h.Context.Request.Path == "/serve")
			{
				h.Context.Response.Write("hello");
				h.CompleteRequest();
			}
		}

#if DEBUG
		[Description("This method cannot be translated to java or php and is to be used only for feature discovery.")]
		public void LaunchJSCSolutionsNETCarouselProgram()
		{
			ScriptCoreLib.Avalon.Desktop.JSCSolutionsNETCarouselProgram.Main(new string [0]);
		}
#endif

	}
}
