using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Ultra.WebService
{

	public class InternalWebMethodWorker
	{
        [Obsolete("We can now use List<>")]
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
            //Console.WriteLine("InternalWebMethodWorker");

            // WriteXDocument Results null

			target.Results = that.ToArray();
		}
	}
}
