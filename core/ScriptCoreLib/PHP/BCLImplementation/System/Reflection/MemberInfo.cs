﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Reflection
{
	[Script(Implements = typeof(global::System.Reflection.MemberInfo))]
	internal abstract class __MemberInfo
	{
		public abstract Type DeclaringType { get; }

		public abstract string Name { get; }
	}
}
