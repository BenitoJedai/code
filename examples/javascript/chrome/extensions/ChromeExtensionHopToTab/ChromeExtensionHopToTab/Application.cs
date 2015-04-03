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
using ChromeExtensionHopToTab;
using ChromeExtensionHopToTab.Design;
using ChromeExtensionHopToTab.HTML.Pages;
using chrome;

namespace ChromeExtensionHopToTab
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
			// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsExperiment\ChromeTabsExperiment\Application.cs

			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_tabs = self_chrome.tabs;

			if (self_chrome_tabs != null)
			{

				chrome.tabs.Updated += async (i, x, tab) =>
				{
					// chrome://newtab/

					if (tab.url.StartsWith("chrome-devtools://"))
						return;

					if (tab.url.StartsWith("chrome://"))
						return;

					if (tab.status != "complete")
						return;

					new chrome.Notification
					{
						Message = "chrome.tabs.Updated " + new
						{
							tab.id,
							tab.url,
							tab.status,
							tab.title
						}
					};

					// 		public static Task<object> insertCSS(this TabIdInteger tabId, object details);
					// public static void insertCSS(this TabIdInteger tabId, object details, IFunction callback);
					await tab.id.insertCSS(
						new
						{
							code = @"

html { 
border-left: 1em solid cyan;
padding-left: 1em; 
}


"
						}
					);

					//tab.i
				};


				return;
			}

			// we are jumping?
		}

	}
}
