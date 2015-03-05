using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test46IfReturn
{
	public class Class1
	{
		public static int foo(object[] arg)
		{

			//https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150305/async


			if (arg.Length > 0)
				return 310;

			return 202;
		}
	}
}
