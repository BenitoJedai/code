using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TestEncoding.Library;
using System.Runtime.InteropServices;

namespace TestEncoding
{
	public partial class Program
	{
		public static void Main(string[] args)
		{
			{
				var a = "hello öäüõ - ﺕ";
				var b = Encoding.UTF8.GetBytes(a);

				foreach (var i in b)
				{
					Console.Write(i.ToString("x2") + " ");
				}
			}

			{
				var a = "hello öäüõ - ﺕ";
				var b = Encoding.ASCII.GetBytes(a);

				foreach (var i in b)
				{
					Console.Write(i.ToString("x2") + " ");
				}
			}
		}
	}
}
