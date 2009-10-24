using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Drawing;

namespace ScriptCoreLibJava.BCLImplementation.System.Drawing
{
	[Script(Implements = typeof(global::System.Drawing.Color))]
	internal class __Color
	{
		byte _R;
		byte _G;
		byte _B;

		public byte R
		{
			get {
				var e = _R;
				return e;
			}
		}

		public byte G
		{
			get {
				var e = _G;
				return e; }
		}

		public byte B
		{
			get { var e = _B;  return e; }
		}

		public __Color()
		{

		}


		public static Color FromArgb(int red, int green, int blue)
		{
			return new __Color { 
				_R = (byte)red,
				_G = (byte)green,
				_B = (byte)blue
			};
		}

		static __Color()
		{
			Transparent = FromArgb(255, 255, 255);
			AliceBlue = FromArgb(240, 248, 255);
			AntiqueWhite = FromArgb(250, 235, 215);
			Aqua = FromArgb(0, 255, 255);
			Aquamarine = FromArgb(127, 255, 212);
			Azure = FromArgb(240, 255, 255);
			Beige = FromArgb(245, 245, 220);
			Bisque = FromArgb(255, 228, 196);
			Black = FromArgb(0, 0, 0);
			BlanchedAlmond = FromArgb(255, 235, 205);
			Blue = FromArgb(0, 0, 255);
			BlueViolet = FromArgb(138, 43, 226);
			Brown = FromArgb(165, 42, 42);
			BurlyWood = FromArgb(222, 184, 135);
			CadetBlue = FromArgb(95, 158, 160);
			Chartreuse = FromArgb(127, 255, 0);
			Chocolate = FromArgb(210, 105, 30);
			Coral = FromArgb(255, 127, 80);
			CornflowerBlue = FromArgb(100, 149, 237);
			Cornsilk = FromArgb(255, 248, 220);
			Crimson = FromArgb(220, 20, 60);
			Cyan = FromArgb(0, 255, 255);
			DarkBlue = FromArgb(0, 0, 139);
			DarkCyan = FromArgb(0, 139, 139);
			DarkGoldenrod = FromArgb(184, 134, 11);
			DarkGray = FromArgb(169, 169, 169);
			DarkGreen = FromArgb(0, 100, 0);
			DarkKhaki = FromArgb(189, 183, 107);
			DarkMagenta = FromArgb(139, 0, 139);
			DarkOliveGreen = FromArgb(85, 107, 47);
			DarkOrange = FromArgb(255, 140, 0);
			DarkOrchid = FromArgb(153, 50, 204);
			DarkRed = FromArgb(139, 0, 0);
			DarkSalmon = FromArgb(233, 150, 122);
			DarkSeaGreen = FromArgb(143, 188, 139);
			DarkSlateBlue = FromArgb(72, 61, 139);
			DarkSlateGray = FromArgb(47, 79, 79);
			DarkTurquoise = FromArgb(0, 206, 209);
			DarkViolet = FromArgb(148, 0, 211);
			DeepPink = FromArgb(255, 20, 147);
			DeepSkyBlue = FromArgb(0, 191, 255);
			DimGray = FromArgb(105, 105, 105);
			DodgerBlue = FromArgb(30, 144, 255);
			Firebrick = FromArgb(178, 34, 34);
			FloralWhite = FromArgb(255, 250, 240);
			ForestGreen = FromArgb(34, 139, 34);
			Fuchsia = FromArgb(255, 0, 255);
			Gainsboro = FromArgb(220, 220, 220);
			GhostWhite = FromArgb(248, 248, 255);
			Gold = FromArgb(255, 215, 0);
			Goldenrod = FromArgb(218, 165, 32);
			Gray = FromArgb(128, 128, 128);
			Green = FromArgb(0, 128, 0);
			GreenYellow = FromArgb(173, 255, 47);
			Honeydew = FromArgb(240, 255, 240);
			HotPink = FromArgb(255, 105, 180);
			IndianRed = FromArgb(205, 92, 92);
			Indigo = FromArgb(75, 0, 130);
			Ivory = FromArgb(255, 255, 240);
			Khaki = FromArgb(240, 230, 140);
			Lavender = FromArgb(230, 230, 250);
			LavenderBlush = FromArgb(255, 240, 245);
			LawnGreen = FromArgb(124, 252, 0);
			LemonChiffon = FromArgb(255, 250, 205);
			LightBlue = FromArgb(173, 216, 230);
			LightCoral = FromArgb(240, 128, 128);
			LightCyan = FromArgb(224, 255, 255);
			LightGoldenrodYellow = FromArgb(250, 250, 210);
			LightGreen = FromArgb(144, 238, 144);
			LightGray = FromArgb(211, 211, 211);
			LightPink = FromArgb(255, 182, 193);
			LightSalmon = FromArgb(255, 160, 122);
			LightSeaGreen = FromArgb(32, 178, 170);
			LightSkyBlue = FromArgb(135, 206, 250);
			LightSlateGray = FromArgb(119, 136, 153);
			LightSteelBlue = FromArgb(176, 196, 222);
			LightYellow = FromArgb(255, 255, 224);
			Lime = FromArgb(0, 255, 0);
			LimeGreen = FromArgb(50, 205, 50);
			Linen = FromArgb(250, 240, 230);
			Magenta = FromArgb(255, 0, 255);
			Maroon = FromArgb(128, 0, 0);
			MediumAquamarine = FromArgb(102, 205, 170);
			MediumBlue = FromArgb(0, 0, 205);
			MediumOrchid = FromArgb(186, 85, 211);
			MediumPurple = FromArgb(147, 112, 219);
			MediumSeaGreen = FromArgb(60, 179, 113);
			MediumSlateBlue = FromArgb(123, 104, 238);
			MediumSpringGreen = FromArgb(0, 250, 154);
			MediumTurquoise = FromArgb(72, 209, 204);
			MediumVioletRed = FromArgb(199, 21, 133);
			MidnightBlue = FromArgb(25, 25, 112);
			MintCream = FromArgb(245, 255, 250);
			MistyRose = FromArgb(255, 228, 225);
			Moccasin = FromArgb(255, 228, 181);
			NavajoWhite = FromArgb(255, 222, 173);
			Navy = FromArgb(0, 0, 128);
			OldLace = FromArgb(253, 245, 230);
			Olive = FromArgb(128, 128, 0);
			OliveDrab = FromArgb(107, 142, 35);
			Orange = FromArgb(255, 165, 0);
			OrangeRed = FromArgb(255, 69, 0);
			Orchid = FromArgb(218, 112, 214);
			PaleGoldenrod = FromArgb(238, 232, 170);
			PaleGreen = FromArgb(152, 251, 152);
			PaleTurquoise = FromArgb(175, 238, 238);
			PaleVioletRed = FromArgb(219, 112, 147);
			PapayaWhip = FromArgb(255, 239, 213);
			PeachPuff = FromArgb(255, 218, 185);
			Peru = FromArgb(205, 133, 63);
			Pink = FromArgb(255, 192, 203);
			Plum = FromArgb(221, 160, 221);
			PowderBlue = FromArgb(176, 224, 230);
			Purple = FromArgb(128, 0, 128);
			Red = FromArgb(255, 0, 0);
			RosyBrown = FromArgb(188, 143, 143);
			RoyalBlue = FromArgb(65, 105, 225);
			SaddleBrown = FromArgb(139, 69, 19);
			Salmon = FromArgb(250, 128, 114);
			SandyBrown = FromArgb(244, 164, 96);
			SeaGreen = FromArgb(46, 139, 87);
			SeaShell = FromArgb(255, 245, 238);
			Sienna = FromArgb(160, 82, 45);
			Silver = FromArgb(192, 192, 192);
			SkyBlue = FromArgb(135, 206, 235);
			SlateBlue = FromArgb(106, 90, 205);
			SlateGray = FromArgb(112, 128, 144);
			Snow = FromArgb(255, 250, 250);
			SpringGreen = FromArgb(0, 255, 127);
			SteelBlue = FromArgb(70, 130, 180);
			Tan = FromArgb(210, 180, 140);
			Teal = FromArgb(0, 128, 128);
			Thistle = FromArgb(216, 191, 216);
			Tomato = FromArgb(255, 99, 71);
			Turquoise = FromArgb(64, 224, 208);
			Violet = FromArgb(238, 130, 238);
			Wheat = FromArgb(245, 222, 179);
			White = FromArgb(255, 255, 255);
			WhiteSmoke = FromArgb(245, 245, 245);
			Yellow = FromArgb(255, 255, 0);
			YellowGreen = FromArgb(154, 205, 50);
		}

		static public implicit operator Color(__Color e)
		{
			return (Color)(object)e;
		}

		static public implicit operator __Color(Color e)
		{
			return (__Color)(object)e;
		}

		static public Color Transparent { get; set; }
		static public Color AliceBlue { get; set; }
		static public Color AntiqueWhite { get; set; }
		static public Color Aqua { get; set; }
		static public Color Aquamarine { get; set; }
		static public Color Azure { get; set; }
		static public Color Beige { get; set; }
		static public Color Bisque { get; set; }
		static public Color Black { get; set; }
		static public Color BlanchedAlmond { get; set; }
		static public Color Blue { get; set; }
		static public Color BlueViolet { get; set; }
		static public Color Brown { get; set; }
		static public Color BurlyWood { get; set; }
		static public Color CadetBlue { get; set; }
		static public Color Chartreuse { get; set; }
		static public Color Chocolate { get; set; }
		static public Color Coral { get; set; }
		static public Color CornflowerBlue { get; set; }
		static public Color Cornsilk { get; set; }
		static public Color Crimson { get; set; }
		static public Color Cyan { get; set; }
		static public Color DarkBlue { get; set; }
		static public Color DarkCyan { get; set; }
		static public Color DarkGoldenrod { get; set; }
		static public Color DarkGray { get; set; }
		static public Color DarkGreen { get; set; }
		static public Color DarkKhaki { get; set; }
		static public Color DarkMagenta { get; set; }
		static public Color DarkOliveGreen { get; set; }
		static public Color DarkOrange { get; set; }
		static public Color DarkOrchid { get; set; }
		static public Color DarkRed { get; set; }
		static public Color DarkSalmon { get; set; }
		static public Color DarkSeaGreen { get; set; }
		static public Color DarkSlateBlue { get; set; }
		static public Color DarkSlateGray { get; set; }
		static public Color DarkTurquoise { get; set; }
		static public Color DarkViolet { get; set; }
		static public Color DeepPink { get; set; }
		static public Color DeepSkyBlue { get; set; }
		static public Color DimGray { get; set; }
		static public Color DodgerBlue { get; set; }
		static public Color Firebrick { get; set; }
		static public Color FloralWhite { get; set; }
		static public Color ForestGreen { get; set; }
		static public Color Fuchsia { get; set; }
		static public Color Gainsboro { get; set; }
		static public Color GhostWhite { get; set; }
		static public Color Gold { get; set; }
		static public Color Goldenrod { get; set; }
		static public Color Gray { get; set; }
		static public Color Green { get; set; }
		static public Color GreenYellow { get; set; }
		static public Color Honeydew { get; set; }
		static public Color HotPink { get; set; }
		static public Color IndianRed { get; set; }
		static public Color Indigo { get; set; }
		static public Color Ivory { get; set; }
		static public Color Khaki { get; set; }
		static public Color Lavender { get; set; }
		static public Color LavenderBlush { get; set; }
		static public Color LawnGreen { get; set; }
		static public Color LemonChiffon { get; set; }
		static public Color LightBlue { get; set; }
		static public Color LightCoral { get; set; }
		static public Color LightCyan { get; set; }
		static public Color LightGoldenrodYellow { get; set; }
		static public Color LightGreen { get; set; }
		static public Color LightGray { get; set; }
		static public Color LightPink { get; set; }
		static public Color LightSalmon { get; set; }
		static public Color LightSeaGreen { get; set; }
		static public Color LightSkyBlue { get; set; }
		static public Color LightSlateGray { get; set; }
		static public Color LightSteelBlue { get; set; }
		static public Color LightYellow { get; set; }
		static public Color Lime { get; set; }
		static public Color LimeGreen { get; set; }
		static public Color Linen { get; set; }
		static public Color Magenta { get; set; }
		static public Color Maroon { get; set; }
		static public Color MediumAquamarine { get; set; }
		static public Color MediumBlue { get; set; }
		static public Color MediumOrchid { get; set; }
		static public Color MediumPurple { get; set; }
		static public Color MediumSeaGreen { get; set; }
		static public Color MediumSlateBlue { get; set; }
		static public Color MediumSpringGreen { get; set; }
		static public Color MediumTurquoise { get; set; }
		static public Color MediumVioletRed { get; set; }
		static public Color MidnightBlue { get; set; }
		static public Color MintCream { get; set; }
		static public Color MistyRose { get; set; }
		static public Color Moccasin { get; set; }
		static public Color NavajoWhite { get; set; }
		static public Color Navy { get; set; }
		static public Color OldLace { get; set; }
		static public Color Olive { get; set; }
		static public Color OliveDrab { get; set; }
		static public Color Orange { get; set; }
		static public Color OrangeRed { get; set; }
		static public Color Orchid { get; set; }
		static public Color PaleGoldenrod { get; set; }
		static public Color PaleGreen { get; set; }
		static public Color PaleTurquoise { get; set; }
		static public Color PaleVioletRed { get; set; }
		static public Color PapayaWhip { get; set; }
		static public Color PeachPuff { get; set; }
		static public Color Peru { get; set; }
		static public Color Pink { get; set; }
		static public Color Plum { get; set; }
		static public Color PowderBlue { get; set; }
		static public Color Purple { get; set; }
		static public Color Red { get; set; }
		static public Color RosyBrown { get; set; }
		static public Color RoyalBlue { get; set; }
		static public Color SaddleBrown { get; set; }
		static public Color Salmon { get; set; }
		static public Color SandyBrown { get; set; }
		static public Color SeaGreen { get; set; }
		static public Color SeaShell { get; set; }
		static public Color Sienna { get; set; }
		static public Color Silver { get; set; }
		static public Color SkyBlue { get; set; }
		static public Color SlateBlue { get; set; }
		static public Color SlateGray { get; set; }
		static public Color Snow { get; set; }
		static public Color SpringGreen { get; set; }
		static public Color SteelBlue { get; set; }
		static public Color Tan { get; set; }
		static public Color Teal { get; set; }
		static public Color Thistle { get; set; }
		static public Color Tomato { get; set; }
		static public Color Turquoise { get; set; }
		static public Color Violet { get; set; }
		static public Color Wheat { get; set; }
		static public Color White { get; set; }
		static public Color WhiteSmoke { get; set; }
		static public Color Yellow { get; set; }
		static public Color YellowGreen { get; set; }
	}
}
