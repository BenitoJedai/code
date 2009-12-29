using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Random))]
	internal class __Random
	{
		public virtual int Next()
		{
			return Native.API.rand();
		}

		public virtual int Next(int maxValue)
		{
			return Native.API.rand() % maxValue;
		}

		public virtual double NextDouble()
		{
			return Native.API.rand(0, int.MaxValue) / int.MaxValue;
		}
	}
}
