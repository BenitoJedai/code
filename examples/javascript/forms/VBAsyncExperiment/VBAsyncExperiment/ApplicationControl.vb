Imports VBAsyncExperiment
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading.Tasks
Imports System

Partial Public Class ApplicationControl
    Inherits UserControl
    Public Sub New()
        Me.InitializeComponent()
    End Sub


    Private Async Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click

        Button2.Enabled = False

        Await FooAsync()

        Button2.Enabled = True

    End Sub


    'script: error JSC1000:
    'error:
    '  statement cannot be a load instruction (or is it a bug?)
    '  [0x0000] ldarg.s    +1 -0

    ' assembly: V:\VBAsyncExperiment.Application.exe
    ' type: VBAsyncExperiment.ApplicationControl+VB$StateMachine_0_Button2_Click+<MoveNext>06000020, VBAsyncExperiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
    ' offset: 0x0000
    '  method:Int32 <00b6> ldarg.0(<MoveNext>06000020, VB$StateMachine_0_Button2_Click ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef)

    Async Function FooAsync() As Task
        ' http://blogs.msdn.com/b/pfxteam/archive/2012/09/11/forking-in-async-methods.aspx

        Console.WriteLine("Starting FooAsync")
        Dim t1 = (Async Function()
                      Console.WriteLine("Starting first async block")
                      Await Task.Delay(1000)
                      Console.WriteLine("Done first block")

                      Return "a"
                  End Function)()
        Dim t2 = (Async Function()
                      Console.WriteLine("Starting second async block")
                      Await Task.Delay(2000)
                      Console.WriteLine("Done second block")

                      Return "b"
                  End Function)()
        Dim x = Await Task.WhenAll(t1, t2)
        Console.WriteLine("Done FooAsync")
    End Function
End Class

