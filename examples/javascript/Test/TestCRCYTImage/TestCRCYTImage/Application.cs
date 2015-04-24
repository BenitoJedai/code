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
using TestCRCYTImage;
using TestCRCYTImage.Design;
using TestCRCYTImage.HTML.Pages;

namespace TestCRCYTImage
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			new { }.With(
				async delegate
				{
					var vid = "TXExg6Xj3aA";



					var thumbnail = $"https://img.youtube.com/vi/{vid}/0.jpg";

					var thumbnailImage = new IHTMLImage
					{
						// 0 pixels?
						crossOrigin = "anonymous",

						src = thumbnail


					};

					//					naturalHeight:
					//					90
					//naturalWidth:
					//					120

					// {{ thumbnail = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, width = 0, height = 0 }}
					new IHTMLPre {
						new { thumbnail, thumbnailImage.complete,  thumbnailImage.naturalWidth, thumbnailImage.naturalHeight}
					}.AttachToDocument();

					await thumbnailImage.async.oncomplete;

					//thumbnail = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, complete = false, naturalWidth = 0, naturalHeight = 0 }}
					//		thumbnail = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, complete = true, naturalWidth = 120, naturalHeight = 90 }}


					//thumbnail = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, complete = true, naturalWidth = 0, naturalHeight = 0 }}
					thumbnailImage.style.SetSize(
						thumbnailImage.naturalWidth, thumbnailImage.naturalHeight
					);

					new IHTMLPre {
						new { thumbnail, thumbnailImage.complete,  thumbnailImage.width, thumbnailImage.naturalWidth, thumbnailImage.naturalHeight }
					}.AttachToDocument();

					//				{
					//					{
					//						thumbnail = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, complete = false, width = 0, height = 0 }}
					//{
					//							{
					//								thumbnail = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, complete = true, width = 0, height = 0 }}

					thumbnailImage.AttachToDocument();

					new IHTMLPre {
						new { thumbnail, thumbnailImage.complete, thumbnailImage.naturalWidth, thumbnailImage.naturalHeight }
					}.AttachToDocument();

					var div = new IHTMLDiv
					{

					}.AttachToDocument();

					// 404 image does show as a background, can we copy it?
					new IStyle(div)
					{
						width = 120 + "px",
						height = 90 + "px",
						//overflow = IStyle.OverflowEnum.hidden,
						//position = IStyle.PositionEnum.relative,
						backgroundImage = $"url('{thumbnail}')",

					};

					//Image from origin 'https://img.youtube.com' has been blocked from loading by Cross - Origin Resource Sharing policy: No 'Access-Control-Allow-Origin' header is present on the requested resource. Origin 'https://192.168.1.12:15367' is therefore not allowed access.The response had HTTP status code 404.

					var idiv = (IHTMLImage)div;

					idiv.AttachToDocument();

					new IHTMLPre {
						new {  idiv.complete,  idiv.width, idiv.naturalWidth, idiv.naturalHeight }
					}.AttachToDocument();
				}
			);

			//			Severity Code    Description Project File Line
			//Error CS7069  Reference to type 'TaskAwaiter<>' claims it is defined in 'mscorlib', but it could not be found TestCRCYTImage X:\jsc.svn\examples\javascript\test\TestCRCYTImage\TestCRCYTImage\Application.cs    56


		}

	}
}
