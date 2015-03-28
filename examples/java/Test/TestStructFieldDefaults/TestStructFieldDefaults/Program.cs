using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestStructFieldDefaults
{
	struct HopToJVM
	{
		public void GetResult() { }
	}

	struct Program
	{
		// 1>script : error JSC1000: Java : class import: no implementation for System.ValueType at TestStructFieldDefaults.HopToJVM

		public HopToJVM __u___awaiter2;


		static void Main()
		{
		}
	}

	[Script(Implements = typeof(global::System.ValueType))]
	internal abstract class __ValueType
	{
		// X:\jsc.svn\examples\java\test\TestStructFieldDefaults\TestStructFieldDefaults\Program.cs

	}
}
