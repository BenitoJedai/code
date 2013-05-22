using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(DragEventArgs))]
    public class __DragEventArgs : EventArgs
    {
        public IDataObject Data
        {
            get;
            set;
        }

        public Action<DragDropEffects> InternalSetEffect;
        public Func<DragDropEffects> InternalGetEffect;

        public DragDropEffects Effect
        {
            get
            {
                return InternalGetEffect();
            }
            set
            {
                InternalSetEffect(value);
            }
        }
    }



}
