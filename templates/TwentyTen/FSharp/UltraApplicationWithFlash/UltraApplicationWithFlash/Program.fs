namespace UltraApplicationWithFlash

module Program =
    [<Microsoft.FSharp.Core.EntryPoint>]
    let Main args =
        let PrimaryApplication = typeof<UltraApplicationWithFlash.Application>
        do global.jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(PrimaryApplication)
        0

