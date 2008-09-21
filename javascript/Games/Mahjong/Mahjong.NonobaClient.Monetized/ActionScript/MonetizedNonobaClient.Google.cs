using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace Mahjong.NonobaClient.Monetized.ActionScript
{
	using TargetCanvas = global::Mahjong.NonobaClient.ActionScript.NonobaClient;
	using ScriptCoreLib.Shared;
	using ScriptCoreLib.ActionScript.MochiLibrary;
	using Mahjong.PromotionalAssets;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[GoogleGadget(
	   author_email = MahjongInfo.EMail,
	   author_link = MahjongInfo.Blog,
	   author = MahjongInfo.Author,
	   category = MahjongInfo.Category1,
	   category2 = MahjongInfo.Category2,
	   screenshot = MahjongInfo.ScreenshotURL,
	   thumbnail = MahjongInfo.ScreenshotSmallURL,
	   description = MahjongInfo.Description,
	   width = TargetCanvas.DefaultWidth,
	   height = TargetCanvas.DefaultHeight,
	   title = MahjongInfo.Title,
	   title_url = MahjongInfo.URL

    )]
	partial class MonetizedNonobaClient
	{

	}

}