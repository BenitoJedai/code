using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    partial class __DataGridView
    {
        public override global::System.Drawing.Size GetPreferredSize(global::System.Drawing.Size proposedSize)
        {
            var x = new global::System.Drawing.Size();

            if (this.ScrollBars == global::System.Windows.Forms.ScrollBars.None)
            {
                x.Width = this.__ContentTable.scrollWidth + 16 + 2;
                x.Height = this.__ContentTable.scrollHeight + 16 + 2;

            }
            else
            {
                x.Width = this.__ContentTable.scrollWidth + 32 + 2;
                x.Height = this.__ContentTable.scrollHeight + 32 + 2;
            }

            return x;
        }



    }
}
