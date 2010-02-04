using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]
namespace TestTryInsideWhile
{
	public class Class1
	{
		public static void Method1()
		{
		}

		public static void Main()
		{
			Method1();
			for (int i = 0; i < 8; i++)
			{
				Method1();
				try
				{
					Method1();
				}
				catch
				{
					Method1();
				}
				Method1();
			}
			Method1();
		}

		public static bool IsActive;

		public static void MainWhile()
		{
			Method1();
			while (IsActive)
			{
				Method1();
				try
				{
					Method1();
				}
				catch
				{
					Method1();
				}
				Method1();
			}
			Method1();
		}
	}
}
