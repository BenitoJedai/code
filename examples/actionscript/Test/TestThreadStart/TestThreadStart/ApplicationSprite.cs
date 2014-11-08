using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection;
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

            //var z = typeof(TheOtherClass);
            //var zz = Type.GetType("TestThreadStart::TheOtherClass");
            //var zz = Type.GetType("TestThreadStart.TheOtherClass");

            //var zMethod = z.GetMethods();

            __MethodInfo p = y.Method;

            //{ { os = Windows 7, version = WIN 15,0,0,189, length = 335268, 
            // FunctionToken_TypeFullName = TestThreadStart.TheOtherClass, 
            // FunctionToken_MethodName = Invoke_6d788eff_0600000c 
            // } }

            //var pt = Type.GetType(p._Method.FunctionToken_TypeFullName);


            IntPtr pp = __IntPtr.OfFunctionToken(null,
                p._Method.FunctionToken_TypeFullName,
                p._Method.FunctionToken_MethodName
            );


            MethodInfo mm = new __MethodInfo { _Method = pp };

            //new ParameterizedThreadStart(null, pp);

            //new Delegate();




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

                    //y.Target,

                    // first step is to call a static method on the other side of thread
                    //y.Method,
                    //y.Method,
                    //y.Method.DeclaringType


                    //z.FullName,

                    //z,

                    p._Method.FunctionToken_TypeFullName,
                    p._Method.FunctionToken_MethodName,

                }.ToString(),

                autoSize = TextFieldAutoSize.LEFT
            };

            t.AttachTo(this);

            t.click += delegate
            {
                var sw = Stopwatch.StartNew();

                t.text = "enter click";

                try
                {
                    // catch {{ err = ArgumentError: Error #1063: Argument count mismatch on TestThreadStart::TheOtherClass$/Invoke_6d788eff_06000013(). Expected 1, got 0. }}

                    mm.Invoke(null, new object[1]);
                    t.text = "after invoke " + new { TheOtherClass.SharedField, sw.ElapsedMilliseconds };
                }
                catch (Exception err)
                {
                    t.text = "catch " + new { err };
                }


            };
        }



    }



    static class TheOtherClass
    {
        public static string SharedField;

        // after invoke {{ SharedField = {{ data = null }} }}


        //null
        //	at TestThreadStart::TheOtherClass$/Invoke_6d788eff_06000013()
        //    at Function/http://adobe.com/AS3/2006/builtin::apply()
        //	at ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection::__MethodInfo/InternalInvoke_4ebbe596_06000018()
        //    at ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection::__MethodInfo/InternalInvoke_4ebbe596_06000013()
        //    at ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection::__MethodBase/Invoke_4ebbe596_06000012()
        //    at TestThreadStart::ApplicationSprite___c__DisplayClass0/__ctor_b__2_6d788eff_06000005()

        public static void Invoke(object data)
        {
            // after invoke {{ SharedField = {{ data = null }}, ElapsedMilliseconds = 1 }}
            // after invoke {{ SharedField = {{ data = null, i = 65534 }}, ElapsedMilliseconds = 830 }}
            // after invoke {{ SharedField = {{ data = null, i = 65534, j = 3 }}, ElapsedMilliseconds = 2908 }}

            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 0xffff; i++)
                {
                    SharedField = new { data, i, j }.ToString();
                }


            //throw null;
        }
    }
}
