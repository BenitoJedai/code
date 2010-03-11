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



	internal sealed class UltraApplicationInline
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

			Action<Action<Action>> SomebodyGiveMeHandlerForCanvasInFlash =
				delegate
				{

				};

			s.click +=
				ee =>
				{
					// we are in flash VM talking about WPF :)

					var r = CreateCanvas();

					ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.AttachToContainer(r, s);

					SomebodyGiveMeHandlerForCanvasInFlash(
						NewHandlerReadyToBeAttached =>
						{
							r.MouseLeftButtonUp +=
								delegate
								{
									// let them know that its time to handle this event
									NewHandlerReadyToBeAttached();
								};
						}
					);

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


			// what about anonymous types?

			var data1 = new { a = 4, b = "hello world" };

			Action<Action<string>> computation1 =
				yield =>
				{
					// if the method call gets to the server
					// did we pass on the closure fields?

					// if we did we can use them and yield

					var r = data1.a + data1.b;

					yield(r);
				};

			computation1.AtServer(
				r =>
				{
					// back from the server...
				}
			);

			// could we yield anonymous types?

			{
				var protocol = new { data1 = 4, data2 = new { text = "hello" } };

				// how can we possibly support this? :D
				var server_function1 = protocol.ToYield(
					protocol_template =>
						(protocol_sent_to_server, yield_protocol_to_client) =>
						{
							var loc1 = protocol;
							var loc2 = protocol_template;
							var loc3 = protocol_sent_to_server;
							var loc4 = yield_protocol_to_client;
						}
				);

				// ok we defined our implementation we want to run on the server

				server_function1.AtServer(
					// creating new data to be sent to server
					new { data1 = DateTime.Now.Millisecond, data2 = new { text = "hello" } },

					r =>
					{
						// back from the server
					}
				);
			}

			{
				var protocol = new { data1 = 4, data2 = new { text = "hello" } };

				// how can we possibly support this? :D
				var server_function1 = protocol.ToYield(
					() =>
						(protocol_sent_to_server, yield_protocol_to_client) =>
						{
							var loc1 = protocol;
							var loc3 = protocol_sent_to_server;
							var loc4 = yield_protocol_to_client;
						}
				);

				// ok we defined our implementation we want to run on the server

				server_function1.AtServer(
					// creating new data to be sent to server
					new { data1 = DateTime.Now.Millisecond, data2 = new { text = "hello" } },

					r =>
					{
						// back from the server
					}
				);
			}



			{
				// how can we possibly support this? :D
				new { data1 = 4, data2 = new { text = "hello" } }.ToYield(
					() =>
						(protocol_sent_to_server, yield_protocol_to_client) =>
						{
							var loc3 = protocol_sent_to_server;
							var loc4 = yield_protocol_to_client;
						}
					// ok we defined our implementation we want to run on the server
				).AtServer(
					// creating new data to be sent to server
					new { data1 = DateTime.Now.Millisecond, data2 = new { text = "hello" } },

					r =>
					{
						// back from the server
					}
				);
			}

			{
				// how can we possibly support this? :D
				new { data1 = 4, data2 = new { text = "hello" } }.ToYield(
					(protocol_sent_to_server, yield_protocol_to_client) =>
					{
						var loc3 = protocol_sent_to_server;
						var loc4 = yield_protocol_to_client;
					}
					// ok we defined our implementation we want to run on the server
				).AtServer(
					// creating new data to be sent to server
					new { data1 = DateTime.Now.Millisecond, data2 = new { text = "hello" } },

					r =>
					{
						// back from the server
					}
				);
			}

			{
				// how can we possibly support this? :D
				// creating new data to be sent to server
				new
				{
					data1 = DateTime.Now.Millisecond,
					data2 = new { text = "hello" }
				}.AtServer(
					(protocol_sent_to_server, yield_protocol_to_client) =>
					{
						var loc3 = protocol_sent_to_server;
						var loc4 = yield_protocol_to_client;
					},
					// ok we defined our implementation we want to run on the server

					r =>
					{
						// back from the server
					}
				);
			}


			{
				// what if we want to fix protocol data and invoke it later?

				var func1 = new
				{
					data1 = DateTime.Now.Millisecond,
					data2 = new { text = "hello" }
				}.ToFixedWebService(
					(protocol_sent_to_server, yield_protocol_to_client) =>
					{
						var loc3 = protocol_sent_to_server;
						var loc4 = yield_protocol_to_client;
					}
				);

				// ok we defined our implementation we want to run on the server
				func1(
					r =>
					{
						// back from the server
						var loc1 = r.data1;
						var loc2 = r.data2;
					}
				);
			}


			{

				var func1 = new
				{
					data1 = DateTime.Now.Millisecond,
					data2 = new { text = "hello" }
				}.ToWebService(
					(protocol_sent_to_server, yield_protocol_to_client) =>
					{
						var loc3 = protocol_sent_to_server;
						var loc4 = yield_protocol_to_client;
					}
				);

				SomebodyGiveMeHandlerForCanvasInFlash +=
					AttachHandler =>
					{
						AttachHandler(
							delegate
							{
								// handling that event now...

								// ok we defined our implementation we want to run on the server
								func1(
									new { data1 = 4, data2 = new { text = "dynamic" } },

									r =>
									{
										// back from the server
										var loc1 = r.data1;
										var loc2 = r.data2;
									}
								);
							}
						);
					};

			}
		}


	}

	internal static class InternalAnonymous2Extensions
	{
		public static Action<T, Action<T>> ToWebService<T>(this T template,
			Action<T, Action<T>> Implementation
		)
		{

			var y = template.ToYield(Implementation);

			return (T data, Action<T> Yield) => y.AtServer(data, Yield);
		}

		public static Action<Action<T>> ToFixedWebService<T>(this T protocol,
			Action<T, Action<T>> Implementation
		)
		{

			var y = protocol.ToYield(Implementation);

			return (Action<T> Yield) => y.AtServer(protocol, Yield);
		}

		public static void AtServer<T>(this T protocol,
			Action<T, Action<T>> Implementation,
			Action<T> Yield
		)
		{
			protocol.ToYield(Implementation).AtServer(protocol, Yield);
		}
	}

	internal static class InternalAnonymousExtensions
	{
		public static Action<T, Action<T>> ToYield<T>(this T template, Func<T, Action<T, Action<T>>> y)
		{
			return y(template);
		}


		public static Action<T, Action<T>> ToYield<T>(this T template, Func<Action<T, Action<T>>> y)
		{
			// not passing template
			return y();
		}

		public static Action<T, Action<T>> ToYield<T>(this T template, Action<T, Action<T>> y)
		{
			// not passing template
			return y;
		}
	}

	internal static class UltraApplicationInlineWebServiceExtensions
	{
		public static void AtServer<T>(this  Action<T, Action<T>> Implementation, T Protocol, Action<T> Yield)
		{
			Implementation(Protocol, Yield);
		}

		public static void AtServer<T>(this Action<Action<T>> Implementation, Action<T> Yield)
		{
			// running in .net, GAEJava, php
			Implementation(Yield);
		}

		public static void AtServer(this Func<string, string> InternalFunction, StringAction yield)
		{
			// running in .net, GAEJava, php
		}

		public static void Method11(this string input1, Func<string, string> InternalFunction, StringAction yield)
		{
			// running in .net, GAEJava, php

		}
	}
}

