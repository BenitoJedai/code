using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.MulticastDelegate))]
	internal class __MulticastDelegate : __Delegate
	{
		public __MulticastDelegate(object e, global::System.IntPtr p)
			: base(e, p)
		{
			this.InternalInvocationList = new __Delegate []
			{
				this
			};
		}



		protected override __Delegate CombineImpl(__Delegate d)
		{
			var a = new __Delegate[InternalInvocationList.Length + 1];
			Array.Copy(InternalInvocationList, a, InternalInvocationList.Length);
			a[InternalInvocationList.Length] = d;
			InternalInvocationList = a;

			return this;
		}

		protected override __Delegate RemoveImpl(__Delegate d)
		{
			var j = -1;

			for (int i = 0; i < InternalInvocationList.Length; i++)
			{
				if (InternalInvocationList[i] == d)
				{
					j = i;
				}
			}

			if (j >= 0)
			{
				// removing last element will result in a null delegate
				// other languages shall be updated to behave the same...
				if (InternalInvocationList.Length == 1)
					return null;

				var a = new __Delegate[InternalInvocationList.Length - 1];

				for (int i = 0; i < InternalInvocationList.Length; i++)
				{
					if (i < j)
					{
						a[i] = InternalInvocationList[i];
					}
					else
					{
						if (i > j)
						{
							a[i - 1] = InternalInvocationList[i];
						}
					}
				}

				InternalInvocationList = a;
			}

			return this;
		}

		__Delegate[] InternalInvocationList;
		
		public override __Delegate[] GetInvocationList()
		{
			return InternalInvocationList;
		}
	}
}
