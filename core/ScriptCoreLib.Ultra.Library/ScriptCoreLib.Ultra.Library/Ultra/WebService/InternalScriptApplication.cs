using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ScriptCoreLib.Ultra.WebService
{
	public class InternalScriptApplication
	{
		public string TypeName;
		public string TypeFullName;

		public class Reference
		{
			public string AssemblyFile;
		}

		public Reference[] References;
	}
}
