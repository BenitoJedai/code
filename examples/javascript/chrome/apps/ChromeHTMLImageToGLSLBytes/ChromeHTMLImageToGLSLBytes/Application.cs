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
using ChromeHTMLImageToGLSLBytes;
using ChromeHTMLImageToGLSLBytes.Design;
using ChromeHTMLImageToGLSLBytes.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;
using System.IO;
using ScriptCoreLib.GLSL;
using System.Linq.Expressions;

namespace ChromeHTMLImageToGLSLBytes
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
			// X:\opensource\googlecode\avalonpipemania\AvalonPipeMania\AvalonPipeMania.Assets\web\assets\AvalonPipeMania.Animation\duck_ride

			// https://www.shadertoy.com/view/4tSGzz


			#region += Launched chrome.app.window
			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
				{
					Console.WriteLine("chrome.app.window.create, is that you?");

					// pass thru
				}
				else
				{
					// should jsc send a copresence udp message?
					chrome.runtime.UpdateAvailable += delegate
					{
						new chrome.Notification(title: "UpdateAvailable");

					};

					chrome.app.runtime.Launched += async delegate
					{
						// 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
						Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

						new chrome.Notification(title: "ChromeUDPSendAsync");

						var xappwindow = await chrome.app.window.create(
							   Native.document.location.pathname, options: null
						);

						//xappwindow.setAlwaysOnTop

						xappwindow.show();

						await xappwindow.contentWindow.async.onload;

						Console.WriteLine("chrome.app.window loaded!");
					};


					return;
				}
			}
			#endregion

			Native.body.style.backgroundColor = "yellow";


			//Warning CS1998  This async method lacks 'await' operators and will run synchronously.Consider using the 'await' operator to await non - blocking API calls, or 'await Task.Run(...)' to do CPU - bound work on a background thread.	ChromeHTMLImageToGLSLBytes X:\jsc.svn\examples\javascript\chrome\apps\ChromeHTMLImageToGLSLBytes\ChromeHTMLImageToGLSLBytes\Application.cs 93


			new HTML.Images.FromAssets.x_01 { }.AttachToDocument().With(
				async refimg =>
				{
					new IHTMLPre { new { refimg.width, refimg.height, refimg.clientWidth, refimg.offsetWidth, refimg.complete } }.AttachToDocument();

					//await refimg.GetAwaiter();
					await refimg;

					new IHTMLPre { new { refimg.width, refimg.height, refimg.clientWidth, refimg.offsetWidth, refimg.complete } }.AttachToDocument();

					// http://www.matthewflickinger.com/lab/whatsinagif/animation_and_transparency.asp

					//do
					{

						new IHTMLHorizontalRule { }.AttachToDocument();

						//int col[6];
						// https://www.shadertoy.com/view/4tSGzz

						var c = (IHTMLCanvas)refimg;

						c.AttachToDocument();

						// The vertex language must provide an integer precision of at least 16 bits, plus a sign bit.

						// http://gamedev.stackexchange.com/questions/8718/glsl-how-do-i-cast-a-float-into-an-int
						//new IHTMLPre { new { c.width, ushorts = c.width / sizeof(ushort), c.height } }.AttachToDocument();
						new IHTMLPre { new { c.width, ushorts = c.width / 16, c.height } }.AttachToDocument();


						var pixels = c.bytes;


						var pixel0r = pixels[0];
						var pixel0g = pixels[1];
						var pixel0b = pixels[2];
						var pixel0a = pixels[3];

						// can we do union casting yet?

						//new IHTMLPre { pixel0r, pixel0g, pixel0b, pixel0a }.AttachToDocument();

						// need sub methods to split the local memory exhaustion
						var glsl0 = new StringBuilder();
						var glsl8 = new StringBuilder();
						var glsl = new StringBuilder();

						//var ytop = 4;
						var ytop = 0;

						//int col[6];

						var colLength = Math.Ceiling(c.width / 16.0);

						glsl.AppendLine("float GetBinary(vec2 coord) {");


						for (int y = ytop; y < c.height; y++)
						{
							var xdiv = new IHTMLDiv { }.AttachToDocument();
							xdiv.style.fontSize = "1px";

							//if (y > ytop) glsl.Append("else ");
							//glsl.AppendLine($"if (y == {y - ytop})");

							var yy = y - ytop;

							var chunk = Math.Floor(yy / 8.0) * 8;
							var chunkNext = chunk + 8;

							if (yy == chunk)
							{

								glsl8.AppendLine($"float GetBinaryLessThan{chunkNext}(vec2 coord)");
								glsl8.AppendLine("{");

								glsl.AppendLine($"if (({c.height - ytop} - int(coord.y)) < {chunkNext}) return GetBinaryLessThan{chunkNext}(coord);");

							}



							glsl8.AppendLine($"if (({c.height - ytop} - int(coord.y)) == {yy}) return GetBinaryAt{yy}(coord.x); "
								//+ new { yy, chunkNext }
								);

							var isLastInChunk = yy == chunkNext - 1;
							var isLastOverall = y == c.height - 1;
							if (isLastInChunk || isLastOverall)
							{
								glsl8.AppendLine("return 0.0;");
								glsl8.AppendLine("}");
							}


							#region GetBinaryAt
							glsl0.AppendLine($"float GetBinaryAt{yy}(float coord_x)");
							glsl0.AppendLine("{");
							glsl0.AppendLine($"float refcol = 0.0;");
							glsl0.AppendLine($"int col[{colLength}];");

							var colindex = 0;
							var colvalue = 0;


							for (int i = 0; i < c.width; i++)
							{
								// we want alpha
								var zero = pixels[3 + i * 4 + 0 + y * c.width * 4];	//| pixels[i * 4 + 1 + y * c.width * 4] | pixels[i * 4 + 2 + y * c.width * 4];

								//new IHTMLCode { new { zero } }.AttachToDocument();

								#region IHTMLSpan
								if (zero < 0xc4)
								{
									new IStyle(new IHTMLSpan { }.AttachTo(xdiv))
									{
										display = IStyle.DisplayEnum.inline_block,
										width = "4px",
										height = "4px",
										fontSize = "1px"
									};
								}
								else
								{
									colvalue |= 1 << (i % 16);

									new IStyle(new IHTMLSpan { }.AttachTo(xdiv))
									{
										backgroundColor = "black",
										color = "yellow",

										display = IStyle.DisplayEnum.inline_block,

										width = "4px",
										height = "4px",
										fontSize = "1px"
									};
								}
								#endregion


								var f0 = i % 16 == 15;
								var f1 = i == c.width - 1;
								if (f0 || f1)
								{
									// col[0] = 0x{1:x4});
									//var colvaluex4 = colvalue.ToString("x4");
									var colvaluex4 = colvalue.ToString("x8");



									glsl0.AppendLine($"col[{colindex}] = 0x{colvaluex4};");

									new IStyle(new IHTMLSpan { }.AttachTo(xdiv))
									{
										backgroundColor = "cyan",

										display = IStyle.DisplayEnum.inline_block,

										width = "4px",
										height = "4px",
										fontSize = "1px"
									};

									colindex++;
									colvalue = 0;
								}

							}


							glsl0.AppendLine($"if (coord_x >= 0.0)");

							for (int i = 0; i < colLength; i++)
							{
								if (i > 0) glsl0.Append("else ");
								glsl0.AppendLine($"if (coord_x < {(i + 1) * 16}.0) refcol = float(col[{i}]);");
							}

							// https://android.googlesource.com/platform/external/mesa3d/+/75d4dfa13601a92bc5dbc62b054f1c130213e7ca/src/glsl/glcpp/glcpp-parse.c

							// script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Expressions.Expression.Modulo(System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)]
							//Expression<Func<float, float>> f = y => (float)(Math.Floor(1.0f) % 2.0f);

							glsl0.AppendLine($@"
	return 
		{nameof(FragmentShader.mod)}(
			{nameof(FragmentShader.floor)}(
				refcol / pow(2.0, 
				{nameof(FragmentShader.floor)}(
					{nameof(FragmentShader.mod)}(
						coord_x, 
						16.0
					)
				)
			)
		),
		2.0
	);
						");

							glsl0.AppendLine("}");
							#endregion



							//new IHTMLBreak().AttachToDocument();
						}



						// ready to export channel1?


						glsl.AppendLine("return 0.0; }");

						// http://stackoverflow.com/questions/15828966/glsl-compile-error-memory-exhausted


						//[{ c.width / 16}]

						//if (y == 0)
						//{
						//	//col[0] = 0x60f8;
						//	//col[1] = 0x0;
						//	//col[2] = 0x60;
						//	col[3] = 0xf800;
						//	col[4] = 0xf;
						//	col[5] = 0x0;
						//}

						//{ c.width / 16}

						// https://www.opengl.org/wiki/Data_Type_(GLSL)
						// no unit for glsl
						// 1010101010101010
						ushort y0bit16 = 0;

						new IHTMLTextArea {

							glsl0.ToString()
							+ glsl8.ToString()
							+ glsl.ToString()
						}.AttachToDocument();
						// y 16
					}
					//while (await xinput.async.onchange);

				}
			);




		}

	}
}
