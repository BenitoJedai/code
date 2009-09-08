using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestEncoding.Library
{
	public static class MyExtensions
	{
		public static void Sleep(this int delay)
		{
			Thread.Sleep(delay);
		}

		public delegate void StringAction(string text);

		public static StringAction ToShowMessage(string a, string b, string c)
		{
			return
				text =>
				{
					const int w = 80;

					Action Padding = () => Console.Write("".PadLeft((w - (text.Length + 4)) / 2));

					Padding();
					Console.Write(a[0]);
					Console.Write("".PadLeft(text.Length + 2, a[1]));
					Console.Write(a[2]);
					Console.WriteLine();

					Padding();
					Console.Write(b[0]);
					Console.Write(" " + text + " ");
					Console.Write(b[2]);
					Console.WriteLine();

					Padding();
					Console.Write(c[0]);
					Console.Write("".PadLeft(text.Length + 2, c[1]));
					Console.Write(c[2]);
					Console.WriteLine();
				};
		}
	}
}
