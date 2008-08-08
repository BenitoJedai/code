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

				if (i == -1)
					throw new Exception("This object is not known shared state");

				return i + MaxObjectsPerSection;
			}
		}

		public ParentRelation<object, object> this[int user, int i]
		{
			get
			{
				if (i == -1)
					return null;

				// 0..999 are not shared objects
				if (i < MaxObjectsPerSection)
					return this.RemoteObjects[user][i].WithParent<object, object>(this.RemoteObjects);

				return this.SharedObjects[i - MaxObjectsPerSection].WithParent<object, object>(this.SharedObjects);
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
