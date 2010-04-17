using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public class CompilationProperty : CompilationMember
	{
		internal const string __Element = "Property";
		internal const string __Name = "Name";

		public string Name { get; set; }

		public CompilationProperty(CompilationType Context, XElement Data)
			: base(Context, Data)
		{
			this.Name = Data.Element(__Name).Value;
		}

		CompilationMethod InternalSetMethod;

		public CompilationMethod GetSetMethod()
		{
			if (InternalSetMethod == null)
			{
				var SetElement = this.Data.Element(CompilationXNames.Set);

				if (SetElement != null)
				{
					var Set = Convert.ToInt32(SetElement.Value);

					var Methods = this.DeclaringType.GetMethods();

					InternalSetMethod = Methods.FirstOrDefault(k => k.MetadataToken == Set);
				}
			}

			return InternalSetMethod;
		}

		CompilationMethod InternalGetMethod;

		public CompilationMethod GetGetMethod()
		{
			if (InternalGetMethod == null)
			{
				var GetElement = this.Data.Element(CompilationXNames.Get);

				if (GetElement != null)
				{
					var Get = Convert.ToInt32(GetElement.Value);

					InternalGetMethod = this.DeclaringType.GetMethods().FirstOrDefault(k => k.MetadataToken == Get);
				}
			}

			return InternalGetMethod;
		}
	}
}
