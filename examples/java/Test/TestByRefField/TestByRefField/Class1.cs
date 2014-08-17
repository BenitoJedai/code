
using System;
[assembly: System.Reflection.Obfuscation(Feature = "script")]


namespace TestByRefField
{
    interface __IAssemblyReferenceToken : ScriptCoreLibJava.IAssemblyReferenceToken { }

    struct __Invoke
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140521

        public int state;



        //public static void __forwardref(__Invoke[] ref_arg1)
        //{
        //    ref_arg1[0].state = (ref_arg1[0].state + 1);
        //    __Console.WriteLine(__String.Concat("exit __forwardref ", new __AnonymousTypes__TestByRefField.__f__AnonymousType0_1<Integer>(ref_arg1[0].state)));
        //}

        public static void __forwardref(ref __Invoke that)
        {
            that.state++;

            Console.WriteLine("exit __forwardref " + new { that.state });
        }


        public void MoveNext()
        {
            Console.WriteLine("enter MoveNext " + new { state });

            var loc0 = this;

            // does JVM support it?
            //__forwardref(ref this);

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140817/async
            __forwardref(ref loc0);

            Console.WriteLine("exit MoveNext " + new { state });

            //0:14ms enter MoveNext { state = 5 }
            //0:14ms exit __forwardref { state = 6 }
            //0:14ms exit MoveNext { state = 6 }

            // CLR:
            //enter MoveNext { state = 5 }
            //exit __forwardref { state = 6 }
            //exit MoveNext { state = 5 }

        }
    }

}
