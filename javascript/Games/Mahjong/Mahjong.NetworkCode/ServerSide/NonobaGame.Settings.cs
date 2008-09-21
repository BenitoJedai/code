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
		"Disable navigation bar", 
		"Disallows players to navigate a step back to their last moves", 
		false)]

	[GameSetup.Boolean(
		SettingsInfo.layoutinput,
		"Disable layout chooser",
		"Disallows players to freely choose between layouts",
		false)]

	[GameSetup.Boolean(
		SettingsInfo.vote,
		"Disable voting",
		"Without voting you can change the layout without asking others",
	false)]

	partial class NonobaGame
	{

	}
}
