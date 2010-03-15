namespace UltraApplication

module Program =
    [<Microsoft.FSharp.Core.EntryPoint>]
    let Main args =
        let PrimaryApplication = typeof<UltraApplication.Application>
        do jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(PrimaryApplication)
        0

