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

// nuget namespace. idl?
[Script(HasNoPrototype = true)]
public class FireBoxRoom
{
	//  Room Contents
	[Script(HasNoPrototype = true)]
	public class Objects
	{
		// perhaps this is just a dictionary?
		// how many objects are there?
		//public int length;

		public Object this[string js_id]
		{
			get
			{
				return null;
			}
		}
	}

	// a pseudo type
	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public sealed class Text : Object
	{
		public Text(string js_id)
		{

		}


		static Text InternalConstructor(string js_id)
		{
			return (Text)room.createObject("Text", new { js_id });
			//return (Text)room.objects[js_id];

		}
	}

	[Script(HasNoPrototype = true)]
	public class Object
	{
		public string js_id;

		public string text;

		public Vector col;
		public Vector pos;
		public Vector scale;
		public Vector fwd;
	}



	//[Script(OptimizedCode = "room")]
	[Script(ExternalTarget = "room")]
	public static Room room;

	[Script(HasNoPrototype = true)]
	public class Room
	{
		//public Action onLoad;
		public IFunction onLoad;

		public Objects objects;

		public Object createObject(string text, object args) { return null; }
	}

	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public class Vector
	{
		public Vector(double x, double y, double z)
		{

		}


		[Script(OptimizedCode = "return Vector(x,y,z);")]
		static Vector InternalConstructor(double x, double y, double z)
		{
			return null;
		}
	}
}

namespace JanusVRExperiment
{
	// this is the way to import js apis
	using static FireBoxRoom;


	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{



		static Application()
		{
			// [K apr 1 22:09:33 2015] JavaScript error in view-source (line 76902): TypeError: Result of expression 'AQAABDWbFDuQBN_brmZIFqQ' [null] is not an object.

			Console.WriteLine("enter Application");

			room.onLoad = IFunction.OfDelegate(
				(Action)delegate
			   {


				   for (var i = 0; i < 9; ++i)
				   {

					   var textid = "new_text" + i;

					   room.createObject("Text", new { js_id = textid });

					   room.objects[textid].pos = new Vector(5 + i, 1 + i, i * 0.1);
					   room.objects[textid].fwd = new Vector(-1, 0, 0);
					   room.objects[textid].text = "Application This is new text with js_id: " + new { room.objects[textid].js_id, i };
					   room.objects[textid].scale = new Vector(5, 5, 5);
					   room.objects[textid].col = new Vector(.5, 1, .5);
				   }

					// how will we interact with the document then?
					// do we have a window? no.
					//room.objects["new_text0"].text = new { hello = "world", Native.self, Native.window }.ToString();
					// are we a worker? no
					//room.objects["new_text0"].text = new { hello = "world", Native.self, Native.worker }.ToString();

				    //var keys = ScriptCoreLib.JavaScript.Runtime.Expando.
					room.objects["new_text0"].text = new { hello = "world", Native.self }.ToString();

					// red
					//room.objects["new_text0"].col = new FireBoxRoom.Vector(1, 0, 0);

					// blue
					room.objects["new_text0"].col = new Vector(0, 0, 1);

				   var x = new Text("new_textX")
				   {
					   text = "look at this",


					   pos = new Vector(6, 1, 0),

					   fwd = new Vector(-1, 0, 0),
					   scale = new Vector(5, 5, 5),
					   col = new Vector(.5, 1, .5)
				   };

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

			//			# Place your workspaces here (use paths relative to the location of JanusVR)
			//.\..\..\..\jsc.svn\examples\javascript\examples\JanusVRExperiment\JanusVRExperiment\bin\Debug\staging\JanusVRExperiment.Application\web\App.htm

			//[K apr 1 22:00:15 2015] error loading file:///X:/jsc.svn/examples/javascript/examples/JanusVRExperiment/JanusVRExperiment/Design/thumb.jpg
			//[K apr 1 22:00:15 2015] error loading file:///X:/jsc.svn/examples/javascript/examples/JanusVRExperiment/JanusVRExperiment/bin/Debug/staging/JanusVRExperiment.Application/web/thumb.jpg
		}

	}
}
