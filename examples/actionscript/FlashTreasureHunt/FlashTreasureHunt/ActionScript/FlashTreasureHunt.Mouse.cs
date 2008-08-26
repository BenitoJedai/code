using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	partial class FlashTreasureHunt
	{
		[Script]
		class ManualControl
		{
			public Action<Point> down;
			public Action<Point> move;
			public Action up;

			public double delta_acc_min;
			public double delta_acc;
			public double delta_acc_acc;

			public double delta_deacc_min;
			public double delta_deacc;

			public ManualControl()
			{
				delta_acc_min = 0.02;
				delta_acc = delta_acc_min;
				delta_acc_acc = delta_acc_min * 0.01;

				delta_deacc_min = 0.03;
				delta_deacc = delta_deacc_min;

			}

		}

		public bool MovementEnabled_IsInGame = true;
		public bool MovementEnabled_IsFocused = true;
		//public bool MovementEnabled_IsAlive = true;
		public bool MovementEnabled
		{
			get
			{
				if (!MovementEnabled_IsInGame)
					return false;

				if (!MovementEnabled_IsFocused)
					return false;

				//if (!MovementEnabled_IsAlive)
				//    return false;

				return true;
			}
		}

		ManualControl AttachMovementInput(ViewEngineBase view, bool EnableMouse, bool Visualize)
		{
			var mc = new ManualControl();

			var stage = this.stage;

			if (stage == null)
				throw new Exception("stage is null");


			var snapcontainer = new Shape();

			var vectorized = new Shape();
			var delta = new Shape { alpha = 0.5 };

			if (Visualize)
			{
				snapcontainer.AttachTo(this);
				vectorized.AttachTo(this);
				delta.AttachTo(this);
			}


			var mouseDown_args = default(Point);
			var mouseUp_fadeOut = default(Timer);

			uint color = 0;

			var snap_radius = 64;

			mc.down =
				p =>
				{
					if (!MovementEnabled)
						return;

					color = 0;


					// snap to old point
					if (mouseDown_args != null)
						if (snapcontainer.alpha > 0)
							if ((mouseDown_args - p).length < snap_radius)
							{
								color = 0xff;

								p = mouseDown_args;
							}

					mouseDown_args = p;
				};

			if (EnableMouse)
				stage.mouseDown +=
						e =>
						{
							mc.down(e.ToStagePoint());

							//Write("down ", new { e.localX, e.localY, e.buttonDown });
						};

			Action<Shape, double, double, uint> DrawArrow =
					(s, x, y, c) =>
					{


						s.graphics.lineStyle(2, c, 1);
						s.graphics.moveTo(mouseDown_args.x, mouseDown_args.y);
						s.graphics.lineTo(x, y);
						s.graphics.drawCircle(x, y, 4);
					};

			var mouseMove_args = default(Point);
			var delta_pos = 0.0;

			mc.move =
				p =>
				{
					if (!MovementEnabled)
						return;
					
					if (mouseDown_args == null)
						return;

					mouseMove_args = p;

					if (mouseUp_fadeOut != null)
						mouseUp_fadeOut.stop();

					vectorized.alpha = 1;
					vectorized.graphics.clear();

					snapcontainer.alpha = 1;
					snapcontainer.graphics.clear();


					snapcontainer.graphics.lineStyle(2, 0xff, 1);
					snapcontainer.graphics.drawCircle(mouseDown_args.x, mouseDown_args.y, snap_radius);

					DrawArrow(vectorized, mouseMove_args.x, mouseMove_args.y, color);
				};

			if (EnableMouse)
				stage.mouseMove +=
					e =>
					{
						if (e.buttonDown)
						{
							mc.move(e.ToStagePoint());
						}
					};

			mc.up +=
				delegate
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

			if (EnableMouse)
				stage.mouseUp +=
					delegate
					{
						mc.up();
					};


	
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
						delta_pos += mc.delta_acc;
						mc.delta_acc += mc.delta_acc_acc;
					}
					else
					{
						mc.delta_acc -= mc.delta_acc_acc * 3;
						if (mc.delta_acc < mc.delta_acc_min)
							mc.delta_acc = mc.delta_acc_min;


						delta_pos -= mc.delta_acc;
					}

					delta_pos = delta_pos.Min(1).Max(0);

					var u = (mouseMove_args - mouseDown_args) * delta_pos;
					var z = mouseDown_args + u;

					var Q1 = mouseDown_args.y < DefaultControlHeight * 1 / 6;
					var Q4 = mouseDown_args.y > DefaultControlHeight * 5 / 6;
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

			return mc;
		}

	}
}