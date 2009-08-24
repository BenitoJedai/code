using System.Threading;
using System;

using ScriptCoreLib;


namespace ConditionalBranch
{

	[Script]
	public class Program
	{

		public static void Main(string[] args)
		{
			InRange(100);
			InRange(200);

		}

		private static bool InRange(int i)
		{
			var rtn = false;
			bool v = ((i >= 97) && (i <= 122));
			if (v)
			{
				rtn = true;
			}
			return rtn;
		}
	}


}
