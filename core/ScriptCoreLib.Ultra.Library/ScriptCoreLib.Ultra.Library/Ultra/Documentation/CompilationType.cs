using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public class CompilationType
	{
		internal const string __Element = "Type";

		internal const string __FullName = "FullName";
		internal const string __NestedType = "NestedType";
		internal const string __DeclaringType = "DeclaringType";
		internal const string __MetadataToken = "MetadataToken";
		internal const string __IsInterface = "IsInterface";
		internal const string __HTMLElement = "HTMLElement";
		internal const string __GenericArgument = "GenericArgument";


		public CompilationAssembly DeclaringAssembly { get; set; }

		public string FullName { get; set; }

		public int MetadataToken { get; set; }

		readonly XElement Data;

		public string Summary
		{
			get
			{
				return Data.Element(CompilationXNames.Summary).Value;
			}
		}

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
				return FullName.TakeUntilLastOrEmpty(".");
			}
		}

		public string Name
		{
			get
			{
				var n = FullName.SkipUntilLastIfAny(".");


				if (IsNested)
					n = n.SkipUntilIfAny("+");

				var w = new StringBuilder();

				w.Append(n.TakeUntilIfAny("`"));

				if (GetGenericArguments().Any())
				{
					w.Append("<");

					var i = 0;
					foreach (var item in GetGenericArguments().ToArray())
					{
						if (i > 0)
							w.Append(",");

						w.Append(item);
						i++;
					}


					w.Append(">");
				}

				return w.ToString();
			}
		}

		public IEnumerable<CompilationConstructor> GetConstructors()
		{
			return this.Data.Elements(CompilationConstructor.__Element).Select(k => new CompilationConstructor(this, k));
		}


		IEnumerable<CompilationMethod> InternalMethods;

		public IEnumerable<CompilationMethod> GetMethods()
		{
			if (InternalMethods == null)
				InternalMethods = this.Data.Elements(CompilationMethod.__Element).Select(k => new CompilationMethod(this, k));

			return InternalMethods;
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

		public IEnumerable<CompilationEvent> GetEvents()
		{
			return this.Data.Elements(CompilationEvent.__Element).Select(k => new CompilationEvent(this, k));
		}

		public IEnumerable<CompilationProperty> GetProperties()
		{
			return this.Data.Elements(CompilationProperty.__Element).Select(k => new CompilationProperty(this, k));
		}

		/// <summary>
		/// Some types in ScriptCoreLib are wrappers for HTML elements. This property has the name of that
		/// HTML element.
		/// </summary>
		public string HTMLElement
		{
			get
			{
				var h = this.Data.Elements(__HTMLElement).Select(k => k.Value).FirstOrDefault();

				if (h == null)
					return "";

				return h;
			}
		}

		public IEnumerable<string> GetGenericArguments()
		{
			return this.Data.Elements(CompilationType.__GenericArgument).Select(k => k.Value);
		}


	}


}
