using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTreasureHunt.ActionScript.Properties
{
	[Script]
	public class Int32Property : Property<int>
	{
		public event Action ValueChangedToNonZero;
		public event Action ValueChangedToZero;

		public Int32Property()
		{
			this.ValueChanging +=
				(o, v) =>
				{
					if (v == 0)
					{
						if (o != 0)
							if (ValueChangedToZero != null)
								ValueChangedToZero();
					}
					else
					{
						if (o == 0)
							if (ValueChangedToNonZero != null)
								ValueChangedToNonZero();
					}
				};
		}

		public static implicit operator int(Int32Property e)
		{
			return e.Value;
		}

		public static implicit operator Int32Property(int e)
		{
			return new Int32Property { Value = e };
		}

	


	}

}
