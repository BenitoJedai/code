using System;
using System.Threading;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestThreadStart
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // "X:\jsc.svn\examples\actionscript\async\AsyncWorkerTask\AsyncWorkerTask.sln"
            // X:\jsc.svn\examples\actionscript\async\AsyncWorkerTask\AsyncWorkerTask\ApplicationSprite.cs
            // X:\jsc.svn\examples\actionscript\FlashWorkerExperiment\FlashWorkerExperiment\ApplicationSprite.cs


            // https://forums.adobe.com/thread/1164500

            // this looks like chrome context capture
            if (!Worker.current.isPrimordial)
            {
                // iOS workers is still on the roadmap.  I don't have a release date, but I know it'll be an extended beta type of feature.  
                // Most of the concurrency work was gated on the new AOT compiler work, which is still being actively worked on.  
                // Lots of bug and performance fixes were added to AIR 15 and we're not stopping there.



                return;
            }

            // {{ os = Windows 7, version = WIN 15,0,0,189, length = 333983, Target = null, Method = { _Target = , _Method = IntPtr { StringToken = , FunctionToken = function Function() {}, ClassToken =  } } }}
            // start0 = new __ParameterizedThreadStart(null, __IntPtr.op_Explicit_4ebbe596_06001686(TheOtherClass.Invoke_6d788eff_0600000c));
            //      start0 = new __ParameterizedThreadStart(null, __IntPtr.OfFunctionToken_4ebbe596_06001687(TheOtherClass.Invoke_6d788eff_0600000c,"TestThreadStart.TheOtherClass","Invoke_6d788eff_0600000c"));
            ParameterizedThreadStart y = TheOtherClass.Invoke;

            // can we call the method
            //            0007 0200034c ScriptCoreLib::ScriptCoreLib.ActionScript.Extensions.ZipFileEntry + Cookie`1

            //Unhandled Exception: System.AggregateException: One or more errors occurred. --->System.InvalidOperationException: internal compiler error at method
            // assembly: X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\bin\Debug\ScriptCoreLib.dll at
            // type: ScriptCoreLib.ActionScript.BCLImplementation.System.__Single, ScriptCoreLib, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null
            // method: CompareTo
            // Object reference not set to an instance of an object.
            //    at System.Collections.Generic.Dictionary`2.Insert(TKey key, TValue value, Boolean add)
            //   at System.Collections.Generic.Dictionary`2.set_Item(TKey key, TValue value)
            //   at jsc.Script.CompilerBase.  .    (Type , MethodBase , LocalVariableInfo , CompilerBase )
            //   at jsc.Script.CompilerBase.<WriteVariableName>b__0(Type , MethodBase , LocalVariableInfo )

            //           Unhandled Exception: System.AggregateException: One or more errors occurred. --->System.InvalidOperationException: internal compiler error at method
            //assembly: X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\bin\Debug\ScriptCoreLib.dll at
            //type: ScriptCoreLib.ActionScript.BCLImplementation.System.__BitConverter, ScriptCoreLib, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null
            //method: GetBytes
            //Object reference not set to an instance of an object.
            //   at System.Collections.Generic.Dictionary`2.Insert(TKey key, TValue value, Boolean add)
            //  at System.Collections.Generic.Dictionary`2.set_Item(TKey key, TValue value)
            //  at jsc.Script.CompilerBase.  .    (Type , MethodBase , LocalVariableInfo , CompilerBase )


            //{{ os = Windows 7, version = WIN 15,0,0,189, length = 334356, Target = null, Method = { _Target = , _Method = IntPtr { StringToken = , FunctionToken = function Function() {}, ClassToken =  } }, z = Type { TypeDescription = 
            // <type name="TestThreadStart::TheOtherClass" base="Class" isDynamic="true" isFinal="true" isStatic="true">
            //  <extendsClass type="Class"/>
            //  <extendsClass type="Object"/>
            //  <accessor name="prototype" access="readonly" type="*" declaredBy="Class"/>
            //  <method name="Invoke_6d788eff_0600000c" declaredBy="TestThreadStart::TheOtherClass" returnType="void">
            //    <parameter index="1" type="Object" optional="false"/>
            //  </method>
            //  <factory type="TestThreadStart::TheOtherClass">
            //    <extendsClass type="Object"/>
            //  </factory>
            //</type> } }}

            //          {{ os = Windows 7, version = WIN 15,0,0,189, length = 334570, Target = null, Method = { _Target = , _Method = IntPtr { StringToken = , 
            //FunctionToken = function Function() {}, ClassToken =  } }, FullName = TestThreadStart::TheOtherClass, z = Type { TypeDescription = <type name="TestThreadStart::TheOtherClass" base="Class" isDynamic="true" isFinal="true" isStatic="true">
//  <extendsClass type="Class"/>
//  <extendsClass type="Object"/>
//  <accessor name="prototype" access="readonly" type="*" declaredBy="Class"/>
//  <method name="Invoke_6d788eff_0600000c" declaredBy="TestThreadStart::TheOtherClass" returnType="void">
//    <parameter index="1" type="Object" optional="false"/>
//  </method>
//  <factory type="TestThreadStart::TheOtherClass">
//    <extendsClass type="Object"/>
//  </factory>
//</type> }, zz = Type { TypeDescription = <type name="TestThreadStart::TheOtherClass" base="Class" isDynamic="true" isFinal="true" isStatic="true">
//  <extendsClass type="Class"/>
//  <extendsClass type="Object"/>
//  <accessor name="prototype" access="readonly" type="*" declaredBy="Class"/>
//  <method name="Invoke_6d788eff_0600000c" declaredBy="TestThreadStart::TheOtherClass" returnType="void">
//    <parameter index="1" type="Object" optional="false"/>
//  </method>
//  <factory type="TestThreadStart::TheOtherClass">
//    <extendsClass type="Object"/>
//  </factory>
//</type> } }}

            var z = typeof(TheOtherClass);
            var zz = Type.GetType("TestThreadStart::TheOtherClass");

            //var zMethod = z.GetMethods();

            var t = new TextField
            {
                multiline = true,
                //wordWrap = true,

                text = new
                {
                    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/Capabilities.html
                    Capabilities.os,
                    Capabilities.version,

                    //WorkerDomain = WorkerDomain.isSupported,
                    //Worker = Worker.isSupported,
                    //Worker.current.isPrimordial,

                    this.loaderInfo.bytes.length,

                    // {{ os = Windows 7, version = WIN 15,0,0,189, length = 333411, Target = null, Method = { InternalFunctionPointer = function Function() {} } }}

                    y.Target,

                    // first step is to call a static method on the other side of thread
                    y.Method,


                    //z.FullName,

                    //z,

                    //zz

                }.ToString(),

                autoSize = TextFieldAutoSize.LEFT
            };

            t.AttachTo(this);


        }



    }



    static class TheOtherClass
    {
        public static void Invoke(object data)
        {

        }
    }
}
