using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using global::System.Collections.Generic;

namespace ButtonExample.js
{

    [Script]
    public delegate T FuncParams<P, T>(params P[] p);

    [Script]
    public delegate T FuncParams<A0, P, T>(A0 a0, params P[] p);


}
