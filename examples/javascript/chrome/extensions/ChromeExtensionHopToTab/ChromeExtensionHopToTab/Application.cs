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


					// while running tabs.insertCSS: The extensions gallery cannot be scripted.
					if (tab.url.StartsWith("https://chrome.google.com/webstore/"))
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


					// while running tabs.insertCSS: The tab was closed.

					// 		public static Task<object> insertCSS(this TabIdInteger tabId, object details);
					// public static void insertCSS(this TabIdInteger tabId, object details, IFunction callback);


					// for some sites the bar wont show as they html element height is 0?
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

					Console.WriteLine(
						"insertCSS done " + new { tab.id, tab.url }
						);


					// where is the hop to iframe?
					// X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs
				};


				return;
			}

			// we are jumping?
		}

	}
}
