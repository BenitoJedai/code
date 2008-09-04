﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using EmbeddedResourcesBrowser.Shared;
using ScriptCoreLib.Shared;


namespace EmbeddedResourcesBrowser.ActionScript
{

	[Script(Implements = typeof(Assets))]
	public class __Assets
	{
		public static readonly __Assets Default = new __Assets();

		public string[] FileNames
		{
			[EmbedGetFileNames]
			get
			{
				throw new NotImplementedException();
			}
		}

		public Class this[string e]
		{
			[EmbedByFileName]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
