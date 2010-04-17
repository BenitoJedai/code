using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Documentation
{
	public abstract class CompilationBase
	{
	

		internal protected readonly List<Func<CompilationArchiveBase>> InternalArchives = new List<Func<CompilationArchiveBase>>();

		public CompilationBase()
		{
		}


		public IEnumerable<CompilationArchiveBase> GetArchives()
		{
			return InternalArchives.Select(k => k());
		}
	}

	internal class CompilationBaseTemplate : CompilationBase
	{
		internal protected void InitializeArchives()
		{
			InternalArchives.Add(AddArchive1);
		}

		internal protected CompilationArchiveBase InternalArchive1;

		internal protected CompilationArchiveBase AddArchive1()
		{
			if (InternalArchive1 == null)
			{
				InternalArchive1 = new CompilationArchiveBaseTemplate { DeclaringCompilation = this };
			}

			return InternalArchive1;
		}
	}
}
