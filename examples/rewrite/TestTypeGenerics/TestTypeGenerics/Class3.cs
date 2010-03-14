using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestTypeGenerics
{
	interface I1
	{
		void A();
	}


	class Class3 : I1
	{
		#region I1 Members

		IEnumerable<Class3> X(AssemblyName n)
		{
			return null;
		}

		void I1.A()
		{
			//((I1)this).A();
			//var i = 0;
			//switch (i)
			//{
			//    case 5:
			//        ((I1)this).A();
			//        break;
			//    case 6:
			//        ((I1)this).A();
			//        break;

			//    default:
			//        break;
			//}


			Func<AssemblyName, IEnumerable<Class3>> a = X;

			  // L_0095: newobj instance void [System.Core]System.Func`2<class [mscorlib]System.Reflection.AssemblyName, class [mscorlib]System.Collections.Generic.IEnumerable`1<class [ScriptCoreLibA]ScriptCoreLib.Shared.ScriptResourcesAttribute>>::.ctor(object, native int)
        
		}

		#endregion
	}
}
