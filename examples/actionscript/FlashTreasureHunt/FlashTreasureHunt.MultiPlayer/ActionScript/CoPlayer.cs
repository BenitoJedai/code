using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlashTreasureHunt.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{

	[Script]
	public class CoPlayer
	{
		public SharedClass1.RemoteEvents.UserPlayerAdvertiseArguments Identity;

		public SpriteInfoExtended Guard;

		public object WeaponIdentity = new object();

		public int Kills;
	}

}
