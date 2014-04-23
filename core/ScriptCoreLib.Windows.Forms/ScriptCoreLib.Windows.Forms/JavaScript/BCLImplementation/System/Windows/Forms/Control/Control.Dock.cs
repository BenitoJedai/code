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

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    using DOMHandler = global::System.Action<DOM.IEvent>;
    using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
    using ScriptCoreLib.JavaScript.DOM;

    public partial class __Control : __Component
    {

        public DockStyle InternalDock;
        public virtual DockStyle Dock
        {
            get { return InternalDock; }
            set
            {
                InternalDock = value;

                //Console.WriteLine(new { InternalDock });

                // X:\jsc.svn\examples\javascript\forms\HashForBindingSource\HashForBindingSource\ApplicationControl.cs

                InternalChildrenAnchorUpdate();
            }
        }

        //public void InternalChildrenAnchorUpdate(int width, int height, int dx, int dy, Control c)

        int InternalChildrenAnchorUpdate_width;
        int InternalChildrenAnchorUpdate_height;




        public IEnumerable<__Control> InternalSiblings
        {
            get
            {
                return from p in new[] { this.Parent }
                       where p != null
                       from i in Enumerable.Range(0, p.Controls.Count)
                       let cc = (__Control)this.Parent.Controls[i]
                       where cc != this
                       select cc;

            }
        }



        public void InternalChildrenAnchorUpdate()
        {
            InternalChildrenAnchorUpdate(
                clientWidth,
                clientHeight,
                0,
                0,

                this

                );
        }

        // is c always eq this? 
        public void InternalChildrenAnchorUpdate(int width, int height, int dx, int dy, __Control c)
        {
            __Control __c = c;

            // this looks wrong!
            dx = width - InternalChildrenAnchorUpdate_width;
            dy = height - InternalChildrenAnchorUpdate_height;

            InternalChildrenAnchorUpdate_width = width;
            InternalChildrenAnchorUpdate_height = height;

            var Siblings = c.InternalSiblings;
            var SiblingsCount = Siblings.Count();


            //Console.WriteLine(
            //    "InternalChildrenAnchorUpdate "
            //    +
            //      new { c.Name, c.Dock, SiblingsCount }
            //      );


            __c.outer_style.position = IStyle.PositionEnum.absolute;

            if (c.Dock == DockStyle.Top)
            {
                __c.outer_style.width = "";

                __c.outer_style.left = "0px";
                __c.outer_style.top = 0 + "px";
                __c.outer_style.right = "0px";

                return;
            }

            if (c.Dock == DockStyle.Bottom)
            {
                __c.outer_style.width = "";

                __c.outer_style.left = "0px";

                __c.outer_style.top = "";
                __c.outer_style.bottom = 0 + "px";

                __c.outer_style.right = "0px";

                // let any other siblings also know?

                //Console.WriteLine("Bottom shall notify Fill?");
                //foreach (var item in
                //    from cc in Siblings
                //    where cc.Dock == DockStyle.Fill
                //    select cc
                //    )
                //{
                //    Console.WriteLine(
                //        new { c.Name, c.Height } +
                //        "Bottom shall notify Fill? " + new { item.Name });
                //    item.InternalChildrenAnchorUpdate();
                //}
                //return;
            }

            #region Fill
            if (c.Dock == DockStyle.Fill)
            {
                //Console.WriteLine("InternalChildrenAnchorUpdate: DockStyle.Fill");
                //c.SetBounds(0, 0, width, height);


                __c.outer_style.left = "0px";

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140423
                // X:\jsc.svn\examples\javascript\forms\FormsNICWithDataSource\FormsNICWithDataSource\ApplicationControl.cs

                // 32:225ms { __Top = 0, __Bottom = 0 }

                var SiblingsTop = Siblings.Where(x => x.Dock == DockStyle.Top).ToArray();
                var SiblingsBottom = Siblings.Where(x => x.Dock == DockStyle.Bottom).ToArray();

                var __Bottom = Enumerable.Sum(
                    from cc in SiblingsBottom
                    select cc.Height
                    );


                var __Top = Enumerable.Sum(
                    from cc in SiblingsTop
                    select cc.Height
                    );



                //33:286ms { Name = nICDataGetInterfacesBindingSourceDataGridView, SiblingsTop = 1, __Top = 25, SiblingsBottom = 1, __Bottom = 25 }
                Console.WriteLine(
                    new
                    {
                        this.Name,
                        SiblingsTop = SiblingsTop.Length,
                        __Top,
                        SiblingsBottom = SiblingsBottom.Length,
                        __Bottom
                    }
                    );

                __c.outer_style.top = __Top + "px";

                //__c.HTMLTargetRef.setAttribute("hint-Dock", "DockStyle.Fill " + new { c.GetHTMLTarget().nodeName });
                // hint-dock="DockStyle.Fill { nodeName = IFRAME }"

                if (c.HTMLTargetRef.nodeName.ToLower() == "iframe")
                {
                    // iframes are special?
                    // X:\jsc.svn\examples\javascript\forms\MSVSFormStyle\MSVSFormStyle\ApplicationControl.cs

                    __c.outer_style.width = "100%";
                    __c.outer_style.height = "100%";
                }
                else
                {

                    __c.outer_style.width = "";
                    __c.outer_style.height = "";


                    __c.outer_style.right = "0px";
                    __c.outer_style.bottom = __Bottom + "px";
                }

                // X:\jsc.svn\examples\javascript\forms\FormsWithVisibleTitle\FormsWithVisibleTitle\Application.cs

                // do we have a test
                // to go back from fill, and what about events?

                return;
            }
            #endregion


            var IsRight = (c.Anchor & AnchorStyles.Right) == AnchorStyles.Right;
            var IsLeft = (c.Anchor & AnchorStyles.Left) == AnchorStyles.Left;
            var IsBottom = (c.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom;
            var IsTop = (c.Anchor & AnchorStyles.Top) == AnchorStyles.Top;




            if (IsRight)
            {
                if (IsLeft)
                {
                    c.Width += dx;
                }
                else
                {
                    c.Left += dx;
                }
            }
            else
            {
                if (IsLeft)
                {
                }
                else
                {
                    c.Left += dx / 2;

                }
            }

            if (IsBottom)
            {
                if (IsTop)
                {
                    c.Height += dy;
                }
                else
                {
                    c.Top += dy;
                }
            }
            else
            {
                if (IsTop)
                {
                }
                else
                {
                    c.Top += dy / 2;

                }
            }
        }


    }
}
