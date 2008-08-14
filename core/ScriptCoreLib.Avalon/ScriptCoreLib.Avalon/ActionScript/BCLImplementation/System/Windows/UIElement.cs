﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Animation;
using System.Windows;
using ScriptCoreLib.ActionScript.flash.display;
using System.Windows.Media.Effects;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Input;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{

	[Script(Implements = typeof(global::System.Windows.UIElement))]
	internal class __UIElement : __Visual, __IAnimatable, __IInputElement
	{
		#region __IInputElement Members

		public InteractiveObject InternalGetDisplayObjectDirect()
		{
			return InternalGetDisplayObject();
		}

		#endregion

		public virtual InteractiveObject InternalGetDisplayObject()
		{
			throw new NotImplementedException();
		}

		public BitmapEffect BitmapEffect
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				var _BitmapEffect = (__BitmapEffect)value;

				InternalGetDisplayObject().filters = new[] { _BitmapEffect.InternalGetBitmapFilter() };
			}
		}

		public static implicit operator __UIElement(UIElement e)
		{
			return (__UIElement)(object)e;
		}

		public double Opacity
		{
			get
			{
				return InternalGetDisplayObject().alpha;
			}
			set
			{
				InternalGetDisplayObject().alpha = value;
			}
		}

		public Transform RenderTransform
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				var AsScaleTransform = value as ScaleTransform;

				if (AsScaleTransform != null)
				{

					var o = InternalGetDisplayObject();

					o.scaleX = AsScaleTransform.ScaleX;
					o.scaleY = AsScaleTransform.ScaleY;

				}
			}
		}


		public event MouseEventHandler MouseMove
		{
			add
			{

				InternalGetDisplayObject().mouseMove +=
					e =>
					{
						
						var args = new __MouseEventArgs
						{
							Internal_stageX = e.stageX,
							Internal_stageY = e.stageY
						};

						value(this, args);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		
	}
}
