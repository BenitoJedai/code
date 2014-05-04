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

        public DockStyle Dock { get; set; }

        public SplitterPanel Panel1 { get; set; }

        public SplitterPanel Panel2 { get; set; }

        public int SplitterDistance { get; set; }


        // 8:66ms missing HTMLTargetRef for { e = <Namespace>.SplitContainer }

        public __SplitContainer()
        {
            this.Panel1 = new SplitterPanel(this);
            this.Panel2 = new SplitterPanel(this);
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
