using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.Lambda
{
	[Script]
	public class BindingListWithEvents<T> : IDisposable
	{
		BindingList<T> InternalList;

		public BindingList<T> Source
		{
			get
			{
				return InternalList;
			}
		}

		public event Action<T, int> Removed;
		public event Action<T, int> Added;

		public BindingListWithEvents(BindingList<T> source)
		{
			this.InternalList = source;

			var cache = new List<T>();

			cache.AddRange(source);

			ListChangedEventHandler h = null;

			this.InternalDispose =
				delegate
				{
					source.ListChanged -= h;
					h = null;
					cache.Clear();
					cache = null;

					this.InternalList = null;
				};

			h = (sender0, args0) =>
			{
				if (args0.ListChangedType == ListChangedType.ItemAdded)
				{
					var k = source[args0.NewIndex];

					cache.Add(k);

					if (this.Added != null)
						this.Added(k, args0.NewIndex);

					return;
				}

				if (args0.ListChangedType == ListChangedType.ItemDeleted)
				{
					var k = cache[args0.NewIndex];

					cache.RemoveAt(args0.NewIndex);

					if (this.Removed != null)
						this.Removed(k, args0.NewIndex);
				}
			};

			source.ListChanged += h;
		}

		Action InternalDispose;

		#region IDisposable Members

		public void Dispose()
		{
			if (InternalDispose != null)
			{
				InternalDispose();
				InternalDispose = null;
			}
		}

		#endregion
	}

}
