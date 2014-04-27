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
        #region Font

        public event EventHandler FontChanged;

        protected virtual void OnFontChanged(EventArgs e)
        {
            //Console.WriteLine("OnFontChanged");

            if (FontChanged != null)
                FontChanged(this, e);
        }




        public virtual void InternalSetFont(Font value)
        {
        }

        private Font InternalFont;
        public Font Font
        {
            get { return InternalFont; }
            set
            {
                InternalFont = value;

                this.HTMLTargetRef.style.font = value.ToCssString();
                InternalSetFont(value);

                OnFontChanged(new EventArgs());


            }
        }
        #endregion

        public static Font DefaultFont
        {
            get
            {
                // will this cause
                // system ttf to be packaged
                // with the app?
                return new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            }
        }

        public void InternalSetDefaultFont()
        {
            this.Font = DefaultFont;
        }
    }
}
