using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Archive.Extensions;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Shared.Archive;
using System.IO;

////[assembly: ScriptResources(FlashTreasureHunt.ActionScript.Assets.Path)]

namespace FlashTreasureHunt.ActionScript
{

	partial class Assets
	{

		public ZIPFile dude5
		{
			get
			{
				return this[Path + "/dude5.zip"].ToZIPFile();
			}
		}

		public ZIPFile gold
		{
			get
			{
				return this[Path + "/gold.zip"].ToZIPFile();
			}
		}

		public ZIPFile stuff
		{
			get
			{
				return this[Path + "/stuff.zip"].ToZIPFile();
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

		public ZIPFile head
		{
			get
			{
				return this[Path + "/head.zip"].ToZIPFile();
			}
		}


		public ZIPFile nonblock
		{
			get
			{
				return this[Path + "/nonblock.zip"].ToZIPFile();
			}
		}
	}
}
