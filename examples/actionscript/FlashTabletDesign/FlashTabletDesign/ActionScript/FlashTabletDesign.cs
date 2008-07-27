﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript;

namespace FlashTabletDesign.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	public class FlashTabletDesign : Sprite
	{

		[Embed("/assets/FlashTabletDesign/gtataxi.png")]
		Class gtataxi;

		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashTabletDesign()
		{


			var info = new TextField
			{
				selectable = false,
				multiline = true,
				width = stage.stageWidth,
				height = stage.stageHeight
			}.AttachTo(this);

			var snapcontainer = this.Attach(new Shape());
			var vectorized = this.Attach(new Shape());
			var delta = new Shape { alpha = 0.5 }.AttachTo(this);

			var ego = new Sprite { mouseEnabled = false, x = stage.stageWidth / 2, y = stage.stageHeight / 2 }.AttachTo(this);
			var ego_img = gtataxi.ToBitmapAsset().AttachTo(ego);

			ego_img.x = -ego_img.width / 2;
			ego_img.y = -ego_img.height / 2;

			Action<string, object> Write = (p, e) =>
					{
						info.appendText(p + e.ToString() + Environment.NewLine);
						info.setSelection(info.text.Length - 1, info.text.Length - 1);
					};

			this.graphics.beginFill(0xefff80);
			this.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight / 2);

			this.graphics.beginFill(0xef8fff);
			this.graphics.drawRect(0, stage.stageHeight / 2, stage.stageWidth, stage.stageHeight / 2);

			var mouseDown_args = default(MouseEvent);
			var mouseUp_fadeOut = default(Timer);

			uint color = 0;

			var snap_radius = 64;

			this.mouseDown +=
					e =>
					{
						color = 0;

						// snap to old point
						if (mouseDown_args != null)
							if (snapcontainer.alpha > 0)
								if ((mouseDown_args.ToStagePoint() - e.ToStagePoint()).length < snap_radius)
								{
									color = 0xff;

									e = mouseDown_args;
								}

						mouseDown_args = e;

						Write("down ", new { e.localX, e.localY, e.buttonDown });
					};

			Action<Shape, double, double, uint> DrawArrow =
					(s, x, y, c) =>
					{


						s.graphics.lineStyle(2, c, 1);
						s.graphics.moveTo(mouseDown_args.stageX, mouseDown_args.stageY);
						s.graphics.lineTo(x, y);
						s.graphics.drawCircle(x, y, 4);
					};

			var mouseMove_args = default(MouseEvent);
			var delta_pos = 0.0;

			this.mouseMove +=
					e =>
					{
						if (e.buttonDown)
						{
							mouseMove_args = e;

							if (mouseUp_fadeOut != null)
								mouseUp_fadeOut.stop();

							vectorized.alpha = 1;
							vectorized.graphics.clear();

							snapcontainer.alpha = 1;
							snapcontainer.graphics.clear();


							snapcontainer.graphics.lineStyle(2, 0xff, 1);
							snapcontainer.graphics.drawCircle(mouseDown_args.stageX, mouseDown_args.stageY, snap_radius);

							DrawArrow(vectorized, e.stageX, e.stageY, color);
						}
						else
						{
							if (delta_pos == 0)
							Write("move ", new { e.stageY, stage.stageHeight, stageScaleY = stage.scaleY, this.scaleY, stage.height });
							//Write("move ", new { e.stageX, e.stageY, stage.stageWidth, stage.stageHeight, stage.scaleX, stage.scaleY });
						}
					};

			this.mouseUp +=
					e =>
					{
						Write("up ", new { e.localX, e.localY, e.buttonDown });

						if (mouseUp_fadeOut != null)
							mouseUp_fadeOut.stop();


						mouseUp_fadeOut = 50.AtInterval(
								t =>
								{
									vectorized.alpha -= 0.02;

									snapcontainer.alpha -= 0.04;
								}
						);
					};


			var delta_acc_min = 0.02;
			var delta_acc = delta_acc_min;
			var delta_acc_acc = delta_acc_min * 0.01;

			var delta_deacc_min = 0.03;
			var delta_deacc = delta_deacc_min;

			(1000 / 24).AtInterval(
				t =>
				{
					if (mouseDown_args == null)
						return;

					if (mouseMove_args == null)
						return;

					delta.graphics.clear();

					if (vectorized.alpha == 1)
					{
						delta_pos += delta_acc;
						//delta_deacc = (delta_deacc - delta_deacc_acc).Max(delta_deacc_min);
						delta_acc += delta_acc_acc;
					}
					else
					{
						delta_pos -= delta_deacc;
						delta_acc -= delta_acc_acc;

						if (delta_acc < delta_acc_min)
							delta_acc = delta_acc_min;

						//delta_deacc += delta_deacc_acc;
					}

					delta_pos = delta_pos.Min(1).Max(0);

					var u = (mouseMove_args.ToStagePoint() - mouseDown_args.ToStagePoint()) * delta_pos;
					var z = mouseDown_args.ToStagePoint() + u;

					if (delta_pos > 0)
						if (mouseDown_args.stageY > stage.height / 2)
						{
							// boolean
							Write("rot ", new { delta_pos, u.x, u.y });

							ego.rotation += u.x * 0.2;

							var p = ego.ToPoint().MoveToArc(((int)ego.rotation).DegreesToRadians(), -u.y * 0.2);

							ego.x = p.x;
							ego.y = p.y;
						}
						else
						{
							Write("pan ", new { delta_pos, u.x, u.y });

							var p = ego.ToPoint().MoveToArc(u.GetRotation() + ((int)ego.rotation + 270).DegreesToRadians(), -u.length * 0.2);

							ego.x = p.x;
							ego.y = p.y;
						}

					DrawArrow(delta, z.x, z.y, 0xff00);
				}
			);
		}
	}
}