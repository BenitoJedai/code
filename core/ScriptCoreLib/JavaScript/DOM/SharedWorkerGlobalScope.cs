﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class SharedWorkerGlobalScope : WorkerGlobalScope
    {
        //public readonly ApplicationCache applicationCache;
        public readonly string name;


        // chrome://inspect/#


        #region event onmessage
        public event Action<MessageEvent> onconnect
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "connect");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "connect");
            }
        }
        #endregion

    }
}
