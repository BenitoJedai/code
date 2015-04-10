using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestLINQSelectByteArray0
{
	class List<T>
	{
		public T[] ToArray()
		{
			return null;
		}
	}

	public class Class1
	{
		static void Invoke(List<byte> e)
		{
			var loc1 = e.ToArray();

			//var c;

			//c = b.AQAABs1LYjG7WTFB2LRv_bQ();
		}
	}
}
