using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mahjong.NetworkCode.Shared;
using Nonoba.GameLibrary;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Shared.Nonoba.Generic;

namespace Mahjong.NetworkCode.ServerSide
{
	using SettingsInfo = VirtualGame.SettingsInfo;

	[GameSetup.Boolean(
		SettingsInfo.navbar, 
		"With Navigation Bar", 
		"Allows players to navigate a step back to their last moves", 
		true)]

	[GameSetup.Boolean(
		SettingsInfo.layoutinput,
		"With Layout Chooser",
		"Allows players to freely choose between layouts",
		true)]

	partial class NonobaGame
	{

	}
}
