:mxmlc
@echo off

::goto :eof

:: Dll name

@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)

:: csc

:: http://msdn.microsoft.com/en-us/library/ms379563(VS.80).aspx
call "C:\WINDOWS\Microsoft.NET\Framework\v3.5\csc.exe" /debug /out:"..\bin\debug\web\test.exe" /t:exe  /recurse:"..\bin\debug\web\*.cs"  /lib:.. /r:""

:: Namespace name, type name

goto :eof

:jsc
pushd ..\bin\debug

call c:\util\jsc\bin\jsc.exe %1.exe -cs2
::call c:\util\jsc\bin\jsc.exe %1.dll -as


popd
goto :eof
