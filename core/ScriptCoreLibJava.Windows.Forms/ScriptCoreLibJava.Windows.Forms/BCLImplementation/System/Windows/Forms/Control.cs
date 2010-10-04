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
        public int Bottom
        {
            get
            {
                return this.Size.Height + this.Location.Y;
            }
        }

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

            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
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



        public void Show()
        {
            this.Visible = true;
        }

        public void Hide()
        {
            this.Visible = false;
        }


        public bool Visible
        {
            get
            {
                return this.InternalGetElement().isVisible();
            }

            set
            {
                this.InternalGetElement().setVisible(value);
            }
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

        Size InternalSize = new Size();
        public Size Size
        {
            get
            {
                return InternalSize;
            }
            set
            {
                InternalSize = value;

                var Width = value.Width;
                var Height = value.Height;


                UpdateBounds(Width, Height);
            }
        }


        public int Left
        {
            get
            {
                return this.Location.X;
            }
            set
            {
                this.Location = new Point(value, this.Top);
            }
        }

        public int Top
        {
            get
            {
                return this.Location.Y;
            }
            set
            {
                this.Location = new Point(this.Left, value);
            }
        }

        public int Width
        {
            get
            {
                return this.Size.Width;
            }
            set
            {
                this.Size = new Size(value, this.Height);
            }
        }

        public int Height
        {
            get
            {
                return this.Size.Height;
            }
            set
            {
                this.Size = new Size(this.Width, value);
            }
        }

        int old_width;
        int old_height;

        private void UpdateBounds(int width, int height)
        {

            this.InternalGetElement().setSize(width, height);


            this.OnSizeChanged(null);

            if (InternalLayoutSuspended)
            {
            }
            else
            {
                var x = width - old_width;
                var y = height - old_height;

                for (int i = 0; i < this.Controls.Count; i++)
                {
                    var item = this.Controls[i];


                    //Console.WriteLine(
                    //    "UpdateBounds " + x + " " + y
                    //);

                    InternalChildrenAnchorUpdate(
                        x,
                        y,
                        item
                    );
                }
            }

            old_width = width;
            old_height = height;
        }

        private void InternalChildrenAnchorUpdate(int x, int y, Control c)
        {

            var IsRight = (c.Anchor & AnchorStyles.Right) == AnchorStyles.Right;
            var IsLeft = (c.Anchor & AnchorStyles.Left) == AnchorStyles.Left;
            var IsBottom = (c.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom;
            var IsTop = (c.Anchor & AnchorStyles.Top) == AnchorStyles.Top;



            if (IsRight)
            {
                if (IsLeft)
                {
                    c.Width += x;
                }
                else
                {
                    c.Left += x;
                }
            }
            else
            {
                if (IsLeft)
                {
                }
                else
                {
                    c.Left += x / 2;

                }
            }

            if (IsBottom)
            {
                if (IsTop)
                {
                    c.Height += y;
                }
                else
                {
                    c.Top += y;
                }
            }
            else
            {
                if (IsTop)
                {
                }
                else
                {
                    c.Top += y / 2;

                }
            }
        }

        bool InternalLayoutSuspended;

        public void SuspendLayout()
        {
            InternalLayoutSuspended = true;
        }

        public void ResumeLayout(bool performLayout)
        {
            InternalLayoutSuspended = false;
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

        public event EventHandler Resize;


        protected virtual void OnResize(EventArgs e)
        {
            if (Resize != null)
                Resize(this, null);
        }

        public event EventHandler SizeChanged;

        protected virtual void OnSizeChanged(EventArgs e)
        {
            this.OnResize(null);

            if (SizeChanged != null)
                SizeChanged(this, null);

        }

        public int TabIndex { get; set; }

        public bool UseVisualStyleBackColor { get; set; }

        public virtual bool AutoSize { get; set; }

        public virtual AnchorStyles Anchor { get; set; }

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
