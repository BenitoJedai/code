using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using Mahjong.NetworkCode.Shared;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	[Script]
	public class CoPlayerGroup
	{
		readonly Func<int, CoPlayer> Constructor;

		public CoPlayerGroup(Func<int, CoPlayer> Constructor)
		{
			this.Constructor = Constructor;
		}

		readonly Dictionary<int, CoPlayer> List = new Dictionary<int, CoPlayer>();

		public CoPlayer this[Communication.RemoteEvents.WithUserArguments k]
		{
			get
			{
				var user = k.user;

				if (this.List.ContainsKey(user))
					return this.List[user];

				var z = this.Constructor(user);

				this.List[user] = z;

				return z;
			}

		}
	}
}
