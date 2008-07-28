:mxmlc
@echo off



:: Dll name
@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)

:: csc

:: http://msdn.microsoft.com/en-us/library/ms379563(VS.80).aspx
call "C:\WINDOWS\Microsoft.NET\Framework\v3.5\csc.exe" /debug /out:"..\bin\debug\web\%1.NonobaServer.dll" /t:library  /recurse:"..\bin\debug\web\*.cs"  /lib:.. /r:"Library\Nonoba.GameLibrary.dll"


:: Namespace name, type name
@call :mxmlc %1/ActionScript %1

goto :eof

:jsc
pushd ..\bin\debug

call c:\util\jsc\bin\jsc.exe %1.dll -cs2 -as


popd
goto :eof

:mxmlc
@echo off
pushd ..\bin\debug\web



call :build %1 %2


popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
call C:\util\flex\bin\mxmlc.exe -debug -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof