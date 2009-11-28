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

	public class Class1
	{
		public static readonly string Empty = "";


		void Test()
		{
			var x = string.Empty;
		}
	}
}
