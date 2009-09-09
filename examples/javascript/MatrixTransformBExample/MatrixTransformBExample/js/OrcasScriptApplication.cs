using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Windows.Forms;
using System;


namespace MatrixTransformBExample.js
{
	[Script, ScriptApplicationEntryPoint]
	public class MatrixTransformBExample
	{

		public MatrixTransformBExample()
		{
			var x = 400;
			var y = 200;
			var w = 200;
			var h = 200;

			
			


			var jzo = new IHTMLDiv();

			jzo.style.background = "black";
			jzo.style.SetLocation(x - w / 2 - 4, y - h / 2 - 1, 8, 2);

			jzo.AttachToDocument();

			var jzoh = new IHTMLDiv();

			jzoh.style.background = "black";
			jzoh.style.SetLocation(x - w / 2 - 1, y - h / 2 - 4, 2, 8);

			jzoh.AttachToDocument();


			jzo.BlinkAt(400);
			jzoh.BlinkAt(400);

			var jo = new IHTMLDiv();

			jo.style.background = "blue";
			jo.style.SetLocation(x - w / 2 - 4, y - h / 2 - 1, 8, 2);

			jo.AttachToDocument();

			var joh = new IHTMLDiv();

			joh.style.background = "blue";
			joh.style.SetLocation(x - w / 2 - 1, y - h / 2 - 4, 2, 8);

			joh.AttachToDocument();

			jo.BlinkAt(400);
			joh.BlinkAt(400);

			var zo = new IHTMLDiv();

			zo.style.background = "black";
			zo.style.SetLocation(x - 4, y - 1, 8, 2);

			zo.AttachToDocument();

			var zoh = new IHTMLDiv();

			zoh.style.background = "black";
			zoh.style.SetLocation(x - 1, y - 4, 2, 8);

			zoh.AttachToDocument();


			var ro = new IHTMLDiv();

			ro.style.background = "red";
			ro.style.SetLocation(x, y, w, h);
			ro.style.Opacity = 0.4;
			ro.AttachToDocument();

			var info = new IHTMLSpan { innerText = "MatrixTransform" };


			info.style.SetLocation(x, y + h, w, h);
			
			info.AttachToDocument();

			var at = new IHTMLDiv();

			at.style.background = "yellow";
			at.style.SetLocation(x - w / 2, y - h / 2, w * 2, h * 2);
			at.style.Opacity = 0.2;
			at.AttachToDocument();

			var o = new IHTMLDiv();

			o.style.background = "blue";
			o.style.SetLocation(x + w / 2 - 4, y + h / 2 - 1, 8, 2);

			o.AttachToDocument();

			var oh = new IHTMLDiv();

			oh.style.background = "blue";
			oh.style.SetLocation(x + w / 2 - 1, y + h / 2 - 4, 2, 8);

			oh.AttachToDocument();

			var r = new IHTMLDiv();

			r.style.background = "black";
			r.style.SetLocation(x, y, w, h);
			r.style.Opacity = 0.3;
			r.AttachToDocument();


			var m = new MatrixModifiers();

			var InteractiveSetOrigin_x = 0;
			var InteractiveSetOrigin_y = 0;

			var InteractiveSetRotation_x = 0;
			var InteractiveSetRotation_y = 0;


			Action<int, int> InteractiveSetRotation = null;
 
			Action<int, int> InteractiveSetOrigin =
				(ox, oy) =>
				{
					InteractiveSetOrigin_x = ox;
					InteractiveSetOrigin_y = oy;

					m.TranslateX.Text = "" + ox;
					m.TranslateY.Text = "" + oy;

					ro.style.SetLocation(x + ox, y + oy, w, h);
					o.style.SetLocation(x - ox - 4, y - oy - 1, 8, 2);
					oh.style.SetLocation(x - ox - 1, y - oy - 4, 2, 8);

					InteractiveSetRotation(InteractiveSetRotation_x, InteractiveSetRotation_y);
				};

			InteractiveSetRotation =
				(ox, oy) =>
				{
					InteractiveSetRotation_x = ox;
					InteractiveSetRotation_y = oy;

					joh.style.SetLocation(x - w / 2 + ox - 1, y - h / 2 + oy - 4, 2, 8);
					jo.style.SetLocation(x - w / 2 + ox - 4, y - h / 2 + oy - 1, 8, 2);

					jzoh.style.SetLocation(x + InteractiveSetOrigin_x - w / 2 + ox - 1, y + InteractiveSetOrigin_y - h / 2 + oy - 4, 2, 8);
					jzo.style.SetLocation(x + InteractiveSetOrigin_x - w / 2 + ox - 4, y + InteractiveSetOrigin_y - h / 2 + oy - 1, 8, 2);

				};

			#region bind InteractiveSetRotation
			at.onclick +=
				e =>
				{
					InteractiveSetRotation(e.OffsetX, e.OffsetY);

				};

			at.onmouseover +=
				delegate
				{
					info.innerText = "Click to set rotation";
				};

			InteractiveSetRotation(0, 0);
			#endregion

			#region bind InteractiveSetOrigin
			m.ButtonClear.Click +=
				delegate
				{
					InteractiveSetOrigin(0, 0);
				};

			r.onclick +=
				e =>
				{
					// 0 0 is top left

					InteractiveSetOrigin(-e.OffsetX, -e.OffsetY);
				};

			r.onmouseover +=
				delegate
				{
					info.innerText = "Click to set origin";
				};


			InteractiveSetOrigin(0, 0);
			#endregion

			//InteractiveSetOrigin(-w / 2, -h / 2);

			var f = new Form { Text = "MatrixModifier" };

			m.BackColor = System.Drawing.Color.White;

			f.Controls.Add(m);
			f.ClientSize = m.Size;

			f.GetHTMLTarget().AttachToDocument();
		}



		static MatrixTransformBExample()
		{
			typeof(MatrixTransformBExample).SpawnTo(i => new MatrixTransformBExample());
		}

	}

}
