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
	public static class Fonts
	{
		public const string Path = "/assets/FlashSpaceInvaders.Assets";


		public static readonly string FontFixedSys = "Fixedsys500c";

		[Embed(Path + "/Fixedsys500c.ttf", fontName = "Fixedsys500c")]
		// You do not use this variable directly. It exists so that 
		// the compiler will link in the font.
		static readonly Class Asset_Fixedsys500c;


		static Fonts()
        {
            Font.registerFont(Asset_Fixedsys500c);
        }
	}

	[Script, EmbedFields(Path, "png")]
	public static class Assets
	{
		public const string Path = "/assets/FlashSpaceInvaders.Assets";



		static public readonly Class aenemy_1;

		static public readonly Class aenemy_2;


		static public readonly Class benemy_1;

		static public readonly Class benemy_2;


		static public readonly Class cenemy_1;

		static public readonly Class cenemy_2;

		static public readonly Class biggun_1;

		static public readonly Class ufo_1;

	}
}
