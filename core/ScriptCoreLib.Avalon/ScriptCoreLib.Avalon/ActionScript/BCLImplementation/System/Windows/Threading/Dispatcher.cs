using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Threading
{
	[Script(Implements = typeof(global::System.Windows.Threading.Dispatcher))]
	public class __Dispatcher
	{
		public object Invoke(Delegate method, params object[] args)
		{
			// http://msdn.microsoft.com/en-us/library/cc647509.aspx

			if (method is Action)
			{
				((Action)method)();

				return null;
			}

			throw new NotSupportedException();
		}
	}
}
