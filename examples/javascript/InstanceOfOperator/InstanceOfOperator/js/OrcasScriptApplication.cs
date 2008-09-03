using ScriptCoreLib;


namespace InstanceOfOperator.js
{
	[Script]
	public class InstanceOfOperatorOther
	{


	}

	[Script]
	public class InstanceOfOperatorBase
	{


	}

	[Script]
	public class InstanceOfOperatorSubclass : InstanceOfOperatorBase
	{


	}

	[Script, ScriptApplicationEntryPoint]
	static class Module1
	{
		[Script(OptimizedCode = "alert(e);")]
		public static void alert(string e)
		{

		}

		[Script(OptimizedCode = "if(!v) throw text;")]
		public static void assert(bool v, string text)
		{
		}

		[Script(IsDebugCode=true)]
		public static void Test_IsBaseType()
		{
			object e = new InstanceOfOperatorSubclass();
			bool y = e is InstanceOfOperatorBase;
			bool n = e is InstanceOfOperatorOther;

			assert(y, "e is InstanceOfOperatorBase");
			assert(!n, "e is InstanceOfOperatorOther");

			alert("All tests are good");
		}


		static Module1()
		{
			Test_IsBaseType();
		}
	}

}
