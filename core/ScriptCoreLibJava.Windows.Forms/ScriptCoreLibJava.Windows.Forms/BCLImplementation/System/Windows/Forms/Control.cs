﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;
using ScriptCoreLib;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.Control))]
	internal partial class __Control : __Component,  __IDropTarget, __ISynchronizeInvoke, __IWin32Window, __IBindableComponent, __IComponent, IDisposable
	{
		public event EventHandler Click;

		public void RaiseClick()
		{
			if (Click != null)
				Click(this, new EventArgs());
		}


		public virtual java.awt.Component InternalGetElement()
		{
			throw new NotImplementedException();
		}

		public __Control()
		{
			this.Controls = new Control.ControlCollection(this);
		}

		public virtual string Text { get; set; }

		public virtual void InternalSetVisible(bool e)
		{
			throw new NotImplementedException();
		}

		public bool Visible
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				InternalSetVisible(value);
			}
		}

		public void Show()
		{
			Visible = true;
		}


		public Control.ControlCollection Controls { get; set; }


		static public implicit operator Control(__Control e)
		{
			return (Control)(object)e;
		}

		static public implicit operator __Control(Control e)
		{
			return (__Control)(object)e;
		}


		public Point Location
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				this.InternalGetElement().setLocation(value.X, value.Y);
			}
		}

		public Size Size
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				this.InternalGetElement().setSize(value.Width, value.Height);
			}
		}

		public void SuspendLayout()
		{

		}

		public void ResumeLayout(bool performLayout)
		{

		}

		public void PerformLayout()
		{
		}

		public string Name { get; set; }

		public Size ClientSize
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				this.Size = new Size(value.Width + 12, value.Height + 32);
			}
		}

		public int TabIndex { get; set; }

		public bool UseVisualStyleBackColor { get; set; }

		public virtual bool AutoSize { get; set; }
	}
}
