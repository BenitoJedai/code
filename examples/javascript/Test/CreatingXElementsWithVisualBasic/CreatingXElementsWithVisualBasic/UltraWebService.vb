Imports ScriptCoreLib.Ultra.Library.Delegates

Public NotInheritable Class UltraWebService
    Public Sub GetTime(ByVal x As String, ByVal result As Action(Of String))
        result.Invoke((x & DateTime.Now))
    End Sub





End Class
