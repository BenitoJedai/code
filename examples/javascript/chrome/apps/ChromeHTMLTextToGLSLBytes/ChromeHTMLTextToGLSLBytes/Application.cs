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
using ChromeHTMLTextToGLSLBytes;
using ChromeHTMLTextToGLSLBytes.Design;
using ChromeHTMLTextToGLSLBytes.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;
using System.IO;

namespace ChromeHTMLTextToGLSLBytes
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
			// https://www.shadertoy.com/view/lsSGRz


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

			new IHTMLTextArea { value = "oncontextlost" }.AttachTo(Native.document.documentElement).With(
				async xinput =>
				{
					new IStyle(xinput)
					{
						position = IStyle.PositionEnum.@fixed,
						left = "0px",
						bottom = "0px"
					};

					do
					{
						Native.body.Clear();

						// host could
						// implement crash management for programs that dont compile well--
						// and refraim from recompiling the next time gpu is reloaded

						var refdiv = new IHTMLDiv { xinput.value }.AttachToDocument();
						refdiv.style.fontFamily = IStyle.FontFamilyEnum.Courier;
						refdiv.style.fontSize = "12px";
						refdiv.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;

						//refdiv.style.fontSize = "10px";
						//refdiv.style.fontSize = "9px";
						//refdiv.style.fontSize = "8px";

						refdiv.style.display = IStyle.DisplayEnum.inline;
						//refdiv.style.backgroundColor = "yellow";
						refdiv.style.backgroundColor = "white";

						//new { }.With(
						//	async delegate
						//	{

						//		await Native.window.async.onframe;

						var inline = IStyle.DisplayEnum.inline;
						var isinline = (refdiv.style.display == inline);


						new IHTMLPre { new { refdiv.clientWidth, refdiv.offsetWidth, isinline, refdiv.clientHeight, refdiv.offsetHeight } }.AttachToDocument();

						//new IHTMLHorizontalRule { }.AttachToDocument();

						//var refsvg = (ISVGSVGElement)refdiv;

						//refsvg.AttachToDocument();

						//new IHTMLPre { new { refsvg.clientWidth, refsvg.offsetWidth, isinline, refsvg.clientHeight, refsvg.offsetHeight } }.AttachToDocument();

						new IHTMLHorizontalRule { }.AttachToDocument();

						//int col[6];
						// https://www.shadertoy.com/view/4tSGzz

						var c = (IHTMLCanvas)refdiv;

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

						var glsl = new StringBuilder();

						var ytop = 4;

						//int col[6];

						var colLength = Math.Ceiling(c.width / 16.0);

						glsl.AppendLine("float GetBinary(vec2 coord) {");
						glsl.AppendLine($"float refcol = 0.0;");
						glsl.AppendLine($"int col[{colLength}];");

						for (int y = ytop; y < c.height; y++)
						{
							var xdiv = new IHTMLDiv { }.AttachToDocument();
							xdiv.style.fontSize = "1px";

							if (y > ytop) glsl.Append("else ");
							//glsl.AppendLine($"if (y == {y - ytop})");

							var yy = y - ytop;
							glsl.AppendLine($"if (({c.height - ytop} - int(coord.y)) == {yy})");


							glsl.AppendLine("{");

							var colindex = 0;
							var colvalue = 0;


							for (int i = 0; i < c.width; i++)
							{
								var zero = pixels[i * 4 + 0 + y * c.width * 4];	//| pixels[i * 4 + 1 + y * c.width * 4] | pixels[i * 4 + 2 + y * c.width * 4];

								//new IHTMLCode { new { zero } }.AttachToDocument();

								if (zero > 0xc4)
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

								var f0 = i % 16 == 15;
								var f1 = i == c.width - 1;
								if (f0 || f1)
								{
									// col[0] = 0x{1:x4});
									//var colvaluex4 = colvalue.ToString("x4");
									var colvaluex4 = colvalue.ToString("x8");


									if (yy > 25)
										glsl.AppendLine($"// /* ldc limit per function reached */ col[{colindex}] = 0x{colvaluex4};");
									else
										glsl.AppendLine($"col[{colindex}] = 0x{colvaluex4};");

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

							glsl.AppendLine("}");


							//new IHTMLBreak().AttachToDocument();
						}



						// ready to export channel1?


						glsl.AppendLine($"if (coord.x >= 0.0)");

						for (int i = 0; i < colLength; i++)
						{
							if (i > 0) glsl.Append("else ");
							glsl.AppendLine($"if (coord.x < {(i + 1) * 16}.0) refcol = float(col[{i}]);");
						}

						glsl.AppendLine("return mod(floor(refcol / pow(2.0, floor(mod(coord.x, 16.0)))), 2.0); }");

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

						new IHTMLTextArea { glsl.ToString() }.AttachToDocument();
						// y 16
					}
					while (await xinput.async.onchange);

				}
			);




		}

	}
}
