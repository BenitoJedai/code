using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Studio;
using TestSolutionBuilder.HTML.Pages;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.JavaScript.Components;
namespace TestSolutionBuilder
{

	[Description("TestSolutionBuilder. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			new SolutionBuilder
			{

			}.WriteTo(
				(SolutionFile f) =>
				{
					var s = new Section().ToSectionConcept();

					s.Header = f.Name;
					s.Header.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Courier;

			
					s.Content = new SolutionFileView { File = f }.Container;

					s.Content.style.border = "1px solid gray";
		
					s.IsExpanded = false;
					s.Target.Container.AttachTo(a.Content);

				}
			);
		}


	}


}
