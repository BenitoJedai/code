using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ArgumentsViaReflection.Library
{
	public class ParameterDispatcher : IEnumerable
	{
		public readonly object Arguments;

		public ParameterDispatcher(object e)
		{
			this.Arguments = e;
		}

		class Option
		{
			public string Name;
			public Action Handler;

			public Option Next;
		}

		Option Options;

		public void Add(string Name, Action Handler)
		{
			this.Options = new Option
			{
				Name = Name,
				Handler = Handler,
				Next = this.Options
			};
		}

		public void Add(Action Handler)
		{
			this.Options = new Option
			{
				Handler = Handler,
				Next = this.Options
			};
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			var p = this.Options;

			return new FuncEnumerator(
				delegate
				{
					var z = p;

					if (z != null)
						p = z.Next;

					return z;
				}
			);
		}

		#endregion

		public void Invoke(string OperationName)
		{
			var done = false;
			foreach (Option item in this)
			{
				if (item.Name != null)
				{
					if (item.Name == OperationName)
					{
						item.Handler();
						done = true;
						break;
					}
				}
			}

			if (!done)
			{
				foreach (Option item in this)
				{
					if (item.Name == null)
					{
						item.Handler();
					}
				}
			}
		}
	}

}
