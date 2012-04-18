using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.mx.graphics
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/mx/graphics/IFill.html
    [Script(IsNative=true)]
    public interface IFill
    {
         void begin(Graphics target, Rectangle rc, Point p);
        
         void end(Graphics target);
    }
}
