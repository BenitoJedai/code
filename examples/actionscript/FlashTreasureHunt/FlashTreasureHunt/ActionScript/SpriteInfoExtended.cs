using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.RayCaster;

namespace FlashTreasureHunt.ActionScript
{
	[Script]
	public class SpriteInfoExtended : SpriteInfo
	{
		public bool IsTaken;

		public Action ItemTaken;


		public Action StopWalkingAnimation;
		public Action StartWalkingAnimation;
		public bool WalkingAnimationRunning;

		public bool AIEnabled = true;

		public double Health = 1;


		public Action<double> TakeDamage;

		public Action<double> TakeDamageDone;

		// this is used while syncing
		public int ConstructorIndexForSync = -1;

		#region Minimap
		public uint MinimapColor = 0x9f008000;
		public int MinimapZIndex = MinimapZIndex_Default;

		public const int MinimapZIndex_Default = 0;
		public const int MinimapZIndex_OnTop = 1;
		#endregion


		public Action PlayShootingAnimation;

	}
}
