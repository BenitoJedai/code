using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
	[Script(Implements = typeof(ParameterInfo))]
	public class __ParameterInfo
	{
		public virtual int Position { get; set; }

		public virtual Type ParameterType { get; set; }

		public static implicit operator ParameterInfo(__ParameterInfo e)
		{
			return (ParameterInfo)(object)e;
		}
	}
}
