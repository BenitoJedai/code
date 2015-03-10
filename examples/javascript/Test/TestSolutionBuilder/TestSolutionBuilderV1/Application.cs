// #define FEATURE_CHROME

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using TestSolutionBuilderV1.HTML.Pages;
using TestSolutionBuilderV1.Views;
using ScriptCoreLib.JavaScript.Runtime;

namespace TestSolutionBuilderV1
{
	/// <summary>
	/// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public sealed class Application
	{
		// ScriptCoreLib.JavaScript.DOM.IStyleSheet.get_all
		//		:8939/view-source:50114 1792ms ItemGroupReferenes...
		//2015-03-10 10:30:46.599 :8939/view-source:120275 Uncaught TypeError: Cannot read property '_4ggABg6gjTWKCO_aNXgMMAA' of null

		//// ScriptCoreLib.Ultra.Studio.SolutionFileComment.WriteTo
		//type$mg7i9CBGlTCpHMdiX219pQ.sgEABiBGlTCpHMdiX219pQ = function(b, c, d)
		//{
		//	var a = [this], e, f, g, h;

		//	e = a[0].IsActiveFilter != null;
		//	f = !a[0].IsActiveFilter._4ggABg6gjTWKCO_aNXgMMAA(d);

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{

			#region 2013
			FormStyler.AtFormCreated = s =>
					 {
						 // border> 8E9BBC
						 // caption 4D6082

						 s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.2) 0px 0px 6px 0px";
						 s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(142, 155, 188);

						 s.TargetInnerBorder.style.borderWidth = "0px";

						 s.CloseButton.style.color = JSColor.FromRGB(206, 212, 221);
						 s.CloseButton.style.backgroundColor = JSColor.None;
						 s.CloseButton.style.borderWidth = "0px";
						 s.CloseButtonContent.style.borderWidth = "0px";

						 s.TargetResizerPadding.style.left = "0px";
						 s.TargetResizerPadding.style.top = "0px";
						 s.TargetResizerPadding.style.right = "0px";
						 s.TargetResizerPadding.style.bottom = "0px";

						 s.Caption.style.backgroundColor = JSColor.FromRGB(77, 96, 130);
						 s.CaptionShadow.style.backgroundColor = JSColor.FromRGB(77, 96, 130);
					 };
			#endregion


			//---------------------------
			//Microsoft Visual Studio Express 2012 for Windows Desktop
			//---------------------------
			//An exception has been encountered. This may be caused by an extension.



			//You can get more information by examining the file 'C:\Users\Arvo\AppData\Roaming\Microsoft\WDExpress\11.0\ActivityLog.xml'.
			//---------------------------
			//OK   
			//---------------------------


#if FEATURE_CHROME

			#region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            if (null != (object)self_chrome)
            {
                object self_chrome_socket = self_chrome.socket;

                if (self_chrome_socket != null)
                {
                    chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                    chrome.Notification.DefaultTitle = "TestSolutionBuilderV1";


                    ChromeTCPServer.TheServerWithStyledForm.Invoke(
                        AppSource.Text,
                        AtFormCreated: s =>
                        {
                            // border> 8E9BBC
                            // caption 41

                            s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.2) 0px 0px 6px 0px";
                            s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(12, 32, 45);

                            s.TargetInnerBorder.style.borderWidth = "0px";

                            s.CloseButton.style.color = JSColor.White;
                            s.CloseButton.style.backgroundColor = JSColor.None;
                            s.CloseButton.style.borderWidth = "0px";
                            s.CloseButtonContent.style.borderWidth = "0px";

                            s.TargetResizerPadding.style.left = "0px";
                            s.TargetResizerPadding.style.top = "0px";
                            s.TargetResizerPadding.style.right = "0px";
                            s.TargetResizerPadding.style.bottom = "0px";

                            s.Caption.style.backgroundColor = JSColor.FromRGB(41, 57, 85);
                            s.CaptionShadow.style.backgroundColor = JSColor.FromRGB(41, 57, 85);
                        }
                    );

                    return;
                }
            }
			#endregion
#endif


			//page.Content = new StudioView(null).Content;
			new StudioView(null).Content.AttachToDocument();



		}

	}
}
