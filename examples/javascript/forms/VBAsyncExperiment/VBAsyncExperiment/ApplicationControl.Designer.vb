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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(154, 137)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ApplicationControl
        '
        Me.Controls.Add(Me.Button2)
        Me.Name = "ApplicationControl"
        Me.Size = New System.Drawing.Size(400, 300)
        Me.ResumeLayout(False)

    End Sub

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		' Note: This jsc project does not support unmanaged resources.
		MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button


End Class

