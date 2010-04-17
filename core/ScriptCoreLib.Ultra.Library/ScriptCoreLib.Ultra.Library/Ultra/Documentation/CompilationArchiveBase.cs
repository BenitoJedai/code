using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Documentation
{
	public abstract class CompilationArchiveBase 
	{
		public string Name { get; set; }

		public CompilationBase DeclaringCompilation { get; set; }

		public CompilationArchiveBase()
		{

		}

		internal protected readonly List<Func<CompilationAssemblyBase>> InternalAssemblies = new List<Func<CompilationAssemblyBase>>();

		public IEnumerable<CompilationAssemblyBase> GetAssemblies()
		{
			return InternalAssemblies.Select(k => k());
		}
	}

	internal class CompilationArchiveBaseTemplate : CompilationArchiveBase
	{
		internal protected void InitializeAssemblies()
		{
			InternalAssemblies.Add(AddInternalAssemblies1);
		}

		internal protected CompilationAssemblyBase InternalAssemblies1;

		internal protected CompilationAssemblyBase AddInternalAssemblies1()
		{
			if (InternalAssemblies1 == null)
			{
				InternalAssemblies1 = new CompilationAssemblyBaseTemplate { DeclaringArchive = this };
			}

			return InternalAssemblies1;
		}
	}
}
