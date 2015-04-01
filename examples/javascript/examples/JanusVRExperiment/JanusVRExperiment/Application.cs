using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using JanusVRExperiment;
using JanusVRExperiment.Design;
using JanusVRExperiment.HTML.Pages;

namespace JanusVRExperiment
{
	[Script(HasNoPrototype = true)]
	public class Room
	{
		//public Action onLoad;
		public IFunction onLoad;
	}

	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		[Script(OptimizedCode = @"
//room.onLoad = function() {

for (var i=0; i<5; ++i) {

	var textid = 'new_text'+i;

	room.createObject('Text', {js_id: textid});

	room.objects[textid].pos = Vector(5+i,1+i,0);
	room.objects[textid].fwd = Vector(-1,0,0);
	room.objects[textid].text = 'Application This is new text with js_id: ' + room.objects[textid].js_id;
        room.objects[textid].scale = Vector(5,5,5);
	room.objects[textid].col = Vector(.5,1,.5);
}

room.objects['new_text0'].text = 'Application Clicks: 0';
        room.objects['new_text0'].col = Vector(1,0,0);

//}
")]
		static void Invoke() { }

		//[Script(OptimizedCode = "room")]
		[Script(ExternalTarget = "room")]
		public static Room room;

		static Application()
		{
			// [K apr 1 22:09:33 2015] JavaScript error in view-source (line 76902): TypeError: Result of expression 'AQAABDWbFDuQBN_brmZIFqQ' [null] is not an object.

			Console.WriteLine("enter Application");
			room.onLoad = IFunction.Of(
				delegate
				{
					Invoke();
				}
			)
			;

		}

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			// .\..\..\..\jsc.svn\examples\javascript\examples\JanusVRExperiment\JanusVRExperiment\Design\App.htm
			// "X:\util\janusvr_windows\JanusVR_Win32\workspaces.txt"

			// http://www.dgp.toronto.edu/~mccrae/projects/firebox/js.html

			//[K apr 1 22:00:15 2015] error loading file:///X:/jsc.svn/examples/javascript/examples/JanusVRExperiment/JanusVRExperiment/Design/thumb.jpg
			//[K apr 1 22:00:15 2015] error loading file:///X:/jsc.svn/examples/javascript/examples/JanusVRExperiment/JanusVRExperiment/bin/Debug/staging/JanusVRExperiment.Application/web/thumb.jpg
		}

	}
}
