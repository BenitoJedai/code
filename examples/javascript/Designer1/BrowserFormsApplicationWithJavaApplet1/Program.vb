Imports jsc.meta.Commands.Rewrite.RewriteToUltraApplication
Imports ScriptCoreLib.Desktop.Forms.Extensions
Imports System

''' <summary>
''' You can debug your application by hitting F5.
''' </summary>
Public Module Program
	Public Sub Main(args As String())
#If DEBUG Then
		DesktopFormsExtensions.Launch(
			Function () New ApplicationControl()
		)
#Else
		RewriteToUltraApplication.AsProgram.Launch(GetType(Application))
#End If
	End Sub 


End Module

