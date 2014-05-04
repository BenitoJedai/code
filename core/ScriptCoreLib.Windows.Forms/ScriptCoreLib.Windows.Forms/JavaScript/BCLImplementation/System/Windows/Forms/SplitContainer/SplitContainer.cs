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

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.SplitContainer))]
    public class __SplitContainer : __ContainerControl, ISupportInitialize
    {
        // X:\jsc.svn\examples\javascript\forms\FormsSplitter\FormsSplitter\ApplicationControl.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140504


        public IHTMLDiv InternalElement = typeof(SplitContainer);



        public DockStyle Dock { get; set; }

        public SplitterPanel Panel1 { get; set; }

        public SplitterPanel Panel2 { get; set; }

        public int SplitterDistance { get; set; }


        // 8:66ms missing HTMLTargetRef for { e = <Namespace>.SplitContainer }


        public override DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

        public __SplitContainer()
        {
            this.Panel1 = new SplitterPanel(this);
            this.Panel2 = new SplitterPanel(this);

            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel2);


            this.SplitterDistance = 200;



            Action AtUpdate = delegate
            {
                // dock left?
                this.Panel1.Left = 0;
                this.Panel1.Top = 0;
                this.Panel1.Width = this.SplitterDistance - 4;
                this.Panel1.Height = this.clientHeight;

                // dock fill?
                this.Panel2.Left = this.SplitterDistance + 4;
                this.Panel2.Top = 0;

                this.Panel2.Width = this.clientWidth - this.Panel2.Left;
                this.Panel2.Height = this.clientHeight;
            };

            this.SizeChanged +=
                delegate
                {
                    AtUpdate();
                };


        }

        public static implicit operator SplitContainer(__SplitContainer x)
        {
            return (SplitContainer)(object)x;
        }

        #region ISupportInitialize
        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
        #endregion

    }
}
