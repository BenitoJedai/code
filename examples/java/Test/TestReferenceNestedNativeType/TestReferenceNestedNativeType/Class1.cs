using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace TestReferenceNestedNativeType
{
    [Script]
    public class Class1
    {
        void Foo()
        {
            var i = AndroidOpenGLESLesson4Activity.R.drawable.bumpy_bricks_public_domain;
            var q = AndroidOpenGLESLesson4Activity.Q.drawable.bumpy_bricks_public_domain;
        }
    }
}
