using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.Extensions
{
    public static class FormsExtensions
    {
        // move to .Forms namespace?
        
        public static T AttachTo<T>(this T source, Control c) where T : Control
        {
            c.Controls.Add(source);

            return source;
        }
    }
}
