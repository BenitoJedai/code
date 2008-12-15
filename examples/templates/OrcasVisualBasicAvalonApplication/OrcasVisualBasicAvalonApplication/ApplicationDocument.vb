Imports OrcasVisualBasicAvalonApplication.Shared
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.JavaScript

Namespace JavaScript


    <Script(), _
     ScriptApplicationEntryPoint(width:=ApplicationCanvas.DefaultWidth, height:=ApplicationCanvas.DefaultHeight)> _
    Public Class ApplicationDocument

        Public Sub New()
            AvalonExtensions.AttachToContainer(New ApplicationCanvas(), Native.Document.body)
        End Sub
        Shared Sub New()
            GetType(ApplicationDocument).SpawnTo(AddressOf Spawn)
        End Sub
        Shared Function Spawn(ByVal i As IHTMLElement) As ApplicationDocument
            Spawn = New ApplicationDocument
        End Function
    End Class
End Namespace
