﻿using System;
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

		[Script]
		class RunnableHandler : Runnable
		{
			public global::System.Threading.ThreadStart Handler;

			public void run()
			{
				Handler();
			}
		}

		internal __Thread()
		{

		}

		public __Thread(global::System.Threading.ThreadStart t)
		{
			InternalValue = new java.lang.Thread(new RunnableHandler { Handler = t });
		}

		public static void Sleep(int millisecondsTimeout)
		{
			try
			{
				Thread.sleep(millisecondsTimeout);
			}
			catch
			{

				throw;
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
				throw;
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
				throw;
			}

			return !InternalValue.isAlive();
		}

		public void Abort()
		{
			InternalValue.stop();
		}
	}
}
