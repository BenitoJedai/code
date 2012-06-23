using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Ultra.Library.Delegates;

namespace TestSolutionBuilder
{
	partial class ApplicationWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}
	}
}
