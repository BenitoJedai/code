using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.TextBox))]
    internal class __TextBox : __TextBoxBase
    {
        private HorizontalAlignment _TextAlign;

        public HorizontalAlignment TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; }
        }
	
    }
}
