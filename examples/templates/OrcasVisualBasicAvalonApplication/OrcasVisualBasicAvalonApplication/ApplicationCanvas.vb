Imports System.Windows.Controls
Imports System.Windows.Media
Imports ScriptCoreLib.Shared.Avalon.Extensions

Namespace [Shared]
    <Script()> _
    Public Class ApplicationCanvas
        Inherits Canvas

        Public Const DefaultWidth As Integer = 400
        Public Const DefaultHeight As Integer = 300

        Dim WithEvents Control As TextBox

        Public Sub New()

            Width = DefaultWidth
            Height = DefaultHeight

            Background = Brushes.LightBlue

            Control = New TextBox With {.Text = "hello world"}
            Control.AttachTo(Me)
        End Sub

        Private Sub Control_GotFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Control.GotFocus
            Control.Foreground = Brushes.Blue
        End Sub
    End Class

End Namespace
