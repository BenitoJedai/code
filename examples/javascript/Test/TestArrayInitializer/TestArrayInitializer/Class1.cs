using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;

[assembly: Obfuscation(Feature = "script")]

namespace TestArrayInitializer
{
	interface IAssemblyReferenceToken : ScriptCoreLib.Shared.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
	{
	}

	//[CompilerGenerated]
	sealed class __c__DisplayClass4<T>
	{
		// Fields
		public Func<T, T, T> f;


		public int i;
		public T x;

		// Methods
		public T[] _SelectWithSeparator_b__3(T c)
		{
			T y = this.x;
			this.x = c;
			this.i++;
			if (this.i > 0)
			{
				return new T[] { this.f(y, c), c };
			}
			return new T[] { c };
		}
	}

	

}



 

 


 

 
