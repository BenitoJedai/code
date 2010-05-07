using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using TestSolutionBuilderWithSave.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Components;
using ScriptCoreLib.Ultra.Studio;

namespace TestSolutionBuilderWithSave
{
	internal sealed class InternalSaveActionSprite : SaveAction, ISaveActionWhenReady
	{
		// this sprite is internal currently because non-internal ultra applications
		// cannot use it.
		// when simplifier is implemented this problem fades away.

		public const int DefaultWidth = 24 - 2;
		public const int DefaultHeight = 24 - 2;

		
		public IApplicationWebService WebService { get; set; }

		public void WhenReady(Action<ISaveAction> y)
		{
			WebService.DebugBreak("i am ready!");

			y(this);
		}
	}

	[Description("TestSolutionBuilderWithSave. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			var s = new InternalSaveActionSprite();

			s.AttachSpriteTo(a.Content);

			s.WebService = new ApplicationWebService();

			s.WhenReady(
				i =>
				{
					var sln = new SolutionBuilder
					{

					};

					i.FileName = sln.Name + ".zip";
					sln.WriteTo(
						(SolutionFile f) =>
						{
							new IHTMLDiv { innerText = f.Name }.AttachTo(a.Content);

							i.Add(f.Name, f.Content);
						}
					);
				}
			);
		}


	}


}
