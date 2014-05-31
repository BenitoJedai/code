Imports jsc.meta.Commands.Rewrite.RewriteToUltraApplication
Imports System

''' <summary>
''' You can debug your application by hitting F5.
''' </summary>
Public Module Program
    Public Sub Main(args As String())
        RewriteToUltraApplication.AsProgram.Launch(GetType(Application))
    End Sub


End Module

