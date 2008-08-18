using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Random))]
	internal class __Random
	{
		public virtual double NextDouble()
		{
			return java.lang.Math.random();
		}
	}
}
