using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace Mahjong.NetworkCode.Shared
{
	[Script]
	public class AvailibleAchievement
	{
		public readonly string Key;

		bool GivenOnce;

		public void Give()
		{
			if (GivenOnce)
				return;

			GivenOnce = true;

			if (_Submit != null)
				_Submit(Key);
		}

		public void GiveMultiple()
		{
			if (_Submit != null)
				_Submit(Key);

		}

		Func<string, uint> _Submit;

		public AvailibleAchievement(Func<string, uint> Submit, string Key)
		{
			this._Submit = Submit;
			this.Key = Key;
		}
	}

}
