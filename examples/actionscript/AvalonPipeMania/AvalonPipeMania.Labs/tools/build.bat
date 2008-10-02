@echo off
set TargetFileName=%2
set ConfigurationName=%3
set OutputDir=%4


if %ConfigurationName%==Debug goto :eof

:: Dll name
@call :jsc

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)
:: Namespace name, type name
@call :mxmlc AvalonPipeMania/Labs/ActionScript LabsFlash


goto :eof

:jsc

pushd ..\%OutputDir%


::call c:\util\jsc\bin\jsc.exe %TargetFileName%  -as -js
call c:\util\jsc\bin\jsc.exe %TargetFileName%  -as -js


popd
goto :eof

:mxmlc
@echo off
pushd ..\%OutputDir%\web



call :build %1 %2


popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
call C:\util\flex\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof