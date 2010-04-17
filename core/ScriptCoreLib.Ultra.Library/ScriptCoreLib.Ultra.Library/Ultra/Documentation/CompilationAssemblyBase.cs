using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public abstract class CompilationAssemblyBase
	{
		public string Name { get; set; }

		public int MetadataToken { get; set; }

		public CompilationArchiveBase DeclaringArchive { get; set; }


	
		public CompilationAssemblyBase()
		{

		}

		internal protected Action<Action<XElement>> LoadImplementation { get; set; }

		CompilationAssembly LoadCache;
		public void WhenReady(Action<CompilationAssembly> yield)
		{
			if (LoadCache != null)
			{
				yield(LoadCache);
				return;
			}

			LoadImplementation(
				r =>
				{
					LoadCache = new CompilationAssembly(this, r);
					yield(LoadCache);
				}
			);
		}
	}

	internal class CompilationAssemblyBaseTemplate : CompilationAssemblyBase
	{
	
	}
}
