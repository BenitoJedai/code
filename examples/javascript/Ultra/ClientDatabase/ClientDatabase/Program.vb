Module Program

    Sub Main()

        For Each k In New DataClasses1DataContext().ClientFoos
            Console.WriteLine(k.Text)

        Next


        jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(GetType(Application))
    End Sub

End Module
