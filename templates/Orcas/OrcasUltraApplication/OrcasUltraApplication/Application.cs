using System;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using OrcasUltraApplication.Advanced;
using ScriptCoreLib.JavaScript;

namespace OrcasUltraApplication
{

	//[Description("OrcasUltraApplication. Write javascript, flash and java applets within a C# project.")]

	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "Ultra Application";

			var c = new IHTMLDiv
			{

			}.AttachToDocument();

			c.onmouseover +=
				delegate
				{
					c.style.backgroundColor = "#efefff";
				};

			c.onmouseout +=
				delegate
				{
					c.style.backgroundColor = "";
				};


			c.style.margin = "2em";
			c.style.padding = "2em";
			c.style.border = "1px solid #777777";
			c.style.borderLeft = "2em solid #777777";


			new IHTMLDiv(
				new IHTMLAnchor
				{
					innerText = "Write javascript, flash and java applets within a C# project.",
					href = "http://www.jsc-solutions.net"
				}
			).AttachTo(c);


			{
				var btn = new IHTMLButton { innerText = "UltraWebService" }.AttachTo(c);

				btn.onclick +=
					delegate
					{

						new UltraWebService().GetTime("time: ",
							result =>
							{
								new IHTMLDiv { innerText = result }.AttachTo(c);

							}
						);

					};
			}

			// do we have javac configured?
			//this.CreateApplet();

			// do we jave mxmlc configured? 
			//this.CreateSprite();
		}


	}

	#region do we have javac configured?
	//public sealed class UltraApplet : UltraAppletBase
	//{
	//    public const int DefaultWidth = UltraAppletBase.DefaultWidth;
	//    public const int DefaultHeight = UltraAppletBase.DefaultHeight;
	//}

	//public static class UltraAppletIntegration
	//{
	//    public static void CreateApplet(this UltraApplication a)
	//    {
	//        var x = new IHTMLButton("create UltraSprite proxied");

	//        x.AttachToDocument();

	//        x.onclick +=
	//            delegate
	//            {
	//                var o = new UltraSprite();

	//                o.AttachSpriteToDocument();
	//            };
	//    }
	//}
	#endregion

	#region do we jave mxmlc configured?
	//public sealed class UltraSprite : UltraSpriteBase
	//{
	//    public const int DefaultWidth = UltraSpriteBase.DefaultWidth;
	//    public const int DefaultHeight = UltraSpriteBase.DefaultHeight;
	//}

	//public static class UltraSpriteIntegration
	//{
	//    public static void CreateSprite(this UltraApplication a)
	//    {
	//        var x = new IHTMLButton("create UltraSprite ");

	//        x.AttachToDocument();

	//        x.onclick +=
	//            delegate
	//            {
	//                var o = new UltraSprite();

	//                o.AttachSpriteToDocument();
	//            };

	//    }
	//}
	#endregion


	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}
	}
}
