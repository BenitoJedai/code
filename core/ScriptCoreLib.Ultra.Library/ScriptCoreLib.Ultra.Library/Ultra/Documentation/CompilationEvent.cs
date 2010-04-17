using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public class CompilationEvent : CompilationMember
	{
		internal const string __Element = "Event";
		internal const string __Name = "Name";

		public string Name { get; set; }


		public CompilationEvent(CompilationType Context, XElement Data)
			: base(Context, Data)
		{
			this.Name = Data.Element(__Name).Value;
		}

		CompilationMethod InternalAddMethod;

		public CompilationMethod GetAddMethod()
		{
			if (InternalAddMethod == null)
			{
				var Add = Convert.ToInt32(this.Data.Element(CompilationXNames.Add).Value);

				InternalAddMethod = this.DeclaringType.GetMethods().Single(k => k.MetadataToken == Add);
			}

			return InternalAddMethod;
		}

		CompilationMethod InternalRemoveMethod;

		public CompilationMethod GetRemoveMethod()
		{
			if (InternalRemoveMethod == null)
			{
				var Remove = Convert.ToInt32(this.Data.Element(CompilationXNames.Remove).Value);

				InternalRemoveMethod = this.DeclaringType.GetMethods().Single(k => k.MetadataToken == Remove);
			}

			return InternalRemoveMethod;
		}
	}
}
