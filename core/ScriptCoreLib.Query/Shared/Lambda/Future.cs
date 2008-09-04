using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Lambda
{
	[Script]
	public class Future<T>
	{
		T _Value;

		public T Value
		{
			get
			{
				return _Value;
			}

			set
			{
				if (_Continue == null)
					throw new Exception("Value can be set only once!");
				_Value = value;

				_Continue.Do();
				_Continue.Clear();
				_Continue = null;




			}
		}

		List<Action> _Continue = new List<Action>();

		public void Continue(Action e)
		{
			if (_Continue != null)
			{
				_Continue.Add(e);
				return;
			}

			e();
		}

		public void Continue(Action<T> e)
		{
			if (_Continue != null)
			{
				_Continue.Add(() => e(this.Value));
				return;
			}

			e(this.Value);
		}
	}


	[Script]
	public class FutureAction<T> : Future<Action<T>>
	{
		public void Continue(T e)
		{
			this.Continue(Handler => Handler(e));
		}

		public static implicit operator Action<T>(FutureAction<T> e)
		{
			return e.Continue;
		}

		public Action this[T e]
		{
			get
			{
				return () => this.Continue(e);
			}
		}
	}
}
