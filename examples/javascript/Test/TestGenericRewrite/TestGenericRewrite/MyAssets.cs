using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript;
using ClassLibrary1;

namespace TestGenericRewrite
{
	public class MyAssets 
	{
		// Note: 
		// in Assets build configuration post build event
		// this assembly is being merged with the UltraSource


		class MyClassT
		{
		}


		class MyMethodT
		{
		}

		public MyAssets()
		{

			Class1<MyClassT>.Method1<MyMethodT>(0, null, null, null);
			Class3<MyClassT>.Method1(0, null, null);
			Class2.Method1<MyMethodT>(0, null, null);
		}
	}
}
