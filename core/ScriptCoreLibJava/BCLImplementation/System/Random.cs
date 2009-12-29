using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Random))]
	internal class __Random
	{
		public virtual int Next()
		{
			return Next(0, int.MaxValue);
		}

		public virtual int Next(int min, int max)
		{
			var len = max - min;
			var r = Math.Floor(java.lang.Math.random() * len);

			return Convert.ToInt32(r) + min;
		}

		public virtual double NextDouble()
		{
			return java.lang.Math.random();
		}
	}
}
