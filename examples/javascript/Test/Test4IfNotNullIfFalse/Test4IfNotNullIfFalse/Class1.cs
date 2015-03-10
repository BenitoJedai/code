using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace Test4IfNotNullIfFalse
{
	class SolutionFileComment
	{
		public Func<object, bool> IsActiveFilter;


		public void foo() { }
		public void WriteTo(object Context)
		{
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150310

			// X:\jsc.svn\examples\javascript\test\Test4IfNotNullIfFalse\Test4IfNotNullIfFalse\Class1.cs
			// X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\Studio\SolutionFileComment.cs
			// broken in roslyn?
			if (this.IsActiveFilter != null)
				if (!this.IsActiveFilter(Context))
					return;

			foo();
		}
		// is rewriter broken?


		//// Test4IfNotNullIfFalse.SolutionFileComment.WriteTo
		//type$R9fhMGxDUTOcH_b3A04rt8g.AgAABmxDUTOcH_b3A04rt8g = function(b)
		//{
		//	var a = [this], c, d;

		//	c = a[0].IsActiveFilter != null;

		//	if (c)
		//	{
		//		d = !a[0].IsActiveFilter.BAAABlSuoDqQ5xjbO_bICBQ(b);

		//		if (d)
		//		{
		//			return;
		//		}

		//	}

		//	a[0].AQAABmxDUTOcH_b3A04rt8g();
		//};

	}

	// script: error JSC1000: No implementation found for this native method, please implement [System.Func`2.Invoke(System.Object)]

	[Script(Implements = typeof(Func<,>))]
	class __Func<T, TResult>
	{
		public TResult Invoke(T a) => default(TResult);


	}
}
