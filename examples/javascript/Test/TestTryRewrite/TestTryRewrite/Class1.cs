using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Script,
    ScriptTypeFilter(ScriptType.JavaScript),
    ScriptTypeFilter(ScriptType.ActionScript),
    ScriptTypeFilter(ScriptType.Java),
    ScriptTypeFilter(ScriptType.PHP)
]
namespace TestTryRewrite
{
    [Script]
    public class Class1
    {
        static public object TryCreate(string z)
        {
            //1>script : error JSC1000: Method: TryCreate, Type: TestTryRewrite.Class1; emmiting failed : System.NotSupportedException: current OpCodes.Leave cannot be understood at
            //1>   type: TestTryRewrite.Class1
            //1>   offset: 0x0015
            //1>    method:System.Object TryCreate(System.String)

            //TryCreate(string) : Object
            //Analysis
            //Attributes
            //Signature Types
            //Declaring Module
            //Declaring Type
            //arg.0 z : string
            //loc.0 : object
            //maxstack 6 (used 1)
            //IL Code (21)
            //0x0000 nop 
            //.try
            //0x0001 nop 
            //0x0002 . ldarg.0      c <- arg.0 z : string
            //0x0003 . newobj       [ScriptCoreLib] ScriptCoreLib.JavaScript.DOM.IActiveX+InternalIActiveX..ctor(c : string)
            //0x0008 stloc.0        loc.0 : object
            //0x0009 leave 
            //.endtry
            //0x0027 nop 
            //0x0028 . ldloc.0      loc.0 : object
            //0x0029 ret 
            //.catch object 
            //[mscorlib] System.Object
            //0x0013 pop 
            //0x0014 nop 
            //0x0015 nop 
            //0x0016 leave 
            //.endtry
            //0x0022 nop 
            //0x0023 . ldnull       null
            //0x0024 stloc.0        loc.0 : object
            //0x0025 br.s 
            //0x0009 0x0025 -> 0x0027
            //0x0027 nop 
            //0x0028 . ldloc.0      loc.0 : object
            //0x0029 ret 

            var x = default(object);

            try
            {

                x = new object();
            }
            catch
            {

            }

            //            020001ba ScriptCoreLib.JavaScript.DOM.IActiveX
            //script: error JSC1000: Method: TryCreate, Type: ScriptCoreLib.JavaScript.DOM.IActiveX; emmiting failed : System.NotSupportedException: current OpCodes.Leave cannot be understood at
            // type: ScriptCoreLib.JavaScript.DOM.IActiveX
            // offset: 0x0016

            //try
            //{

            //    return (IActiveX)(new InternalIGeckoActiveX(z) as object);
            //}
            //catch
            //{

            //}

            return x;
        }
    }
}
