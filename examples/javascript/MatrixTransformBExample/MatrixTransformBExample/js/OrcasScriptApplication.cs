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

			var ro_matrix = new IHTMLDiv();

			ro_matrix.style.backgroundColor = "#8080ff";
			ro_matrix.style.SetLocation(x, y);
			ro_matrix.AttachToDocument();
			//ro_matrix.style.paddingLeft = w + "px";
			//ro_matrix.style.paddingTop = h + "px";
			var ro_matric_content = new IHTMLDiv();

			ro_matric_content.style.backgroundColor = "#0000ff";
			ro_matric_content.style.SetSize(w, h);
			ro_matric_content.AttachTo(ro_matrix);


			var r_matrix = new IHTMLDiv();

			r_matrix.style.backgroundColor = "#80ff80";
			r_matrix.style.SetLocation(x, y);
			r_matrix.AttachToDocument();

			//r_matrix.style.paddingLeft = w + "px";
			//r_matrix.style.paddingTop = h + "px";
			var r_matric_content = new IHTMLDiv();

			r_matric_content.style.backgroundColor = "#00ff00";
			r_matric_content.style.SetSize(w, h);
			r_matric_content.AttachTo(r_matrix);


			#region blue rotation
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
			#endregion

			#region black origin
			var zo = new IHTMLDiv();

			zo.style.background = "black";
			zo.style.SetLocation(x - 4, y - 1, 8, 2);

			zo.AttachToDocument();

			var zoh = new IHTMLDiv();

			zoh.style.background = "black";
			zoh.style.SetLocation(x - 1, y - 4, 2, 8);

			zoh.AttachToDocument();
			#endregion




			var ro = new IHTMLDiv();

			ro.style.background = "red";
			ro.style.SetLocation(x, y, w, h);
			ro.style.Opacity = 0.3;
			ro.AttachToDocument();



			var info = new IHTMLSpan { innerText = "MatrixTransform" };


			info.style.SetLocation(x, y + h, w, h);

			info.AttachToDocument();

			var at = new IHTMLDiv();

			at.style.background = "yellow";
			at.style.SetLocation(x - w / 2, y - h / 2, w * 2, h * 2);
			at.style.Opacity = 0.5;
			at.AttachToDocument();



			var r = new IHTMLDiv();

			r.style.background = "black";
			r.style.SetLocation(x, y, w, h);
			r.style.Opacity = 0.3;

			r.AttachToDocument();
			r.style.cursor = IStyle.CursorEnum.pointer;

			#region blue origin
			var o = new IHTMLDiv();

			o.style.background = "blue";
			o.style.SetLocation(x + w / 2 - 4, y + h / 2 - 1, 8, 2);

			o.AttachToDocument();

			var oh = new IHTMLDiv();

			oh.style.background = "blue";
			oh.style.SetLocation(x + w / 2 - 1, y + h / 2 - 4, 2, 8);

			oh.AttachToDocument();
			#endregion



			#region black rotation
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
			#endregion


			var m = new MatrixModifiers();

			var InteractiveSetOrigin_x = 0;
			var InteractiveSetOrigin_y = 0;

			var InteractiveSetRotation_x = 0;
			var InteractiveSetRotation_y = 0;


			Action<int, int> InteractiveSetRotation = null;

			Action<int, int> SetPadding =
				(ox, oy) =>
				{
					// -0 ... -w

					ox += w / 2;
					oy += h / 2;

					info.innerText = "padding x: " + ox + " y:" + oy;

					if (ox > 0)
					{
						ro_matrix.style.paddingLeft = ox * 2 + "px";
						ro_matrix.style.paddingRight = 0 + "px";
					}
					else
					{
						ro_matrix.style.paddingLeft = 0 + "px";
						ro_matrix.style.paddingRight = ox * -2 + "px";
					}

					if (oy > 0)
					{
						ro_matrix.style.paddingTop = oy * 2 + "px";
						ro_matrix.style.paddingBottom = 0 + "px";
					}
					else
					{
						ro_matrix.style.paddingTop = 0 + "px";
						ro_matrix.style.paddingBottom = oy * -2 + "px";
					}



					if (ox > 0)
					{
						r_matrix.style.paddingLeft = ox * 2 + "px";
						r_matrix.style.paddingRight = 0 + "px";
					}
					else
					{
						r_matrix.style.paddingLeft = 0 + "px";
						r_matrix.style.paddingRight = ox * -2 + "px";
					}

					if (oy > 0)
					{
						r_matrix.style.paddingTop = oy * 2 + "px";
						r_matrix.style.paddingBottom = 0 + "px";
					}
					else
					{
						r_matrix.style.paddingTop = 0 + "px";
						r_matrix.style.paddingBottom = oy * -2 + "px";
					}
				};

			m.Visual1.CheckedChanged +=
				delegate
				{
					r_matrix.Show(m.Visual1.Checked);
				};

			m.Visual2.CheckedChanged +=
				delegate
				{
					ro_matrix.Show(m.Visual2.Checked);
				};

			m.Debug1.CheckedChanged +=
				delegate
				{
					if (m.Debug1.Checked)
					{
						ro_matrix.style.backgroundColor = "#8080ff";
						r_matrix.style.backgroundColor = "#80ff80";

						return;
					}

					ro_matrix.style.backgroundColor = "";
					r_matrix.style.backgroundColor = "";
				};

			Action<int, int> InteractiveSetOrigin =
				(ox, oy) =>
				{
					var dx = InteractiveSetOrigin_x - ox;
					var dy = InteractiveSetOrigin_y - oy;

					InteractiveSetOrigin_x = ox;
					InteractiveSetOrigin_y = oy;

					SetPadding(ox, oy);

					m.TranslateX.Text = "" + ox;
					m.TranslateY.Text = "" + oy;

					ro.style.SetLocation(x + ox, y + oy, w, h);
					ro_matrix.style.SetLocation(x + ox, y + oy /*, w, h*/);
					o.style.SetLocation(x - ox - 4, y - oy - 1, 8, 2);
					oh.style.SetLocation(x - ox - 1, y - oy - 4, 2, 8);

					InteractiveSetRotation(InteractiveSetRotation_x + dx, InteractiveSetRotation_y + dy);
				};

			InteractiveSetRotation =
				(ox, oy) =>
				{
					InteractiveSetRotation_x = ox;
					InteractiveSetRotation_y = oy;

					var ax = x - w / 2 + ox;
					var ay = y - h / 2 + oy;

					var bx = x - InteractiveSetOrigin_x;
					var by = y - InteractiveSetOrigin_y;

					var dx = ax - bx;
					var dy = ay - by;

					var rotation = Extensions.GetRotation(dx, dy);
					var rotation_degrees = rotation.RadiansToDegrees();

					var costheta = Math.Cos(rotation);
					var sintheta = Math.Sin(rotation);

					var M11 = costheta;
					var M12 = -sintheta;
					var M21 = sintheta;
					var M22 = costheta;

					m.M11.Text = "" + M11;
					m.M12.Text = "" + M12;
					m.M21.Text = "" + M21;
					m.M22.Text = "" + M22;

					info.innerText = "rotation: " + rotation_degrees + "° x: " + InteractiveSetOrigin_x + " y:" + InteractiveSetOrigin_y;
					//Native.Document.title = new { ax, bx, dx, rotation_degrees }.ToString();

					joh.style.SetLocation(ax - 1, y - h / 2 + oy - 4, 2, 8);
					jo.style.SetLocation(x - w / 2 + ox - 4, ay - 1, 8, 2);

					jzoh.style.SetLocation(x + InteractiveSetOrigin_x - w / 2 + ox - 1, y + InteractiveSetOrigin_y - h / 2 + oy - 4, 2, 8);
					jzo.style.SetLocation(x + InteractiveSetOrigin_x - w / 2 + ox - 4, y + InteractiveSetOrigin_y - h / 2 + oy - 1, 8, 2);

					var mm = new[]
					{
						M11, M21,
						M12, M22,
						
						0, 0
						//0.838670551776886,0.5446390509605408,-0.5446390509605408,0.838670551776886,0,0
					};

					var code = @"
			q.style.filter = ""progid:DXImageTransform.Microsoft.Matrix(M11='"" + m[0] + ""',M12='"" + m[2] + ""',M21='"" + m[1] + ""', M22='"" + m[3] + ""', sizingmethod='auto expand');"";
	
			q.style.MozTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
			
			q.style.WebkitTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
				";

					new IFunction("q", "m", code).apply(null, r_matrix, mm);
					new IFunction("q", "m", code).apply(null, ro_matrix, mm);


					var r_matrix_adj_x = (r_matrix.clientWidth - r_matrix.offsetWidth) / 2;
					var r_matrix_adj_y = (r_matrix.clientHeight - r_matrix.offsetHeight) / 2;

					var ro_matrix_adj_x = (ro_matrix.clientWidth - ro_matrix.offsetWidth) / 2;
					var ro_matrix_adj_y = (ro_matrix.clientHeight - ro_matrix.offsetHeight) / 2;



					r_matrix.style.SetLocation(x + r_matrix_adj_x, y + r_matrix_adj_y/*, w, h*/);
					ro_matrix.style.SetLocation(x + InteractiveSetOrigin_x + ro_matrix_adj_x, y + InteractiveSetOrigin_y + ro_matrix_adj_y/*, w, h*/);

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
					if (m.Debug1.Checked)
					{
						info.innerText = "Click to set padding";
						return;
					}
					info.innerText = "Click to set origin";
				};


			#endregion

			//InteractiveSetOrigin(0, 0);
			InteractiveSetOrigin(-w / 2, -h / 2);

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
