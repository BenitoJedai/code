using System;
using System.Collections.Generic;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.RayCaster;
using System.Collections.Specialized;
using ScriptCoreLib.ActionScript.flash.utils;




namespace RayCaster6.ActionScript
{


	partial class RayCaster6
	{
		void AttachMovementInput(ViewEngineBase view)
		{
			var stage = this.stage;

			if (stage == null)
				throw new Exception("stage is null");


			var snapcontainer = new Shape().AttachTo(this);
			var vectorized = new Shape().AttachTo(this);
			var delta = new Shape { alpha = 0.5 }.AttachTo(this);




			var mouseDown_args = default(MouseEvent);
			var mouseUp_fadeOut = default(Timer);

			uint color = 0;

			var snap_radius = 64;

			stage.mouseDown +=
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

						//Write("down ", new { e.localX, e.localY, e.buttonDown });
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

			stage.mouseMove +=
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
					};

			stage.mouseUp +=
					e =>
					{
						if (mouseUp_fadeOut != null)
							mouseUp_fadeOut.stop();

						var _vectorized = vectorized;
						var _snapcontainer = snapcontainer;

						mouseUp_fadeOut = 50.AtInterval(
								t =>
								{
									if (vectorized.alpha < 0)
									{
										t.stop();
										return;
									}

									_vectorized.alpha -= 0.02;
									_snapcontainer.alpha -= 0.04;
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
						delta_acc += delta_acc_acc;
					}
					else
					{
						delta_acc -= delta_acc_acc * 3;
						if (delta_acc < delta_acc_min)
							delta_acc = delta_acc_min;


						delta_pos -= delta_acc;
					}

					delta_pos = delta_pos.Min(1).Max(0);

					var u = (mouseMove_args.ToStagePoint() - mouseDown_args.ToStagePoint()) * delta_pos;
					var z = mouseDown_args.ToStagePoint() + u;

					var Q1 = mouseDown_args.stageY < stage.height * 1 / 6;
					var Q4 = mouseDown_args.stageY > stage.height * 5 / 6;
					var IsPan = Q1 || Q4;


					if (delta_pos > 0)
						if (!IsPan)
						{

							view.ViewDirection += u.x * 0.0004;

							view.ViewPosition = view.ViewPosition.MoveToArc(view.ViewDirection, -u.y.Max(-snap_radius * 2).Min(snap_radius * 2) * 0.001);
						}
						else
						{
							view.ViewPosition = view.ViewPosition.MoveToArc(u.GetRotation() + view.ViewDirection + 270.DegreesToRadians(), -(u.length.Min(snap_radius * 2)) * 0.001);
						}

					DrawArrow(delta, z.x, z.y, 0xff00);
				}
			);
		}
	}
}