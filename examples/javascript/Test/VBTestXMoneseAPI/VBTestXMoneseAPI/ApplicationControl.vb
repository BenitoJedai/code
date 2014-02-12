Imports VBTestXMoneseAPI
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Partial Public Class ApplicationControl
    Inherits UserControl
    Public Sub New()
        Me.InitializeComponent()
    End Sub


    Private Sub MoneseWebServices1_RegisterUserShortAsyncComplete(obj As Long) Handles MoneseWebServices1.RegisterUserShortAsyncComplete
        '+		InnerException	{"Conversion from string \"obj: \" to type 'Double' is not valid."}	System.Exception {System.InvalidCastException}
        '// script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.VisualBasic.CompilerServices.Conversions.ToString(System.Int64)]

        'script: error JSC1000: unsupported flow detected, try to simplify.
        ' Assembly V:\VBTestXMoneseAPI.Application.exe
        ' DeclaringType monese.experimental.MoneseWebServices, VBTestXMoneseAPI.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        '        OwnerMethod add_RegisterUserShortAsyncComplete
        '        Offset 26
        ' . Try ommiting the return, break or continue instruction.

        ' jsc does not support events for vb anymore?

        'MessageBox.Show("obj: " & obj)
        MessageBox.Show("obj: " & System.Convert.ToString(obj))

    End Sub

    Private Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click

        Me.MoneseWebServices1.RegisterUserShortAsync("VBTestXMoneseAPI", "1234")

    End Sub
End Class

