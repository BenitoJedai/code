using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Documentation
{
	public abstract class CompilationAssemblyBase
	{
		public string Name
		{
			get
			{
				return InternalGetName();
			}
		}

		internal protected virtual string InternalGetName()
		{
			return default(string);
		}

		public CompilationArchiveBase DeclaringArchive { get; set; }

		public IEnumerable<CompilationTypeBase> GetTypes()
		{
			return null;
		}
	}

	internal class CompilationAssemblyBaseTemplate : CompilationAssemblyBase
	{

	}
}
