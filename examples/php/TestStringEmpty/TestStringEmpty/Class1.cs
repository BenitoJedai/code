using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]
namespace TestStringEmpty
{
	[ScriptParameterByVal]
	[Script(Implements = typeof(global::System.String), InternalConstructor = true)]
	internal class __String
	{
		public static readonly string Empty = "";
	}

	[ScriptParameterByVal]
	[Script(Implements = typeof(global::System.Char), InternalConstructor = true)]
	internal class __Char
	{
		[Script(DefineAsStatic = true)]
		public override string ToString()
		{
			return ((char)(object)this).ToString();
		}
	}


	public class Class1
	{
		public static readonly string Empty = "";
		public static readonly char DirectorySeparatorChar = 'u';


		void Test()
		{
			var x = string.Empty;
			var y = DirectorySeparatorChar.ToString();
		}
	}
}
