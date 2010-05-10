﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Components;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Ultra.Components.Avalon.Images;

namespace ScriptCoreLib.JavaScript.Components
{
	public class SaveContainerTuple<T>
	{
		public IHTMLDiv Container;
		public T Save;
	}


	public static class VisualStudioViewExtensions
	{
		public static SaveContainerTuple<T> AddSaveTo<T>(this T Save, VisualStudioView vsv, Action<ISaveAction> y)
			where T : Sprite, ISaveActionWhenReady
		{
			var x = vsv.AddSave(Save);

			x.Container.style.backgroundColor = Color.Gray;
			x.Save.WhenReady(
				i =>
				{
					x.Container.style.backgroundColor = Color.None;
					y(i);	
				}
			);

			return x;
		}

		public static SaveContainerTuple<T> AddSave<T>(this VisualStudioView vsv, T Save) where T : Sprite
		{
			var SaveContainer = new IHTMLDiv().With(k => vsv.ApplyToolbarButtonStyle(k)).AttachTo(vsv.PriorityButtons);


			SaveContainer.style.SetSize(
				22,
				22
			);

			SaveContainer.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.inline_block;

			//var Save = new SaveActionSprite();

			Save.ToTransparentSprite();
			Save.AttachSpriteTo(SaveContainer);





			return new SaveContainerTuple<T> { Container = SaveContainer, Save = Save };
		}
	}
}
