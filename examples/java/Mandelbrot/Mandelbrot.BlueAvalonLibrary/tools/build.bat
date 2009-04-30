@echo off

set TargetFileName=%2
set ConfigurationName=%3

if %ConfigurationName%==Debug (
  echo Debug mode will not perform post build!
  goto :eof
)


:: Dll name
@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)


:: http://weblogs.asp.net/mschwarz/archive/2007/06/05/how-to-create-silverlight-applications-with-notepad.aspx
:: http://msdn.microsoft.com/en-us/library/ms379563(VS.80).aspx
set References=
set References=%References% /r:"c:\Program Files\Reference Assemblies\Microsoft\Framework\Silverlight\v3.0\mscorlib.dll"
set References=%References% /r:"c:\Program Files\Reference Assemblies\Microsoft\Framework\Silverlight\v3.0\system.dll"

call "C:\WINDOWS\Microsoft.NET\Framework\v3.5\csc.exe" %References% /nostdlib+ /noconfig  /debug /out:"..\bin\%ConfigurationName%\web\%1.dll" /t:library  /recurse:"..\bin\%ConfigurationName%\web\*.cs"  /lib:.. 


goto :eof

:jsc
pushd ..\bin\%ConfigurationName%
call c:\util\jsc\bin\jsc.exe %TargetFileName%  -cs2
popd
goto :eof
