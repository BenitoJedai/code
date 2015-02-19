using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Windows.Forms;
using ScriptCoreLib.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Controls;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/SplitContainer.cs

    [Script(Implements = typeof(global::System.Windows.Forms.SplitContainer))]
    public class __SplitContainer : __ContainerControl, ISupportInitialize
    {
        // how many times have we implemented a split container?

        // http://www.webcomponentsshift.com/#40


        // X:\jsc.svn\examples\javascript\forms\FormsSplitter\FormsSplitter\ApplicationControl.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140504


        public IHTMLDiv InternalElement = typeof(SplitContainer);


        // why do we need to redefine dock?
        //public DockStyle Dock { get; set; }

        public SplitterPanel Panel1 { get; set; }

        public SplitterPanel Panel2 { get; set; }



        #region SplitterDistance
        Action InternalSplitterDistanceChanged;
        int InternalSplitterDistance;
        public int SplitterDistance
        {
            get
            {
                return InternalSplitterDistance;
            }
            set
            {
                InternalSplitterDistance = value;
                if (InternalSplitterDistanceChanged != null)
                    InternalSplitterDistanceChanged();

            }
        }
        #endregion




        // 8:66ms missing HTMLTargetRef for { e = <Namespace>.SplitContainer }


        public override DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }




        #region Orientation
        // X:\jsc.svn\examples\javascript\forms\ThreeWay\ThreeWay\ApplicationControl.Designer.cs
        Orientation InternalOrientation = Orientation.Vertical;
        public event Action InternalOrientationChanged;

        public Orientation Orientation
        {
            get { return InternalOrientation; }
            set
            {
                InternalOrientation = value;


                if (InternalOrientationChanged != null)
                    InternalOrientationChanged();

            }
        }
        #endregion

        public __SplitContainer()
        {
            //InternalElement.style.backgroundColor = "red";

            this.Panel1 = new SplitterPanel(this);
            this.Panel2 = new SplitterPanel(this);

            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel2);



            var TheSplitter = new IHTMLDiv { };

            TheSplitter.css.hover.style.backgroundColor = "blue";
            TheSplitter.css.active.style.backgroundColor = "blue";

            TheSplitter.style.cursor = DOM.IStyle.CursorEnum.move;


            TheSplitter.AttachTo(
                InternalElement
            );

            this.SplitterDistance = 200;



            var h = new DragHelper(TheSplitter);

            h.Enabled = true;
            //h.Position.X = this.SplitterDistance;

            h.DragMove +=
                delegate
                {
                    //this.SplitterDistance = h.Position.X;

                    if (this.InternalOrientation == global::System.Windows.Forms.Orientation.Horizontal)
                    {

                        TheSplitter.style.SetLocation(0, h.Position.Y);
                    }
                    else
                    {

                        TheSplitter.style.SetLocation(h.Position.X, 0);
                    }

                };

            // X:\jsc.svn\examples\javascript\forms\FormsSplitter\FormsSplitter\ApplicationControl.cs



            TheSplitter.style.position = DOM.IStyle.PositionEnum.absolute;


            Action AtUpdate = delegate
            {
                __Panel p1 = this.Panel1;
                __Panel p2 = this.Panel2;


                if (this.InternalOrientation == global::System.Windows.Forms.Orientation.Horizontal)
                {
                    h.Position.Y = this.SplitterDistance;




                    p1.InternalElement.style.position = DOM.IStyle.PositionEnum.absolute;
                    p1.InternalElement.style.left = "0px";
                    p1.InternalElement.style.top = "0px";
                    p1.InternalElement.style.width = "";
                    p1.InternalElement.style.bottom = "";
                    p1.InternalElement.style.right = "0px";
                    p1.InternalElement.style.height = (this.SplitterDistance - 4) + "px";



                    p2.InternalElement.style.position = DOM.IStyle.PositionEnum.absolute;
                    p2.InternalElement.style.left = "0px";
                    p2.InternalElement.style.top = (this.SplitterDistance + 4) + "px";
                    p2.InternalElement.style.width = "";
                    p2.InternalElement.style.bottom = "0px";
                    p2.InternalElement.style.right = "0px";
                    p2.InternalElement.style.height = "";




                    TheSplitter.style.left = "0px";
                    TheSplitter.style.top = (this.SplitterDistance - 4) + "px";
                    TheSplitter.style.right = "0px";
                    TheSplitter.style.height = "8px";


                    TheSplitter.style.width = "";
                    TheSplitter.style.bottom = "";
                }
                else
                {
                    h.Position.X = this.SplitterDistance;

                    // dock left?


                    p1.InternalElement.style.position = DOM.IStyle.PositionEnum.absolute;
                    p1.InternalElement.style.left = "0px";
                    p1.InternalElement.style.top = "0px";
                    p1.InternalElement.style.width = (this.SplitterDistance - 4) + "px";
                    p1.InternalElement.style.bottom = "0px";
                    p1.InternalElement.style.right = "";
                    p1.InternalElement.style.height = "";



                    p2.InternalElement.style.position = DOM.IStyle.PositionEnum.absolute;
                    p2.InternalElement.style.left = (this.SplitterDistance + 4) + "px";
                    p2.InternalElement.style.top = "0px";
                    p2.InternalElement.style.width = "";
                    p2.InternalElement.style.bottom = "0px";
                    p2.InternalElement.style.right = "0px";
                    p2.InternalElement.style.height = "";


                    TheSplitter.style.left = (this.SplitterDistance - 4) + "px";
                    TheSplitter.style.top = "0px";
                    TheSplitter.style.width = "8px";
                    TheSplitter.style.bottom = "0px";

                    TheSplitter.style.right = "";
                    TheSplitter.style.height = "";

                }


                p1.InternalClientSizeChanged0();
                p2.InternalClientSizeChanged0();
            };


            h.DragStop +=
                delegate
                {
                    if (this.InternalOrientation == global::System.Windows.Forms.Orientation.Horizontal)
                    {
                        this.SplitterDistance = h.Position.Y;
                    }
                    else
                    {
                        this.SplitterDistance = h.Position.X;

                    }

                    //AtUpdate();

                };




            this.InternalSplitterDistanceChanged +=
                delegate
                {
                    AtUpdate();
                };

            this.InternalOrientationChanged +=
                delegate
                {
                    Console.WriteLine(
                        "InternalOrientationChanged " + new
                        {
                            this.Name,
                            this.Orientation
                        }
                    );

                    AtUpdate();
                };

            this.SizeChanged +=
                delegate
                {
                    //Console.WriteLine("__SplitContainer SizeChanged");


                    AtUpdate();
                };


            AtUpdate();
        }

        public static implicit operator SplitContainer(__SplitContainer x)
        {
            return (SplitContainer)(object)x;
        }

        #region ISupportInitialize
        public void BeginInit()
        {
        }


        Action InternalAtEndInit;

        public void EndInit()
        {
            if (InternalAtEndInit != null)
                InternalAtEndInit();

        }
        #endregion

    }
}
