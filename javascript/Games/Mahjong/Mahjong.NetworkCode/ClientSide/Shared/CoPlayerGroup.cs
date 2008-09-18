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

		public readonly Dictionary<int, CoPlayer> List = new Dictionary<int, CoPlayer>();

		public CoPlayer this[int user]
		{
			get
			{

				if (this.List.ContainsKey(user))
					return this.List[user];

				var z = this.Constructor(user);

				this.List[user] = z;

				return z;
			}

		}

		public void Remove(int user)
		{
			this.List.Remove(user);
		}

	
	}
}
