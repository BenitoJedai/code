using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.AppEngine.Extensions.memcache
{
	[Script]
	public class MemcacheCollection : IEnumerable
	{
		public MemcacheEntry Context;

		Type InternalElementType;
		public Type ElementType
		{
			get
			{
				return InternalElementType;
			}
			set
			{
				InternalElementType = value;
				Context = value;
			}
		}

		MemcacheEntry InternalIndex
		{
			get
			{
				var i = Context.ToEntry(".Index", IndexExpiationInSeconds);

				if (!i.Exists)
					i.ValueInt64 = 0;

				return i;
			}
		}

		public long Generation
		{
			get
			{
				return InternalIndex.ValueInt64;
			}
		}

		public int IndexExpiationInSeconds = 8 * 60;
		public int ElementExpiationInSeconds = 5 * 60;

		public void Add(object e)
		{
			var n = this.InternalIndex.Increment();

			Context.ToEntry("#" + n, ElementExpiationInSeconds).Value = e;
		}

		public IEnumerator GetEnumerator()
		{
			return new Enumerator { Context = Context, Index = InternalIndex.ValueInt64 };
		}


		public int Count
		{
			get
			{
				var c = 0;

				foreach (var k in this)
					c++;

				return c;
			}
		}

		[Script]
		public class Enumerator : IEnumerator, IDisposable
		{
			public MemcacheEntry Context;
			public long Index;

			public MemcacheEntry InternalCurrent;

			#region IEnumerator Members

			public object Current
			{
				get { return InternalCurrent.Value; }
			}

			public bool MoveNext()
			{
				var e = Context.ToEntry("#" + Index);
				Index--;
				if (Index >= 0)
					if (e.Exists)
					{
						this.InternalCurrent = e;
						return true;
					}

				this.InternalCurrent = null;
				return false;
			}

			public void Reset()
			{
				throw new NotImplementedException();
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{
				this.InternalCurrent = null;
				this.Context = null;

			}

			#endregion
		}


	}


}
