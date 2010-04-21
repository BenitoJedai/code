using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Ultra.Library.Delegates;

namespace UltraRaycast6
{
	public sealed class UltraWebService
	{
		public void PlayerGotItem(string x, StringAction result)
		{
			Console.WriteLine(x);

			result("yay - " + x);
		}

	

	}
}
