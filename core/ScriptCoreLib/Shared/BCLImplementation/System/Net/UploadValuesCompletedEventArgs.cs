using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Net
{
    [Script(Implements = typeof(global::System.Net.UploadValuesCompletedEventArgs))]
    public class __UploadValuesCompletedEventArgs : __AsyncCompletedEventArgs
    {
        public byte[] Result { get; set; }
    }
}
