using System;
using System.Collections.Generic;
using System.Text;
using java.lang;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
	[Script(Implements = typeof(global::System.Threading.Thread))]
	internal class __Thread
	{
		public Thread InternalValue;

		public static void Sleep(int millisecondsTimeout)
		{
			try
			{
				Thread.sleep(millisecondsTimeout);
			}
			catch
			{
				throw new csharp.RuntimeException();
			}
		}

		public void Start()
		{
			InternalValue.start();
		}

		public string Name
		{
			get
			{
				return InternalValue.getName();
			}
			set
			{
				InternalValue.setName(value);
			}
		}

		public bool IsAlive
		{
			get
			{
				return InternalValue.isAlive();
			}
		}

		public bool IsBackground { get { return InternalValue.isDaemon(); } set { InternalValue.setDaemon(value); } }

		public void Join()
		{
			try
			{
				InternalValue.join();
			}
			catch
			{
				throw new csharp.RuntimeException();
			}
		}

		public bool Join(int ms)
		{
			try
			{
				InternalValue.join(ms);
			}
			catch
			{
				throw new csharp.RuntimeException();
			}

			return !InternalValue.isAlive();
		}

		public void Abort()
		{
			InternalValue.stop();
		}
	}
}
