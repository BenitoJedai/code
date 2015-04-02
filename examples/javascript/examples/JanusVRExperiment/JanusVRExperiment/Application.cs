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
	[Script(OptimizedCode = "print(e);")]
	public static void print(string e) { }

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

	[Script(HasNoPrototype = true)]
	public class Cookies
	{
		public string this[string key]
		{
			get
			{
				return null;
			}
		}
	}

	// a pseudo type
	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public sealed class Cube : Object
	{
		public static List<Cube> Elements = new List<Cube>();


		public Cube()
		{

		}

		static Cube InternalConstructor()
		{
			var js_id = "js_id_Cube" + Elements.Count;

			var x = (Cube)room.createObject("Object", new
			{
				id = "cube",
				js_id
				//, collision_id = "", collision_radius = 2
			});

			Elements.Add(x);

			return x;
		}
	}

	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public sealed class Pyramid : Object
	{
		public static List<Pyramid> Elements = new List<Pyramid>();


		public Pyramid()
		{

		}

		static Pyramid InternalConstructor()
		{
			var js_id = "js_id_Pyramid" + Elements.Count;

			var x = (Pyramid)room.createObject("Object", new { id = "pyramid", js_id });

			Elements.Add(x);

			return x;
		}
	}

	// a pseudo type
	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public sealed class Text : Object
	{
		public static List<Text> Elements = new List<Text>();


		public Text(string js_id)
		{

		}

		static Text InternalConstructor(string js_id)
		{
			return (Text)room.createObject("Text", new { js_id });
			//return (Text)room.createObject("Text");// crash
			//return (Text)room.objects[js_id];

		}

		public Text()
		{

		}

		static Text InternalConstructor()
		{
			var js_id = "js_id_Text" + Elements.Count;

			var x = (Text)room.createObject("Text", new { js_id });

			Elements.Add(x);

			return x;
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

	[Script(HasNoPrototype = true)]
	public class Player
	{
		public Vector pos;

		/// <summary>
		///  String for the URL of the room.  Can be compared with player.url
		/// to determine if player is in the room.
		/// </summary>
		public string url;

		public Vector view_dir;
	}

	//[Script(OptimizedCode = "room")]
	[Script(ExternalTarget = "player")]
	public static Player player;

	//[Script(ExternalTarget = "room")]
	public static Room room
	{
		get
		{
			// variable ma or may not exist, if running inside JanusVR or not 
			return (Native.self as dynamic).room;
		}
	}

	[Script(HasNoPrototype = true)]
	public class Room
	{
		//public Action onLoad;
		/// <summary>
		/// Invoked before the first update of the room. Note that this is not when the room is loaded, but when the user first steps into the room.
		/// </summary>
		public IFunction onLoad;

		//public IFunction<double> update;
		/// <summary>
		/// Invoked on each frame before the world is drawn. dt, an
		/// optional parameter, is the amount of time that elapsed between this update
		/// and the previous update, useful for ensuring objects move at the same speed
		/// regardless of framerate.
		/// </summary>
		public IFunction update;

		public IFunction onClick;

		public IFunction onCollision;

		/// <summary>
		///  The objects in the room. Instantiated after the user
		/// enters the room, before onLoad is invoked.A dictionary that maps the js_id
		/// attribute of each object to a script object that has that object's
		/// attributes.Modifying the attributes of this object will modify the
		/// attributes of the object in the room.
		/// </summary>
		public Objects objects;


		public void removeObject(Object e) { }
		public Object createObject(string text, object args) { return null; }

		public Cookies cookies;
		public void addCookie(string text, string value) { }
		//public Object createObject(string text) { return null; }
	}

	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public class Vector
	{
		public double x;
		public double y;
		public double z;

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
	using ScriptCoreLib.JavaScript.Runtime;
	using System.Diagnostics;
	// this is the way to import js apis
	using static FireBoxRoom;


	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{



		static Application()
		{
			if (room == null)
			{
				//Native.document.body.style.backgroundColor = "red";
				return;
			}


			// [K apr 1 22:09:33 2015] JavaScript error in view-source (line 76902): TypeError: Result of expression 'AQAABDWbFDuQBN_brmZIFqQ' [null] is not an object.

			// click c to see it
			//print("enter Application");

			Console.WriteLine("enter Application");


			room.onLoad = IFunction.OfDelegate(
				(Action)delegate
			   {
				   new Cube
				   {
					   pos = new Vector(0, 0, -4),

					   fwd = new Vector(-1, 0, 0),
					   // hightower
					   //scale = new Vector(1, 10, 1),
					   scale = new Vector(4.0, 4.0, 18.0),
					   col = new Vector(.5, 0.5, .5)
				   };

				   for (var i = 0; i < 29; ++i)
				   {

					   var textid = "new_text" + i;

					   room.createObject("Text", new { js_id = textid });

					   room.objects[textid].pos = new Vector(2 + i, 1 + i * 0.7, 4);
					   room.objects[textid].fwd = new Vector(-1, 0, 0);
					   room.objects[textid].text = "Application This is new text with js_id: " + new { room.objects[textid].js_id, i };
					   room.objects[textid].scale = new Vector(5, 5, 5);
					   room.objects[textid].col = new Vector(.5, 1, .5);


					   new Cube
					   {
						   pos = new Vector(2 + i, 1 + i * 0.6, 0),

						   fwd = new Vector(-1, 0, 0),
						   // hightower
						   //scale = new Vector(1, 10, 1),
						   scale = new Vector(3, 0.5, 1),
						   col = new Vector(.5, 1, .5)
					   };
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



				   new Pyramid
				   {
					   pos = new Vector(4, 0, 4),

					   fwd = new Vector(-1, 0, 0),
					   scale = new Vector(1, 1, 1),
					   col = new Vector(.5, 1, .5)
				   };

				   var x = new Text
				   {
					   text = "look at this",

					   pos = new Vector(6, 1, 0),

					   fwd = new Vector(-1, 0, 0),
					   scale = new Vector(8, 8, 8),
					   col = new Vector(.5, 1, .5)
				   };

				   var sw = Stopwatch.StartNew();
				   var onCollisionCounter = 0;
				   var clickCounter = 0;
				   var c = 0;

				   room.onCollision = new Action<Object, Object>(
					   (o, other) =>
					   {
						   onCollisionCounter++;
					   }
				   );

				   room.onClick = new Action(
					   delegate
					   {
						   clickCounter++;
					   }
				   );

				   room.addCookie("cookie1", "cookie1");

				   // look we found onframe.
				   room.update = new Action(
					   delegate
					   {
						   c++;

						   var cookie1 = room.cookies["cookie1"];

						   x.text = "status: " + new
						   {
							   c,
							   sw.ElapsedMilliseconds,

							   cookie1,

							   clickCounter,
							   onCollisionCounter,

							   player = new { player.pos.x, player.pos.y, player.pos.z }

						   };
						   x.col = new Vector(
							   (Math.Sin(sw.ElapsedMilliseconds * 0.01) + 1.0) / 2.0,
							   0,
							   0
						   );



						   //this causes the image to always face the player
						   // its flipped?
						   //x.fwd = player.view_dir;

						   // stariways?
						   // ceiling
						   //player.pos.y = Math.Min(player.pos.y, player.pos.x * 0.5);

						   if (player.pos.z < -1)
							   player.pos.y = Math.Max(player.pos.y, 4.5);
						   else if (player.pos.z < 2)
							   player.pos.y = Math.Max(player.pos.y, player.pos.x * 0.6);


						   x.pos = new Vector(
							   player.pos.x + 2,
							   player.pos.y + 1,
							   player.pos.z
						   );

						   //player.pos.y += (1 / 15.0) * (Math.Max(player.pos.y, player.pos.x * 0.5) - player.pos.y);
					   }
				   );
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
			if (Native.window == null)
				return;

			// Cookies can also be saved/loaded via the JS (in addition to those cookies set through AssetWebSurfaces). 
			// This can be used for inter-communication between the FireBoxRoom, the JS/AssetScripts, and AssetWebSurfaces in the room.)

			// AssetWebSurface


			Native.document.body.style.backgroundColor = "cyan";

			Console.WriteLine("enter Application");

			new IHTMLCode {
				() => "are we hosted within JanusVR? " + new { Native.window.navigator.userAgent, Native.document.location, Native.document.cookie
				} }.AttachToDocument();


			// file:///X:/jsc.svn/examples/javascript/examples/JanusVRExperiment/JanusVRExperiment/bin/Debug/staging/JanusVRExperiment.Application/web/App.htm

			//var w = new IFunction("return window;").apply(null);
			//var doc = new IFunction("return document;").apply(null);

			//var selfp = (Native.self as dynamic).prototype;
			//var selfpc = (Native.self as dynamic).prototype.constructor;

			//var m = string.Join(",\r\n", Expando.InternalGetMemberNames(Native.self));
			// print, gc, version, __FILE__, __LINE__, room, Vector, V, translate, equals, scalarMultiply, cross, normalized, distance, removeKey, uniqueId, debug, __string, __this,

			//throw new Exception(new { Native.self, selfp, selfpc, m }.ToString());


			// http://www.reddit.com/r/janusVR/comments/2pg82o/release_310_the_janusvr_is_now_a_functional_web/
			//	To specify a new URL for an AssetWebSurface, press tab while the mouse / keyboard are bound to it and specify a URL. When you press enter, the AssetWebSurface will navigate to the URL given.
			//Next up -collaborative web browsing with AssetWebSurfaces!
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
