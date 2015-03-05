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
using ChromeLauncherMenu;
using ChromeLauncherMenu.Design;
using ChromeLauncherMenu.HTML.Pages;

namespace ChromeLauncherMenu
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
			// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionContextMenu\ChromeExtensionContextMenu\Application.cs


			#region self_chrome_tabs
			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_tabs = self_chrome.tabs;

			if (self_chrome_tabs != null)
			{
				// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs

				// can assetslib use a rebuilt roslyn to find errors and try to fix em?
				// Error	3	The name 'chrome' does not exist in the current context	X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionContextMenu\ChromeExtensionContextMenu\Application.cs	49	17	ChromeExtensionContextMenu
				// add as source project?
				// add dep
				#region Installed

				chrome.runtime.Installed += async delegate
				{
					// our API does not have a Show
					new chrome.Notification
					{
						Message = "App Installed!"
					};


					// stackoverflow.com/questions/20306548/how-to-use-system-indicator-in-the-manisfet-of-a-chrome-packaged-app
					//chrome.systemIndicator.Clicked +=
					//    delegate
					//    {
					//        new chrome.Notification
					//        {
					//            Message = "systemIndicator.Clicked"
					//        };

					//    };


					//chrome.systemIndicator.enable();


					//  once installed extend the menu?

					// https://developer.chrome.com/extensions/contextMenus
					chrome.contextMenus.Clicked +=
						(e, tab) =>
						{// 0:87108ms at Delay {{ _title = , _message = Menu Clicked: {{ e = [object Object] }} }} 
						 // jsc. whats in the object?

							//checked: false
							//editable:
							//false
							//menuItemId:
							//"menu1"
							//pageUrl:
							//"http://watch32.com/movies-online/event-horizon-245488/full.html"
							//selectionText:
							//"tronauts are"
							//wasChecked:
							//true

							new chrome.Notification
							{
								Message = "Menu Clicked: " + new
								{
									e.menuItemId,
									e.pageUrl,

									e.selectionText,

									tab.id

								}
							};
						};


					new[] { // "page", "frame", "link", "editable", "image", "video", "audio",

						// Unchecked runtime.lastError while running contextMenus.create: Only packaged apps are allowed to use 'launcher' context
						 "launcher"
					}.WithEach(
						async context =>
						{

							var menu0 = await chrome.contextMenus.create(
							   new
							   {
								   type = "checkbox",
							   //id = "menu1",
							   title = new { context }.ToString(),


							   // <exception>: Error: Invalid value for argument 1. Property 'checked': Expected 'boolean' but got 'integer'.
							   // first time an API complains. jsc fix booleans thanks.
							   //@checked = true,

							   // launcher' context is only supported by apps and is used to add menu items to the context menu that appears when clicking on the app icon in the launcher/taskbar/dock/etc.

							   // this works well for selected text.
							   contexts = new[] { context }
							   }
						   );
						}
					);


					// this will create a sub menu
					var menu1 = await chrome.contextMenus.create(
						new
						{
							type = "checkbox",
							id = "menu1",
							title = "selection: %s",


							// <exception>: Error: Invalid value for argument 1. Property 'checked': Expected 'boolean' but got 'integer'.
							// first time an API complains. jsc fix booleans thanks.
							//@checked = true,

							// launcher' context is only supported by apps and is used to add menu items to the context menu that appears when clicking on the app icon in the launcher/taskbar/dock/etc.

							// this works well for selected text.
							contexts = new[] { "selection" }
						}
					);

					new chrome.Notification
					{
						Message = "Menus Created!"
					};

				};
				#endregion
				return;
			}
			#endregion
		}

	}
}
