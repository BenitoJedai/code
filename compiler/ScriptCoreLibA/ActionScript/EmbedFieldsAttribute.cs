using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
	[global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class EmbedFieldsAttribute : Attribute
	{

		public EmbedFieldsAttribute()
		{

		}

		public EmbedFieldsAttribute(string Path, string FileExtension)
		{
			this.Path = Path;
			this.FileExtension = FileExtension;
		}


		public string FileExtension { get; set; }

		public string Path { get; set; }
	
	}
}
