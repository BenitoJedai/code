using System;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using UltraApplicationWithAssets.Advanced;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using UltraApplicationWithAssets.HTML.Audio.FromAssets;

namespace UltraApplicationWithAssets
{

	//[Description("UltraApplicationWithAssets. Write javascript, flash and java applets within a C# project.")]

	public sealed partial class UltraApplication
	{

		public UltraApplication(IHTMLElement e)
		{
			Native.Document.title = "Ultra Application";

			var a = new HTML.Pages.FromAssets.AboutJSC();

			a.Container.AttachToDocument();

			a.WebService_GetTime.onclick +=
				delegate
				{

					new UltraWebService().GetTime("time: ",
						result =>
						{
							new IHTMLPre { innerText = result }.AttachTo(a.WebServiceContainer);

						}
					);

				};

			a.Inline1.onclick +=
				delegate
				{
					try
					{
						// are we running HTML5 browser
						new rooster().play();
					}
					catch
					{
					}

					new Timer(
						delegate
						{
							a.Inline1.style.color = "";
						}
					).StartTimeout(1000);
				};

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
