using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class Property<T>
	{
		public event Action ValueChanged;

		T _Value;

		public T Value { get { return _Value; } set { _Value = value; if (ValueChanged != null) ValueChanged(); } }
	}
}
