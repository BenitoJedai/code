using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Remoting.DOM;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.JavaScript.Remoting.Extensions;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.Extensions;

namespace UltraApplicationWithFlash
{
	partial class UltraSprite
	{
		public Action<PHTMLElement> EnhanceLogo { get; set; }
	}

	static class UltraSpriteExtensions
	{
		static void At(Action<IEvent> h)
		{
			//var a = OtherPointers.ToArray();

			var sh = new IHTMLDiv();

			sh.style.cursor = IStyle.CursorEnum.crosshair;
			sh.style.position = IStyle.PositionEnum.absolute;
			sh.style.SetLocation(0, 0);
			sh.style.width = "100%";
			sh.style.height = "100%";

			sh.style.backgroundColor = "black";
			sh.style.Opacity = 0.2;

			sh.AttachToDocument();

			sh.onclick +=
				e =>
				{
					//foreach (var item in a)
					//{
					//    item();
					//}

					sh.Orphanize();
					h(e);
				};
		}

		//static readonly List<Func<Action>> OtherPointers = new List<Func<Action>>();

		
		static Action Pointer(IEvent ee)
		{
			var sh = new IHTMLDiv();

			sh.style.position = IStyle.PositionEnum.absolute;
			sh.style.SetLocation(ee.OffsetX - 4, ee.OffsetY - 4, 8, 8);

			sh.style.backgroundColor = "black";

			sh.AttachToDocument();

			//OtherPointers.Add(
			//    delegate
			//    {
			//        sh.style.backgroundColor = "blue";

			//        sh.style.Opacity = 0.5;

			//        sh.AttachToDocument();

			//        return delegate
			//        {
			//            sh.Orphanize();
			//        };
			//    }
			//);

			return delegate
			{
				sh.Orphanize();
			};
		}

		public static void Implement(this UltraSprite o)
		{
			o.EnhanceLogo =
				i =>
				{
					IHTMLElement j = ((PIHTMLElement)i);

					var a = new AffineTransformBase
					{
						Width = 96,
						Height = 96,

						X1 = 100 + 96 * 2,
						Y1 = 100,

						X2 = 100,
						Y2 = 100 + 96 * 2,

						X3 = 100,
						Y3 = 100,

					};

					j.style.position = IStyle.PositionEnum.absolute;
					j.style.top = "0px";
					j.style.left = "0px";



					j.style.SetMatrixTransform(a);

					var mode = new IHTMLButton("Set Corners");

					mode.AttachToDocument();

					mode.onclick +=
						delegate
						{


							At(
								z1 =>
								{
									a.X1 = z1.OffsetX;
									a.Y1 = z1.OffsetY;

									var p1 = Pointer(z1);

									At(
										z2 =>
										{
											a.X2 = z2.OffsetX;
											a.Y2 = z2.OffsetY;

											var p2 = Pointer(z2);

											At(
												z3 =>
												{
													a.X3 = z3.OffsetX;
													a.Y3 = z3.OffsetY;

													var jj = (IHTMLElement)j.cloneNode(true);

													jj.AttachToDocument();
													jj.style.SetMatrixTransform(a);

													// remove pointer
													p1();
													p2();
												}
											);
										}
									);
								}
							);


						};
				};
		}
	}
}
