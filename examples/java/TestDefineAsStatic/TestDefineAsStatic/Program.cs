using System;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace TestDefineAsStatic
{
	[Script(Implements = typeof(global::System.Array), IsArray = true)]
	internal class __Array
	{
		public int Length
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return 0;
			}
		}

		[Script(DefineAsStatic = true)]
		public void SetValue(object value, int index)
		{
		}
	}

	[Script]
	class Program
	{
		static void Main(string[] args)
		{
			Array a = null;
			var x = a.Length;
			a.SetValue(null, 0);
		}
	}
}
