using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.Serialization
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/serialization/formatterservices.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/Serialization/FormatterServices.cs
    // http://msdn.microsoft.com/en-us/library/system.runtime.serialization.formatterservices.getuninitializedobject(v=vs.110).aspx
    [Script(Implements = typeof(global::System.Runtime.Serialization.FormatterServices))]
    public class __FormatterServices
    {
        // X:\jsc.svn\examples\javascript\test\TestGetUninitializedObject\TestGetUninitializedObject\Application.cs

        public static object GetUninitializedObject(Type type)
        {
			// X:\jsc.svn\examples\javascript\async\test\TestEditAndContinue\TestEditAndContinue\ApplicationWebService.cs
			// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Activator.cs

			var ctor = ((__Type)type).AsExpando().constructor;

            // 0:31ms { type = <Namespace>.Foo, ctor = function (b)

            //Console.WriteLine(new { type, ctor });
            //[Script(OptimizedCode = @"return new f();")]

            // how to apply ctor args correctly?

            return new IFunction("f",
                "return new f();").apply(null, ctor);
        }

        public static MemberInfo[] GetSerializableMembers(Type type)
        {
            return type.GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );
        }



        public static object[] GetObjectData(object obj, MemberInfo[] members)
        {
            //Console.WriteLine("enter GetObjectData");

            var a = new List<object>();
            foreach (var item in members)
            {
                var f = item as FieldInfo;
                if (f != null)
                {
                    a.Add(
                        f.GetValue(obj)
                    );
                }

            }
            return a.ToArray();
        }

        public static object PopulateObjectMembers(object obj, MemberInfo[] members, object[] data)
        {
            // http://msdn.microsoft.com/en-us/library/system.runtime.serialization.formatterservices.populateobjectmembers(v=vs.110).aspx

            members.Zip(data,
                (m, value) =>
                {
                    var f = m as FieldInfo;
                    if (f != null)
                    {
                        f.SetValue(obj, value);
                    }

                    return false;
                }
            ).ToArray();


            return obj;
        }

    }
}
