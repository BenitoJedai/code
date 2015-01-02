using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    using DOMHandler = global::System.Action<DOM.IEvent>;

    public partial class __Control : __Component
    {

        #region Size
        protected int width;
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.SetBounds(this.x, this.y, value, this.height, BoundsSpecified.Width);
            }
        }

        protected int height;
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.SetBounds(this.x, this.y, this.width, value, BoundsSpecified.Height);
            }
        }


        public Size Size
        {
            get
            {
                return new Size(this.width, this.height);
            }
            set
            {
                //Console.WriteLine("set_Size");
                this.SetBounds(this.x, this.y, value.Width, value.Height, BoundsSpecified.Size);
            }
        }
        #endregion



        public int Bottom
        {
            get
            {
                return this.y + this.height;
            }
            set
            {
                this.SetBounds(this.x, value - this.height, this.width, this.height, BoundsSpecified.Y);
            }
        }

        public int Right
        {
            get
            {
                return this.x + this.width;
            }
            set
            {
                this.SetBounds(value - this.width, this.y, this.width, this.height, BoundsSpecified.X);
            }
        }

        #region Location

        protected int x;
        protected int y;

        public int Left
        {
            get
            {
                return this.x;
            }
            set
            {
                this.SetBounds(value, this.y, this.width, this.height, BoundsSpecified.X);
            }
        }

        public int Top
        {
            get
            {
                return this.y;
            }
            set
            {
                this.SetBounds(this.x, value, this.width, this.height, BoundsSpecified.Y);
            }
        }

        public Point Location
        {
            get
            {
                return new Point(this.x, this.y);
            }
            set
            {
                this.SetBounds(value.X, value.Y, this.width, this.height, BoundsSpecified.Location);
            }
        }
        #endregion


        #region SetBounds
        public void SetBounds(int x, int y, int width, int height)
        {
            var _x = (this.x != x);
            var _y = (this.y != y);
            var _width = (this.width != width);
            var _height = this.height != height;

            var _xy = (_x || _y);
            var _wh = (_width || _height);


            // why bother?
            //if (_xy || _wh)
            {
                UpdateBounds(x, y, width, height);
                //this.SetBoundsCore(x, y, width, height, BoundsSpecified.All);
                //LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
            //else
            //{
            //    this.InitScaling(BoundsSpecified.All);
            //}
        }


        //int SetBoundsCounter = 0;

        public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
        {

            #region BoundsSpecified
            if ((specified & BoundsSpecified.X) == BoundsSpecified.None)
            {
                x = this.x;
            }
            if ((specified & BoundsSpecified.Y) == BoundsSpecified.None)
            {
                y = this.y;
            }
            if ((specified & BoundsSpecified.Width) == BoundsSpecified.None)
            {
                width = this.width;
            }
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.None)
            {
                height = this.height;
            }
            #endregion

            #region min max
            if (this.MinimumSize.Width > 0)
                width = Math.Max(this.MinimumSize.Width, width);

            if (this.MinimumSize.Height > 0)
                height = Math.Max(this.MinimumSize.Height, height);


            if (this.MaximumSize.Width > 0)
                width = Math.Min(this.MaximumSize.Width, width);

            if (this.MaximumSize.Height > 0)
                height = Math.Min(this.MaximumSize.Height, height);
            #endregion



            var _x = (this.x != x);
            var _y = (this.y != y);
            var _width = (this.width != width);
            var _height = this.height != height;

            var _xy = (_x || _y);
            var _wh = (_width || _height);
            var _any = _xy || _wh;

            // let first tmers pass?
            //_any |= SetBoundsCounter == 0;


            // why bother?
            //if (_any)
            {
                UpdateBounds(x, y, width, height);
                //this.SetBoundsCore(x, y, width, height, specified);
                //LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
            //else
            //{
            //    // this.InitScaling(specified);
            //}

            //SetBoundsCounter++;
        }


        protected virtual void UpdateBounds(int x, int y, int width, int height/*, int clientWidth, int clientHeight*/)
        {
            InternalUpdateBounds(x, y, width, height);
        }


        public virtual void InternalUpdateBoundsSetLocation(int x, int y)
        {
            this.HTMLTargetRef.style.SetLocation(x, y);
        }

        protected void InternalUpdateBounds(int x, int y, int width, int height/*, int clientWidth, int clientHeight*/)
        {

            // let's remember old size for anchoring..
            var old_width = this.width;
            var old_height = this.height;

            var _x = (this.x != x);
            var _y = (this.y != y);
            var _width = (this.width != width);
            var _height = this.height != height;

            bool flag = _x || _y;
            bool flag2 = _width || _height /*|| (this.clientWidth != clientWidth)) || (this.clientHeight != clientHeight)*/;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            this.clientWidth = width;
            this.clientHeight = height;

            // this Control is used but not shown.
            if (this.HTMLTargetRef == null)
                return;

            //if (flag)
            {
                if (this.Parent is FlowLayoutPanel)
                {
                    // X:\jsc.svn\examples\javascript\forms\Test\TestLinearFlow\TestLinearFlow\ApplicationControl.cs
                    // auto flow
                }
                else
                {
                    InternalUpdateBoundsSetLocation(x, y);
                }

                this.OnLocationChanged(null);
            }

            //Console.WriteLine("before InternalClientSizeChanged " + new { flag2 });
            //if (flag2)
            {
                //throw new Exception("Html element not set: " + this.Name);

                var this_as_Form = this as __Form;
                if ((this_as_Form != null) && this_as_Form.WindowState == FormWindowState.Maximized)
                {
                    // X:\jsc.svn\examples\javascript\forms\FormsWithVisibleTitle\FormsWithVisibleTitle\Application.cs
                    // skip it for now
                }
                else
                {
                    this.HTMLTargetRef.style.SetSize(width, height);
                }

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131005-chrome-frame-server
                // chrome app? may not have render frames
                Native.setTimeout(
                  delegate
                  {
                      InternalClientSizeChanged0();

                  },
                  1
                );
            }


        }

        public void InternalClientSizeChanged0()
        {
            //Console.WriteLine(new { this.Name } + " enter InternalClientSizeChanged");

            this.clientWidth = this.HTMLTargetContainerRef.clientWidth;
            this.clientHeight = this.HTMLTargetContainerRef.clientHeight;

            //Console.WriteLine("InternalClientSizeChanged " + new { @this = this, clientWidth, clientHeight });


            this.OnSizeChanged(null);
            this.OnClientSizeChanged(null);

            //CommonProperties.xClearPreferredSizeCache(this);
            //LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);

            if (InternalLayoutSuspended)
            {
            }
            else
            {
                InternalChildrenAnchorUpdate4(
                    clientWidth,
                    clientHeight,
                    dx: 0,
                    dy: 0
                );

            }
        }

        // who is calling this?
        public void InternalChildrenAnchorUpdate4(int width, int height, int dx, int dy)
        {
            // X:\jsc.svn\examples\javascript\Test\Test453For\Test453For\Class1.cs
            // X:\jsc.svn\examples\javascript\Test\Test453While\Test453While\Class1.cs
            // X:\jsc.svn\examples\javascript\forms\Test\Test453Forms\Test453Forms\Application.cs

            for (int i = 0; i < this.Controls.Count; i++)
            {
                var item = this.Controls[i];

                InternalChildrenAnchorUpdate(
                    width,
                    height,
                    dx,
                    dy,
                    item
                );
            }
        }
        #endregion


    }
}
