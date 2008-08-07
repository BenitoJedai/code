using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public abstract class RoutedActionInfoBase
	{
		public string EventName { get; set; }

		protected void RaiseBaseHandler()
		{
			if (BaseHandler != null)
				BaseHandler(this);
		}

		public event Action<RoutedActionInfoBase> BaseHandler;
	}

	[Script]
	public class RoutedActionInfo : RoutedActionInfoBase
	{
		public Action Direct;

		public void Chained()
		{
			if (Direct != null)
				Direct();

			if (Handler != null)
				Handler();

				RaiseBaseHandler();
		}

		public event Action Handler;
	}



	[Script]
	public class RoutedActionInfo<T1> : RoutedActionInfoBase
	{
		public Action<T1> Direct;

		public void Chained(T1 t1)
		{
			if (Direct != null)
				Direct(t1);

			if (Handler != null)
				Handler(t1);

			RaiseBaseHandler();

		}

		public event Action<T1> Handler;

		public static implicit operator RoutedActionInfo<T1>(string EventName)
		{
			return new RoutedActionInfo<T1> { EventName = EventName };
		}
	}

	[Script]
	public class RoutedActionInfo<T1, T2> : RoutedActionInfoBase
	{
		public Action<T1, T2> Direct;

		public void Chained(T1 t1, T2 t2)
		{
			if (Direct != null)
				Direct(t1, t2);

			if (Handler != null)
				Handler(t1, t2);

			RaiseBaseHandler();

		}

		public event Action<T1, T2> Handler;

		public static implicit operator RoutedActionInfo<T1, T2>(string EventName)
		{
			return new RoutedActionInfo<T1, T2> { EventName = EventName };
		}
	}

	[Script]
	public class RoutedActionInfo<T1, T2, T3> : RoutedActionInfoBase
	{
		public Action<T1, T2, T3> Direct;

		public void Chained(T1 t1, T2 t2, T3 t3)
		{
			if (Direct != null)
				Direct(t1, t2, t3);

			if (Handler != null)
				Handler(t1, t2, t3);

			RaiseBaseHandler();

		}

		public event Action<T1, T2, T3> Handler;

		public static implicit operator RoutedActionInfo<T1, T2, T3>(string EventName)
		{
			return new RoutedActionInfo<T1, T2, T3> { EventName = EventName };
		}
	}
}
