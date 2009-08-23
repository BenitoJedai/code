using System.Threading;
using System;

using ScriptCoreLib;


namespace OpCodes_isinst
{
	[Script]
	public interface I
	{
		string Title { get; set; }
	}

	[Script]
	public class A : I
	{
		#region I Members

		public string Title
		{
			get;
			set;
		}

		#endregion
	}

	[Script]
	public class B : A
	{
	}

	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			// http://geekspeak.creatrixgames.com/when-instanceof-doesnt-recognize-an-instanceof.html

			object a = new B();

			{
				var x = a is A;

				if (x)
					Console.WriteLine("object a is A");
				else
					Console.WriteLine("object a is not A");
			}


			{
				var x = a is B;

				if (x)
					Console.WriteLine("object a is B");
				else
					Console.WriteLine("object a is not B");
			}


			{
				var x = a is I;

				if (x)
					Console.WriteLine("object a is I");
				else
					Console.WriteLine("object a is not I");
			}

			{
				var y = a as A;
				if (y == null)
					Console.WriteLine("y == null");
				else
					Console.WriteLine("y != null");
			}
			

			{
				var y = GetObject() as A;
				if (y == null)
					Console.WriteLine("y == null");
				else
					Console.WriteLine("y != null");
			}
		}

		public static object GetObject()
		{
			return new B();
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
