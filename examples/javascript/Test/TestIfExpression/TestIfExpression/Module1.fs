// Learn more about F# at http://fsharp.net
namespace FooNamespace


    open System.Reflection
    open ScriptCoreLib

    [<assembly: Script()>] do()
    [<assembly: ScriptTypeFilter(ScriptType.JavaScript)>] do()

    [<Script>]
    module Module1 =
        let Foo() =
            let x = "hello"
            ()

        let Bar() =
            let x = "world"
            ()

        (*

    L_0000: nop 
    L_0001: call void FooNamespace.Module1::Foo()
    L_0006: nop 
    L_0007: ldarg.0 
    L_0008: ldc.i4.7 
    L_0009: ceq 
    L_000b: stloc.0 
    L_000c: ldloc.0 
    L_000d: brfalse.s L_0011
    L_000f: br.s L_0013
    L_0011: br.s L_001c
    L_0013: call void FooNamespace.Module1::Bar()
    L_0018: nop 
    L_0019: nop 
    L_001a: br.s L_001d
    L_001c: nop 
    L_001d: call void FooNamespace.Module1::Foo()
    L_0022: nop 
    L_0023: ret 

        

        *)
        let Switch(i : int) =
        
            Foo()

            let IsBar = i = 7

            if IsBar then
                Bar()

            Foo()

        let Switch2(i : int) =
        
            Foo()

            let IsBar = i <> 7

            if IsBar then Bar()

            Foo()