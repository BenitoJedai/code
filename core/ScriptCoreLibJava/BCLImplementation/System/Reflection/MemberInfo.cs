using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
	[Script(Implements = typeof(MemberInfo))]
	internal abstract class __MemberInfo
	{
		public abstract string Name { get; }

		public abstract Type DeclaringType { get; }
	}
}
