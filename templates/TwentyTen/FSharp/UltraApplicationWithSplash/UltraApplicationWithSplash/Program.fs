namespace UltraApplicationWithSplash

module Program =
    [<Microsoft.FSharp.Core.EntryPoint>]
    let Main args =
        let PrimaryApplication = typeof<UltraApplicationWithSplash.Application>
        do global.jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(PrimaryApplication)
        0

