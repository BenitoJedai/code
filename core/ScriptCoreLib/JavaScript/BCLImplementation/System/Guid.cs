using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Guid))]
	internal class __Guid
	{
		public byte[] InternalValue = new byte[16];

		public static __Guid NewGuid()
		{
			var r = new Random();
			var n = new __Guid();
			r.NextBytes(n.InternalValue);
			return n;
		}

		public override string ToString()
		{
			// 43a781bf-2c14-4dcc-98b3-d422fdddafe6

			var w = new StringBuilder();

			for (int i = 0; i < this.InternalValue.Length; i++)
			{
				if (i == 4) w.Append("-");
				if (i == 6) w.Append("-");
				if (i == 8) w.Append("-");
				if (i == 10) w.Append("-");

				w.Append(this.InternalValue[i].ToString("x2"));
			}

			return w.ToString();
		}

	
	}
}
