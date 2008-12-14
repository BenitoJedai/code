using ScriptCoreLib;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;

namespace FeatureTest_Constructors_A.ActionScript
{

	[Script]
	public class A
	{
		public A(string a)
		{

		}
	}

	#if T2
	// this will fail intentionally

	[Script]
	public class B
	{
		public B(object a)
		{
			var i = 2;
		}

		public B(string a)
		{
			var i = 1;
		}
	}
	#endif

	[Script]
	public class C
	{
		public C() : this("constant")
		{
		}

		public C(string a)
		{
			var i = 1;
		}
	}
}