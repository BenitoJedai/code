using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScriptCoreLib.Ultra.Library.Delegates;
using System.Diagnostics;

namespace UltraWebApplicationWithAssets
{
	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			Debugger.Break();

			result(x + DateTime.Now);
		}
	}
}