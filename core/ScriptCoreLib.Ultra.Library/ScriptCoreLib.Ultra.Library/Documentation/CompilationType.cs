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
		internal const string __NestedType = "NestedType";
		internal const string __DeclaringType = "DeclaringType";
		internal const string __MetadataToken = "MetadataToken";
		internal const string __IsInterface = "IsInterface";


		public CompilationAssembly DeclaringAssembly { get; set; }

		public string FullName { get; set; }

		public int MetadataToken { get; set; }

		readonly XElement Data;

		public bool IsInterface
		{
			get
			{
				var n = Convert.ToBoolean(Data.Element(__IsInterface).Value);

				return n;
			}
		}

		public bool IsNested
		{
			get
			{
				var DeclaringType = Convert.ToInt32(Data.Element(__DeclaringType).Value);

				if (DeclaringType == 0)
					return false;

				return true;
			}
		}

		public CompilationType DeclaringType
		{
			get
			{
				var DeclaringType = Convert.ToInt32(Data.Element(__DeclaringType).Value);

				if (DeclaringType == 0)
					return null;

				return this.DeclaringAssembly.GetTypes().Single(k => k.MetadataToken == DeclaringType);
			}
		}

		public CompilationType(CompilationAssembly Context, XElement Data)
		{
			this.Data = Data;
			this.DeclaringAssembly = Context;

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
				var n = FullName.SkipUntilLastIfAny(".");

				if (IsNested)
					return n.SkipUntilIfAny("+");

				return n;
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

		public IEnumerable<CompilationType> GetNestedTypes()
		{
			return this.Data.Elements(CompilationType.__NestedType)
				.Select(
					k => Convert.ToInt32(k.Value)
				)
				.Select(k => this.DeclaringAssembly.GetTypes().Single(kk => kk.MetadataToken == k));
		}
	}


}
