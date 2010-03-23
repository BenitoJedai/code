using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace ScriptCoreLib.Documentation
{
	public abstract class CompilationTypeBase 
	{
		public CompilationAssemblyBase DeclaringType { get; set; }

		public CompilationAssemblyBase DeclaringAssembly { get; set; }

		public string FullName { get; set; }

		public int MetadataToken { get; set; }

		public CompilationTypeBase()
		{

		}

		public string Namespace
		{
			get
			{
				return FullName.TakeUntilLastIfAny(".");
			}
		}

		public string Name
		{
			get
			{
				return FullName.SkipUntilLastIfAny(".");
			}
		}
	}

	internal class CompilationTypeBaseTemplate : CompilationTypeBase
	{
	}
}
