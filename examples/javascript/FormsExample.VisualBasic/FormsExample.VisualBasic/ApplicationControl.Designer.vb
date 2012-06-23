Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Partial Public Class ApplicationControl
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As IContainer

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		' Note: This jsc project does not support unmanaged resources.
		MyBase.Dispose(disposing)
	End Sub 

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Public Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ApplicationControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Name = "ApplicationControl"
        Me.Size = New System.Drawing.Size(560, 394)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


End Class

