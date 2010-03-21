using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Documentation
{
	public abstract class CompilationTypeBase
	{
		public CompilationAssemblyBase DeclaringType
		{
			get;
			set;
		}

		public CompilationAssemblyBase DeclaringAssembly
		{
			get;
			set;
		}
	}
}
