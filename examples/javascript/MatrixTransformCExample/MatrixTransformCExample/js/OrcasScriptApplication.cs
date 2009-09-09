using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using System;


namespace MatrixTransformCExample.js
{
	[Script, ScriptApplicationEntryPoint]
	public class MatrixTransformCExample
	{
		[Script]
		class XRectangle
		{
			public IHTMLElement Element;
			public IHTMLElement Rotor;
			public IHTMLElement Content;

			public string Text;

			public XRectangle()
			{
				var aelement = new IHTMLDiv();



				// WPF: Rectangle, Image or Label
				var acontent = new IHTMLDiv();

				acontent.style.backgroundColor = "#ff0000";

				this.Rotor = new IHTMLDiv();

				//this.Rotor.style.backgroundColor = "#ffd0d0";

				this.Rotor.style.border = "1px solid black";

				//this.Rotor.style.paddingRight = "10px";

				this.Rotor.AttachTo(aelement);

				acontent.AttachTo(this.Rotor);

				aelement.AttachToDocument();

				Element = aelement;
				Content = acontent;
			}

			public void ApplyMatrix(double OriginX, double OriginY, double Rotation, double Dx, double Dy)
			{
				var costheta = Math.Cos(Rotation);
				var sintheta = Math.Sin(Rotation);

				var M11 = costheta;
				var M12 = -sintheta;
				var M21 = sintheta;
				var M22 = costheta;

				ApplyMatrix(OriginX, OriginY, M11, M12, M21, M22, Dx, Dy);
			}

			public void ApplyMatrix(double OriginX, double OriginY, double M11, double M12, double M21, double M22, double Dx, double Dy)
			{
				var mm = new[]
					{
						M11, M21,
						M12, M22,
						
						0, 0
						//0.838670551776886,0.5446390509605408,-0.5446390509605408,0.838670551776886,0,0
					};

				this.Content.innerText = new
				{
					OriginX,
					OriginY,
					M11,
					M12,
					M21,
					M22,
					Dx,
					Dy,

					Text
				}.ToString();

				var code = @"
			q.style.filter = ""progid:DXImageTransform.Microsoft.Matrix(M11='"" + m[0] + ""',M12='"" + m[2] + ""',M21='"" + m[1] + ""', M22='"" + m[3] + ""', sizingmethod='auto expand');"";
	
			q.style.MozTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
			
			q.style.WebkitTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
				";

				var zx = 0;
				var zy = 0;

				if (OriginX == -this.Content.clientWidth / 2)
					if (OriginY == -this.Content.clientHeight / 2)
					{
						this.Rotor.style.paddingLeft = this.Content.clientWidth + "px";
						this.Rotor.style.paddingTop = this.Content.clientHeight + "px";

						//zx -= this.Rotor.clientWidth;
						zy -= this.Rotor.clientHeight / 2;
						zx -= this.Rotor.clientWidth / 2;
					}

				if (OriginX == this.Content.clientWidth / 2)
					if (OriginY == this.Content.clientHeight / 2)
					{
						this.Rotor.style.paddingRight = this.Content.clientWidth + "px";
						this.Rotor.style.paddingBottom = this.Content.clientHeight + "px";

						////zx -= this.Rotor.clientWidth;
						//zy += this.Rotor.clientHeight / 2;
						//zx += this.Rotor.clientWidth / 2;
					}



				new IFunction("q", "m", code).apply(null, this.Rotor, mm);

				zx += (this.Rotor.clientWidth - this.Rotor.offsetWidth) / 2;
				zy += (this.Rotor.clientHeight - this.Rotor.offsetHeight) / 2;


				this.Rotor.style.SetLocation(Convert.ToInt32(Dx) + zx, Convert.ToInt32(Dy) + zy);

			}
		}

		public MatrixTransformCExample()
		{

			var btn = new IHTMLButton("+");

			{
				var r = new XRectangle();

				r.Element.style.SetLocation(400, 50);
				r.Content.style.SetSize(400, 100);

				r.Element.style.Opacity = 0.3;
			}
			{
				var r = new XRectangle { Text = "center" };

				r.Element.style.SetLocation(400, 50);
				r.Content.style.SetSize(400, 100);


				var angle = 22;

				// rotate at top left
				r.ApplyMatrix(0, 0, angle.DegreesToRadians(), 0, 0);

				btn.onclick +=
					delegate
					{
						angle += 5;
						r.ApplyMatrix(0, 0, angle.DegreesToRadians(), 0, 0);
					};

			}
			{
				var r = new XRectangle { Text = "center offset to left" };

				r.Element.style.SetLocation(400, 50);
				r.Content.style.SetSize(400, 100);
				r.Content.style.backgroundColor = "blue";

				var angle = 22;

				// rotate at top left
				r.ApplyMatrix(0, 0, angle.DegreesToRadians(), -400, 0);

				btn.onclick +=
					delegate
					{
						angle += 5;
						r.ApplyMatrix(0, 0, angle.DegreesToRadians(), -400, 0);
					};
			}


			{
				var r = new XRectangle();

				r.Element.style.SetLocation(400, 200);
				r.Content.style.SetSize(400, 100);

				r.Element.style.Opacity = 0.3;
			}

			{
				var r = new XRectangle { Text = "left top" };

				r.Element.style.SetLocation(400, 200);
				r.Content.style.SetSize(400, 100);


				var angle = 22;

				// rotate at center
				r.ApplyMatrix(-200, -50, angle.DegreesToRadians(), 0, 0);

				btn.onclick +=
					delegate
					{
						angle += 5;
						r.ApplyMatrix(-200, -50, angle.DegreesToRadians(), 0, 0);
					};
			}




			{
				var r = new XRectangle();

				r.Element.style.SetLocation(400, 350);
				r.Content.style.SetSize(400, 100);

				r.Element.style.Opacity = 0.3;
			}

			{
				var r = new XRectangle { Text = "right bottom" };

				r.Element.style.SetLocation(400, 350);
				r.Content.style.SetSize(400, 100);


				var angle = 22;

				// rotate at center
				r.ApplyMatrix(200, 50, angle.DegreesToRadians(), 0, 0);

				btn.onclick +=
					delegate
					{
						angle += 5;
						r.ApplyMatrix(200, 50, angle.DegreesToRadians(), 0, 0);
					};
			}

			{
				var r = new XRectangle { Text = "right bottom offset right" };
				r.Content.style.backgroundColor = "blue";
				r.Element.style.SetLocation(400, 350);
				r.Content.style.SetSize(400, 100);


				var angle = 22;

				// rotate at center
				r.ApplyMatrix(200, 50, angle.DegreesToRadians(), 400, 0);

				btn.onclick +=
					delegate
					{
						angle += 5;
						r.ApplyMatrix(200, 50, angle.DegreesToRadians(), 400, 0);
					};
			}

			btn.AttachToDocument();
			btn.style.SetLocation(32, 32);

		}



		static MatrixTransformCExample()
		{
			typeof(MatrixTransformCExample).SpawnTo(i => new MatrixTransformCExample());
		}

	}

}
