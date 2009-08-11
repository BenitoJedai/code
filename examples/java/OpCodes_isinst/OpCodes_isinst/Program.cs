using System.Threading;
using System;

using ScriptCoreLib;


namespace OpCodes_isinst
{
	[Script]
	public class A
	{
		public string Title;
	}

	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			object a = new A();
			var x = a is A;
			var y = a as A;
		}

		public static bool Equals(object x, object y)
		{
			var x_ = x as A;
			if (x_ == null)
				return false;

			var y_ = y as A;
			if (y_ == null)
				return false;

			return x_.Title == y_.Title;
		}
	
	}


}
