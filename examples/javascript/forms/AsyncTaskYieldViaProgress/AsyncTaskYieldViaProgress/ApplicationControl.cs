using AsyncTaskYieldViaProgress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Threading.Tasks;

namespace AsyncTaskYieldViaProgress
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            // X:\jsc.svn\examples\javascript\AsyncWithProgressAndStateViaTupleExperiment\AsyncWithProgressAndStateViaTupleExperiment\Application.cs

            button1.Enabled = false;

            foreach (var item in X.Invoke())
            {

                var x = new { item };

                Console.WriteLine(x);
                button1.Text = x.ToString();
                Thread.Yield();

                //type: AsyncTaskYieldViaProgress.ApplicationControl, AsyncTaskYieldViaProgress.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                System.Windows.Forms.Application.DoEvents();
            }

            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Progress<string>(
           x =>
           {
               button2.Text = x;

               Console.WriteLine("DOM Progress: " + new { x, Thread.CurrentThread.ManagedThreadId });
           }
        ) as IProgress<string>).With(
                //async 
                progress =>
                {
                    Console.WriteLine("before");
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
                    //var xxx = await 
                    Task.Factory.StartNew(
                     Tuple.Create(progress, new { hello = "world!" }),
                      scope =>
                      {
                          var xprogress = scope.Item1;

                          foreach (var item in X.Invoke())
                          //var item = X.Invoke().First();
                          {

                              var x = new { item, Thread.CurrentThread.ManagedThreadId };

                              Console.WriteLine(x);



                              xprogress.Report(x.ToString());

                              // Cross-thread operation not valid: Control 'button1' accessed from a thread other than the thread it was created on.
                              //Thread.Yield();



                              //System.Windows.Forms.Application.DoEvents();
                          }

                          //await Task.Delay(333);

                          return "";
                      }
                  ).ContinueWithResult(
                          xxx =>
                        Console.WriteLine("after")
                    );

                }
        );
        }

        private
            //async 
            void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("before");

            // Error	4	The type arguments for method 'ScriptCoreLib.Extensions.TaskAsyncExtensions.StartNew<TSource,TResult>(System.Threading.Tasks.TaskFactory, TSource, System.Action<TResult>, System.Func<System.Tuple<System.IProgress<TResult>,TSource>,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\forms\AsyncTaskYieldViaProgress\AsyncTaskYieldViaProgress\ApplicationControl.cs	97	29	AsyncTaskYieldViaProgress
            //Error	4	The type arguments for method 'ScriptCoreLib.Extensions.TaskAsyncExtensions.StartNewWithProgress<TSource,TResult>(System.Threading.Tasks.TaskFactory, TSource, System.Action<TResult>, System.Func<System.Tuple<System.IProgress<TResult>,TSource>,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\forms\AsyncTaskYieldViaProgress\AsyncTaskYieldViaProgress\ApplicationControl.cs	101	29	AsyncTaskYieldViaProgress

            //before
            //after { hello = early done { ManagedThreadId = 4 } }
            //{ item = 4, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 4, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 8, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 8, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 15, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 15, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 16, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 16, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 23, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 23, ManagedThreadId = 5 }, GUI = 3 }
            //{ item = 42, ManagedThreadId = 5 }
            //DOM Progress: { hello = { item = 42, ManagedThreadId = 5 }, GUI = 3 }


            //at System.Collections.Generic.Dictionary`2.FindEntry(TKey key)
            //at System.Collections.Generic.Dictionary`2.ContainsKey(TKey key)
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 91


            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.WriteSwitchRewrite(MethodBase SourceMethodOrConstructor, TypeBuilder DeclaringType, ILTranslationContext context, Type[] ParametersTypes, Type ReturnType, Action`2 ILOverride, ExceptionHandlingClause[] ExceptionHandlingClauses, ILBlock xb, ILGenerator kmil, MethodBuilder MethodBuilder, ConstructorBuilder ConstructorBuilder, RewriteToAssembly Command)

            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(AssemblyBuilder a, ModuleBuilder m, MethodInfo SourceMethod, TypeBuilder DeclaringType, VirtualDictionary`2 NameObfuscation, Assembly PrimarySourceAssembly, Delegate codeinjecton, Func`2 codeinjectonparams, Action`2 ILOverride, Action`3 BeforeInstructions, ILTranslationContext context, RewriteToAssembly Command, VirtualDictionary`2 ShadowTypeCache, VirtualDictionary`2 ShadowSignatureTypeDefinitionCache, Func`4 AtCodeTraceDefineMethod, Action`1 AtCodeTraceDefineGenericParameters)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__376(MethodInfo SourceMethod)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyTypeMembers(Type SourceType, VirtualDictionary`2 NameObfuscation, TypeBuilder DeclaringType, ILTranslationContext context, Type[] ExtensionTypes)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyType(Type SourceType, AssemblyBuilder a, ModuleBuilder m, TypeBuilder OverrideDeclaringType, VirtualDictionary`2 TypeRenameCache, VirtualDictionary`2 NameObfuscation, Func`2 ShouldCopyType, Func`3 FullNameFixup, Action`1 PostTypeRewrite, Action`1 PreTypeRewrite, Action`1 TypeCreated, RewriteToAssembly r, ILTranslationContext context, Action AtCodeTraceCreateType)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__3ae(Type SourceType)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Library.CostumAttributeBuilderExtensions.<>c__DisplayClassd.<>c__DisplayClass17.<ToCustomAttributeBuilder>b__3(Object k)
            //at System.Linq.Enumerable.WhereSelectArrayIterator`2.MoveNext()
            //at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
            //at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
            //at jsc.meta.Library.CostumAttributeBuilderExtensions.<>c__DisplayClassd.<>c__DisplayClass17.<ToCustomAttributeBuilder>b__2(Object[] e)
            //at jsc.meta.Library.CostumAttributeBuilderExtensions.<>c__DisplayClassd.<ToCustomAttributeBuilder>b__1(ILTranslationContext context)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(AssemblyBuilder a, ModuleBuilder m, MethodInfo SourceMethod, TypeBuilder DeclaringType, VirtualDictionary`2 NameObfuscation, Assembly PrimarySourceAssembly, Delegate codeinjecton, Func`2 codeinjectonparams, Action`2 ILOverride, Action`3 BeforeInstructions, ILTranslationContext context, RewriteToAssembly Command, VirtualDictionary`2 ShadowTypeCache, VirtualDictionary`2 ShadowSignatureTypeDefinitionCache, Func`4 AtCodeTraceDefineMethod, Action`1 AtCodeTraceDefineGenericParameters)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__376(MethodInfo SourceMethod)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.Library.VirtualDictionary`2.<>c__DisplayClassb.<op_Implicit>b__a(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 241
            //at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<.ctor>b__40(ILRewriteContext )
            //at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.   .    (ILRewriteContext )
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass8e.<>c__DisplayClass9e.<>c__DisplayClassb0.<WriteSwitchRewrite>b__59(ILInstruction[] ii)
            //at ScriptCoreLib.Extensions.LinqExtensions.With[T](T e, Action`1 h) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 21
            //at ScriptCoreLib.Extensions.LinqExtensions.InternalWithEach[T](IEnumerable`1 collection, Action`1 h) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 162
            //at ScriptCoreLib.Extensions.LinqExtensions.WithEach[T](IEnumerable`1 collection, Action`1 h) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 134
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass8e.<>c__DisplayClass9e.<WriteSwitchRewrite>b__4c(ILGenerator flow_il)
            //at ScriptCoreLib.Extensions.LinqExtensions.With[T](T e, Action`1 h) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 21
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass8e.<WriteSwitchRewrite>b__3c(ILFlow SourceFlow)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass8e.<WriteSwitchRewrite>b__3c(ILFlow SourceFlow)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass8e.<WriteSwitchRewrite>b__3c(ILFlow SourceFlow)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass8e.<WriteSwitchRewrite>b__3c(ILFlow SourceFlow)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass8e.<WriteSwitchRewrite>b__3c(ILFlow SourceFlow)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass8e.<WriteSwitchRewrite>b__3c(ILFlow SourceFlow)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.WriteSwitchRewrite(MethodBase SourceMethodOrConstructor, TypeBuilder DeclaringType, ILTranslationContext context, Type[] ParametersTypes, Type ReturnType, Action`2 ILOverride, ExceptionHandlingClause[] ExceptionHandlingClauses, ILBlock xb, ILGenerator kmil, MethodBuilder MethodBuilder, ConstructorBuilder ConstructorBuilder, RewriteToAssembly Command)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(AssemblyBuilder a, ModuleBuilder m, MethodInfo SourceMethod, TypeBuilder DeclaringType, VirtualDictionary`2 NameObfuscation, Assembly PrimarySourceAssembly, Delegate codeinjecton, Func`2 codeinjectonparams, Action`2 ILOverride, Action`3 BeforeInstructions, ILTranslationContext context, RewriteToAssembly Command, VirtualDictionary`2 ShadowTypeCache, VirtualDictionary`2 ShadowSignatureTypeDefinitionCache, Func`4 AtCodeTraceDefineMethod, Action`1 AtCodeTraceDefineGenericParameters)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__376(MethodInfo SourceMethod)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyTypeMembers(Type SourceType, VirtualDictionary`2 NameObfuscation, TypeBuilder DeclaringType, ILTranslationContext context, Type[] ExtensionTypes)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyType(Type SourceType, AssemblyBuilder a, ModuleBuilder m, TypeBuilder OverrideDeclaringType, VirtualDictionary`2 TypeRenameCache, VirtualDictionary`2 NameObfuscation, Func`2 ShouldCopyType, Func`3 FullNameFixup, Action`1 PostTypeRewrite, Action`1 PreTypeRewrite, Action`1 TypeCreated, RewriteToAssembly r, ILTranslationContext context, Action AtCodeTraceCreateType)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__3ae(Type SourceType)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Library.CostumAttributeBuilderExtensions.<>c__DisplayClassd.<>c__DisplayClass17.<ToCustomAttributeBuilder>b__3(Object k)
            //at System.Linq.Enumerable.WhereSelectArrayIterator`2.MoveNext()
            //at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
            //at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
            //at jsc.meta.Library.CostumAttributeBuilderExtensions.<>c__DisplayClassd.<>c__DisplayClass17.<ToCustomAttributeBuilder>b__2(Object[] e)
            //at jsc.meta.Library.CostumAttributeBuilderExtensions.<>c__DisplayClassd.<ToCustomAttributeBuilder>b__1(ILTranslationContext context)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(AssemblyBuilder a, ModuleBuilder m, MethodInfo SourceMethod, TypeBuilder DeclaringType, VirtualDictionary`2 NameObfuscation, Assembly PrimarySourceAssembly, Delegate codeinjecton, Func`2 codeinjectonparams, Action`2 ILOverride, Action`3 BeforeInstructions, ILTranslationContext context, RewriteToAssembly Command, VirtualDictionary`2 ShadowTypeCache, VirtualDictionary`2 ShadowSignatureTypeDefinitionCache, Func`4 AtCodeTraceDefineMethod, Action`1 AtCodeTraceDefineGenericParameters)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__376(MethodInfo SourceMethod)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.Library.VirtualDictionary`2.<>c__DisplayClassb.<op_Implicit>b__a(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 241
            //at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<.ctor>b__40(ILRewriteContext )
            //at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.   .    (ILRewriteContext )
            //at jsc.Languages.IL.ILTranslationExtensions.EmitTo(MethodBase , ILGenerator , EmitToArguments , TypeBuilder )
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(AssemblyBuilder a, ModuleBuilder m, MethodInfo SourceMethod, TypeBuilder DeclaringType, VirtualDictionary`2 NameObfuscation, Assembly PrimarySourceAssembly, Delegate codeinjecton, Func`2 codeinjectonparams, Action`2 ILOverride, Action`3 BeforeInstructions, ILTranslationContext context, RewriteToAssembly Command, VirtualDictionary`2 ShadowTypeCache, VirtualDictionary`2 ShadowSignatureTypeDefinitionCache, Func`4 AtCodeTraceDefineMethod, Action`1 AtCodeTraceDefineGenericParameters)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__376(MethodInfo SourceMethod)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyTypeMembers(Type SourceType, VirtualDictionary`2 NameObfuscation, TypeBuilder DeclaringType, ILTranslationContext context, Type[] ExtensionTypes)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyType(Type SourceType, AssemblyBuilder a, ModuleBuilder m, TypeBuilder OverrideDeclaringType, VirtualDictionary`2 TypeRenameCache, VirtualDictionary`2 NameObfuscation, Func`2 ShouldCopyType, Func`3 FullNameFixup, Action`1 PostTypeRewrite, Action`1 PreTypeRewrite, Action`1 TypeCreated, RewriteToAssembly r, ILTranslationContext context, Action AtCodeTraceCreateType)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__3ae(Type SourceType)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__390(FieldInfo SourceField)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyType(Type SourceType, AssemblyBuilder a, ModuleBuilder m, TypeBuilder OverrideDeclaringType, VirtualDictionary`2 TypeRenameCache, VirtualDictionary`2 NameObfuscation, Func`2 ShouldCopyType, Func`3 FullNameFixup, Action`1 PostTypeRewrite, Action`1 PreTypeRewrite, Action`1 TypeCreated, RewriteToAssembly r, ILTranslationContext context, Action AtCodeTraceCreateType)
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass426.<InternalInvoke>b__3ae(Type SourceType)
            //at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 94
            //at jsc.Library.VirtualDictionary`2.<get_Item>b__0(TKey kk) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 77
            //at System.Linq.Enumerable.WhereSelectArrayIterator`2.MoveNext()
            //at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
            //at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
            //at jsc.Library.VirtualDictionary`2.get_Item(TKey[] k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 77
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalInvoke()
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalInvokeWithCache()
            //at jsc.meta.Commands.Rewrite.RewriteToAssembly.Invoke()


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
            Task.Factory.StartNewWithProgress(
                 new { hello = "world!" },

                 progress:
                     x =>
                     {
                         button3.Text = x.hello;

                         //Console.WriteLine("DOM Progress: " + new { x.hello, GUI = Thread.CurrentThread.ManagedThreadId });
                     },

                 function:
                     scope =>
                     {
                         var xprogress = scope.Item1;

                         // this will spawn another thread
                         // if current worker already exited... ?
                         //await Task.Delay(333);

                         //foreach (var item in X.Invoke().ToArray())


                         Action work = delegate
                         {
                             foreach (var item in X.Invoke())

                             //var item = X.Invoke().First();
                             {

                                 var x = new { item, DelayThread = Thread.CurrentThread.ManagedThreadId };

                                 //Console.WriteLine(x);



                                 xprogress.Report(
                                     new { hello = x.ToString() }
                                 );

                                 // Cross-thread operation not valid: Control 'button1' accessed from a thread other than the thread it was created on.
                                 //Thread.Yield();



                                 //System.Windows.Forms.Application.DoEvents();
                             }
                         };

                         //cript: error JSC1000: No implementation found for this native method, please implement 
                         // [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

                         Task.Delay(1).GetAwaiter().OnCompleted(work);


                         //yield();


                         return new { hello = "early done " + new { Thread.CurrentThread.ManagedThreadId } };
                     }



              ).ContinueWithResult(
                xxx =>
                {

                    Console.WriteLine("after " + new { xxx.hello });
                }
            );

        }

    }


    static class X
    {


        public static IEnumerable<int> Invoke()
        {
            //script: error JSC1000: No implementation found for this native method, please implement [static System.Environment.get_CurrentManagedThreadId()]
            //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
            //script: error JSC1000: error at AsyncTaskYieldViaProgress.X+<Invoke>d__0..ctor,
            // assembly: V:\AsyncTaskYieldViaProgress.Application.exe
            // type: AsyncTaskYieldViaProgress.X+<Invoke>d__0, AsyncTaskYieldViaProgress.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x000e
            //  method:Void .ctor(Int32)

            var x = System.Environment.CurrentManagedThreadId;

            // http://lostpedia.wikia.com/wiki/The_Numbers

            var value = new[] { 4, 8, 15, 16, 23, 42 };

            for (int i = 0; i < value.Length; i++)
            {
                Thread.Sleep(
                    new Random().Next(100, 1600)
                );


                yield return value[i];
            }

        }
    }
}
