using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Ultra.WebService
{

	public class InternalWebMethodWorker
	{
		public readonly ArrayList Results = new ArrayList();

		public static void Add(InternalWebMethodWorker that, InternalWebMethodInfo value)
		{
			that.Results.Add(value);
		}

		public InternalWebMethodInfo[] ToArray()
		{
			return (InternalWebMethodInfo[])this.Results.ToArray(typeof(InternalWebMethodInfo));
		}

		public static void ApplyTo(InternalWebMethodWorker that, InternalWebMethodInfo target)
		{
			target.Results = that.ToArray();
		}
	}
}
