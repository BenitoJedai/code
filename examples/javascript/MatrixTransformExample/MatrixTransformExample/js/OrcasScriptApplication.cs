using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM;
using System;
using ScriptCoreLib.JavaScript.Runtime;


namespace MatrixTransformExample.js
{
	[Script, ScriptApplicationEntryPoint]
	public class MatrixTransformExample
	{

		public MatrixTransformExample()
		{
			// see: http://webfx.eae.net/tools/filtertool.html

			Native.Document.body.style.backgroundColor = "yellow";

			var timer = new Timer(
				delegate
				{

				}
			, 0, 1000 / 30);

			new IHTMLDiv
			{

				innerText =
					@"The rotating element needs to adjust its 
location in IE. IE can also handle only inline elements. In WPF 
this would mean we need to know our actual location. It seems IE wont allow
clip also.

We could however support WPF FlowDocument for inline elemnts.
Example: left = (a.clientWidth - a.offsetWidth) / 2"
			}.AttachToDocument();

			new IHTMLImage("assets/MatrixTransformExample/bg.png").AttachToDocument(); ;

			{
				var content = new IHTMLDiv();
				content.style.position = IStyle.PositionEnum.absolute;
				content.style.left = "500px";
				content.style.top = "200px";
			
				//content.style.Opacity = 0.3;
				content.AttachToDocument();

				CreateRotor(45, content);
				CreateRotor2(45, content, new IHTMLImage("assets/MatrixTransformExample/gtataxi.png"), timer);
				//CreateRotor(33);
			}
			{
				var content = new IHTMLDiv();
				content.style.position = IStyle.PositionEnum.absolute;
				content.style.left = "208px";
				content.style.top = "108px";
				//content.style.Opacity = 0.3;
				content.AttachToDocument();

				CreateRotor2(45, content, new IHTMLImage("assets/MatrixTransformExample/gtataxi_big_shadow.png"), timer);
				//CreateRotor(33);
			}

			{
				var content = new IHTMLDiv();
				content.style.position = IStyle.PositionEnum.absolute;
				content.style.left = "200px";
				content.style.top = "100px";
				//content.style.Opacity = 0.3;
				content.AttachToDocument();

				CreateRotor2(45, content, new IHTMLImage("assets/MatrixTransformExample/gtataxi_big.png"), timer);
				//CreateRotor(33);
			}




			{
				var content = new IHTMLDiv();
				content.style.position = IStyle.PositionEnum.absolute;
				content.style.left = "204px";
				content.style.top = "204px";
				//content.style.Opacity = 0.3;
				content.AttachToDocument();

				CreateRotor2(45, content, new IHTMLImage("assets/MatrixTransformExample/gtataxi_big_mask.png"), timer);
				//CreateRotor(33);
			}

			{
				var content = new IHTMLDiv();
				content.style.position = IStyle.PositionEnum.absolute;
				content.style.left = "200px";
				content.style.top = "200px";
				//content.style.Opacity = 0.3;
				content.AttachToDocument();

				CreateRotor2(45, content, new IHTMLImage("assets/MatrixTransformExample/gtataxi_big.png"), timer);
				//CreateRotor(33);
			}
		}

		private static void CreateRotor(double _angle, IHTMLElement _container)
		{

			var shadow = new IHTMLDiv();
			shadow.style.position = IStyle.PositionEnum.absolute;
			shadow.style.left = "0px";
			shadow.style.top = "0px";
			shadow.style.width = "600px";
			shadow.style.height = "400px";
			shadow.style.backgroundColor = "black";
			shadow.style.Opacity = 0.3;
			shadow.AttachTo(_container);

			var borders = new IHTMLDiv();
			borders.style.position = IStyle.PositionEnum.absolute;
			borders.style.left = "0px";
			borders.style.top = "0px";
			//borders.style.width = "400px";
			//borders.style.height = "300px";
			borders.style.border = "4px solid black";
			borders.style.Opacity = 0.1;
			borders.AttachTo(_container);

			var a = new IHTMLDiv();

			// we can specify rotation origin within red area!

			a.style.backgroundColor = "red";

			a.style.marginTop = "-200px";
			a.style.marginLeft = "-300px";
			a.style.paddingLeft = "300px";
			a.style.paddingTop = "200px";

			var c = new IHTMLDiv();

			var z = new IHTMLButton("hi");


			z.AttachTo(c);
			c.AttachTo(a);

			//a.style.width = "300px";
			//a.style.height = "200px";

			c.style.width = "300px";
			c.style.height = "200px";
			//c.style.position = IStyle.PositionEnum.relative;
			//c.style.SetLocation(0, 0);
			//c.style.clip = "rect(0px,60px,200px,0px)";

			c.appendChild("hello world");
			c.appendChild(new IHTMLButton("click me"));

			var xx = new IHTMLButton("xx");

			xx.style.SetLocation(32, 32);

			c.appendChild(xx);
			c.appendChild("hello world");

			new IHTMLDiv
			{
				innerText =
					@"As it seems we can use apng files but 
we cannot have absolute children. This means we will be able to support
non container objects."
			}.AttachTo(c);


			c.style.SetBackground("assets/MatrixTransformExample/bg.png", true);

			var t = new IHTMLDiv();

			c.appendChild(t);

			//a.style.width = "400px";
			//a.style.height = "300px";


			
			var angle = _angle;
			ApplyRotation2(a, angle, 1, 200, 0);

			new Timer(
				delegate
				{
					t.innerHTML = "" + angle;
					angle -= 2;
					ApplyRotation2(a, angle, 1, 200, 0);

					borders.style.SetSize(a.offsetWidth, a.offsetHeight);

					borders.style.SetLocation((a.clientWidth - a.offsetWidth) / 2, (a.clientHeight - a.offsetHeight) / 2);
					a.style.SetLocation((a.clientWidth - a.offsetWidth) / 2, (a.clientHeight - a.offsetHeight) / 2);

				}
				, 0, 250
			);


			a.AttachTo(_container);
		}

		private static void CreateRotor2(double _angle, IHTMLElement _container, IHTMLImage img, Timer timer)
		{



			var a = img;




			//a.style.width = "400px";
			//a.style.height = "300px";



			var angle = _angle;
			ApplyRotation(a, angle, 2);

			timer.Tick +=
				delegate
				{
					angle -= 0.25 * 8;
					ApplyRotation(a, angle, 2);


					a.style.SetLocation((a.clientWidth - a.offsetWidth) / 2, (a.clientHeight - a.offsetHeight) / 2);

				};


			a.AttachTo(_container);
		}


		private static void ApplyRotation(IHTMLElement a, double angle, double Zoom)
		{
			// https://developer.mozilla.org/en/CSS_Reference/Mozilla_Extensions

			var deg2rad = Math.PI * 2 / 360;
			var rad = angle * deg2rad;
			var costheta = Math.Cos(rad);
			var sintheta = Math.Sin(rad);



			var m = new[]
			{
				costheta * Zoom, sintheta * Zoom,
				-sintheta * Zoom, costheta * Zoom,
				
				0,0
				//0.838670551776886,0.5446390509605408,-0.5446390509605408,0.838670551776886,0,0
			};

			var code = @"
			q.style.filter = ""progid:DXImageTransform.Microsoft.Matrix(M11='"" + m[0] + ""',M12='"" + m[2] + ""',M21='"" + m[1] + ""', M22='"" + m[3] + ""', sizingmethod='auto expand');"";
	
			q.style.MozTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
			
			q.style.WebkitTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
				";

			new IFunction("q", "m", code).apply(null, a, m);

			// http://www.dhtmlcentral.com/tutorials/tutorials_p3_4.php
			//Native.Document.title = new { a.offsetWidth,  a.clientWidth, a.scrollWidth }.ToString();


		}

		private static void ApplyRotation2(IHTMLElement a, double angle, double Zoom, double Dx, double Dy)
		{
			// https://developer.mozilla.org/en/CSS_Reference/Mozilla_Extensions

			var deg2rad = Math.PI * 2 / 360;
			var rad = angle * deg2rad;
			var costheta = Math.Cos(rad);
			var sintheta = Math.Sin(rad);



			var m = new[]
			{
				costheta * Zoom, sintheta * Zoom,
				-sintheta * Zoom, costheta * Zoom,
				
				0, 0
				//0.838670551776886,0.5446390509605408,-0.5446390509605408,0.838670551776886,0,0
			};

			var code = @"
			q.style.filter = ""progid:DXImageTransform.Microsoft.Matrix(M11='"" + m[0] + ""',M12='"" + m[2] + ""',M21='"" + m[1] + ""', M22='"" + m[3] + ""', sizingmethod='auto expand');"";
	
			q.style.MozTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
			q.style.WebkitTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
				";

			new IFunction("q", "m", code).apply(null, a, m);

			// http://www.dhtmlcentral.com/tutorials/tutorials_p3_4.php
			//Native.Document.title = new { a.offsetWidth,  a.clientWidth, a.scrollWidth }.ToString();


		}

		static MatrixTransformExample()
		{
			typeof(MatrixTransformExample).SpawnTo(i => new MatrixTransformExample());
		}

	}

}
