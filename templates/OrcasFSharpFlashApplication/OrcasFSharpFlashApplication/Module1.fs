#light

namespace Example

open System
open System.Reflection
open System.Diagnostics

[<DebuggerDisplay("{DisplayMe()}")>]
type D =
    | A of int
    | B of string * bool
    member this.DisplayMe() =
        sprintf "%A" this
    //override this.ToString() =
    //    sprintf "%A" this

module MainMod =
    let Main() =
        let x = A(3)
        let y = B("cool", true)
        printfn "Hello" 
        Console.ReadKey() |> ignore    
        
    [<assembly: AssemblyVersion("1.2.3.4")>]
    do
        Main()