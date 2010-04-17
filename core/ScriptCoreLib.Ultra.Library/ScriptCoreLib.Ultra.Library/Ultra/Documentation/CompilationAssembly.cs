using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public class CompilationAssembly
	{
		readonly List<CompilationType> InternalTypes = new List<CompilationType>();


		public CompilationAssembly(CompilationAssemblyBase Context, XElement Data)
		{
			foreach (var item in Data.Elements("Type"))
			{
				InternalTypes.Add(new CompilationType(this, item));
			}
		}

		public IEnumerable<CompilationType> GetTypes()
		{
			return InternalTypes;
		}
	}
}
