using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.SizeF))]
    internal class __SizeF
    {
        // X:\jsc.svn\examples\java\android\forms\FormsShowDialog\FormsShowDialog\ApplicationActivity.cs

        public __SizeF()
            : this(0, 0)
        {

        }

        public __SizeF(float width, float height)
        {
            this.Width = width;
            this.Height = height;
        }

        public float Width { get; set; }
        public float Height { get; set; }
    }
}
