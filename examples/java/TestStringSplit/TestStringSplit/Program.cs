using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TestStringSplit.Library;
using System.Runtime.InteropServices;

namespace TestStringSplit
{
	public partial class Program
	{


		public static void Main(string[] args)
		{

			var u = "aa##bb##cc".Split(new [] {"##"}, StringSplitOptions.None );

			foreach (var item in u)
			{
				Console.WriteLine(item);
			}


		}


	}
}
