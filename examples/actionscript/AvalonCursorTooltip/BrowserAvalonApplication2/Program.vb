Imports jsc.meta.Commands.Rewrite.RewriteToUltraApplication
Imports ScriptCoreLib.Desktop.Extensions
Imports System

''' <summary>
''' You can debug your application by hitting F5.
''' </summary>
Public Module Program
	Public Sub Main(args As String())
#if DEBUG
			DesktopAvalonExtensions.Launch(
				Function () New ApplicationCanvas()
			)
#else
			RewriteToUltraApplication.AsProgram.Launch(GetType(Application))
#endif
	End Sub 


End Module

