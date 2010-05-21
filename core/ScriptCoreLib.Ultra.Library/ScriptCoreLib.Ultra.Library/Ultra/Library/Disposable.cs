using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Library
{
	public class Disposable : IDisposable
	{
		public Action AtDispose
		{
			get;
			set;
		}

		public event Action Disposing
		{
			add
			{
				AtDispose += value;
			}
			remove
			{
				AtDispose -= value;
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (AtDispose != null)
				AtDispose();
		}

		#endregion

		public static implicit operator Disposable(Action AtDispose)
		{
			return new Disposable
			{
				AtDispose = AtDispose
			};
		}

		public static implicit operator Disposable(IDisposable[] e)
		{
			return new Disposable
			{
				AtDispose =
					delegate
					{
						foreach (var item in e)
						{
							item.Dispose();
						}
					}
			};
		}

	}
}
