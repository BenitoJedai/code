using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Delegates;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.flash.display;

namespace jsc.meta.Library.Mashups
{
	public static class UltraApplicationInlineWebServiceExtensions
	{
		public static void AtServer(this Func<string, string> InternalFunction, StringAction yield)
		{
			// running in .net, GAEJava, php
		}

		public static void Method11(this string input1, Func<string, string> InternalFunction, StringAction yield)
		{
			// running in .net, GAEJava, php

		}
	}


	public sealed class UltraApplicationInline
	{
		public UltraApplicationInline(IHTMLElement p)
		{
			var s = new Sprite
			{
				width = 32,
				height = 32,

				// can we inherit and extend in F# ad hoc?
			};

			Func<Canvas> CreateCanvas =
				delegate
				{
					// is this js or flash VM? both
					// cannot pass object over boundaries tho

					var r = new Canvas
					{
						Background = Brushes.Red,
						Width = 32,
						Height = 32
					};

					return r;
				};

			Func<int> InlineMethod1 = () => 1;


			"parameter 1".Method11(
				yy =>
				{
					// we cannot use closure fields here, because we will be running in the server :)
					// actually we could if we passed the closure data object over the wire...

					// how would we know then if this is the result or incoming calculus delegate?

					// referenced methods need also be "interned"
					return "yy: " + yy + InlineMethod1();
				},
				yy =>
				{
					// yay, back from the server
				}
			);

			Func<string, string> ServerSideFunction1 =
				yy =>
				{
					// we cannot use closure fields here, because we will be running in the server :)
					// actually we could if we passed the closure data object over the wire...

					// how would we know then if this is the result or incoming calculus delegate?

					// referenced methods need also be "interned"
					return "yy: " + yy + InlineMethod1();
				};


			ServerSideFunction1.AtServer(
				result =>
				{
					// yay, back from the server
				}
			);

			Action<StringAction> ServerSideFunction1_AtServer = ServerSideFunction1.AtServer;

			s.click +=
				ee =>
				{
					// we are in flash VM talking about WPF :)

					var r = CreateCanvas();

					ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.AttachToContainer(r, s);

					// could we raise some events?
					// could we use closure values?


					// can we call WebServer from javascript from flash?
					// ServerSideFunction1_AtServer was exposed to us via current proxy implementation

					ServerSideFunction1_AtServer(
						result =>
						{
							// yay, back from the server
						}
					);
				};


			ScriptCoreLib.JavaScript.Extensions.SpriteExtensions.AttachSpriteToDocument(s);
		}


	}
}
