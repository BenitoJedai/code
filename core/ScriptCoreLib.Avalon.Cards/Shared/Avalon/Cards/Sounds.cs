using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public class Sounds
	{
		public Action deal;
		public Action click;
		public Action drag;
		public Action win;

		public Sounds()
		{
			this.deal = delegate { };
			this.click = delegate { };
			this.drag = delegate { };
			this.win = delegate { };
		}
	}
}
