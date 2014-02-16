using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestByRefAwaitUnsafeOnCompleted
{
    public interface __IAsyncStateMachine
    {
    }
    public interface __INotifyCompletion
    {
    }


    public class Class1
    {
        //.method public hidebysig instance void  AwaitUnsafeOnCompleted<TAwaiter,TStateMachine>(!!TAwaiter& awaiter,
        //                                                                                       !!TStateMachine& stateMachine) cil managed
        //{
        //  // Code size       36 (0x24)
        //  .maxstack  1
        //  .locals init ([0] class TestByRefAwaitUnsafeOnCompleted.__IAsyncStateMachine xstateMachine,
        //           [1] class TestByRefAwaitUnsafeOnCompleted.__INotifyCompletion xawaiter)
        //  IL_0000:  nop
        //  IL_0001:  ldarg.2
        //  IL_0002:  ldobj      !!TStateMachine
        //  IL_0007:  box        !!TStateMachine

        //  IL_000c:  castclass  TestByRefAwaitUnsafeOnCompleted.__IAsyncStateMachine
        //  IL_0011:  stloc.0
        //  IL_0012:  ldarg.1
        //  IL_0013:  ldobj      !!TAwaiter
        //  IL_0018:  box        !!TAwaiter
        //  IL_001d:  castclass  TestByRefAwaitUnsafeOnCompleted.__INotifyCompletion
        //  IL_0022:  stloc.1
        //  IL_0023:  ret
        //} // end of method Class1::AwaitUnsafeOnCompleted



        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
             ref  TAwaiter awaiter,
             ref  TStateMachine stateMachine
        )
        {
            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            var xawaiter = (__INotifyCompletion)(object)awaiter;

            //// TestByRefAwaitUnsafeOnCompleted.Class1.AwaitUnsafeOnCompleted
            //type$IhMWPYj_ajDem3aLC95jqxQ.AQAABoj_ajDem3aLC95jqxQ = function (ref$b, ref$c)
            //{
            //  var a = [this], d, e;

            //  d = ref$c[0];
            //  e = ref$b[0];
            //};

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216


            //machine0 = ((__IAsyncStateMachine)ref_arg2[0]);
            //completion1 = ((__INotifyCompletion)ref_arg1[0]);

        }
    }
}
