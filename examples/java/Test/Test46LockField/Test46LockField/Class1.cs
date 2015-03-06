using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace Test46LockField
{
    public class Class1
    {
		object Context = new object();

		public bool Set()
		{
			// roslyn 4.6 changes it?
			// X:\jsc.svn\examples\java\hybrid\JVMCLRNIC\JVMCLRNIC\Program.cs
			// X:\jsc.svn\examples\java\test\Test46LockField\Test46LockField\Class1.cs

			lock (this.Context)
			{
				notify();
				//this.Context.notify();
			}

			return false;
		}

		private void notify()
		{
		}
	}
}
