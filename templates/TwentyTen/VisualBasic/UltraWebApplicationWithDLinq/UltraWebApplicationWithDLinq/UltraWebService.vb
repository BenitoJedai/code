Imports ScriptCoreLib.Ultra.Library.Delegates

Public Delegate Sub EnumerateAction(ByVal Key As String, ByVal Field1 As String)





Public NotInheritable Class UltraWebService
    ' Methods
    Public Sub GetTime(ByVal x As String, ByVal result As StringAction)
        Debugger.Break()
        result.Invoke((x & DateTime.Now))
    End Sub


    Public Sub Add(ByVal Text1 As String, ByVal Continuation As StringAction)
        Using ctx As DataClasses1DataContext = New DataClasses1DataContext
            ctx.Table1s.InsertOnSubmit(New Table1 With { _
            .Text1 = Text1 _
        })
            ctx.SubmitChanges()
        End Using
        If (Not Continuation Is Nothing) Then
            Continuation.Invoke("")
        End If
    End Sub


    Public Sub Delete(ByVal ID As String, ByVal Continuation As StringAction)
        Using ctx As DataClasses1DataContext = New DataClasses1DataContext
            ctx.Table1s.DeleteAllOnSubmit(Of Table1)((From k In ctx.Table1s
                Where (k.ID = Convert.ToInt32(ID))
                Select k))
            ctx.SubmitChanges()
        End Using
        If (Not Continuation Is Nothing) Then
            Continuation.Invoke("")
        End If
    End Sub



    Public Sub Enumerate(ByVal yield As EnumerateAction)
        Using ctx As DataClasses1DataContext = New DataClasses1DataContext
            Dim item As Table1
            For Each item In ctx.Table1s
                yield.Invoke((item.ID), item.Text1)
            Next
        End Using
    End Sub








End Class


