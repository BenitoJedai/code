using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Documentation
{
	public abstract class CompilationAssemblyBase 
	{
		public string Name { get; set; }

		public int MetadataToken { get; set; }

		public CompilationArchiveBase DeclaringArchive { get; set; }


		internal protected readonly List<Func<CompilationTypeBase>> InternalTypes = new List<Func<CompilationTypeBase>>();

		public IEnumerable<CompilationTypeBase> GetTypes()
		{
			return InternalTypes.Select(k => k());
		}

		public CompilationAssemblyBase()
		{

		}
	}

	internal class CompilationAssemblyBaseTemplate : CompilationAssemblyBase
	{
		internal protected void InitializeTypes()
		{
			InternalTypes.Add(Add);
		}

		internal protected CompilationTypeBase Internal;

		internal protected CompilationTypeBase Add()
		{
			if (Internal == null)
			{
				Internal = new CompilationTypeBaseTemplate { DeclaringAssembly = this };
			}

			return Internal;
		}
	}
}
