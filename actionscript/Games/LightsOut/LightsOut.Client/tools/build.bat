:mxmlc
@echo off

::goto :eof

:: Dll name
::@call :jsc %1 -as

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)

:: csc
pushd ..\bin\debug\web

:: http://msdn.microsoft.com/en-us/library/ms379563(VS.80).aspx
::call "C:\WINDOWS\Microsoft.NET\Framework\v3.5\csc.exe"/out:LightsOut.Server.dll /t:library  /recurse:*.cs  /lib:.. /r:Nonoba.GameLibrary.dll

popd
goto :eof

:: Namespace name, type name
@call :mxmlc %1/ActionScript %1

goto :eof

:jsc
pushd ..\bin\debug

call c:\util\jsc\bin\jsc.exe %1.dll  -as


popd
goto :eof

:mxmlc
@echo off
pushd ..\bin\debug\web



::call :build %1 %2
call :build LightsOut/ActionScript/Client TeamPlay
call :build LightsOut/ActionScript/Client/Monetized MochiPreloader


popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
call C:\util\flex\bin\mxmlc.exe -debug -use-network -warnings=false -incremental=true -output=%2.swf -sp=. %1/%2.as
goto :eof