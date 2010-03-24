using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Delegates;

namespace UltraApplication
{
	
	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}
	}
}
