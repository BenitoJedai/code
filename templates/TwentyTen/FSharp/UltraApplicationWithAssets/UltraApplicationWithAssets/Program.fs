namespace UltraApplicationWithAssets

open jsc.meta.Commands.Rewrite.RewriteToUltraApplication
 
module Program =
    [<Microsoft.FSharp.Core.EntryPoint>]
    Program.Main args =
        // Prepare the yield value for
        do RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)
        ()

        

