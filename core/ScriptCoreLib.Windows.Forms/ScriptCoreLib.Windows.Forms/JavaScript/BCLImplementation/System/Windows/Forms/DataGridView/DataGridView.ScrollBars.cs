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

        #region ScrollBars
        public ScrollBars InternalScrollBars;
        public ScrollBars ScrollBars
        {
            get
            {
                return InternalScrollBars;
            }
            set
            {
                InternalScrollBars = value;

                if (value == global::System.Windows.Forms.ScrollBars.None)
                {
                    this.InternalScrollContainerElement.style.overflowX = IStyle.OverflowEnum.hidden;
                    this.InternalScrollContainerElement.style.overflowY = IStyle.OverflowEnum.hidden;
                    return;
                }

                if (value == global::System.Windows.Forms.ScrollBars.Horizontal)
                {
                    this.InternalScrollContainerElement.style.overflowX = IStyle.OverflowEnum.auto;
                    this.InternalScrollContainerElement.style.overflowY = IStyle.OverflowEnum.hidden;
                    return;
                }

                if (value == global::System.Windows.Forms.ScrollBars.Vertical)
                {
                    this.InternalScrollContainerElement.style.overflowX = IStyle.OverflowEnum.hidden;
                    this.InternalScrollContainerElement.style.overflowY = IStyle.OverflowEnum.auto;
                    return;
                }

                this.InternalScrollContainerElement.style.overflowX = IStyle.OverflowEnum.auto;
                this.InternalScrollContainerElement.style.overflowY = IStyle.OverflowEnum.auto;
            }
        }
        #endregion



    }
}
