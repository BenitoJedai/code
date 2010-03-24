using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Xml.Linq;

namespace ScriptCoreLib.Documentation
{
	public class CompilationType
	{
		public CompilationType DeclaringType { get; set; }

		public CompilationAssembly DeclaringAssembly { get; set; }

		public string FullName { get; set; }

		public int MetadataToken { get; set; }

		public CompilationType(CompilationAssembly Context, XElement Data)
		{
			this.FullName = Data.Element("FullName").Value;
			this.MetadataToken = Convert.ToInt32(Data.Element("MetadataToken").Value);
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

	
}
