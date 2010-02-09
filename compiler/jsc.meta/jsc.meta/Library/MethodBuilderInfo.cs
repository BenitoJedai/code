using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace jsc.meta.Library
{
	public class MethodBuilderInfo
	{
		// why oh why cannot we ask MethodBuilder how many parameters does it have?

		public MethodBuilder Method;
		public Type[] Parameters;
	}
}
