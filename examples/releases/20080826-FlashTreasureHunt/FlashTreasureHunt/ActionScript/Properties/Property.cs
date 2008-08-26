using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTreasureHunt.ActionScript.Properties
{
	[Script]
	public class Property<T>
	{
		public event Action<T, T> ValueChanging;
		public event Action<T> ValueChangedTo;
		public event Action ValueChanged;

		T _Value;



		public T Value
		{
			get { return _Value; }
			set
			{
				var _old = _Value;



				_Value = value;

				if (ValueChanging != null)
					ValueChanging(_old, value);

				

				if (ValueChangedTo != null)
					ValueChangedTo(value);

				if (ValueChanged != null)
					ValueChanged();
			}
		}

		public void LinkTo(Property<T> e)
		{
			this.ValueChanged +=
				delegate
				{
					e.Value = this.Value;
				};
		}

		public static implicit operator Action<T>(Property<T> e)
		{
			return value => e.Value = value;
		}

	}
}
