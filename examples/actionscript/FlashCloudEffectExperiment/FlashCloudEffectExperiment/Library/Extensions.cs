﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace TheCloudEffect.ActionScript
{
	/// <summary>
	/// This class defines the extension methods for this project
	/// </summary>
	internal static class Extensions
	{
		static Random InternalRandom = new Random();
		public static int Random(this int e)
		{
			return InternalRandom.Next(e);
		}
	}
}
