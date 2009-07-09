using System.Threading;
using System;

using ScriptCoreLib;
using ScriptCoreLib.YAML;
using System.Diagnostics;


namespace YAMLExample
{


	[Script]
	public class Program
	{


		public static void Main(string[] args)
		{
			new Program().TestMethod1();
		}

		[Script]
		sealed class TestMethod1_Type
		{
			public string Field1;
			public string Field2;
		}

		public void TestMethod1()
		{
			var a = YAMLDocument.WriteMappingsSequence(
				typeof(TestMethod1_Type),
				new TestMethod1_Type { Field1 = "1", Field2 = "2" },
				new TestMethod1_Type { Field1 = "3", Field2 = "4" }
			);

			Console.WriteLine(a);

			var n = (TestMethod1_Type[])
				YAMLDocument.FromMappingsSequence(
					typeof(TestMethod1_Type), a
				);

			Trace.Assert(n[0].Field1 == "1");
			Trace.Assert(n[0].Field2 == "2");

			Trace.Assert(n[1].Field1 == "3");
			Trace.Assert(n[1].Field2 == "4");
		}
	}


}
