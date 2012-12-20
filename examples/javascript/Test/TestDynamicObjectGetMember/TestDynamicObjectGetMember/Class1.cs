using ScriptCoreLib.JavaScript;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestDynamicObjectGetMember
{
    public class Class1
    {
        ScriptCoreLib.Shared.IAssemblyReferenceToken ref0;

        private
            //static
    string
            getfoo()
        {
            var u0 = typeof(Class1);
            var u1 = typeof(string);

            var x = new DynamicLocalStorage { };

            dynamic xlocalStorage = x;

            string foo = xlocalStorage.foo;
            return foo;
        }

    }

    class DynamicLocalStorage : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var value = Native.Window.localStorage[binder.Name];

            result = value;

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Native.Window.localStorage[binder.Name] = value + "";
            return true;
        }
    }
}
