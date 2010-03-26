Imports ScriptCoreLib.Ultra.Library.Delegates

Public NotInheritable Class UltraWebService
    ' Methods
    Public Sub GetTime(ByVal x As String, ByVal result As StringAction)
        Debugger.Break()
        result.Invoke((x & DateTime.Now))
    End Sub

End Class


