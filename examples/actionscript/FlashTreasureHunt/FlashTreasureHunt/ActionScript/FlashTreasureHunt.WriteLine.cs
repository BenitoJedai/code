using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Archive.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.Shared.Maze;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{
		public Action<string> WriteLine;

		public TextField WriteLineControl;

		private void InitializeWriteLine()
		{
			var dumper = new TextField
			{
				width = DefaultControlWidth,
				height = DefaultControlHeight / 2,
				textColor = 0xffff00,
				mouseEnabled = false,
				y = DefaultControlHeight / 4,

			}.AttachTo(this);

			WriteLineControl = dumper;
			var dumper_queue = new Queue<string>();

			WriteLine =
				text =>
				{
					dumper_queue.Enqueue(text);

					while (dumper_queue.Count > 10)
						dumper_queue.Dequeue();

					dumper.text = "";

					foreach (var v in dumper_queue)
					{
						dumper.appendText(v + Environment.NewLine);
					}
				};

			this.EgoView.WriteLine = this.WriteLine;

		}
	}
}