using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.text.method
{
    [Script(IsNative = true)]
    public class PasswordTransformationMethod : TransformationMethod
    {
        public static PasswordTransformationMethod getInstance()
        {
            return default(PasswordTransformationMethod);
        }
    }
}
