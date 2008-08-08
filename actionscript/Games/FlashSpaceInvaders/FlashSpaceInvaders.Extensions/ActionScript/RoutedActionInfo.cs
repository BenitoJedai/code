using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public delegate void Action<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);


	[Script]
	public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);

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

	[Script]
	public class RoutedActionInfo<T1, T2, T3, T4> : RoutedActionInfoBase
	{
		public Action<T1, T2, T3, T4> Direct;

		public void Chained(T1 t1, T2 t2, T3 t3, T4 t4)
		{
			if (Direct != null)
				Direct(t1, t2, t3, t4);

			if (Handler != null)
				Handler(t1, t2, t3, t4);

			RaiseBaseHandler();

		}

		public event Action<T1, T2, T3, T4> Handler;

		public static implicit operator RoutedActionInfo<T1, T2, T3, T4>(string EventName)
		{
			return new RoutedActionInfo<T1, T2, T3, T4> { EventName = EventName };
		}
	}

	[Script]
	public class RoutedActionInfo<T1, T2, T3, T4, T5> : RoutedActionInfoBase
	{
		public Action<T1, T2, T3, T4, T5> Direct;

		public void Chained(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
		{
			if (Direct != null)
				Direct(t1, t2, t3, t4, t5);

			if (Handler != null)
				Handler(t1, t2, t3, t4, t5);

			RaiseBaseHandler();

		}

		public event Action<T1, T2, T3, T4, T5> Handler;

		public static implicit operator RoutedActionInfo<T1, T2, T3, T4, T5>(string EventName)
		{
			return new RoutedActionInfo<T1, T2, T3, T4, T5> { EventName = EventName };
		}
	}


	[Script]
	public class RoutedActionInfo<T1, T2, T3, T4, T5, T6> : RoutedActionInfoBase
	{
		public Action<T1, T2, T3, T4, T5, T6> Direct;

		public void Chained(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
		{
			if (Direct != null)
				Direct(t1, t2, t3, t4, t5, t6);

			if (Handler != null)
				Handler(t1, t2, t3, t4, t5, t6);

			RaiseBaseHandler();

		}

		public event Action<T1, T2, T3, T4, T5, T6> Handler;

		public static implicit operator RoutedActionInfo<T1, T2, T3, T4, T5, T6>(string EventName)
		{
			return new RoutedActionInfo<T1, T2, T3, T4, T5, T6> { EventName = EventName };
		}
	}
}
