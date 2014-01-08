using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestLock
{
    public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        //{ Location =
        // assembly: X:\jsc.svn\examples\javascript\Test\TestLock\TestLock\bin\Debug\TestLock.dll
        // type: TestLock.Class1, TestLock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x0008
        //  method:Void foo() }
        //script: error JSC1000: Method: foo, Type: TestLock.Class1; emmiting failed : System.NotImplementedException: { ParameterType = System.Boolean&, p = [0x0008] call       +0 -2{[0x0004] dup        +2 -1{[0x0003] ldarg.0    +1 -0} } {[0x0006] ldloca.s   +1 -0} , m = Void Enter(System.Object, Boolean ByRef) }
        //   at jsc.IdentWriter.JavaScript_WriteParameters(Prestatement p, ILInstruction i, ILFlowStackItem[] s, Int32 offset, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\IdentWriter.cs:line 824

        void foo()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140108-lock
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Monitor.cs

            //lock (this)
            //{
            //    foo();
            //}


            //  method:Void foo() }
            //script: error JSC1000: Method: foo, Type: TestLock.Class1; emmiting failed : System.NotImplementedException: { ParameterType = System.Boolean&, p = [0x0006] call       +0 -2{[0x0003] ldarg.0    +1 -0} {[0x0004] ldloca.s   +1 -0} , m = Void Enter(System.Object, Boolean ByRef) }

            var loc0 = false;
            var loc1 = 0;
            //System.Threading.Monitor.Enter(this, ref loc0);

            //Monitor_Enter(this, ref loc1);
            Monitor_Enter(this, ref loc0);
        }

        //static void Monitor_Enter(object x, ref int r)
        static void Monitor_Enter(object x, ref bool r)
        {

        }

        //foo() : void
        //Analysis
        //Attributes
        //Signature Types
        //Declaring Module
        //Declaring Type
        //loc.0 <- 0x0001 ldc.i4.0     0 (0x00000000)
        //loc.1 <- 0x0004 dup 
        //loc.2 <- 0x001b ceq 
        //maxstack 2
        //IL Code (27)
        //0x0000 nop 
        //0x0001 . ldc.i4.0       0 (0x00000000)
        //0x0002 stloc.0          loc.0 : bool
        //.try
        //0x0003 . ldarg.0        this [TestLock] TestLock.Class1
        //0x0004 . . dup 
        //0x0005 . stloc.1        loc.1 : Class1
        //0x0006 . . ldloca.s     lockTaken <- loc.0 : bool
        //0x0008 call             [mscorlib] System.Threading.Monitor.Enter(obj : object, lockTaken : ref bool) : void
        //0x000d nop 
        //0x000e nop 
        //0x000f . ldarg.0        this [TestLock] TestLock.Class1
        //0x0010 call             [TestLock] TestLock.Class1.foo() : void
        //0x0015 nop 
        //0x0016 nop 
        //0x0017 leave.s 
        //.endtry
        //.finally
        //0x0019 . ldloc.0        loc.0 : bool
        //0x001a . . ldc.i4.0     0 (0x00000000)
        //0x001b . ceq 
        //0x001d stloc.2          loc.2 : bool
        //0x001e . ldloc.2        loc.2 : bool
        //0x001f brtrue.s 
        //0x001f -> 0x0021 0x0028 
        //       0x001f -> 0x0021 ldloc.1 
        //0x0021 . ldloc.1        obj <- loc.1 : Class1
        //0x0022 call             [mscorlib] System.Threading.Monitor.Exit(obj : object) : void
        //0x0027 nop 
        //0x001f 0x0027 -> 0x0028
        //0x001f 0x0027 -> 0x0028 endfinally 
        //0x0028 endfinally 

    }
}
