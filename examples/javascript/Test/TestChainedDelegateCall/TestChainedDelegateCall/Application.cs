using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestChainedDelegateCall.Design;
using TestChainedDelegateCall.HTML.Pages;

namespace TestChainedDelegateCall
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            /*
 
            .ctor(IApp)
            <.ctor>b__0() : void
            Analysis
            Attributes
            Signature Types
            Declaring Module
            Declaring Type
            loc.0 <- 0x0010 ldloc.1      loc.1 : string[]
            loc.1 <- 0x0002 newarr       [mscorlib] System.String
            maxstack 3
            IL Code (34)
            0x0000 nop 
            0x0001 . ldc.i4.1         1 (0x00000001)
            0x0002 . newarr           [mscorlib] System.String
            0x0007 stloc.1            loc.1 : string[]
            0x0008 . ldloc.1          loc.1 : string[]
            0x0009 . . ldc.i4.0       0 (0x00000000)
            0x000a . . . ldstr        "hi"
            0x000f stelem.ref 
            0x0010 . ldloc.1          loc.1 : string[]
            0x0011 stloc.0            loc.0 : string[]
            0x0012 . ldstr            value <- "before..."
            0x0017 call               [mscorlib] System.Console.WriteLine(value : string) : void
            0x001c nop 
            0x001d . ldloc.0          source <- loc.0 : string[]
            0x001e . . ldsfld         [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate5 : 
            0x0023 . brtrue.s 
            0x0023 -> 0x0025 0x0038 
            0x0023 -> 0x0025 ldnull 
            0x0025 . . ldnull         object <- null
            0x0026 . . . ldftn        method <- [TestChainedDelegateCall] TestChainedDelegateCall.Application.<.ctor>b__2(k : string, i : int, next : () -> void) : void
            0x002c . . newobj         [mscorlib] System.Action`3..ctor(object : object, method : IntPtr)
            0x0031 . stsfld           [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate5 : 
            0x0036 . br.s 
            0x0023 0x0036 -> 0x0038
            0x0038 . . ldsfld         handler <- [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate5 : 
            0x003d . call             [TestChainedDelegateCall] TestChainedDelegateCall.X.ForEach(source : this IEnumerable`1<string>, handler : ) : 
            0x0042 . . ldsfld         [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate4 : () -> void
            0x0047 . brtrue.s 
            0x0047 -> 0x0049 0x005c 
            0x0047 -> 0x0049 ldnull 
            0x0047 0x005a -> 0x005c ldsfld 
            0x0023 0x0036 -> 0x0038 ldsfld 
            0x0038 . . ldsfld         handler <- [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate5 : 
            0x003d . call             [TestChainedDelegateCall] TestChainedDelegateCall.X.ForEach(source : this IEnumerable`1<string>, handler : ) : 
            0x0042 . . ldsfld         [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate4 : () -> void
            0x0047 . brtrue.s 
            0x0047 -> 0x0049 0x005c 
            0x0047 -> 0x0049 ldnull 
            0x0049 . . ldnull         object <- null
            0x004a . . . ldftn        method <- [TestChainedDelegateCall] TestChainedDelegateCall.Application.<.ctor>b__1() : void
            0x0050 . . newobj         [mscorlib] System.Action..ctor(object : object, method : IntPtr)
            0x0055 . stsfld           [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate4 : () -> void
            0x005a . br.s 
            0x0047 0x005a -> 0x005c
            0x0047 0x005a -> 0x005c ldsfld 
            0x005c . . ldsfld         obj <- [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate4 : () -> void
            0x0061 callvirt           [mscorlib] System.Action`1.Invoke(obj : () -> void) : void
            0x0066 nop 
            0x0067 ret 
            <.ctor>b__1() : void
            <.ctor>b__2(string, int, () -> void) : void
            Analysis
            Attributes
            Signature Types
            Declaring Module
            Declaring Type
            arg.0 page : IApp
            loc.0 <- 0x002d ldsfld       [TestChainedDelegateCall] TestChainedDelegateCall.Application.CS$<>9__CachedAnonymousMethodDelegate3 : () -> void
            maxstack 2
            IL Code (21)

 
             */
            Action x = delegate
            {
                var source = new[] { "hi" };

                Console.WriteLine("before...");

                // jsc does not supprt invoking the returned delegate inside a delegate.
                // this will result in invalid behaviour at runtime
                // where ForEach function will be called twice.
                // jsc rewriter shall simplify IL in the future to not confuse jsc.

                var yield = source.ForEach(
                    (k, i, next) =>
                    {
                        Console.WriteLine("at...");

                    }
                );

                yield(
                    delegate
                    {
                        Console.WriteLine("done!");
                    }
                );
            };

            x();
        }

    }

    static class X
    {
        public static Action<Action> ForEach<T>(this IEnumerable<T> source, Action<T, int, Action> handler)
        {
            handler(
                source.First(), 0, delegate
                {
                }
            );

            return y =>
            {
                y();
            };
        }

    }
}
