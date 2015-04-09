using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Friendface.Design;
using Friendface.HTML.Pages;

namespace Friendface
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application
	{

		//Error The imported project "X:\jsc.svn\examples\javascript\android\Friendface\.nuget\nuget.targets" was not found.Confirm that the path in the<Import> declaration is correct, and that the file exists on disk.Friendface X:\jsc.svn\examples\javascript\android\DCIMCameraAppWithThumbnails\DCIMCameraAppWithThumbnails\DCIMCameraAppWithThumbnails.csproj	155


		public readonly ApplicationWebService service = new ApplicationWebService();

		// jsc is confused if only mentioned on the server side?
		static Images ref0;

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{

			page.MyContacts.Clear();

			// http://stackoverflow.com/questions/6318659/string-error-constant-string-too-long

			service.AddContact(
				Name =>
				{
					var c = new Contact();

					c.Name.innerText = Name;

					c.Container.AttachTo(
						page.MyContacts
					);
				}
			);

			service.AddTimelineUnit(
				Content =>
				{
					var u = new TimelineTextUnit();

					u.Content.innerText = Content;

					u.Container.AttachTo(
						page.u_0_15
					);
				}
			);


			service.AddTimelinePictureUnit(
				(PictureContent, dataside) =>
				{
					var u = new TimelinePictureUnit();

					//u.Content.innerText = Content;
					//data-side="l"

					u.Container.setAttribute("data-side", dataside);

					u.PictureContent.src = PictureContent;

					u.Container.AttachTo(
						page.u_0_15
					);
				}
			);


		}

	}
}
