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
			var _a = "╔═╗";
			var _b = "║ ║";
			var _c = "╚═╝";

			var ShowMessage = MyExtensions.ToShowMessage(_a, _b, _c);

			ShowMessage("hello world");
			ShowMessage("this is a message box");

			{
				var a = "hello öäüõ - ░▒ €§žšŠŽÜÕÄÖ";
				var b = Encoding.UTF8.GetBytes(a);

				Console.WriteLine(a);
				foreach (var i in b)
				{
					Console.Write(i.ToString("x2") + " ");
				}
				Console.WriteLine();
				Console.WriteLine(Encoding.UTF8.GetString(b));

			}

			{
				var a = "hello öäüõ - ﺕ";
				var b = Encoding.ASCII.GetBytes(a);

				Console.WriteLine(a);
				foreach (var i in b)
				{
					Console.Write(i.ToString("x2") + " ");
				}
				Console.WriteLine();
				Console.WriteLine(Encoding.ASCII.GetString(b));
			}
		}
	}
}
