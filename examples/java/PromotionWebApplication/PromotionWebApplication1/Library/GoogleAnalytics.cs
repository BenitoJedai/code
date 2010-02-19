using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace PromotionWebApplication1.Library
{
	[Script(InternalConstructor = true)]
	public class GoogleAnalyticsTracker
	{
		public void _setDomainName(string newDomainName)
		{
		}

		public void _trackPageview()
		{
		}

		public void _trackPageview(string opt_pageURL)
		{

		}
	}

	[Script(InternalConstructor = true)]
	public class GoogleAnalytics
	{
		// todo: can we get this class to be generated from the docs by jsc?
		// http://code.google.com/apis/analytics/docs/gaJS/gaJSApi_gat.html
		// http://code.google.com/apis/analytics/docs/gaJS/gaJSApiDomainDirectory.html#_gat.GA_Tracker_._setDomainName
		// http://code.google.com/apis/analytics/docs/gaJS/gaJSApiBasicConfiguration.html#_gat.GA_Tracker_._trackPageview




		public GoogleAnalyticsTracker _getTracker(string e)
		{
			return default(GoogleAnalyticsTracker);
		}

		static GoogleAnalytics InternalGoogleAnalytics;



		public static void Default(GoogleAnalyticsAction e)
		{
			if (InternalGoogleAnalytics != null)
			{
				e(InternalGoogleAnalytics);
				return;
			}

			var analytics = new IHTMLScript
			{
				type = "text/javascript",
				src = "http://www.google-analytics.com/ga.js"
			};

			analytics.onload +=
				delegate
				{
					1.AtDelay(
						delegate
						{
							if (InternalGoogleAnalytics == null)
							{
								InternalGoogleAnalytics = (GoogleAnalytics)new IFunction("return _gat;").apply(Native.Window);
							}

							e(InternalGoogleAnalytics);
						}
					);
				};


			analytics.AttachToDocument();
		}

	}

	public delegate void GoogleAnalyticsTrackerAction(GoogleAnalyticsTracker e);
	public delegate void GoogleAnalyticsAction(GoogleAnalytics e);

	public static class GoogleAnalyticsExtensions
	{
		public static void ToGoogleAnalyticsTracker(this string x, GoogleAnalyticsTrackerAction e)
		{
			GoogleAnalytics.Default(g => e(g._getTracker(x)));
		}
	}
}
