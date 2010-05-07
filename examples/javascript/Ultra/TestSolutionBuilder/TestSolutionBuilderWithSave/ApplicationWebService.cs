using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Ultra.Library.Delegates;
using System.Diagnostics;

namespace TestSolutionBuilderWithSave
{
	public sealed partial class ApplicationWebService : TestSolutionBuilderWithSave.IApplicationWebService
	{
		public void DebugBreak(string x)
		{
			Debugger.Break();
		}
	}
}
