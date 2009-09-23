using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
	/// <summary>
	/// Renames a native namespace. For example java.lang.String could be written as ScriptCoreLibJava.java.lang.String.
	/// </summary>
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public sealed class ScriptNamespaceRenameAttribute : Attribute
	{
		public string NativeNamespaceName;
		public string VirtualNamespaceName;

		/// <summary>
		/// Only native classes shall be considered while renaming
		/// </summary>
		public bool FilterToIsNative;

		public ScriptNamespaceRenameAttribute()
		{

		}
	}

}
