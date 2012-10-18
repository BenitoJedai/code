﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.FrameworkElement))]
    internal class __FrameworkElement : __UIElement, __ISupportInitialize
	{
		public string Name
		{
			set
			{
				this.InternalGetDisplayObjectDirect().name = value;
			}
		}

		public virtual void InternalSetWidth(double value)
		{
			throw new NotImplementedException();
		}

		public virtual void InternalSetHeight(double value)
		{
			throw new NotImplementedException();
		}

        public double ActualHeight { get { return Height; } }
        public double ActualWidth { get { return Width; } }

		public double Width
		{
			get
			{
				return InternalGetWidth();

			}
			set
			{
				InternalSetWidth(value);
                InternalRaiseSizeChanged();
            }
		}

		public double Height
		{
			get
			{
				return InternalGetHeight();

			}
			set
			{
				InternalSetHeight(value);
                InternalRaiseSizeChanged();
            }
		}


        #region SizeChanged
        public event SizeChangedEventHandler SizeChanged;

        Size InternalPreviousSize;

        bool InternalRaiseSizeChangedReentryGuard;

        private void InternalRaiseSizeChanged()
        {
            if (InternalRaiseSizeChangedReentryGuard)
                return;

            this.InternalUpdateClip();

            if (SizeChanged == null)
                return;

            InternalRaiseSizeChangedReentryGuard = true;
            var NewSize = new Size(this.Width, this.Height);

            SizeChanged(this,
                (SizeChangedEventArgs)(object)new __SizeChangedEventArgs
                {
                    NewSize = NewSize,
                    PreviousSize = InternalPreviousSize
                }
            );

            InternalPreviousSize = NewSize;
            InternalRaiseSizeChangedReentryGuard = false;
        }
        #endregion

		public Cursor InternalCursorValue;

		[Script(IsNative = true)]
		internal class InternalStyleCursorMixin
		{
			public string cursor;
		}

		public  void InternalSetCursor(Cursor value)
		{
			InternalCursorValue = value;


			var s = ((InternalStyleCursorMixin)(object)InternalGetDisplayObjectDirect().style);

			if (InternalCursorValue == Cursors.None)
				s.cursor = "url('assets/ScriptCoreLib.Avalon/transparent.cur'), auto";

			// http://www.w3schools.com/CSS/pr_class_cursor.asp

			if (InternalCursorValue == Cursors.Arrow)
				s.cursor = "auto";

			if (InternalCursorValue == Cursors.Hand)
				s.cursor = "pointer";
		}

		public Cursor Cursor
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				InternalSetCursor(value);
			}
		}

		public DependencyObject InternalParent;

		public DependencyObject Parent { get { return this.InternalParent; } }

		public static implicit operator global::System.Windows.FrameworkElement(__FrameworkElement e)
		{
			return (global::System.Windows.FrameworkElement)(object)e;
		}

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}
