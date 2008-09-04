﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace System_IO_StringReader.JavaScript
{
	using TargetCanvas = global::System_IO_StringReader.Shared.MyCanvas;

	[Script, ScriptApplicationEntryPoint]
	public class MyCanvasScriptControl
	{
		public MyCanvasScriptControl(IHTMLElement e)
		{
			// wpf here
			var clip = new IHTMLDiv();

			clip.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
			clip.style.SetSize(TargetCanvas.DefaultWidth, TargetCanvas.DefaultHeight);
			clip.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

			if (e == null)
				clip.AttachToDocument();
			else
				e.insertPreviousSibling(clip);

			AvalonExtensions.AttachToContainer(new TargetCanvas(), clip);

		}

		static MyCanvasScriptControl()
		{
			typeof(MyCanvasScriptControl).SpawnTo(i => new MyCanvasScriptControl(i));
		}

	}
}
