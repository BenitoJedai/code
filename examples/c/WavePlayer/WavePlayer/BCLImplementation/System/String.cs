using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WavePlayer.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.String))]
	internal class __String
	{
		[Script(OptimizedCode = @"return e[o];")]
		internal static char StringChar(string e, int o)
		{
			return default(char);
		}

		public char get_Chars(int i)
		{
			return StringChar((string)(object)this, i);
		}

		public int Length
		{
			get
			{
				return string_h.strlen((string)(object)this);
			}
		}

		public char[] ToCharArray()
		{
			var that = (string)(object)this;

			var length = that.Length;
			var c = new char[length];

			for (int i = 0; i < length; i++)
			{
				c[i] = that[i];
			}

			return c;
		}
	}

}
