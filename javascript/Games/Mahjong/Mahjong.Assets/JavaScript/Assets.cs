using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared;

namespace Mahjong.JavaScript
{
	[Script(Implements=typeof(Mahjong.Shared.Assets))]
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


	}
}
