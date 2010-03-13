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
	}

	[TestTypeGenerics.My("Y", NamedInt = 5)]
	public class Class2
	{
		//[DllImport("Kernel32.dll")]
		//internal static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, uint ucchMax);


		[TestTypeGenerics.My("Y", NamedInt = 5)]
		public void Method<M>(M m)
		{

		}
	}
}
