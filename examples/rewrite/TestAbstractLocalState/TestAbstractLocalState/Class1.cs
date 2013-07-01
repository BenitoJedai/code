using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAbstractLocalState
{



    public abstract class AbstractFlowPass<LocalState> where LocalState : AbstractFlowPass<LocalState>.AbstractLocalState
    //public abstract class AbstractFlowPass<LocalState> where LocalState : AbstractLocalState<LocalState>
    {
        public interface AbstractLocalState
        {
            //1:001c:0001 XTestAbstractLocalState define TestAbstractLocalState.AbstractFlowPass`1
            //1:001d:0002 XTestAbstractLocalState define TestAbstractLocalState.AbstractFlowPass`1_AbstractLocalState
            //1:001a:0000 XTestAbstractLocalState create interface TestAbstractLocalState.AbstractFlowPass`1_AbstractLocalState
            //1:001d:0001 XTestAbstractLocalState create TestAbstractLocalState.AbstractFlowPass`1
            //1:001c:0006 XTestAbstractLocalState define TestAbstractLocalState.DataFlowPass
            //1:0028:0007 XTestAbstractLocalState define TestAbstractLocalState.DataFlowPass_DataFlowPassXLocalState
            //define shadow TestAbstractLocalState.DataFlowPass_DataFlowPassXLocalState
            //define shadow TestAbstractLocalState.AbstractFlowPass`1_AbstractLocalState
            //1:001a:0002 XTestAbstractLocalState create TestAbstractLocalState.DataFlowPass
            //GenericArguments[0], 'TestAbstractLocalState.DataFlowPass_DataFlowPassXLocalState', on 'TestAbstractLocalState.AbstractFlowPass`1[LocalState]' violates the constraint of type parameter 'LocalState'.

        }
    }
    // Error	1	The type 'TestAbstractLocalState.DataFlowPass.LocalState' cannot be used as type parameter 'LocalState' in the generic type or method 'TestAbstractLocalState.AbstractFlowPass<LocalState>'. There is no boxing conversion from 'TestAbstractLocalState.DataFlowPass.LocalState' to 'TestAbstractLocalState.AbstractFlowPass<TestAbstractLocalState.DataFlowPass.LocalState>.AbstractLocalState'.	X:\jsc.svn\examples\rewrite\TestAbstractLocalState\TestAbstractLocalState\Class1.cs	16	18	TestAbstractLocalState

    //public class DataFlowPass : AbstractFlowPass<DataFlowPass.LocalState>
    public class DataFlowPass : AbstractFlowPass<DataFlowPass.DataFlowPassXLocalState>
    {

        //        .class /*02000004*/ public auto ansi beforefieldinit 'TestAbstractLocalState'.'DataFlowPass'
        //       extends class 'TestAbstractLocalState'.'AbstractFlowPass`1'/*02000002*/<valuetype ['XTestAbstractLocalState'/*23000002*/]'TestAbstractLocalState'.'DataFlowPass_LocalState'/*01000002*/>/*1B000002*/
        //{
        //} // end of class 'TestAbstractLocalState'.'DataFlowPass'

        //        1:001a:0002 XTestAbstractLocalState create TestAbstractLocalState
        //DataFlowPass
        //GenericArguments[0], 'TestAbstractLocalState.DataFlowPass+LocalState', on 'TestAbstractLocalState.AbstractFlowPass`1[LocalState]' violates the constraint of type parameter 'LocalState'.


        //public struct LocalState : AbstractFlowPass<LocalState>.AbstractLocalState


        // is this a shadow type? what about interfaces?
        public struct DataFlowPassXLocalState : AbstractFlowPass<DataFlowPassXLocalState>.AbstractLocalState
        {
            // jsc shadow type used?

            //            .class public auto ansi beforefieldinit TestAbstractLocalState.DataFlowPass
            //       extends class TestAbstractLocalState.AbstractFlowPass`1<valuetype [XTestAbstractLocalState]TestAbstractLocalState.DataFlowPass/LocalState>
            //{
            //} // end of class TestAbstractLocalState.DataFlowPass


        }
    }

    //public struct LocalState : AbstractFlowPass<LocalState>.AbstractLocalState
    //public struct DataFlowPassXLocalState : AbstractLocalState<DataFlowPassXLocalState>


}
