using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;
[assembly: Obfuscation(Feature = "script")]
namespace TestIsinst
{
    [Script(IsNative = true)]
    class mysqli
    {
    }


	public class Class1
	{
        void Test1(object e)
        {
            var y = e is mysqli;
        }



        void Test2(object e)
        {
            var y = e as mysqli;
        }
	}

	class MyClass
	{
		void Test1(object e)
		{
			var y = e is Class1;
		}



		void Test2(object e)
		{
			var y = e as Class1;
		}
	}
}
