using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/weakreference.cs
    [Script(Implements = typeof(global::System.WeakReference))]
    internal class __WeakReference
    {
        // how would we be able to implement it for other VMs ?

        // http://www.nczonline.net/blog/2012/11/06/ecmascript-6-collections-part-3-weakmaps/?utm_source=feedburner&utm_medium=feed&utm_campaign=Feed%3A+nczonline+%28NCZOnline+-+The+Official+Web+Site+of+Nicholas+C.+Zakas%29

        public __WeakReference(object e)
        {
            // weak reference not supported
        }
    }
}
