using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDynamicKeyword
{
    public class Class1
    {
        static void Foo(dynamic foo)
        {
            // jsc has to decide whether or not to use language level dynamic features
            /*
             * 
                IDynamicMetaObjectProvider Interface
                jsc will need to support casting to interface in javascript to
                get this working on custom dynamic implementations via reflection
                http://msdn.microsoft.com/en-us/library/system.dynamic.idynamicmetaobjectprovider.aspx
             * 
             * 
             * 
             * 
            private static void Foo([Dynamic] object foo)
            {
                if (<Foo>o__SiteContainer0.<>p__Site1 == null)
                {
                    <Foo>o__SiteContainer0.<>p__Site1 = CallSite<Action<CallSite, object, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "HelloWorld", null, typeof(Class1), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                }
                <Foo>o__SiteContainer0.<>p__Site1.Target(<Foo>o__SiteContainer0.<>p__Site1, foo, "hello world");
            }
             */
            foo.HelloWorld("hello world");
        }
    }
}
