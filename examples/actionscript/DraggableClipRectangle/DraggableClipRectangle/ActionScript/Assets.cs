using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

//[assembly: ScriptResources(DraggableClipRectangle.ActionScript.Assets.Path)]

namespace DraggableClipRectangle.ActionScript
{

	[Script]
	public class Assets
	{
		public const string Path = "assets/DraggableClipRectangle";

		public static readonly Assets Default = new Assets();

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
