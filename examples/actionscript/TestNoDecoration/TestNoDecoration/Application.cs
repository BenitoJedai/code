using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;

namespace TestNoDecoration
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class Application : Sprite
	{
		public Application()
		{

			new TextField
			{
				width = 600,
				height = 400,
				x = 20,
				y = 20,
				defaultTextFormat = new TextFormat
				{
					size = 30,
					color = 0xff,
					font = "Verdana"
				},
				text = "powered by jsc - " + new string(new MyServiceProvider().InstanceMethod1('x')),

				filters = new BitmapFilter[] { new DropShadowFilter() },
			}.AttachTo(this);


		}


	}

	[Script]
	public class MyServiceProvider
	{
		public static string StaticField1;
		public string InstanceField2;

		public char[] InstanceMethod1(char f)
		{

			return new char[] { 'a', 'b', 'c', f };
		}

		public static void StaticMethod2()
		{
		}
	}

}