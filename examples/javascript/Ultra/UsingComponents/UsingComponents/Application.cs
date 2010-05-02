using System;
using System.ComponentModel;
using System.Linq;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Lambda;
using UsingComponents.HTML.Pages;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.ActionScript.Components;
using ScriptCoreLib;
using ScriptCoreLib.Ultra.Studio;

namespace UsingComponents
{
	internal sealed class InternalSaveActionSprite : SaveAction, ISaveActionWhenReady
	{
		// this sprite is internal currently because non-internal ultra applications
		// cannot use it.
		// when simplifier is implemented this problem fades away.

		public const int DefaultWidth = 24 - 2;
		public const int DefaultHeight = 24 - 2;


		public void WhenReady(Action<ISaveAction> y)
		{
			y(this);
		}
	}


	[Description("UsingComponents. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			var vsv = new VisualStudioView();

			vsv.Container.AttachTo(a.Content);

			var Save = new InternalSaveActionSprite().AddSaveTo(vsv,
				i =>
				{
					i.FileName = "Project1.zip";


					new SolutionBuilder
					{
						Name = "VisualCSharpProjectX1"
					}.WriteTo(i.Add);

				}
			);
		}
	}
}
