using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IXMLHttpRequest.cs
    public class IXMLHttpRequest
    {
        public IXMLHttpRequestEventTarget upload;
    }

    [Script(HasNoPrototype = true)]
    public class IXMLHttpRequestEventTarget : IEventTarget
    {
        #region event onprogress
        public event System.Action<ProgressEvent> onprogress
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "progress");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "progress");
            }
        }
        #endregion
    }
}
