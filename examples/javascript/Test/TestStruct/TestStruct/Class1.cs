using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestStruct
{
    //sealed class Class3 : IClass
    //{

    //    public int ii
    //    {
    //        set;
    //        get;
    //    }
    //}

    //public class Class2
    //{
    //    public void Foo()
    //    {
    //        Class1 c;

    //        c.i = 5;
    //        c.ii = 6;

    //        FooBar(ref c);

    //        var i = c.i;


    //        var cc = CopyOfBar(c);

    //        var ii = cc.i;

    //        var cco_0 = CopyOfBarObject(c);
    //        var cco_1 = CopyOfBarObject(null);
    //        var cco_2 = CopyOfBarObject(new object());

    //    }

    //    public void FooBar(ref Class1 x)
    //    {
    //        x.i = 8;
    //    }

    //    public Class1 CopyOfBar(Class1 x)
    //    {
    //        x.i = 9;

    //        return x;
    //    }

    //    public Class1 CopyOfBarObject(object xx)
    //    {
    //        // Error	1	Cannot convert null to 'TestStruct.Class1' because it is a non-nullable value type	
    //        // X:\jsc.svn\examples\javascript\Test\TestStruct\TestStruct\Class1.cs	43	24	TestStruct
    //        var x = new Class1();

    //        if (xx is Class1)
    //        {
    //            x = (Class1)xx;
    //            x.i = 9;
    //        }

    //        return x;
    //    }

    //}

    //public struct Class1x
    //{
    //    // Error	2	Struct member 'TestStruct.Class1.builder' of type 'TestStruct.Class1x' causes a cycle in the struct layout	X:\jsc.svn\examples\javascript\Test\TestStruct\TestStruct\Class1.cs	71	24	TestStruct
    //    //public Class1 foo;
    //}

    //public struct Class1 : IClass
    //{
    //    public int i;
    //    public Class1x builder;

    //    //public override string ToString()
    //    //{
    //    //    return new { i }.ToString();
    //    //}

    //    public int ii
    //    {
    //        set { i = value; }
    //    }
    //}

    //public interface IClass
    //{
    //    int ii { set; }
    //}


    public class Program
    {
        public static void Main(string[] e)
        {
            //var c = new Class2();

            //c.Foo();


            var uu = new _button_Click1_d__0();

            uu.MoveNext();
        }
    }


    public struct _button_Click1_d__0 : __IAsyncStateMachine
    {
        __AsyncTaskMethodBuilder builder;

        public int i;

        // Error	1	Structs cannot contain explicit parameterless constructors	
        // X:\jsc.svn\examples\javascript\Test\TestByRefParameterApp\TestByRefParameterApp\Application.cs	55	16	
        // TestByRefParameterApp


        public _button_Click1_d__0(int ii = 0)
        {
            i = ii;
        }

        public void MoveNext()
        {
            i = 5;

            builder = new __AsyncTaskMethodBuilder();
            builder.Start(ref this);

            //Console.WriteLine(new { i });
        }

        public void SetStateMachine(__IAsyncStateMachine stateMachine)
        {
            i = 6;
        }
    }


    public interface __IAsyncStateMachine
    {
        void MoveNext();

        void SetStateMachine(
            __IAsyncStateMachine stateMachine
        );
    }

    public struct __AsyncTaskMethodBuilder
    {
        public void Start<TStateMachine>(
          ref  TStateMachine stateMachine
      )
            where TStateMachine : __IAsyncStateMachine
        {
            // we need ref support in JSC!

            stateMachine.SetStateMachine(stateMachine);
        }
    }
}
