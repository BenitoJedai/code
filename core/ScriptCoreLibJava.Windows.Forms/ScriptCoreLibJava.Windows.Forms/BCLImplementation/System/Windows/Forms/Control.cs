using System;
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
	internal partial class __Control : __Component, __IDropTarget, __ISynchronizeInvoke, __IWin32Window, __IBindableComponent, __IComponent, IDisposable
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

		public virtual bool InternalGetEnabled()
		{
			return this.InternalGetElement().isEnabled();
		}

		public virtual void InternalSetEnabled(bool value)
		{
			this.InternalGetElement().setEnabled(value);
		}

		public bool Enabled
		{
			get
			{
				return InternalGetEnabled();
			}

			set
			{
				InternalSetEnabled(value);
			}
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

		Point InternalLocation = new Point();

		public Point Location
		{
			get
			{
				return InternalLocation;

			}
			set
			{
				InternalLocation = value;

				this.InternalGetElement().setLocation(value.X, value.Y);
			}
		}

		Size InternalSize;
		public Size Size
		{
			get
			{
				return InternalSize;
			}
			set
			{
				InternalSize = value;

				this.InternalGetElement().setSize(value.Width, value.Height);

				if (this.SizeChanged != null)
					this.SizeChanged(this, new EventArgs());

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

		public event EventHandler SizeChanged;

		public int TabIndex { get; set; }

		public bool UseVisualStyleBackColor { get; set; }

		public virtual bool AutoSize { get; set; }

		Color InternalForeColor;

		public virtual Color ForeColor
		{
			get
			{
				return InternalForeColor;
			}
			set
			{
				InternalForeColor = value;

				int R = value.R;
				int G = value.G;
				int B = value.B;

				var c = new java.awt.Color(R, G, B);

				this.InternalGetElement().setForeground(c);
			}
		}


		Color InternalBackColor;

		public virtual Color BackColor
		{
			get
			{
				return InternalBackColor;
			}
			set
			{
				InternalBackColor = value;

				int R = value.R;
				int G = value.G;
				int B = value.B;

				var c = new java.awt.Color(R, G, B);

				this.InternalGetElement().setBackground(c);
			}
		}
		
	}
}
