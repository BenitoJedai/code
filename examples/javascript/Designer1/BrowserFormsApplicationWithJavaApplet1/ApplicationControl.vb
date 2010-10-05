Imports BrowserFormsApplicationWithJavaApplet1
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


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MessageBox.Show("hello ")


    End Sub
End Class

