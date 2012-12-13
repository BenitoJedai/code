using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.ActionScript.FragileEntities;
using ScriptCoreLib.ActionScript.flash.geom;

using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public partial class GameSharedState
	{
		public const int MaxObjectsPerSection = 1000;
		public const int MaxPlayers = 32;

		public readonly List<object> LocalObjects = new List<object>();

		public readonly List<object> SharedObjects = new List<object>();

		public readonly Dictionary<int, List<object>> RemoteObjects = new Dictionary<int, List<object>>();

		public GameSharedState()
		{
			for (int i = 0; i < MaxPlayers; i++)
			{
				this.RemoteObjects[i] = new List<object>();
			}
		}

		public int this[object e]
		{
			get
			{
				var i = this.LocalObjects.IndexOf(e);

				if (i != -1)
					return i;

				i = this.SharedObjects.IndexOf(e);

				if (i != -1)
					return i + MaxObjectsPerSection;
			
				// are we talking about remote objects?
				for (int p = 0; p < MaxPlayers; p++)
				{
					var s = this.RemoteObjects[p];

					i = s.IndexOf(e);

					if (i != -1)
					{
						return i + MaxObjectsPerSection * p + MaxObjectsPerSection * 2;
					}
				}
				
				throw new Exception("This object is not known shared state");
			}
		}

		public ParentRelation<object, object> this[int user, int i]
		{
			get
			{
				// if a user reports a local object, we will treat it as his local object.

				if (i == -1)
					return null;

				// 0..999 are not shared objects
				if (i < MaxObjectsPerSection)
					return this.RemoteObjects[user][i].WithParent<object, object>(this.RemoteObjects);

				if (i < MaxObjectsPerSection * 2)
					return this.SharedObjects[i - MaxObjectsPerSection].WithParent<object, object>(this.SharedObjects);

				var j = i - MaxObjectsPerSection * 2;
				var p = 0;

				while (j >= MaxObjectsPerSection)
				{
					p++;
					j -= MaxObjectsPerSection;
				}

				if (p >= MaxPlayers)
					throw new Exception("i out of range");

				// we never report a local object, as they should be mapped as remote objects too...
				var r = this.RemoteObjects[p];

				if (j >= r.Count)
					throw new Exception("player " + p + " does not have entity " + j);

				return r[j].WithParent<object, object>(this.RemoteObjects);
			}
		}

	}

	partial class Game
	{
		public readonly GameSharedState SharedState = new GameSharedState();

		void InitializeSharedState()
		{

		}

	}
}
