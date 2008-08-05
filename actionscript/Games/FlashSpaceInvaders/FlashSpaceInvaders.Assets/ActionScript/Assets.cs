using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.text;


namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public static class Assets
	{
		public const string Path = "/assets/FlashSpaceInvaders.Assets";

		public const string FontFixedSys = "Fixedsys500c";

		[Embed(Path + "/Fixedsys500c.ttf", fontName = FontFixedSys)]
		// You do not use this variable directly. It exists so that 
		// the compiler will link in the font.
		static public readonly Class Asset_Fixedsys500c;


		[Embed(source = Path + "/aenemy_1.png")]
		static public readonly Class aenemy_1;

		[Embed(source = Path + "/aenemy_2.png")]
		static public readonly Class aenemy_2;


		[Embed(source = Path + "/benemy_1.png")]
		static public readonly Class benemy_1;

		[Embed(source = Path + "/benemy_2.png")]
		static public readonly Class benemy_2;


		[Embed(source = Path + "/cenemy_1.png")]
		static public readonly Class cenemy_1;

		[Embed(source = Path + "/cenemy_2.png")]
		static public readonly Class cenemy_2;

		[Embed(source = Path + "/biggun_1.png")]
		static public readonly Class biggun_1;

		[Embed(source = Path + "/ufo_1.png")]
		static public readonly Class ufo_1;

		static Assets()
        {
            Font.registerFont(Assets.Asset_Fixedsys500c);
        }
	}
}
