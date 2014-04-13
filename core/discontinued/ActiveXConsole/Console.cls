VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "Console"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

'LinearConnections 2002


Public Function CloseConsole() As Boolean
    If CloseHandle(hConsole) <> 0 Then hConsole = 0
   FreeConsole
  
End Function

Public Function OpenConsole() As Boolean
    
    'We open the Window
    If AllocConsole() Then
        hConsole = GetStdHandle(STD_OUTPUT_HANDLE)
        If hConsole = 0 Then
            FreeConsole
        Else
            SetConsoleCtrlHandler AddressOf ConsoleHandler, True
            OpenConsole = True
        End If

    End If

End Function

Public Function WriteString(sText As String) As Boolean
  
    Dim sOut As String, cWritten As Long

    sOut = sText
    'Write the text to the console
    WriteString = WriteConsole(hConsole, ByVal sOut, Len(sOut), cWritten, ByVal 0&)
 
End Function

Private Sub Class_Terminate()
    'If a console is open then
    If hConsole <> 0 Then
        'close our handle to it and
        CloseHandle hConsole
        'disconnect it from the process.
        FreeConsole
    End If
End Sub

