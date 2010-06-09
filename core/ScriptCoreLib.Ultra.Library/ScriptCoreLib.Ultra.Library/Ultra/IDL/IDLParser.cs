using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public static class IDLParser
    {


        public static IDLModule ToModule(IDLParserToken source)
        {
            var m = new IDLModule();
            

            // what are we looking at?
            // at first we are looking at zero length empty token

            var f = source.Next;
            var w = f.Next;
            var module = w.Next;
            var module_w = module.Next;
            var module_w_Name = module_w.Next;
            var module_w_Name_w = module_w_Name.Next;
            var module_w_Name_w_parenthesis = module_w_Name_w.Next;

            return m;
        }
    }
}
