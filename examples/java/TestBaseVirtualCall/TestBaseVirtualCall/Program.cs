using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: System.Reflection.Obfuscation(Feature = "script")]

namespace TestBaseVirtualCall
{
	class A
	{
		protected virtual void Dispose(bool e)
		{

		}
	}

	class B : A
	{
		protected override void Dispose(bool e)
		{
			base.Dispose(e);
		}
	}
	class Program : B
	{
		public Program()
		{
			this.Dispose(false);
			base.Dispose(false);
		}

		static void Main(string[] args)
		{
			
		}

		protected override void Dispose(bool e)
		{
			base.Dispose(e);
		}
	}
}
