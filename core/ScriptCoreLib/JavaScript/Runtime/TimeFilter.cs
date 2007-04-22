using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace ScriptCoreLib.JavaScript.Runtime
{

    /// <summary>
    /// provides the functionality to deal with exessive event flushing, like mouse move
    /// </summary>
    [Script]
    public class TimeFilter
    {
        public double Value;
        public int Window;

        public bool IsValid
        {
            get
            {
                return Native.Math.abs(Value - IDate.Now) > Window;
            }
        }

        public TimeFilter(int w)
        {
            Window = w;
        }

        public void Update()
        {
            Value = IDate.Now;
        }

        internal void Invoke(EventHandler h)
        {
            if (!IsValid)
                return;

            Helper.Invoke(h);

            Update();
        }
    }

}
