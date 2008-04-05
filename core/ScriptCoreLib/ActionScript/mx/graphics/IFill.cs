using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.mx.graphics
{
    [Script(IsNative=true)]
    public interface IFill
    {
         void begin(Graphics target, Rectangle rc);
        
         void end(Graphics target);
    }
}
