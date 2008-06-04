:mxmlc
@echo off

:: Dll name
@call :jsc %1

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


:: build default
call :build %1 %2

:: build custom
call :build "%1/Monetized" MochiPreloader
call :build "%1/Monetized" NewgroundsPreloader

popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: http://download.macromedia.com/pub/flex/sdk/flex_sdk_3.zip

:: -compiler.verbose-stacktraces 
call C:\util\flex\bin\mxmlc.exe -warnings=false   -incremental=true -output=%2.swf -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -warnings=false  -compiler.verbose-stacktraces  -keep-as3-metadata -incremental=true -output=%2.swf -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe  -compiler.verbose-stacktraces  -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof