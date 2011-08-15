Namespace Library.Internal

    Public Class InternalTryFilterHandler
        ' see also: http://msdn.microsoft.com/en-us/library/79dh1480.aspx
        Shared Sub Foo()
            Console.WriteLine("pre")

            Try
                Console.WriteLine("try")


            Catch ex As Exception When ex.Message.Contains("When pigs fly")

                Console.WriteLine("catch")

            End Try

            Console.WriteLine("post")
        End Sub

        Shared Sub ResumeStatementDemo()
            On Error GoTo ErrorHandler   ' Enable error-handling routine.
            Dim x As Integer = 32
            Dim y As Integer = 0
            Dim z As Integer
            z = x / y   ' Creates a divide by zero error
            Exit Sub   ' Exit Sub to avoid error handler.
ErrorHandler:  ' Error-handling routine.
            Select Case Err.Number   ' Evaluate error number.
                Case 6   ' "Divide by zero" error.
                    y = 1 ' Sets the value of y to 1 and tries the calculation again.
                Case Else
                    ' Handle other situations here....
            End Select
            Resume   ' Resume execution at same line
            ' that caused the error.
        End Sub


    End Class

End Namespace
