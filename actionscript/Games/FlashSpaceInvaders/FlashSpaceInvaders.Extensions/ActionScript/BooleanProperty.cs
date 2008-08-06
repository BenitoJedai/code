using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class BooleanProperty : Property<bool>
	{
		public event Action ValueChangedToTrue;
		public event Action ValueChangedToFalse;

		public BooleanProperty()
		{
			this.ValueChanged +=
				delegate
				{
					if (this.Value)
					{
						if (ValueChangedToTrue != null)
							ValueChangedToTrue();
					}
					else
					{
						if (ValueChangedToFalse != null)
							ValueChangedToFalse();
					}
				};
		}

		public static implicit operator bool(BooleanProperty e)
		{
			return e.Value;
		}

		public static implicit operator BooleanProperty(bool e)
		{
			return new BooleanProperty { Value = e };
		}

		public void Toggle()
		{
			this.Value = !this.Value;
		}
	}

}
