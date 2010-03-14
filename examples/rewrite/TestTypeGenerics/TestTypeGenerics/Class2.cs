using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

[assembly: TestTypeGenerics.My("Y", NamedInt = 5)]

namespace TestTypeGenerics
{
	[global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	sealed class MyAttribute : Attribute
	{
		// See the attribute guidelines at 
		//  http://go.microsoft.com/fwlink/?LinkId=85236
		readonly string positionalString;

		// This is a positional argument
		public MyAttribute(string positionalString)
		{
			this.positionalString = positionalString;

		}

		public string PositionalString
		{
			get { return positionalString; }
		}

		// This is a named argument
		public int NamedInt { get; set; }


		public Type T;
	}

	[TestTypeGenerics.My("Y", NamedInt = 5)]
	public class Class2
	{
		[DllImport("Kernel32.dll")]
		internal static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, uint ucchMax);

		struct MARGINS
		{

			public int Left;
			public int Right;
			public int Top;
			public int Bottom;
		}

		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

		[TestTypeGenerics.My("Y", NamedInt = 5, T = typeof(Class2))]
		public void Method<M>(M m)
		{
			int i = 0;
			if (i > 0)
			{
				Console.WriteLine();
				try
				{
					switch (i)
					{
						case 1: Console.WriteLine(); break;
						case 2: Console.WriteLine(); break;
						case 3: Console.WriteLine(); break;
						case 4: Console.WriteLine(); break;

					}

					switch (i)
					{
						case 1: Console.WriteLine(); break;
						case 2: Console.WriteLine(); break;
						case 3: Console.WriteLine(); break;
						case 4: Console.WriteLine(); break;

					}

					switch (i)
					{
						case 1: Console.WriteLine(); break;
						case 2: Console.WriteLine(); break;
						case 3: Console.WriteLine(); break;
						case 4: Console.WriteLine(); break;

					}

					switch (i)
					{
						case 1: Console.WriteLine(); break;
						case 2: Console.WriteLine(); break;
						case 3: Console.WriteLine(); break;
						case 4: Console.WriteLine(); break;

					}

					switch (i)
					{
						case 1: Console.WriteLine(); break;
						case 2: Console.WriteLine(); break;
						case 3: Console.WriteLine(); break;
						case 4: Console.WriteLine(); break;

					}

					switch (i)
					{
						case 1: Console.WriteLine(); break;
						case 2: Console.WriteLine(); break;
						case 3: Console.WriteLine(); break;
						case 4: Console.WriteLine(); break;

					}
				}
				catch (MyException ex)
				{

				}
			}
		}
	}

	[global::System.Serializable]
	public class MyException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public MyException() { }
		public MyException(string message) : base(message) { }
		public MyException(string message, Exception inner) : base(message, inner) { }
		protected MyException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
