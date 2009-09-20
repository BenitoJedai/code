using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;

namespace MatrixTransformC1Example.js
{
	[Script]
	public class XRectangle
	{
		public IHTMLElement Element;
		public IHTMLElement Rotor;
		public IHTMLElement Content;
		public IHTMLImage ContentImage;

		public string Text;

		public XRectangle()
		{
			var aelement = new IHTMLDiv();



			// WPF: Rectangle, Image or Label
			var acontent = new IHTMLDiv();

			//acontent.style.backgroundColor = "#ff0000";

			this.Rotor = new IHTMLDiv();

			//this.Rotor.style.backgroundColor = "#ffd0d0";

			//this.Rotor.style.border = "1px solid black";

			//this.Rotor.style.paddingRight = "10px";

			this.Rotor.AttachTo(aelement);

			acontent.AttachTo(this.Rotor);

			Element = aelement;
			Content = acontent;
		}
	}

	[Script]
	static class Extensions
	{
		public static void ApplyMatrix(this XRectangle k, double OriginX, double OriginY, double M11, double M12, double M21, double M22, double Dx, double Dy)
		{
			// if this information came via WPF we should do Dx -= OriginX
			var mm = new[]
					{
						M11, M12,
						M21, M22,
						
						0, 0
						//0.838670551776886,0.5446390509605408,-0.5446390509605408,0.838670551776886,0,0
					};

			//this.Content.innerText = new
			//{
			//    OriginX,
			//    OriginY,
			//    M11,
			//    M12,
			//    M21,
			//    M22,
			//    Dx,
			//    Dy,

			//    Text
			//}.ToString();

			var code = @"
			q.style.filter = ""progid:DXImageTransform.Microsoft.Matrix(M11='"" + m[0] + ""',M12='"" + m[2] + ""',M21='"" + m[1] + ""', M22='"" + m[3] + ""', sizingmethod='auto expand');"";
	

			q.style.MozTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
			
			q.style.WebkitTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
				";

			var zx = 0;
			var zy = 0;


			//if (OriginX == -k.Content.clientWidth / 2)
			//    if (OriginY == -k.Content.clientHeight / 2)
				{

					k.Rotor.style.paddingLeft = 185 + "px";
					k.Rotor.style.paddingTop = 100 + "px";

					zy -= 185;
					zx -= 100;
				}

			//if (OriginX == k.Content.clientWidth / 2)
			//    if (OriginY == k.Content.clientHeight / 2)
			//    {
				//k.Rotor.style.paddingRight = 369 + "px";
				//k.Rotor.style.paddingBottom = 200 + "px";

				////zx -= this.Rotor.clientWidth;
				//zy += this.Rotor.clientHeight / 2;
				//zx += this.Rotor.clientWidth / 2;
			//    }

			var Rotor = k.Rotor;

			new IFunction("q", "m", code).apply(null, Rotor, mm);

			zx += (Rotor.clientWidth - Rotor.offsetWidth) / 2;
			zy += (Rotor.clientHeight - Rotor.offsetHeight) / 2;


			Rotor.style.SetLocation(Convert.ToInt32(Dx) + zx, Convert.ToInt32(Dy) + zy);

		}


		public static T MoveTo<T>(this T e, int x, int y) where T : IHTMLElement
		{
			e.style.SetLocation(x, y);

			return e;
		}
		public static int Random(this int i)
		{
			return new Random().Next(i);
		}
	}
}
