Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Partial Public Class ApplicationControl
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As IContainer

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Public Sub InitializeComponent()
		Me.Name = "ApplicationControl"
		Me.Size = New Size(400, 300)
	End Sub 

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		' Note: This jsc project does not support unmanaged resources.
		MyBase.Dispose(disposing)
	End Sub 


End Class

