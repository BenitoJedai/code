using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Web;
using System;
using System.Net;
using ScriptCoreLib.Shared.Drawing;
using UltraTutorial09.HTML.Pages.FromWeb;

namespace UltraTutorial09
{


	public sealed partial class UltraApplication : IWebServiceEnabled
	{
//        partial void ContinueBuildingApplication()
//        {

//            var note1 = new IHTMLPre
//            {
//                innerHTML = @"Notice: If flash does not respond to events, you need to clear your cache. 
//					+ Chrome flash in 'localhost' on Cassini always fails?
//					  - If so Try http://127.0.0.1 or http://COMPUTERNAME
//					+ Opera does not pass delegates?
//					+ IE cannot return from javascript to flash
//					+ Web page could be delivered within flash package
//					+ Javascript rewrite could omit unused types and methods
//					"

//            }.AttachToDocument();

//            note1.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.pre;
//            note1.style.fontSize = "small";
//            note1.style.color = Color.Red;

//            ContinueBuildingApplicationWithSprite();

//            new IHTMLBreak().AttachToDocument();


//            this.WebServiceEnabled = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox);

//            var WebServiceEnabledLabel = new IHTMLLabel("WebService is enabled for flash", this.WebServiceEnabled);

//            new IHTMLDiv(
//                WebServiceEnabledLabel,
//                WebServiceEnabled
//            ).AttachToDocument();

//            ButtonsForWebService();
//        }

		private static void ContinueBuildingApplicationWithSprite()
		{

			var s = new UltraSprite();

			//            s.AttachSpriteToDocument();

			//            var SayHello = new IHTMLButton { innerText = "Say Hello to flash!" }.AttachToDocument();

			//            SayHello.onclick +=
			//                delegate
			//                {
			//                    s.AppendLine("Hello from javascript");

			//                    s.WhenReady(
			//                        delegate
			//                        {
			//                            s.AppendLine("What about using the web service?");
			//                        }
			//                    );
			//                };

			//            s.AppendLine("This call is delayed until flash is loaded (1)");
			//            s.AppendLine("This call is delayed until flash is loaded (2)");
			//            s.AppendLine("This call is delayed until flash is loaded (3)");

			//            s.WhenReady(
			//                delegate
			//                {
			//                    s.AppendLine("What about using the web service?");
			//                }
			//            );

			//            // passing the interface to flash is delayed until it is loaded, using the getter will fault
			//            s.WebService = new AlphaWebService();
			//            s.WebServiceEnabled = this;

			//            s.AppendLine("This call is delayed until flash is loaded (4)");
		}
		
	}

}
