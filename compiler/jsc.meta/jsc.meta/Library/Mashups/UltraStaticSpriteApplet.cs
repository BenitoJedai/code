using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace jsc.meta.Library.Mashups
{
	internal static class MyUltraExtensions
	{
		// runs in any VM?

		public static string Computation1(string e)
		{
			// this method
			return "x: " + e;
		}
	}

	internal static class MyParallelExtensions
	{
		// run in multiple components?
		// AsParallel?
	}

	internal static class MyAppletExtensions
	{

	}

	internal static class MySpriteExtensions
	{

		public static void Method3(this string input, Func<string, string> c, StringAction yield)
		{
			// could we pass a pointer here which is actually already "internal"?

			// SpriteExtensions is the keyword

			// we are in the flash VM.

			// we cannot call JavaScript API here yet.
			// we can call flash API

			// all static methods shall be grouped to one hidden flash component

			if (c == null)
				c = MyUltraExtensions.Computation1;
			else
			{
				// the pointer could be to external or internal func
			}
		}
	}

	internal sealed class UltraStaticSpriteApplet
	{


		public class UltraSprite : Sprite
		{
			// note: javascript-flash boundary break here

			// note: static variables to be considered "thread" transient, where each flash component is in its own "thread"
			// is "alchemy" fast enough for us? :)

			// flash components could be talking to each other via local connections...
			// note: phase0 rewrite should also enable secondary pages

			static UltraSprite __UltraSprite;

			public static void Method1(StringAction a)
			{
				// is this a phase 0 rewrite?

				if (__UltraSprite == null)
				{
					__UltraSprite = new UltraSprite();
					__UltraSprite.AttachSpriteToDocument();
				}

				__UltraSprite.__static_UltraSprite(a);
			}

			public void __static_UltraSprite(StringAction a)
			{
				// phase 1 rewrite

				InternalMethod1(a);
			}

			public int Field1;

			public void InternalMethod1(StringAction a)
			{
				a("Field1: " + Field1);
			}
		}

		public static void Method2(StringAction m)
		{
			// are we creating a proxy in javascript or are we inside flash vm?
			var s = new UltraSprite
			{
				// if we touch a field, it cannot possibly be a proxy 
				Field1 = 4
			};

			// can be both...
			s.InternalMethod1(m);
		}



		public UltraStaticSpriteApplet(IHTMLElement p)
		{
			{
				Action<StringAction> Method1 = UltraSprite.Method1;

				Method1(
					x =>
					{
						// can we load BAML?
					}
				);
			}

			{
				Action<StringAction> Method1 = Method2;

				Method1(
					x =>
					{
						// can we load BAML?
					}
				);
			}

			{
				Action<StringAction> Method1 =
					// WARNING! 
					// we must be careful here. if we use closure values
					// then we might get in trouble :)
					m =>
					{
						// are we creating a proxy in javascript or are we inside flash vm?
						var s = new UltraSprite
						{
							// if we touch a field, it cannot possibly be a proxy 
							Field1 = 4
							// this is running inside a flash VM
						};

						// can be both...
						s.InternalMethod1(m);
					};

				Method1(
					x =>
					{
						// can we load BAML?
					}
				);
			}

			{
				Func<string, string> c1 = u => "y: " + u;

				"A1".Method3(c1, y => { });

				// we should indicate that we are sending an internal pointer here
				"A1".Method3(MyUltraExtensions.Computation1, y => { });
			}

			{
				// can we build anonymous sprites?

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


				s.click += 
					ee =>
					{
						// we are in flash VM talking about WPF :)

						var r = CreateCanvas();

						ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.AttachToContainer(r, s);	

						// could we raise some events?
						// could we use closure values?
					};


				s.AttachSpriteToDocument();
			}
		}
	}
}
