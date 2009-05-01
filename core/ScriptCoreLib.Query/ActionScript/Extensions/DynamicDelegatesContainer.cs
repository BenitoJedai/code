using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public class DynamicDelegatesContainer : DynamicContainer, IEnumerable<object>
	{
		public DynamicDelegatesContainer()
		{
			this.Subject = new object();

			// remember in actionscript the object is extendable aka dynamic
		}

		public void Add<T>(string name, Action<T> handler)
		{
			Add(name, (Delegate)handler);
		}

		public void Add<T, R>(string name, Converter<T, R> handler)
		{
			Add(name, (Delegate)handler);

		}
		public void Add(string name, Delegate handler)
		{
			this[name] = handler.ToFunction();
		}


		public Func<R> ToFunc<R>(string name)
		{
			return
				() =>
				{
					var f = this[name] as Function;

					var a = new ScriptCoreLib.ActionScript.Array();
					return (R)f.apply(this.Subject, a);
				};
		}

		public Func<T, R> ToFunc<T, R>(string name)
		{
			return
				t =>
				{
					var f = this[name] as Function;

					var a = new ScriptCoreLib.ActionScript.Array();
					a.push(t);
					return (R)f.apply(this.Subject, a);
				};
		}


		public Func<T, T2, R> ToFunc<T, T2, R>(string name)
		{
			return
				(t, t2) =>
				{
					var f = this[name] as Function;

					var a = new ScriptCoreLib.ActionScript.Array();
					a.push(t);
					a.push(t2);
					return (R)f.apply(this.Subject, a);
				};
		}


		#region IEnumerable<object> Members

		public IEnumerator<object> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}

}
