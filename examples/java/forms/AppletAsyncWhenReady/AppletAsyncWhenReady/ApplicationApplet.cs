using AppletAsyncWhenReady;
using java.applet;
using ScriptCoreLib.Java.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppletAsyncWhenReady
{
    public interface IWhenReady<T>
    {
        void WhenReady(Action<T> yield);
    }

    //0f60:01:01 0028:000a AppletAsyncWhenReady.ApplicationApplet create interface AppletAsyncWhenReady::AppletAsyncWhenReady.ILocalTasks
    //RewriteToAssembly error: System.Collections.Generic.KeyNotFoundException: The given key was not present in the dictionary.
    //   at System.Collections.Generic.Dictionary`2.get_Item(TKey key)
    //   at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.ExternalInterfaceProvider.Implement()
    //   at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.<>c__DisplayClass1e8.<InternalInvoke>b__177(TypeRewriteArguments a)
    //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass420.<>c__DisplayClass4c1.<InternalInvoke>b__3b0(TypeBuilder t)
    //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyType(Type SourceType, AssemblyBuilder a, ModuleBuilder m, TypeBuilder OverrideDeclaringType, VirtualDictionary`2 TypeRenameCache, VirtualDictionary`2 NameObfuscation, Func`2 ShouldCopyType, Func`3 FullNameFixup, Action`1 PostTypeRewrite, Action`1 PreTypeRewrite, Action`1 TypeCreated, RewriteToAssembly r, ILTranslationContext context, Action AtCodeTraceCreateType)

    public interface ILocalTasks_content_button1_click : IWhenReady<string>
    {
    }

    //public interface ILocalTasks_content_button2_click : IWhenReady<string>
    //{
    //}

    public interface ILocalTasks
    //IWhenReady<ILocalTasks_content_button1_click>,
    //IWhenReady<ILocalTasks_content_button2_click>
    {
        void WithButton1(Action<IWhenReady<string>> yield);

    }

    public sealed class ApplicationApplet : Applet,
        IWhenReady<ILocalTasks>
    {
        public readonly ApplicationControl content = new ApplicationControl();

        public override void init()
        {
            content.AttachTo(this);
            content.AutoSizeTo(this);
            this.EnableVisualStyles();
        }




        class XLocalTasks : ILocalTasks
        {
            //class XLocalTasks_content_button1_click : ILocalTasks_content_button1_click
            //{
            //    // extension interface implementtion? 
            //    public Action<Action<string>> AtWhenReady;


            //    // ScriptCoreLib.Document
            //    public void WhenReady(Action<string> yield)
            //    {
            //        AtWhenReady(yield);
            //    }
            //}


            class XLocalTasks_content_button2_click : IWhenReady<string>
            {
                // extension interface implementtion? 
                public Action<Action<string>> AtWhenReady;


                // ScriptCoreLib.Document
                public void WhenReady(Action<string> yield)
                {
                    AtWhenReady(yield);
                }
            }


            public ApplicationApplet Context;

            public void WithButton1(Action<IWhenReady<string>> yield)
            {
                Console.WriteLine("WithButton1");
                yield(
                   new XLocalTasks_content_button2_click
                    {
                        AtWhenReady = yield2 =>
                        {
                            Console.WriteLine("IWhenReady<ILocalTasks_content_button2_click>.WhenReady XLocalTasks_content_button2_click");

                            Context.content.button2.Click +=
                                delegate
                                {
                                    Console.WriteLine("IWhenReady<ILocalTasks_content_button2_click>.WhenReady click");

                                    yield2("button2");
                                };
                        }
                    }
                );
            }

            //void IWhenReady<ILocalTasks_content_button1_click>.WhenReady(Action<ILocalTasks_content_button1_click> yield)
            //{
            //    Console.WriteLine("IWhenReady<ILocalTasks_content_button1_click>.WhenReady");

            //    yield(new XLocalTasks_content_button1_click
            //    {
            //        AtWhenReady = yield1 =>
            //        {
            //            Console.WriteLine("IWhenReady<ILocalTasks_content_button1_click>.WhenReady XLocalTasks_content_button1_click");

            //            Context.content.button1.Click +=
            //                delegate
            //                {
            //                    Console.WriteLine("IWhenReady<ILocalTasks_content_button1_click>.WhenReady click");

            //                    yield1("button1");
            //                };
            //        }
            //    });



            //}

            //void IWhenReady<ILocalTasks_content_button2_click>.WhenReady(Action<ILocalTasks_content_button2_click> yield)
            //{
            //    Console.WriteLine("IWhenReady<ILocalTasks_content_button2_click>.WhenReady");

            //    yield(new XLocalTasks_content_button2_click
            //    {
            //        AtWhenReady = yield2 =>
            //        {
            //            Console.WriteLine("IWhenReady<ILocalTasks_content_button2_click>.WhenReady XLocalTasks_content_button2_click");

            //            Context.content.button2.Click +=
            //                delegate
            //                {
            //                    Console.WriteLine("IWhenReady<ILocalTasks_content_button2_click>.WhenReady click");

            //                    yield2("button2");
            //                };
            //        }
            //    });
            //}


        }

        public void WhenReady(Action<ILocalTasks> yield)
        {
            Console.WriteLine("WhenReady");

            yield(new XLocalTasks { Context = this });
        }
    }



    public static class X
    {

        public static TaskAwaiter<T> GetAwaiter<T>(this IWhenReady<T> sprite)
        {
            var s = new TaskCompletionSource<T>();


            sprite.WhenReady(s.SetResult);

            return s.Task.GetAwaiter();
        }
    }
}
