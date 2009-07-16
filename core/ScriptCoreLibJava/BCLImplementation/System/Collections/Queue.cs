using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(global::System.Collections.Queue))]
	internal class __Queue
	{
		internal ArrayList InternalQueue = new ArrayList();

		public virtual object Dequeue()
		{
			var a = InternalQueue[0];

			InternalQueue.RemoveAt(0);

			return a;
		}
		public virtual void Enqueue(object obj)
		{
			InternalQueue.Add(obj);
		}

		public virtual int Count
		{
			get
			{
				return InternalQueue.Count;
			}
		}
	}
}
