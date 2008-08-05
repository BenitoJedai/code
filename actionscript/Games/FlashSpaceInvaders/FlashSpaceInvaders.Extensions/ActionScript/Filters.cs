﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public static class Filters
	{
		const double r = 0.212671;
		const double g = 0.715160;
		const double b = 0.072169;

		public static ColorMatrixFilter GrayScaleFilter
		{
			get
			{
				return new ColorMatrixFilter(
					r, g, b, 0, 0,
					r, g, b, 0, 0,
					r, g, b, 0, 0,
					0, 0, 0, 1, 0
				);
			}
		}

		public static ColorMatrixFilter RedChannelFilter
		{
			get
			{
				return new ColorMatrixFilter(
					1, 1, 1, 0, 255,
					r, g, b, 0, 0,
					r, g, b, 0, 0,
					0, 0, 0, 1, 0
				);
			}
		}

		//redResult   = (a[0]  * srcR) + (a[1]  * srcG) + (a[2]  * srcB) + (a[3]  * srcA) + a[4]
		//greenResult = (a[5]  * srcR) + (a[6]  * srcG) + (a[7]  * srcB) + (a[8]  * srcA) + a[9]
		//blueResult  = (a[10] * srcR) + (a[11] * srcG) + (a[12] * srcB) + (a[13] * srcA) + a[14]
		//alphaResult = (a[15] * srcR) + (a[16] * srcG) + (a[17] * srcB) + (a[18] * srcA) + a[19]

//[ "Swap Color RGB2GBR", 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, _
//        1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1], _
//        ["Swap Color RGB2BRG", 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, _
//        0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1], _
//        ["Swap Color RGB2RBG", 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, _
//        0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1], _
//        [ "Swap Colors Transparent", 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, _
//        1, 0, 0, 0, 0, 0, 0, 0, 0.5, 0, 0, 0, 0, 0, 1], _

		

	}
}
