﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.mx.core;

////[assembly: ScriptResources(FlashTreasureHunt.ActionScript.Assets.Path)]

namespace FlashTreasureHunt.ActionScript
{
	
	partial class Assets
	{

		public ZipFileEntry[] dude5
		{
			get
			{
				return this[Path + "/dude5.zip"].ToFiles();
			}
		}

		public ZipFileEntry[] gold
		{
			get
			{
				return this[Path + "/gold.zip"].ToFiles();
			}
		}

		public ZipFileEntry[] stuff
		{
			get
			{
				return this[Path + "/stuff.zip"].ToFiles();
			}
		}

		public BitmapAsset compass
		{
			get
			{
				return this[Path + "/compass.png"].ToBitmapAsset();
			}
		}

		public BitmapAsset compasscolor
		{
			get
			{
				return this[Path + "/compasscolor.png"].ToBitmapAsset();
			}
		}

		public BitmapAsset hud
		{
			get
			{
				return this[Path + "/hud.png"].ToBitmapAsset();
			}
		}
	}
}
