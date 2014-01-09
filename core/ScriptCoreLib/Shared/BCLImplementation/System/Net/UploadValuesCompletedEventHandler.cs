using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Net
{
    [Script(Implements = typeof(global::System.Net.UploadValuesCompletedEventHandler))]
    internal delegate void __UploadValuesCompletedEventHandler(object sender, UploadValuesCompletedEventArgs e);
}
