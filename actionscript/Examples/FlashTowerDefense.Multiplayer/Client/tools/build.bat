@echo off

::goto :skipped



:: Dll name
@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)
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
call :build "FlashTowerDefense/ActionScript/Client" FlashTowerDefenseClient

::call :build "FlashTowerDefense/ActionScript/Client/Monetized" MochiPreloader

popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -debug -warnings=false -keep-as3-metadata -incremental=true -output=%2.swf -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -use-network -debug -compiler.verbose-stacktraces -warnings=false -keep-as3-metadata -incremental=true -output=%2.swf -sp=. %1/%2.as
:: call C:\util\flex\bin\mxmlc.exe -optimize -use-network -warnings=false -incremental=true -output=%2.swf -sp=. %1/%2.as
call C:\util\flex\bin\mxmlc.exe -debug -use-network -warnings=false -incremental=true -output=%2.swf -sp=. %1/%2.as
goto :eof
:skipped
echo Skipped
goto :eof