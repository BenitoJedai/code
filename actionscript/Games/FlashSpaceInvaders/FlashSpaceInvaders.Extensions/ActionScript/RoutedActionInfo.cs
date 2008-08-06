using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class RoutedActionInfo
	{
		public Action Direct;

		public void Chained()
		{
			if (Direct != null)
				Direct();

			if (Handler != null)
				Handler();
		}

		public event Action Handler;
	}

	[Script]
	public class RoutedActionInfo<T1>
	{
		public Action<T1> Direct;

		public void Chained(T1 t1)
		{
			if (Direct != null)
				Direct(t1);

			if (Handler != null)
				Handler(t1);
		}

		public event Action<T1> Handler;
	}

	[Script]
	public class RoutedActionInfo<T1, T2>
	{
		public Action<T1, T2> Direct;

		public void Chained(T1 t1, T2 t2)
		{
			if (Direct != null)
				Direct(t1, t2);

			if (Handler != null)
				Handler(t1, t2);
		}

		public event Action<T1, T2> Handler;
	}

	[Script]
	public class RoutedActionInfo<T1, T2, T3>
	{
		public Action<T1, T2, T3> Direct;

		public void Chained(T1 t1, T2 t2, T3 t3)
		{
			if (Direct != null)
				Direct(t1, t2, t3);

			if (Handler != null)
				Handler(t1, t2, t3);
		}

		public event Action<T1, T2, T3> Handler;
	}
}
