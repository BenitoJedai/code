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



	internal sealed class UltraApplicationInline2
	{
		public UltraApplicationInline2(IHTMLElement p)
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


			Action<Action<Action>> SomebodyGiveMeHandlerForCanvasInFlash =
				delegate
				{
					// by default we are not going to register any handlers
				};

			s.click +=
				ee =>
				{
					// we are in flash VM talking about WPF :)

					// calling an inline function
					var r = CreateCanvas();

					// attach XAML object into flash DOM
					ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.AttachToContainer(r, s);

					// when we click we are creating this canvas above.
					// at that time anybody who wants to attach as a click handler is ready.

					// we are letting them know that its time to attach
					SomebodyGiveMeHandlerForCanvasInFlash(
						// someone sent us a handler
						NewHandlerReadyToBeAttached =>
						{
							// lets tell XAML canvas that when its clicked we know whom to call
							r.MouseLeftButtonUp +=
								delegate
								{
									// let them know that its time to handle this event
									NewHandlerReadyToBeAttached();
								};
						}
					);

				};

			// lets add this Inline Flash Sprite into javascript DOM
			ScriptCoreLib.JavaScript.Extensions.SpriteExtensions.AttachSpriteToDocument(s);


			{

				var func1 = new
				{
					// we are going to send an anonymous type to the server and its going
					// to respond with that type zero to multiple times.

					data1 = default(int),
					data2 = new { text = default(string) }

					// this type could also be a named type
				}.ToWebService(
					(
						// this is the data send to the server
						protocol_sent_to_server,

						// this is the yeld method
						yield_protocol_to_client
					) =>
					{
						var loc3 = protocol_sent_to_server;
						var loc4 = yield_protocol_to_client;

						// because we are running on a server we could do some
						// server to server - business to business calls
						// or we could just do some data mining or storage

						// todo: we need to expose some LINQ to SQL like API here



						{
							// lets compute an anwser on the servere
							var result = new
							{
								data1 = loc3.data1 + 1,
								data2 = new { text = "server hello #1: " + loc3.data2.text }
							};

							// lets send the data twice
							loc4(result);
						}

						{
							// lets compute an anwser on the servere
							var result = new
							{
								data1 = loc3.data1 + 2,
								data2 = new { text = "server hello #2: " + loc3.data2.text }
							};

							// lets send the data twice
							loc4(result);
						}

						foreach (var item in
							from row in InternalDatabase1.Table1
							where row.Field1 < 4
							select row.Field2
							)
						{

							{
								// lets compute an anwser on the servere
								var result = new
								{
									data1 = 0,
									data2 = new { text = "data #2: " + item }
								};

								// lets send the data twice
								loc4(result);
							}
						}
					}
				);

				// lets register our handler factory so that when we click on the Sprite
				// a new XAML canvas is created and we want to add our onclick handler
				SomebodyGiveMeHandlerForCanvasInFlash +=
					AttachHandler =>
					{
						// its time, not we are not returning, we are "tail" calling
						// IE has some problems with returning from javascript to flash
						AttachHandler(
							delegate
							{
								// handling that event now...

								// ok we defined our implementation we want to run on the server
								func1(
									// this is now running under javascript
									new { data1 = 4, data2 = new { text = "dynamic" } },

									r =>
									{
										// for every result from the server
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

	internal class InternalData1
	{
		public int Field1;
		public string Field2;
	}

	internal class InternalDatabase1
	{
		static public IQueryable<InternalData1> Table1 { get; set; }
	}
}

