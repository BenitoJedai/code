using ScriptCoreLib.ActionScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAttributeField
{
    public class Class1
    {
        [Embed("/Fixedsys500c.ttf", fontName = "Fixedsys500c")]
        // You do not use this variable directly. It exists so that 
        // the compiler will link in the font.
        static readonly Class Asset_Fixedsys500c;
    }
}
