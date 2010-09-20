Imports ScriptCoreLib.Extensions
Imports ScriptCoreLib.Shared.Avalon.Extensions
Imports System
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Shapes
Imports System.Xml
Imports System.Xml.Linq

Public Class ApplicationCanvas
    Inherits BrowserAvalonApplicationWithAdobeFlash2.ApplicationCanvas

    Public ReadOnly rr As New Rectangle()

	Public Sub New()
        rr.Fill = Brushes.Green
        rr.Opacity = 0.1

        Dim t = "fc"


        SupportsContainerExtensions.AttachTo(rr, Me)
        SupportsContainerExtensions.MoveTo(rr, 8, 8)
        AddHandler Me.SizeChanged,
            Sub(s, e) SupportsContainerExtensions.SizeTo(rr, Math.Min(64, Me.Width - 16), Me.Height - 16)

	End Sub 


End Class

