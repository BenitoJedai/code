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

		readonly XElement Data;



		public CompilationType(CompilationAssembly Context, XElement Data)
		{
			this.Data = Data;

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

		public IEnumerable<CompilationConstructor> GetConstructors()
		{
			return this.Data.Elements(CompilationConstructor.__Element).Select(k => new CompilationConstructor(this, k));
		}


		public IEnumerable<CompilationMethod> GetMethods()
		{
			return this.Data.Elements(CompilationMethod.__Element).Select(k => new CompilationMethod(this, k));
		}

		public IEnumerable<CompilationField> GetFields()
		{
			return this.Data.Elements(CompilationField._Field).Select(k => new CompilationField(this, k));
		}
	}

	
}
